public enum AttackType
{
    BasicAttack,
    Bash,
    Crush
}

public static class AttackUtils
{
    public static AttackType TagToAttack(string tag) =>
        tag switch
        {
            "BasicAttack" => AttackType.BasicAttack,
            "Bash" => AttackType.Bash,
            "Crush" => AttackType.Crush,
            _ => throw new System.ArgumentException($"Unknown tag: {tag}")
        };
}

public interface IAttack
{
    AttackType Type { get; }
    float MPCost { get; }
    void Execute(ICombatant attacker, ICombatant target);
}
