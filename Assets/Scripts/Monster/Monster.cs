using System;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;
using static UnityEngine.UI.GridLayoutGroup;

[RequireComponent(typeof(MonsterFSM))]
public abstract class Monster : Character , IInitializableItem<MonsterDataSO>
{
    #region Monster Stats
    private float rotSpeed = 10f;
    public float chaseRange;    // 추격 시작 범위
    public float attackRange;   // 공격 가능 범위
    public event Action<Monster> OnDeath;
    MonsterFSM monsterFSM = null;

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
    public int level;

    [SerializeField] private Transform roomGrid;

    protected bool IsAnimPlay(float addTime = 0f)
    {        
        var _animLength =  GetAnimator().GetCurrentAnimatorStateInfo(0).length + addTime;
        return (animElapsed += Time.deltaTime) < _animLength;
    }
    #endregion



    public void SetData(MonsterDataSO model)
    {
        level = model.level;
        attribute = new AttributeSet(model.attribute);
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

        monsterFSM = GetComponent<MonsterFSM>();
        monsterFSM.Initialized(this, _dead, _hit, _reLoad, _attack, _chase, _patrol, _idle);
    }

    public void SetNode(Room room , float scaleMultiplier = 1.0f)
    {
        GridNode node = room.grid.GetRandomGridNode();
        startNode = node;
       SetRoomGrid(room.grid.transform);
       transform.localScale *= scaleMultiplier;
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

        //애니메이션
        GetModelController().SetMoveDirection(moveDir.x, moveDir.z);

        // 이동
        characterController.Move(moveDir * speed * Time.deltaTime);
    }

    public virtual void PatrolAction()
    {
        var localPos = new Vector3(transform.position.x, 0, transform.position.z);
        patrolTargetPos = new Vector3(patrolTargetPos.x,0, patrolTargetPos.z);
        var _moveDir = patrolTargetPos - transform.position;
        var speed = attribute.GetCurValue(eAttributeType.Speed);
        MoveAction(_moveDir.normalized, speed);

        // 목표 위치에 도달하면 Idle 상태로 변경
        if (Vector3.Distance(patrolTargetPos, localPos) < 1.0f)
        {
            patrolElapsed = 0f;
            patrolTargetPos = default(Vector3);
        }
    }

    public virtual void ChaseAction()
    {       
        if (chaseTarget == null) return;

        var _moveDir = chaseTarget.transform.position - transform.position;
        var speed = attribute.GetCurValue(eAttributeType.Speed);
        MoveAction(_moveDir.normalized, speed * 2f);
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
        aggro = true;
        chaseTarget = GameManager.instance.GetPlayer().transform;
    }
    
    public void DeadAction()
    {
        if (IsAnimPlay(0.5f)) return;
        isDead = true;
        OnDeath?.Invoke(this);
        gameObject.SetActive(false);
        if (SoundManager.instance != null)
            SoundManager.instance.PlayEffect(eEffectType.oop);

        if(UserData.Instance != null)
            UserData.Instance.SetKillMonster(level);
    }
    #endregion

    #region State Init
    public virtual void InitAttack()
    {
        IsAtk = true;

        ExecuteAttack();
        if (MaxBullet > 0) CurBullet--;
    }
    #endregion

    protected abstract void ExecuteAttack();
    
    private void DecideNextMovePos()
    {
        if (patrolTargetPos != default(Vector3)) return;

        // 맵에 배치 되어 있는 grid를 기준으로 patrol 진행
        var _gridTrans = roomGrid.GetChild(0);

        var _randY = UnityEngine.Random.Range(0, _gridTrans.childCount);
        var _randX = UnityEngine.Random.Range(0, _gridTrans.GetChild(_randY).childCount);

        patrolTargetPos = roomGrid.GetChild(_randY).GetChild(_randX).position;
    }

    private void RotateTowardTarget(Vector3 dir)
    {
        var _targetRot = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRot, rotSpeed * Time.deltaTime);
    }
    public bool aggro;
    private void SearchTarget()
    {
        if (aggro && chaseTarget != null)
            return;

        var _hits = Physics.OverlapSphere(transform.position, chaseRange, LayerMask.GetMask("Player"));

        chaseTarget = _hits == null || _hits.Length.Equals(0) ? null: _hits[0].transform;
    }

    protected virtual void TakeDamage()
    {
        animElapsed = 0f;
        var CurHealth = attribute.GetCurValue(eAttributeType.Health);
        if (CurHealth <= 0)
        {
            GetComponent<Collider>().enabled = false;
            return;
        }
        IsHit = true;
    }

    protected void ApplyGravity()
    {
        GroundCheck();
        if (attribute.GetCurValue(eAttributeType.Health) <= 0) return;

        if (isGrounded && calcVelocity.y < 0 || onlyIdle)
        {
            calcVelocity.y = 0;
        }

        calcVelocity.y += gravity * Time.deltaTime;

        characterController.Move(calcVelocity * Time.deltaTime);
    }
    protected override void Awake()
    {
        base.Awake();
        onlyIdle = true;
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

    float deltime = 0;
    public bool onlyIdle;

    void Update()
    {
        SearchTarget();
        ApplyGravity();
        
        if(aggro)
        {            
            if(deltime > 4)
            {
                deltime = 0;
                aggro = false;
            }
            deltime += Time.deltaTime;

        }

    }

    public void SetRoomGrid(Transform grid)
    {
        roomGrid = grid;
    }

    public override bool GetDead()
    {
        return isDead;
    }

    public void ResetPos()
    {
        gameObject.transform.position = startNode.GetItemPos();
    }

    public void Kill()
    {
        GameEffect effect = new GameEffect(eModifier.Add);
        effect.AddModifier(eAttributeType.Health, -10000);
        ApplyEffect(effect);
    }
}