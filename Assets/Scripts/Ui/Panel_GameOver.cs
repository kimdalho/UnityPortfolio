using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Panel_GameOver : MonoBehaviour ,IOnGameOver
{
    public FadeController fadeController;
    public TextMeshProUGUI Tmp_Score;
    public Button reStart;

    public void SetData()
    {
        gameObject.SetActive(false);
        reStart.gameObject.SetActive(false);
        reStart.onClick.AddListener(OnClickRestartButton);
        GameManager.OnGameOver += OnGameOver;
    }

    private void OnClickRestartButton()
    {
        SceneContainer.Instance.LoadScene(eSceneType.LobbyScene);
    }

    public void OnGameOver()
    {
        gameObject.SetActive(true);
        StartCoroutine(CoOnGameOver());
    }

    float duration = 3;
    private IEnumerator CoOnGameOver()
    {
        float deltime = 0;
        string str;
        yield return fadeController.CoStartFadeIn();
        
        yield return new WaitForSeconds(0.3f);

        while (deltime < duration)
        {
            float t = deltime / duration;
            var value = Mathf.Lerp(0, 999, t);
            str = string.Format("스코어 {0}",value);
            Tmp_Score.text = str;
            deltime += Time.deltaTime;
            yield return null;
        }
        str = string.Format("스코어 {0}", 999);
        Tmp_Score.text = str;
        deltime += Time.deltaTime;

        yield return new WaitForSeconds(0.3f);
        reStart.gameObject.SetActive(true);


    }

    



    private void OnDestroy()
    {
        
        GameManager.OnGameOver -= OnGameOver;
    }
}
