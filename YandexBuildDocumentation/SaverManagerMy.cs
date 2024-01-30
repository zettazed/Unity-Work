using UnityEngine;
using UnityEngine.UI;
using YG;

public class SaverManagerMy : MonoBehaviour
{
    public static SaverManagerMy Instance;
    /*[SerializeField] InputField integerText;
    [SerializeField] InputField stringifyText;
    [SerializeField] Text systemSavesText;
    [SerializeField] Toggle[] booleanArrayToggle;*/

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    public void Save()
    {
        /*YandexGame.savesData.money = int.Parse(integerText.text);
        YandexGame.savesData.newPlayerName = stringifyText.text.ToString();

        for (int i = 0; i < booleanArrayToggle.Length; i++)
            YandexGame.savesData.openLevels[i] = booleanArrayToggle[i].isOn;*/

        YandexGame.savesData.FirstLaunch = 1;

        YandexGame.savesData.Coin = GamePlayerPrefs.coin.Value;
        YandexGame.savesData.Diamond = GamePlayerPrefs.diamond.Value;
        YandexGame.savesData.SearchItem = GamePlayerPrefs.searchItem.Value;
        YandexGame.savesData.BombItem = GamePlayerPrefs.bombItem.Value;
        YandexGame.savesData.RefreshItem = GamePlayerPrefs.refreshItem.Value;
        YandexGame.savesData.EnergyItem = GamePlayerPrefs.energyItem.Value;
        YandexGame.savesData.CountEnemy = GamePlayerPrefs.countEnemy;
        YandexGame.savesData.DaySave = GamePlayerPrefs.daySave;

        YandexGame.savesData.Lands = GamePlayerPrefs.Lands;
        YandexGame.savesData.Tutorials = GamePlayerPrefs.Tutorials;
        YandexGame.savesData.PackPurchase = GamePlayerPrefs.packPurchase;

        YandexGame.savesData.HeroPrefs = GamePlayerPrefs.HeroPrefs;
        YandexGame.savesData.DragonPrefs = GamePlayerPrefs.DragonPrefs;
        YandexGame.savesData.MapPrefs = GamePlayerPrefs.MapPrefs;
        YandexGame.savesData.HardMapPrefs = GamePlayerPrefs.HardMapPrefs;

        YandexGame.SaveProgress();
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoad()
    {
        /*integerText.text = string.Empty;
        stringifyText.text = string.Empty;

        integerText.placeholder.GetComponent<Text>().text = YandexGame.savesData.money.ToString();
        stringifyText.placeholder.GetComponent<Text>().text = YandexGame.savesData.newPlayerName;

        for (int i = 0; i < booleanArrayToggle.Length; i++)
            booleanArrayToggle[i].isOn = YandexGame.savesData.openLevels[i];

        systemSavesText.text = $"Language - {YandexGame.savesData.language}\n" +
        $"First Session - {YandexGame.savesData.isFirstSession}\n" +
        $"Prompt Done - {YandexGame.savesData.promptDone}\n";*/
        GamePlayerPrefs.coin.Value = YandexGame.savesData.Coin;
        GamePlayerPrefs.diamond.Value = YandexGame.savesData.Diamond;
        if (YandexGame.savesData.FirstLaunch == 0)
            return;

        
        GamePlayerPrefs.searchItem.Value = YandexGame.savesData.SearchItem;
        GamePlayerPrefs.bombItem.Value = YandexGame.savesData.BombItem;
        GamePlayerPrefs.refreshItem.Value = YandexGame.savesData.RefreshItem;
        GamePlayerPrefs.energyItem.Value = YandexGame.savesData.EnergyItem;
        GamePlayerPrefs.countEnemy = YandexGame.savesData.CountEnemy;
        GamePlayerPrefs.daySave = YandexGame.savesData.DaySave;

        GamePlayerPrefs.Lands = YandexGame.savesData.Lands;
        GamePlayerPrefs.Tutorials = YandexGame.savesData.Tutorials;
        GamePlayerPrefs.packPurchase = YandexGame.savesData.PackPurchase;

        GamePlayerPrefs.HeroPrefs = YandexGame.savesData.HeroPrefs;
        GamePlayerPrefs.DragonPrefs = YandexGame.savesData.DragonPrefs;
        GamePlayerPrefs.MapPrefs = YandexGame.savesData.MapPrefs;
        GamePlayerPrefs.HardMapPrefs = YandexGame.savesData.HardMapPrefs;
    }
}