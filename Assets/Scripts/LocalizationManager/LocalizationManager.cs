using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager 
{
    public static LocalizationManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LocalizationManager();
                LoadCSV(currentLanguage);
            }
            return instance;
        }
    }
    private static LocalizationManager instance;
    public enum Language { KO, EN, JP }

    public delegate void LanguageChanged();
    public static event LanguageChanged OnLanguageChanged;

    private static Dictionary<string, string> localizedText;
    [SerializeField]
    private static Language currentLanguage = Language.KO;


    public static void LoadCSV(Language language)
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

    public Language GetLanguage()
    {
        return currentLanguage;
    }
}