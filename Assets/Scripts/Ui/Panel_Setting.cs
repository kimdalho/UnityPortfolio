using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Setting : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tmp_BGM;
    [SerializeField]
    private TextMeshProUGUI tmp_soundEffect;

    [SerializeField]
    private Slider sliderBGM;
    [SerializeField]
    private Slider sliderSoundEffect;

    [SerializeField]
    private Button btn_Ko;
    [SerializeField]
    private Button btn_En;
    [SerializeField]
    private Button btn_Jp;
    [SerializeField]
    private Button btn_Ok;

    private void Awake()
    {
        btn_Ko.onClick.AddListener(OnClickKoButton);
        btn_En.onClick.AddListener(OnClickEnButton);
        btn_Jp.onClick.AddListener(OnClickJpButton);
        btn_Ok.onClick.AddListener(OnClickOkButton);
    }


    private void OnEnable()
    {
       var curLanguage =  LocalizationManager.Instance.GetLanguage();
       //솔직히 확장성은 없다
       //언어를 확장할거라면 딕셔너리로 만들었어야함

       switch(curLanguage)
       {
            case LocalizationManager.Language.KO:
                ChangeButtonColor(btn_Ko.image, btn_En.image, btn_Jp.image);
                break;
            case LocalizationManager.Language.EN:
                ChangeButtonColor(btn_En.image, btn_Jp.image, btn_Ko.image);
                break;
            case LocalizationManager.Language.JP:
                ChangeButtonColor(btn_Jp.image, btn_En.image, btn_Ko.image);
                break;
            default:
                Debug.LogWarning($"NotFound Current LangaugeType {curLanguage}");
                break;

       }

    }


    private void OnClickKoButton()
    {
        LocalizationManager.Instance.ChangeLanguage(LocalizationManager.Language.KO);
        ChangeButtonColor(btn_Ko.image, btn_En.image, btn_Jp.image);
    }

    private void OnClickEnButton()
    {
        LocalizationManager.Instance.ChangeLanguage(LocalizationManager.Language.EN);
        ChangeButtonColor(btn_En.image, btn_Jp.image, btn_Ko.image);
    }

    private void OnClickJpButton()
    {
        LocalizationManager.Instance.ChangeLanguage(LocalizationManager.Language.JP);
        ChangeButtonColor(btn_Jp.image, btn_En.image, btn_Ko.image);
    }

    private void OnClickOkButton()
    {
        gameObject.SetActive(false);
    }

    private void ChangeButtonColor(Image selected,Image unselected1,Image unselected2)
    {
        selected.color = Color.blue;
        unselected1.color = Color.white;
        unselected2.color = Color.white;
    }








}
