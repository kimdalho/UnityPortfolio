using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;

[RequireComponent(typeof(MonsterFSM))]
public abstract class Monster : Character
{
    #region Monster Stats
    private float rotSpeed = 10f;
    public float chaseRange;    // 추격 시작 범위
    public float attackRange;   // 공격 가능 범위
    public bool isDead = false;
    public event Action<Monster> OnDeath;

    [SerializeField] protected int MaxBullet = -1;
    public int CurBullet { get; private set; }
    public bool IsAtk { get; set; } = false;    // 애니메이션이 진행 중인지 확인
    public bool IsAtkCool { get; set; } = false;    // 공격 쿨타임 상태 확인

    public bool IsReloading { get; set; } = false;    // 로딩 상태 확인

    public bool IsHit { get; set; } = false;
    public Transform chaseTarget { get; private set; } = null;  // 추적할 대상
    #endregion

    #region Progress

    public float patrolTime = 2f;
    private float patrolElapsed = 0f;
    public float animElapsed { get; set; }
    public Vector3 patrolTargetPos { get; set; } = default(Vector3);

    public GridNode startNode;
    [SerializeField] private Transform roomGrid;

    protected bool IsAnimPlay(float addTime = 0f)
    {
        var _animLength = animator.GetCurrentAnimatorStateInfo(0).length + addTime;
        return (animElapsed += Time.deltaTime) < _animLength;
    }
    #endregion

    public void SetData(MonsterLevelDataSO model)
    {
        GameEffectSelf effect = new GameEffectSelf(model.attribute);
        effect.modifierOp = eModifier.Add;
        effect.ApplyGameplayEffectToSelf(this);
        transform.position = startNode.GetItemPos();
    }

    protected virtual void Initialized()
    {
        CurBullet = MaxBullet;

        // Hit 상태로 변경될 수 있도록 구독
        OnHit += TakeDamage;

        // 상태 초기화
        var _idle = StateFactory.GetState(MonsterState.Idle);
        var _patrol = StateFactory.GetState(MonsterState.Patrol);
        var _chase = StateFactory.GetState(MonsterState.Chase);
        var _attack = StateFactory.GetState(MonsterState.Attack);
        var _reLoad = StateFactory.GetState(MonsterState.Reload);
        var _hit = StateFactory.GetState(MonsterState.Hit);
        var _dead = StateFactory.GetState(MonsterState.Dead);

        GetComponent<MonsterFSM>().Initialized(this, _dead, _hit, _reLoad, _attack, _chase, _patrol, _idle);
    }

    #region Action
    public void IdleAction()
    {
        // 추적할 상대가 있을 경우에는 대상을 향하도록 Rotate
        if (chaseTarget != null) RotateTowardTarget(chaseTarget.position - transform.position);

        // 다음 위치가 정해지기 전까지 Idle 상태
        if ((patrolElapsed += Time.deltaTime) < patrolTime) return;
        DecideNextMovePos();
    }

    protected void MoveAction(Vector3 moveDir, float speed)
    {
        // 이동하려는 방향으로 로테이션 변경
        RotateTowardTarget(moveDir);

        // 이동
        characterController.Move(moveDir * speed * Time.deltaTime);
    }

    public virtual void PatrolAction()
    {
        var _moveDir = patrolTargetPos - transform.position;
        MoveAction(_moveDir.normalized, attribute.speed);

        // 목표 위치에 도달하면 Idle 상태로 변경
        if (Vector3.Distance(patrolTargetPos, transform.position) < 0.01f)
        {
            patrolElapsed = 0f;
            patrolTargetPos = default(Vector3);
        }
    }

    public virtual void ChaseAction()
    {       
        if (chaseTarget == null) return;

        var _moveDir = chaseTarget.transform.position - transform.position;
        MoveAction(_moveDir.normalized, attribute.speed * 2f);
    }

    public virtual void AttackAction()
    {
        if (IsAnimPlay()) return;
        IsAtk = false;
        IsReloading = true;
    }

    public virtual void ReLoadAction()
    {
        if (IsAnimPlay()) return;
        CurBullet = MaxBullet;
        IsReloading = false;
    }

    public virtual void HitAction()
    {
        if (IsAnimPlay()) return;
        IsHit = false;
    }
    
    public void DeadAction()
    {
        if (IsAnimPlay(0.5f)) return;
        isDead = true;
        OnDeath?.Invoke(this);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
    #endregion

    #region State Init
    public virtual void InitAttack()
    {
        IsAtk = true;

        ExecuteAttack();
        if (MaxBullet > 0) CurBullet--;
    }

    public virtual void InitReLoad()
    {
        animator.SetTrigger("Trg_ReLoad");
    }
    #endregion

    protected abstract void ExecuteAttack();
    
    private void DecideNextMovePos()
    {
        if (patrolTargetPos != default(Vector3)) return;

        // 맵에 배치 되어 있는 grid를 기준으로 patrol 진행
        var _gridTrans = roomGrid.GetChild(2);

        var _randY = UnityEngine.Random.Range(0, _gridTrans.childCount);
        var _randX = UnityEngine.Random.Range(0, _gridTrans.GetChild(_randY).childCount);

        //patrolTargetPos = roomGrid.GetChild(2).GetChild(_randY).GetChild(_randX).position;
        patrolTargetPos = roomGrid.GetChild(_randY).GetChild(_randX).position;
    }

    private void RotateTowardTarget(Vector3 dir)
    {
        var _targetRot = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRot, rotSpeed * Time.deltaTime);
    }

    private void SearchTarget()
    {
        var _hits = Physics.OverlapSphere(transform.position, chaseRange, LayerMask.GetMask("Player"));

        chaseTarget = _hits == null || _hits.Length.Equals(0) ? null: _hits[0].transform;
    }

    protected virtual void TakeDamage()
    {
        animElapsed = 0f;

        if (attribute.CurHart <= 0)
        {
            GetComponent<Collider>().enabled = false;
            return;
        }
        IsHit = true;
    }

    protected void ApplyGravity()
    {
        GroundCheck();
        if (attribute.CurHart <= 0) return;

        if (isGrounded && calcVelocity.y < 0)
        {
            calcVelocity.y = 0;
        }
        calcVelocity.y += gravity * Time.deltaTime;

        characterController.Move(calcVelocity * Time.deltaTime);
    }

    protected virtual void Start()
    {
        Initialized();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
#endif

    void Update()
    {
        SearchTarget();
        ApplyGravity();
    }

    public void SetRoomGrid(Transform grid)
    {
        roomGrid = grid;
    }

    public override bool GetDead()
    {
        return isDead;
    }


}