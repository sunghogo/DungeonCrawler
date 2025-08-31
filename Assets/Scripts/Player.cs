using UnityEngine;

public class Player : Character
{
    public static Player Instance { get; private set; }

    void HandleGameOver()
    {
        InitializeStats();
    }

    void HandleGameStart()
    {
        StatsPanel.Instance.UpdateStatsPanel();
    }

    protected override void OnAwake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        GameManager.OnGameOver += HandleGameOver;
        GameManager.OnGameStart += HandleGameStart;
    }

    protected override void OnDestroyed()
    {
        GameManager.OnGameOver -= HandleGameOver;
        GameManager.OnGameStart -= HandleGameStart;
    }

    protected override void OnTakeDamage()
    {
        StatsPanel.Instance.UpdateStatsPanel();
    }

    protected override void OnXPGain()
    {
        StatsPanel.Instance.UpdateStatsPanel();
    }

    protected override void OnLevelUp()
    {
        HP = MaxHP;
        MP = MaxMP;
        StatsPanel.Instance.UpdateStatsPanel();
        LevelUpPanel.Instance.Activate();
    }

    protected override void OnDeath()
    {
        GameManager.Instance.EndGame();
    }
}
