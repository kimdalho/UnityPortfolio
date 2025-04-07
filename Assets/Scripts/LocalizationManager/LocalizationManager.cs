using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    public enum Language { KO, EN, JP }

    public delegate void LanguageChanged();
    public static event LanguageChanged OnLanguageChanged;

    private Dictionary<string, string> localizedText;
    [SerializeField]
    private Language currentLanguage = Language.EN;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCSV(currentLanguage);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadCSV(Language language)
    {
        currentLanguage = language;
        localizedText = new Dictionary<string, string>();

        TextAsset csvData = Resources.Load<TextAsset>("Localization");
        string[] lines = csvData.text.Split('\n');
        int langIndex = (int)language + 1;

        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Trim().Split(',');

            if (fields.Length > langIndex)
            {
                string key = fields[0];
                string value = fields[langIndex];
                localizedText[key] = value;
            }
        }

        OnLanguageChanged?.Invoke();
    }

    public string GetText(string id)
    {
        if (localizedText.TryGetValue(id, out string value))
            return value;

        return $"#{id}";
    }

    public void ChangeLanguage(Language newLang)
    {
        LoadCSV(newLang);
    }
}