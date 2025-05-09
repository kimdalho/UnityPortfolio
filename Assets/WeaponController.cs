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
        try
        {
            SoundManager.instance.PlayEffect(eEffectType.Shoot, bulletStartPos);
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
            muzzle.Play();
        }            
    }


}
