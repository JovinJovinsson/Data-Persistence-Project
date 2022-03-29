using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Added to make this into a Singleton for persistence in session
    public static ScoreManager Instance;

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
    }
}
