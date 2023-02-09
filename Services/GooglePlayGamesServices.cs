// Сохранить в облако - OpenSavedGame(true);
// Загрузить сохранения из облака - OpenSavedGame(false);
// Записать сохранения в файл для облака - OnSavedGameOpened()
// Загрузить сохранения из файла из облака - OnSavedGameDataRead()
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Text;

public class GPGSManager : MonoBehaviour
{
    private bool isSaving;
    private DateTime startDateTime;
    public static GPGSManager Instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
        {
            if (result == SignInStatus.Success)
            {
                startDateTime = DateTime.Now;
                OpenSavedGame(false);
            }
            else
            {

            }
        });
    }

    public void OpenSavedGame(bool saving)
    {
        isSaving = saving;
        OpenSavedGame("PlayerStats");
    }

    private void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (isSaving)
            {
                int money = PlayerSkinData.Instance.coins_Player;

                int noAds = PlayerSkinData.Instance.getAds();

                int[] persons = PlayerSkinData.Instance.UnlockedSkins;
                string personsUnlocked = "";
                foreach (int person in persons)
                    personsUnlocked = person.ToString() + ";";

                int highScore = PlayerSkinData.Instance.highScore_Player;

                string data = money + ";" + noAds + ";" + personsUnlocked + ";" + highScore;
                byte[] saveData = Encoding.UTF8.GetBytes(data);
                SaveGame(game, saveData);
            }
            else
            {
                LoadGameData(game);
            }
        }
        else
        {

        }
    }

    private void SaveGame(ISavedGameMetadata game, byte[] savedData)
    {
        TimeSpan currentSpan = DateTime.Now - startDateTime;
        TimeSpan totalPlaytime = game.TotalTimePlayed + currentSpan;
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Успешно сохранил");
        }
        else
        {

        }
    }

    private void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (data.Length > 0)
            {
                string dataGoogle = Encoding.ASCII.GetString(data);

                string[] s = dataGoogle.Split(';');

                //Money
                PlayerSkinData.Instance.coins_Player = Parser.StringToInt(s[0]);
                //NoAds
                if (Parser.StringToInt(s[1]) == 1)
                    PlayerSkinData.Instance.SetAds(1);
                else
                    PlayerSkinData.Instance.SetAds(0);
                //Unlocked Skins
                for (int i = 0; i < PlayerSkinData.Instance.UnlockedSkins.Length; i++)
                {
                    PlayerSkinData.Instance.UnlockedSkins[i] = Parser.StringToInt(s[i+2]);
                }
                //High Score
                PlayerSkinData.Instance.highScore_Player = Parser.StringToInt(s[7]);

                Debug.Log("Успешно загрузил");
            }
            else
            {
                Debug.Log("Нет данных, первое сохранение");
            }
        }
        else
        {

        }
    }

    public void OnApplicationQuit() => OpenSavedGame(true);
}
