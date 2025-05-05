using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private float time;
    [SerializeField]
    private Slider slider_Loading;
    

    private IEnumerator Start()
    {
        yield return null;
        var str = SceneContainer.Instance.GetTargetSceneString();
        StartCoroutine(LoadingAsync(str));
    }

    IEnumerator LoadingAsync(string name)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        asyncOperation.allowSceneActivation = false; //로딩이 완료되는대로 씬을 활성화할것인지

        while (asyncOperation.progress < 0.9f)
        {                       
            slider_Loading.value = Mathf.Lerp(0, 1, asyncOperation.progress) ;         
            yield return null;
        }
        slider_Loading.value = 1f;
        
        asyncOperation.allowSceneActivation = true; //씬 활성화        
    }
}
