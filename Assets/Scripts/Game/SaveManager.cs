using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string BEST_SCORE_KEY = "BestScore";
    public static void SaveBestScore(int score)
    {
        PlayerPrefs.SetInt(BEST_SCORE_KEY, score);
        PlayerPrefs.Save();
    }
    public static int GetBestScore()
    {
        return PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
    }
    public static void ResetBestScore()
    {
        PlayerPrefs.DeleteKey(BEST_SCORE_KEY);
    }
}