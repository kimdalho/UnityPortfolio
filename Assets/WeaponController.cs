using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Animator Animator;
    public eWeaponType eWeaponType;
    public Transform bulletStartPos;
    public ParticleSystem muzzle;
    
    private void Awake()
    {
        //���� ���� �ִϸ����Ͱ� ���� ������ ������
        if(TryGetComponent<Animator>(out Animator) == true)
        {

        }

    }

    public void PlayEffect()
    {
        SoundManager.instance.PlayEffect(eEffectType.Shoot, bulletStartPos);
        muzzle.Play();
    }


}
