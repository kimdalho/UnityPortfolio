using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Animator Animator;
    public eWeaponType eWeaponType;
    public Transform bulletStartPos;
    
    private void Awake()
    {
        //���� ���� �ִϸ����Ͱ� ���� ������ ������
        if(TryGetComponent<Animator>(out Animator) == true)
        {

        }

    }



}
