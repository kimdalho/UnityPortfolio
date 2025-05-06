using NUnit.Framework.Interfaces;
using System;
using UnityEngine;

public abstract class EquipmentItem : BaseItem
{
    //장비 아이템은 어느 파츠인지 타입을 가지고있다.


    private Vector3 rotationSpeed = new Vector3(0, 30, 0);

   
    public override void OnPickup(Character source) 
    {        
        Player player = source.GetComponent<Player>();

        if (abilityPrefab != null)
        {
            FXFactory.Instance.GetFX("Effect_NinjaSkill", source.transform);
            SoundManager.instance.PlayEffect(eEffectType.levelup);
            var newskill = Instantiate(abilityPrefab);
            var skillCompo = newskill.GetComponent<GameAbility>();
            player.GetAbilitySystem().AddAndActivateAbility(skilltag, skillCompo,source);
            UserData.Instance.SetPickupedItem(rank);
        }
        else
        {
            var newSKill = new GameObject();
            var skillCompo = CreateSkill(skilltag, newSKill);
            player.GetAbilitySystem().AddAndActivateAbility(skilltag, skillCompo, source);                      
        }

        ItemApplyEffect(source, this);
    }




    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
    /// <summary>
    /// 살짝 잘못짰다.. 에초에 웨폰이랑 아이템을 고유한 클래스로 만들었다면 이게 필요없는데..
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public GameAbility CreateSkill(eTagType tag,GameObject obj)
    { 
        switch(tag)
        {
            case eTagType.ninjahead:
                return obj.AddComponent<GA_NinjaHead>();
            case eTagType.alienhead:
                return obj.AddComponent<GA_AlienHead>();
            case eTagType.bearhead:
                return obj.AddComponent<GA_BearHead>();
            case eTagType.ninjabody:
                return obj.AddComponent<GA_NinjaBody>();
            case eTagType.grasshead:
                return obj.AddComponent<GA_GrassHead>();
            case eTagType.Roostershead:
                return obj.AddComponent<GA_RoostersHead>();
            case eTagType.clownhair:
                return obj.AddComponent<GA_ClownHair>();
            case eTagType.boxhead:
                return obj.AddComponent<GA_BoxHead>();
            case eTagType.beartorso:
                return obj.AddComponent<GA_BearTorso>();
            case eTagType.grasstrunk:
                return obj.AddComponent<GA_GrassTrunk>();
            case eTagType.Roostersbody:
                return obj.AddComponent<GA_RoostersBody>();
            case eTagType.clowntorso:
                return obj.AddComponent<GA_ClownTorso>();
            case eTagType.boxbody:
                return obj.AddComponent<GA_BoxBody>();
            default:
                break;
               
        }

        return null;


    }

    public override void Init(PickupItemData data) 
    {
        base.Init(data);
        rank = data.Rank;
    }     

}


