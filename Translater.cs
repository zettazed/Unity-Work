using UnityEngine;

/// <summary>
/// Класс работы с переводом текстов.
/// Обязательно нужно создать файл Dictionary.text
/// в папке  Assets/Resources/Data/ перед использованием
/// </summary>
public class Translater : MonoBehaviour
{
    public static Language m_Language;
    [SerializeField] private static string RU_Dictionary;
    [SerializeField] private static string EN_Dictionary;

    public enum Language
    {
        RU,
        EN
    };

    private void Start()
    {
        LoadLanguage();
    }

    private void LoadLanguage()
    {
        string langCode = PlayerPrefs.GetString("CurrentLanguage", "EN");
        SetLanguageLocal(langCode);
    }

    public static string GetTranslate(string text)
    {
        string[] DictionaryLanguages;
        string[] DictionaryWordsToTranslate;
        int wordIndex = -1;

        switch (m_Language)
        {
            case Language.EN:
                DictionaryLanguages = FileManager.ReadFile(Application.persistentDataPath + "/MoonCatResources/Data/Dictionary.txt").Split('/'); ;
                foreach (string DictionaryLanguage in DictionaryLanguages)
                {
                    string[] DictionaryWords = DictionaryLanguage.Split('_');
                    for (int i = 0; i < DictionaryWords.Length; i++)
                    {
                        if (text == DictionaryWords[i])
                        {
                            wordIndex = i;
                            break;
                        }
                    }

                    if (wordIndex != -1)
                        break;
                }

                DictionaryWordsToTranslate = DictionaryLanguages[0].Split('_');
                text = DictionaryWordsToTranslate[wordIndex];
                break;

            case Language.RU:
                DictionaryLanguages = FileManager.ReadFile(Application.persistentDataPath + "/MoonCatResources/Data/Dictionary.txt").Split('/'); ;
                foreach (string DictionaryLanguage in DictionaryLanguages)
                {
                    string[] DictionaryWords = DictionaryLanguage.Split('_');
                    for (int i = 0; i < DictionaryWords.Length; i++)
                    {
                        if (text == DictionaryWords[i])
                        {
                            wordIndex = i;
                            break;
                        }
                    }

                    if (wordIndex != -1)
                        break;
                }

                DictionaryWordsToTranslate = DictionaryLanguages[1].Split('_');
                text = DictionaryWordsToTranslate[wordIndex];
                break;
        }
        

        return text;
    }

    public static void SetLanguage(string langCode)
    {
        switch (langCode)
        {
            case "EN":
                m_Language = Language.EN;
                PlayerPrefs.SetString("CurrentLanguage", "EN");
                break;

            case "RU":
                m_Language = Language.RU;
                PlayerPrefs.SetString("CurrentLanguage", "RU");
                break;
        }
    }

    private void SetLanguageLocal(string langCode)
    {
        switch (langCode)
        {
            case "EN":
                m_Language = Language.EN;
                break;

            case "RU":
                m_Language = Language.RU;
                break;
        }
    }
}