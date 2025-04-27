using TMPro;
using UnityEngine;
/// <summary>
/// RequireComponent 처음 써봄 역할
/// TMP_Text 컴포넌트를 강제화 한다 없으면 에러로 간주된다.
/// </summary>
[RequireComponent(typeof(TMP_Text))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    protected string localizationID;

    protected TMP_Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        LocalizationManager.OnLanguageChanged += UpdateLocalizedText;
    }

    public void UnUpdate()
    {
        localizationID = string.Empty;
    }

    private void OnDestroy()
    {
        LocalizationManager.OnLanguageChanged -= UpdateLocalizedText;
    }

    private void OnEnable()
    {
        // 초기화가 안됐으면 무시하고, 이벤트를 통해 갱신되도록 대기
        if (LocalizationManager.Instance != null)
        {
            UpdateLocalizedText();
        }
    }

    public virtual void UpdateLocalizedText()
    {
        if (string.IsNullOrEmpty(localizationID))
        {
            Debug.LogWarning($"[{name}] Localization ID is empty.");
            return;
        }

        if (LocalizationManager.Instance == null)
            return;

        string localizedText = LocalizationManager.Instance.GetText(localizationID);
        textComponent.text = localizedText;
    }

    public void UpdateLocalizedText(float value)
    {
        if (string.IsNullOrEmpty(localizationID))
        {
            Debug.LogWarning($"[{name}] Localization ID is empty.");
            return;
        }

        if (LocalizationManager.Instance == null)
            return;

        string localizedText = LocalizationManager.Instance.GetText(localizationID);
        localizedText = string.Format(localizedText, value);
        textComponent.text = localizedText;
    }

    public void UpdateLocalizedText(float value1, float value2)
    {
        if (string.IsNullOrEmpty(localizationID))
        {
            Debug.LogWarning($"[{name}] Localization ID is empty.");
            return;
        }

        if (LocalizationManager.Instance == null)
            return;

        string localizedText = LocalizationManager.Instance.GetText(localizationID);
        localizedText = string.Format(localizedText, value1, value2);
        textComponent.text = localizedText;
    }

    public void SetLocalizationID(string newID)
    {
        localizationID = newID;
        UpdateLocalizedText();
    }
}