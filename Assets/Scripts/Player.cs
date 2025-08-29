using UnityEngine;

public class Player : Character
{
    protected override void OnStart() {
    }

    protected override void OnUpdate() {

    }

    protected override void OnAttack() {

    }

    protected override void OnTakeDamage() {
        StatsPanel.Instance.UpdateStatsPanel();
    }
}
