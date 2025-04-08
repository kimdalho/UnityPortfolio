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
        asyncOperation.allowSceneActivation = false; //�ε��� �Ϸ�Ǵ´�� ���� Ȱ��ȭ�Ұ�����

        while (!asyncOperation.isDone)
        { //isDone�� �ε��� �Ϸ�Ǿ����� Ȯ���ϴ� ����
            time += Time.deltaTime; //�ð��� ������
            print(asyncOperation.progress); //�ε��� �󸶳� �Ϸ�Ǿ����� 0~1�� ������ ������
            slider_Loading.value = asyncOperation.progress;
            //�̰� �ε��� �ʹ� ���� �ۼ��ѰŶ�, ���ſ� �� �ε��Ҷ� �ð� üũ�ϴ� �κ���
            //�����ص� �����ϴ�!
            if (time > 3)
            { //3�� ��ٸ�(��������)
                asyncOperation.allowSceneActivation = true; //�� Ȱ��ȭ
            }
            yield return null;
        }
        slider_Loading.value = 1f;

    }
}
