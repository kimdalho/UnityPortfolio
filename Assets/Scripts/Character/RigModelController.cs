using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class RigModelController : ModelController
{

    //public Rig HeadAnim;
    //public Rig HandRighAnim;
    //public Rig HandLeftAnim;
    //public Rig LowerarmLeftAnim;
    //public Rig LowerarmRightAnim;


    //파츠바디 타입에는 무기를 포함하지않는public Dictionary<eEuipmentType, List<GameObject>> partsByType = new Dictionary<eEuipmentType, List<GameObject>>();다.    
    protected override  void InitializeParts()
    {
        base.InitializeParts();     
    }

    private void Awake()
    {
        InitializeParts();
        dicWeapons = new Dictionary<eWeaponType, WeaponController>();
        foreach (var weapon in m_weapons)
        {
            dicWeapons.Add(weapon.eWeaponType, weapon);
        }
    }
}
