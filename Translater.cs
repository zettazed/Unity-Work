using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Класс работы с переводом текстов.
/// Обязательно нужно создать файл Dictionary.text
/// в папке  Assets/Resources/Data/ перед использованием
/// </summary>
public class Translater : MonoBehaviour, ITranslator
{
    public static Translater Instance;
    private string[] DictionaryLanguages;
    private string[] DictionaryWordsToTranslate;
    private int wordIndex = -1;
    private Language m_Language = Language.EN;
    [SerializeField] private List<Text> translateObjects = new List<Text>();

    private enum Language
    {
        RU,
        EN
    };

    private void Awake() => Instance = this;

    private void Start() => LoadLanguage();

    private void ChangeLanguage()
    {
        foreach (Text translateObject in translateObjects)
            translateObject.text = GetTranslate(translateObject.text);
    }

    private void LoadLanguage()
    {
        string langCode = PlayerPrefs.GetString("CurrentLanguage", "EN");
        SetLanguageLocal(langCode);
    }

    public string GetTranslate(string text, Text translateObject)
    {
        translateObjects.Add(translateObject);

        return Translate(text);
    }

    public string GetTranslate(string text)
    {
        return Translate(text);
    }

    private string Translate(string text)
    {
        switch (m_Language)
        {
            case Language.EN:
                Debug.Log("EN");
                DictionaryLanguages = FileManager.ReadFile("C:/Users/Kenny McCormic/Desktop/GameTemplates/aMoonCat-main/Assets/MoonCatResources/Data/Dictionary.txt").Split('/');
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

            case Language.RU:
                Debug.Log("RU");
                DictionaryLanguages = FileManager.ReadFile("C:/Users/Kenny McCormic/Desktop/GameTemplates/aMoonCat-main/Assets/MoonCatResources/Data/Dictionary.txt").Split('/');
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
        }

        return text;
    }

    public void SetLanguage(string langCode)
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
        ChangeLanguage();
        PlayerPrefs.Save();
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
