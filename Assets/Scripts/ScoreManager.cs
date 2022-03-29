using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    // Added to make this into a Singleton for persistence in session
    public static ScoreManager Instance;

    readonly string saveFile = Application.persistentDataPath + "/highscore.json";

    [SerializeField] private string playerName;

    [SerializeField] private string highScorePlayerName;
    [SerializeField] private int highScore;

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
    }

    [System.Serializable]
    class SaveData
    {
        public string highScorePlayerName;
        public int highScore;
    }

    private void SaveHighScore(int newScore)
    {
        SaveData data = new SaveData();

        data.highScore = newScore;
        data.highScorePlayerName = PlayerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFile, json);
    }

    private void LoadHighScore()
    {
        if (File.Exists(saveFile))
        {
            string json = File.ReadAllText(saveFile);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScorePlayerName = data.highScorePlayerName;
            highScore = data.highScore;
        }
    }
}
