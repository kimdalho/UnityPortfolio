
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem.XInput;


public class Player : PlayerControllerBase, IOnGameOver ,IOnNextFlow
{
    #region 룩엣
    [SerializeField]
    private CinemachineCamera lookatCam;
    #endregion

    private HashSet<Collider> detectedItems = new HashSet<Collider>();

    #region 아이템 설명창
    public Color gizmoColor = GlobalDefine.Red;
    public float scanRadius = GlobalDefine.ScanBaseRadius;
    public LayerMask itemLayer;
    private Collider[] buffer = GlobalDefine.Basebuffer;
    public ItemdescriptionView itemdescriptionView;
    #endregion

    public readonly float portalDelay = GlobalDefine.PlayerPortalDelayTime;
    protected override void Awake()
    {
        base.Awake();

        itemLayer = LayerMask.NameToLayer(GlobalDefine.String_Item);
        gameObject.tag = GlobalDefine.String_Player;

        if (abilitySystem == null)
        {
            Debug.LogError("AbilitySystem을 찾을 수 없습니다!");
            return;
        }

        GameManager.OnGameOver += OnGameOver;
        GameManager.OnNextFlow += OnNextFlow;
        PCInputController pc = inputController as PCInputController;
        if (pc != null)
        {
            pc.Onfire += ActivateAbilityAttack;
        }
        
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver -= OnGameOver;
        GameManager.OnNextFlow -= OnNextFlow;
        PCInputController pc = inputController as PCInputController;
        if (pc != null)
        {
            pc.Onfire -= ActivateAbilityAttack;
        }
    }
    

    private void Update()
    {
        if (GetDead())
            return;

        PCMove();
        RotateToMouseDirection();
        HeadNameless();
        HasPickupablesNearby();

        FallDeathCheck();
    } 

    /// <summary>
    /// 공격 어빌리티
    /// </summary>
    protected virtual void ActivateAbilityAttack()
    {
        abilitySystem.ActivateAbility(eTagType.Attack, this);
    }

    public void OnJumpStart()
    {
        controller.SetState(AnimState.JumpStart);
    }

    public void OnJumpEnd()
    {
        controller.SetState(AnimState.Land);
    }  

    public void FallDeathCheck()
    {
        if(transform.position.y < GlobalDefine.FallDeath)
        {
            GameManager.instance.GameOver();
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (detectedItems.Contains(other)) return;
        detectedItems.Add(other);

        IPickupable pickup = other.GetComponent<IPickupable>();
        if (pickup != null)
        {            
            pickup.OnPickup(this);
        }
    }

    #region 아이템 설명창
    public void HasPickupablesNearby()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, scanRadius, buffer, itemLayer);
        if(count  == 0 )
        {
            itemdescriptionView.gameObject.SetActive(false);
            return;
        }
        
        if (buffer[0] != null && buffer[0].TryGetComponent<IPickupable>(out var pickup))
        {             
            itemdescriptionView.SetData(pickup);
        }
    }
    #endregion
    
    public void OnGameOver()
    {
        isDead = true;
        gameplayTagSystem.AddTag(eTagType.Player_State_IgnoreInput);
        GetModelController().SetState(AnimState.Death);
    }

    


    public void PortalDelay()
    {
        StartCoroutine(CoPortalDelay());
    }

    private IEnumerator CoPortalDelay()
    {
        gameplayTagSystem.AddTag(eTagType.Player_State_IgnorePortal);
        float deltime = 0f;
        while(deltime < portalDelay)
        {
            deltime += Time.deltaTime;
            yield return null;
        }
        gameplayTagSystem.RemoveTag(eTagType.Player_State_IgnorePortal);
    }

    public void OnNextFlow()
    {     
        gameplayTagSystem.AddTag(eTagType.Player_State_IgnoreInput);
        StartCoroutine(CoOnNextLeveling());
    
    }

    private IEnumerator CoOnNextLeveling()
    {
        gameplayTagSystem.AddTag(eTagType.Player_State_IgnoreInput);      
        yield return GlobalDefine.FadeInDelayTime;
        transform.position = Vector3.zero;

        while (GameManager.isAnimAction)
        {
            yield return null;
        }
        gameplayTagSystem.RemoveTag(eTagType.Player_State_IgnoreInput);
    }

}

