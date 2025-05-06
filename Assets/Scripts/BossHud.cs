
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHud : MonoBehaviour
{
    public float fillDuration = 2;
    public Slider slider;
    public Monster bossMonster;

    public IEnumerator SetData(Monster _bossMonster)
    {
        bossMonster = _bossMonster;
        bossMonster.OnHit += OnHit;
        gameObject.SetActive(true);

        SoundManager Sm = SoundManager.instance;
        if(Sm != null)
        {
            Sm.audioSource.Pause();            
        }

        float deltime = 0;
        while (deltime < fillDuration)
        {
            var t = deltime/fillDuration;

            slider.value = Mathf.Lerp(0,1,t);           
            deltime += Time.deltaTime;
            yield return null;  
        }

        yield return null;

        if (Sm != null)
        {
            Sm.PlayBGM(eBGMType.BossTrack);
            yield return null;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);

    }

    public void OnHit()
    {
        //slider.value = Mathf.Lerp(bossMonster.attribute.GetCurValue(eAttributeType.Health), bossMonster.attribute.GetCurValue(eAttributeType.Health),1);
    }

}
