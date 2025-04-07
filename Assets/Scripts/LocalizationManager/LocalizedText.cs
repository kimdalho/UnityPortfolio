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
    private string localizationID;

    private TMP_Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        LocalizationManager.OnLanguageChanged += UpdateLocalizedText;
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

    public void UpdateLocalizedText()
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

    public void SetLocalizationID(string newID)
    {
        localizationID = newID;
        UpdateLocalizedText();
    }
}