using UnityEngine;

public class Player : Character
{
    public static Player Instance { get; private set; }

    protected override void OnAwake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    protected override void OnStart()
    {
    }

    protected override void OnUpdate() {

    }

    protected override void OnAttack() {

    }

    protected override void OnTakeDamage() {
        StatsPanel.Instance.UpdateStatsPanel();
    }
}
