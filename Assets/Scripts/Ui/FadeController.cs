using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public interface IOnNextFlow
{
    public void OnNextFlow();
   
}

public class FadeController : MonoBehaviour , IOnNextFlow
{
    public CanvasGroup img;
    float duration = 0.4f;

    private void Awake()
    {
        GameManager.OnNextFlow += OnNextFlow;
        StartFadeOut();
    }

    private void OnDestroy()
    {
        GameManager.OnNextFlow -= OnNextFlow;
    }

    public void OnNextFlow()
    {
        StartCoroutine(CoNextFlow());
    }

    private IEnumerator CoNextFlow()
    {
        yield return CoStartFadeIn();

        yield return new WaitForSeconds(0.3f);

        while (GameManager.Leveling)
        {
            yield return null;
        }

        yield return CoStartFadeOut();
    }



    public void StartFadeIn()
    {
        StartCoroutine(CoStartFadeIn());
    }

    public IEnumerator CoStartFadeIn()
    {
        float deltime = 0f;
       
        img.alpha = 0;
        gameObject.SetActive(true);
        while (deltime < duration)
        {
            float t = deltime / duration;
            img.alpha = Mathf.Lerp(0, 1,t);
            yield return null;
            deltime += Time.deltaTime;
        }
        img.alpha = 1f;
    }

    public void StartFadeOut()
    {
        StartCoroutine(CoStartFadeOut());
    }

    private IEnumerator CoStartFadeOut()
    {
        float deltime = 0f;
        
        img.alpha = 1;
        gameObject.SetActive(true);
        while (deltime < duration)
        {
            float t = deltime / duration;
            img.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
            deltime += Time.deltaTime;
        }
        img.alpha = 0f;      
    }
}
