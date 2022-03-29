using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    // Added to make this into a Singleton for persistence in session
    public static ScoreManager Instance;

    [SerializeField] private string highScorePlayerName;
    public string HighScorePlayerName
    {
        get { return highScorePlayerName; }
    }
    [SerializeField] private int highScore;
    public int HighScore
    {
        get { return highScore; }
    }

    [SerializeField] private string playerName;
    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadHighScore();
    }

    // This is an update method for the score
    public void UpdateHighScore(int score)
    {
        // If the high score has been passed, update the values
        if (score >= highScore)
        {
            highScorePlayerName = PlayerName;
            highScore = score;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string highScorePlayerName;
        public int highScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();

        data.highScore = HighScore;
        data.highScorePlayerName = HighScorePlayerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    private void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScorePlayerName = data.highScorePlayerName;
            highScore = data.highScore;
        } else
        {
            highScorePlayerName = "_ _ _";
            highScore = 0;
        }
    }
}
