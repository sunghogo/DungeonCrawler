using UnityEngine;

[System.Serializable]
public class BasicAttack : IAttack
{
    public AttackType Type => AttackType.BasicAttack;
    public float MPCost => 0f;

    public void Execute(ICombatant attacker, ICombatant target)
    {
        float damage = Mathf.Max(attacker.ATK - target.DEF, 1f);
        target.TakeDamage(damage);
    }
}

[System.Serializable]
public class Bash : IAttack
{
    public AttackType Type => AttackType.Bash;
    public float MPCost => 3f;

    public void Execute(ICombatant attacker, ICombatant target)
    {
        float damage = Mathf.Max((attacker.ATK * 1.5f) - target.DEF, 1f);
        target.TakeDamage(damage);
    }
}

[System.Serializable]
public class Crush : IAttack
{
    public AttackType Type => AttackType.Crush;
    public float MPCost => 5f;

    public void Execute(ICombatant attacker, ICombatant target)
    {
        float damage = attacker.ATK * 1.25f;
        target.TakeDamage(damage);
    }
}
