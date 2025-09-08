using UnityEngine;
using System.Collections.Generic;

public class Player : Character
{
    public static Player Instance { get; private set; }

    void HandleGameOver()
    {
        InitializeStats();
        SelectAttack(AttackType.BasicAttack);
    }

    void HandleGameStart()
    {
        InitializeStats();
        SelectAttack(AttackType.BasicAttack);
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

    protected override void OnStart()
    {
        Attacks.Add(new Bash());
        Attacks.Add(new Crush());
    }

    protected override void OnDestroyed()
    {
        GameManager.OnGameOver -= HandleGameOver;
        GameManager.OnGameStart -= HandleGameStart;
    }

    protected override void OnSelectAttack()
    {
        SkillPanel.Instance.UpdateTexts();
    }

    protected override void OnAttack()
    {
        StatsPanel.Instance.UpdateStatsPanel();
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
