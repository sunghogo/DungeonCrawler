using UnityEngine;
using System;
using System.Collections.Generic;

public enum GameState
{
    StartingScreen,
    GameStart,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnHighScoreChanged;
    public static event Action OnNextLevel;
    public static event Action OnGameStart;
    public static event Action OnGameOver;
    public static event Action OnScreenStart;


    [field: Header("Game States")]
    [field: SerializeField] public bool StartingScreen { get; private set; }
    [field: SerializeField] public bool GameStart { get; private set; }
    [field: SerializeField] public bool GameOver { get; private set; }
    [field: SerializeField] public bool ChangeLevel { get; private set; } = false;

    [field: Header("Shared Data")]
    [field: SerializeField] public int Score { get; private set; } = 0;
    [field: SerializeField] public int HighScore { get; private set; } = 0;
    [field: SerializeField] public int SpawnCount { get; private set; } = 1;
    [field: SerializeField] public List<Enemy> Enemies { get; private set; } = new List<Enemy>();

    public void IncrementScore()
    {
        ++Score;
        OnScoreChanged?.Invoke(Score);
    }

    public void ResetScore()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);
    }

    void UpdateHighScore()
    {
        HighScore = Score;
        OnHighScoreChanged?.Invoke(HighScore);
    }

    public void DecrementSpawnCount()
    {
        --SpawnCount;
        if (SpawnCount <= 0 && GameStart) OnNextLevel?.Invoke();;
    }

    public void SetSpawnCount(int newSpawnCount) => SpawnCount = newSpawnCount;

    public void AddEnemy(Enemy enemy) => Enemies.Add(enemy);

    public void RemoveEnemy(Enemy enemy) => Enemies.Remove(enemy);

    public void ProcessTurn(Enemy targetedEnemy)
    {
        List<ICombatant> combatants = new List<ICombatant>
        {
            Player.Instance,
        };
        combatants.AddRange(Enemies);
        combatants.Sort((a, b) => b.SPD.CompareTo(a.SPD));

        foreach (var combatant in combatants)
        {
            if (!combatant.IsAlive()) continue;

            if (combatant is Enemy enemy)
            {
                if (Player.Instance.IsAlive())
                {
                    enemy.Attack(Player.Instance);
                }
            }
            else if (combatant is Player p)
            {
                if (targetedEnemy.IsAlive())
                {
                    p.Attack(targetedEnemy);
                }
            }
        }
    }

    public void StartGame()
    {
        StartingScreen = false;
        GameStart = true;
        GameOver = false;
        ChangeLevel = false;
        ResetScore();
        OnGameStart?.Invoke();
        OnScoreChanged?.Invoke(Score);
        OnHighScoreChanged?.Invoke(HighScore);
    }

    public void EndGame()
    {
        StartingScreen = false;
        GameStart = false;
        GameOver = true;
        ChangeLevel = false;

        if (Score > HighScore)
        {
            UpdateHighScore();
        }
        OnGameOver?.Invoke();
    }

    public void StartScreen()
    {
        StartingScreen = true;
        GameStart = false;
        GameOver = false;
        ChangeLevel = false;
        OnScreenStart?.Invoke();
    }

    public void NextLevel()
    {
        ChangeLevel = true;
        OnNextLevel?.Invoke();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Instance.StartGame();   
    }
}
