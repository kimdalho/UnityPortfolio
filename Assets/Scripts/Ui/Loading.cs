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

    private void Awake()
    {
        var str = SceneContainer.Instance.GetTargetSceneString();
        Debug.Log($"?? {str}");
        StartCoroutine(LoadingAsync(str));
    }

    IEnumerator LoadingAsync(string name)
    {

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        asyncOperation.allowSceneActivation = false; //로딩이 완료되는대로 씬을 활성화할것인지

        while (!asyncOperation.isDone)
        { //isDone는 로딩이 완료되었는지 확인하는 변수
            time += Time.deltaTime; //시간을 더해줌
            print(asyncOperation.progress); //로딩이 얼마나 완료되었는지 0~1의 값으로 보여줌
            slider_Loading.value = asyncOperation.progress;
            //이건 로딩이 너무 빨라서 작성한거라, 무거운 씬 로딩할땐 시간 체크하는 부분은
            //생략해도 무방하다!
            if (time > 3)
            { //3초 기다림(변동가능)
                asyncOperation.allowSceneActivation = true; //씬 활성화
            }
            yield return null;
        }
        slider_Loading.value = 1f;

    }
}
