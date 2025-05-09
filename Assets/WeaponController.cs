using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Animator Animator;
    public eWeaponType eWeaponType;
    public Transform bulletStartPos;
    public ParticleSystem muzzle;
    
    private void Awake()
    {
        //주의 사항 애니메이터가 없는 웨폰도 존재함
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
