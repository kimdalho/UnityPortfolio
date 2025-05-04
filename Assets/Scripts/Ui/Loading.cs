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
        asyncOperation.allowSceneActivation = false; //�ε��� �Ϸ�Ǵ´�� ���� Ȱ��ȭ�Ұ�����

        while (asyncOperation.progress < 0.9f)
        {                       
            slider_Loading.value = Mathf.Lerp(slider_Loading.value, asyncOperation.progress, Time.deltaTime) ;         
            yield return null;
        }
        slider_Loading.value = 1f;
        yield return new WaitForSeconds(2);
        asyncOperation.allowSceneActivation = true; //�� Ȱ��ȭ        
    }
}
