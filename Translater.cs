using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Класс работы с переводом текстов.
/// Обязательно нужно создать файл Dictionary.text
/// в папке  Assets/Resources/Data/ перед использованием
/// </summary>
public class Translater : MonoBehaviour, ITranslator
{
    public static Translater Instance;
    [SerializeField] private string[] DictionaryLanguages;
    [SerializeField] private string[] DictionaryWordsToTranslate;
    [SerializeField] private int wordIndex = -1;
    [SerializeField] private Language m_Language = Language.EN;
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
        translateObjects[0].text = Translater.Instance.GetTranslate("Получи 100 монет за рекламу!");
        translateObjects[1].text = Translater.Instance.GetTranslate("Увеличишь свой рекорд?");
        translateObjects[2].text = Translater.Instance.GetTranslate("Пройдёшь 80 препятствий?");
        translateObjects[3].text = Translater.Instance.GetTranslate("Сможешь получить 150 монет?");
        translateObjects[4].text = Translater.Instance.GetTranslate("Продержишься в игре 3 минуты?");
        translateObjects[5].text = Translater.Instance.GetTranslate("Не хотите оценить игру?");
        translateObjects[6].text = Translater.Instance.GetTranslate("Нет");
        translateObjects[7].text = Translater.Instance.GetTranslate("Позже");
        translateObjects[8].text = Translater.Instance.GetTranslate("Да");
        translateObjects[9].text = Translater.Instance.GetTranslate("Игра Окончена");
        translateObjects[10].text = Translater.Instance.GetTranslate("Реклама пока не готова...");
        translateObjects[11].text = Translater.Instance.GetTranslate("Желаете ли продолжить за просмотр рекламы?");
        translateObjects[12].text = Translater.Instance.GetTranslate("Нет");
        translateObjects[13].text = Translater.Instance.GetTranslate("Да");
        if (Parser.IntParsing(translateObjects[14].text) == 0)
            translateObjects[14].text = Translater.Instance.GetTranslate("Выбрать");
        if (Parser.IntParsing(translateObjects[15].text) == 0)
            translateObjects[15].text = Translater.Instance.GetTranslate("Выбрать");
        if (Parser.IntParsing(translateObjects[16].text) == 0)
            translateObjects[16].text = Translater.Instance.GetTranslate("Выбрать");
        if (Parser.IntParsing(translateObjects[17].text) == 0)
            translateObjects[17].text = Translater.Instance.GetTranslate("Выбрать");
        if (Parser.IntParsing(translateObjects[18].text) == 0)
            translateObjects[18].text = Translater.Instance.GetTranslate("Выбрать");
        if (Parser.IntParsing(translateObjects[19].text) == 0)
            translateObjects[19].text = Translater.Instance.GetTranslate("Выбрать");
        if (Parser.IntParsing(translateObjects[20].text) == 0)
            translateObjects[20].text = Translater.Instance.GetTranslate("Выбрать");
        if (Parser.IntParsing(translateObjects[21].text) == 0)
            translateObjects[21].text = Translater.Instance.GetTranslate("Выбрать");
        if (Parser.IntParsing(translateObjects[22].text) == 0)
            translateObjects[22].text = Translater.Instance.GetTranslate("Выбрать");
        if (Parser.IntParsing(translateObjects[23].text) == 0)
            translateObjects[23].text = Translater.Instance.GetTranslate("Выбрать");
    }

    private void LoadLanguage()
    {
        string langCode = PlayerPrefs.GetString("CurrentLanguage", "EN");
        GameManager.instance.SetCheckmarksOnLanguage(langCode);
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
        ChangeLanguage();
    }
    public void DictionaryLanguagesSplit()
    {
        #if UNITY_EDITOR
        DictionaryLanguages = FileManager.ReadFile("C:/Users/Kenny McCormic/Desktop/GameTemplates/aMoonCat-main/Assets/MoonCatResources/Data/Dictionary.txt").Split('/');
        #endif
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(Translater))]
public class TranslaterEditor : Editor
{
    public Translater translater;

    private void OnEnable()
    {
        translater = (Translater)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Dictionary Languages Split"))
            translater.DictionaryLanguagesSplit();
    }
}
#endif