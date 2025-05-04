using TMPro;
using UnityEngine;
/// <summary>
/// RequireComponent ó�� �ẽ ����
/// TMP_Text ������Ʈ�� ����ȭ �Ѵ� ������ ������ ���ֵȴ�.
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
        // �ʱ�ȭ�� �ȵ����� �����ϰ�, �̺�Ʈ�� ���� ���ŵǵ��� ���
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