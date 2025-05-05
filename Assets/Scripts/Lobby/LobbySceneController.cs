using UnityEngine;
using UnityEngine.UI;
public class LobbySceneController : MonoBehaviour
{
    public static LobbySceneController Instance { get; private set; }
    public Button BtnNew;    
    public Button BtnSetting;
    public Button BtnExit;

    public GameObject panel_Title;
    public Panel_Setting panel_Setting;
    public Panel_CharacterSelect panel_CharacterSelect;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        SoundManager.instance.PlayBGM(eBGMType.LobbySoundTrack);
        BtnNew.GetComponent<Button>().onClick.AddListener(OnClickCharacterSelect);        
        BtnSetting.GetComponent<Button>().onClick.AddListener(OnClickSettingButton);
        BtnExit.GetComponent<Button>().onClick.AddListener(OnClickExitButton);
    }


    public void OnClickCharacterSelect()
    {
        panel_CharacterSelect.gameObject.SetActive(true);
        panel_Title.gameObject.SetActive(false);

    }

    public void OnClickSettingButton()
    {
        panel_Setting.gameObject.SetActive(true);

    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }



}

