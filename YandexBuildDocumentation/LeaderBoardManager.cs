using UnityEngine;
using UnityEngine.UI;
using YG;

public class LeaderBoardManager : MonoBehaviour
{
    public static LeaderBoardManager Instance;

    [SerializeField]
    private LeaderboardYG leaderboardYG;

    public void Awake()
    {
        if (LeaderBoardManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PauseSound()
    {
        AudioListener.volume = 0f;
        Time.timeScale = 0f;
    }

    public void ResumeSound()
    {
        AudioListener.volume = 1f;
        Time.timeScale = 1f;
    }

    public void Start()
    {
        //YandexGame.ConsumePurchases();
    }

    public void NewScore(int score)
    {
        // ??????????? ????? ?????????? ?????? ???????
        YandexGame.NewLeaderboardScores(leaderboardYG.nameLB, score);

        // ????? ?????????? ?????? ??????? ?????????? ? ?????????? LeaderboardYG
        // leaderboardYG.NewScore(int.Parse(scoreLbInputField.text));
    }
}
