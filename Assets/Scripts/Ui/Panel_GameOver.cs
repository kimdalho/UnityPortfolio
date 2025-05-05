using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
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
        int _Score = GetScore();
        while (deltime < duration)
        {
            float t = deltime / duration;
            var value = Mathf.Lerp(0, _Score, t);
            str = string.Format("스코어 {0}",value);
            Tmp_Score.text = str;
            deltime += Time.deltaTime;
            yield return null;
        }
        str = string.Format("스코어 {0}", _Score);
        Tmp_Score.text = str;
        deltime += Time.deltaTime;

        yield return new WaitForSeconds(0.3f);
        reStart.gameObject.SetActive(true);
        UserData.Instance.DataReset();

    }

    public int GetScore()
    {
        int score = 0;
        var ud = UserData.Instance;
        score += ud.CurIndex * 100;
        score += ud.level1_itemCount * 30;
        score += ud.level2_itemCount * 50;
        score += ud.level3_itemCount * 50;

        score += ud.level1_killMonster * 60;
        score += ud.level2_killMonster * 80;
        score += ud.level3_killMonster * 100;

        return score;
    }





    private void OnDestroy()
    {
        
        GameManager.OnGameOver -= OnGameOver;
    }
}


