using UnityEngine;

public enum MonsterState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Reload,
    Hit,
    Dead
}

[RequireComponent(typeof(MonsterFSM))]
public abstract class Monster : Character
{
    #region Monster Stats

    private float rotSpeed = 10f;
    public float chaseRange;    // 추격 시작 범위
    public float attackRange;   // 공격 가능 범위

    [SerializeField] private int MaxBullet = -1;
    public int CurBullet { get; private set; }

    public bool IsAtk { get; protected set; } = false;    // 애니메이션이 진행 중인지 확인
    public bool IsAtkCool { get; protected set; } = false;    // 공격 쿨타임 상태 확인
    public bool IsReLoad { get; protected set; } = false;
    public bool IsHit { get; protected set; } = false;
    public Transform chaseTarget { get; private set; } = null;  // 추적할 대상

    #endregion

    #region 
    [SerializeField] private float atkCoolTime = 2f;
    [SerializeField] private float attackDelayRatio = 0.3f;
    private float patrolTime = 2f;

    private float coolElapsed = 0f;
    private float patrolElapsed = 0f;
    private float animElapsed = 0f;
    public Vector3 patrolTargetPos = default(Vector3);

    public Transform roomGrid;

    protected bool IsAnimPlay(float addTime = 0f)
    {
        var _animLength = animator.GetCurrentAnimatorStateInfo(0).length + addTime;
        return (animElapsed += Time.deltaTime) < _animLength;
    }
    #endregion

    protected virtual void Initialized()
    {
        CurBullet = MaxBullet;

        // Hit 상태로 변경될 수 있도록 구독
        onHit += TakeDamage;
    }

    #region Action
    private void IdleAction()
    {
        animator.SetBool(moveHash, false);

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

        animator.SetBool(moveHash, true);
    }

    private void PatrolAction()
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

    private void ChaseAction()
    {
        if (chaseTarget == null) return;

        var _moveDir = chaseTarget.transform.position - transform.position;
        MoveAction(_moveDir.normalized, attribute.speed * 2f);
    }

    private void AttackAction()
    {
        if (!IsAtk) InitAttack();

        var _animLength = animator.GetCurrentAnimatorStateInfo(0).length;
        var _attackDelay = _animLength * attackDelayRatio;

        animElapsed += Time.deltaTime;

        if (animElapsed >= _attackDelay && !IsAtkCool)
        {
            IsAtkCool = true;
            coolElapsed = 0f;
            ExecuteAttack();
        }

        if (animElapsed < _animLength) return;

        IsAtk = false;
    }

    protected virtual void ReLoadAction()
    {
        if (!IsReLoad)
        {
            IsReLoad = true;
            animElapsed = 0f;
            animator.SetTrigger("Trg_ReLoad");
        }

        if (IsAnimPlay()) return;

        IsReLoad = false;
        CurBullet = MaxBullet;
    }

    protected virtual void HitAction()
    {
        if (IsAnimPlay()) return;

        IsHit = false;
    }

    private void DeadAction()
    {
        if (IsAnimPlay(0.5f)) return;

        Destroy(gameObject);
    }
    #endregion

    protected virtual void InitAttack()
    {
        IsAtk = true;

        animator.SetBool(moveHash, false);
        animator.SetTrigger("Trg_Attack");

        animElapsed = 0f;

        if (MaxBullet > 0) CurBullet--;
    }

    protected abstract void ExecuteAttack();
    
    private void DecideNextMovePos()
    {
        if (patrolTargetPos != default(Vector3)) return;

        // 맵에 배치 되어 있는 grid를 기준으로 patrol 진행
        var _gridTrans = roomGrid.GetChild(2);

        var _randY = Random.Range(0, _gridTrans.childCount);
        var _randX = Random.Range(0, _gridTrans.GetChild(_randY).childCount);

        patrolTargetPos = roomGrid.GetChild(2).GetChild(_randY).GetChild(_randX).position;
        //patrolTargetPos = transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
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
        IsAtk = false;
        IsReLoad = false;
        animElapsed = 0f;

        if (attribute.CurHart <= 0)
        {
            animator.SetTrigger("Trg_Dead");
            GetComponent<Collider>().enabled = false;
            return;
        }
        IsHit = true;
        animator.SetTrigger("Trg_Hit");
    }

    protected void ApplyGravity()
    {
        if (attribute.CurHart <= 0) return;

        isGrounded = characterController.isGrounded;
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
        GetComponent<MonsterFSM>().Initialized(this, IdleAction, PatrolAction, ChaseAction, AttackAction, ReLoadAction, HitAction, DeadAction);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void Update()
    {
        SearchTarget();
        ApplyGravity();

        var _atkCoolTime = atkCoolTime / Mathf.Max(attribute.attackSpeed, 0.01f);
        IsAtkCool = (coolElapsed += Time.deltaTime) < _atkCoolTime;
    }
}