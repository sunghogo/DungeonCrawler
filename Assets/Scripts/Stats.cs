public enum StatType
{
    LVL,
    MAX_HP,
    HP,
    MAX_MP,
    MP,
    MAX_XP,
    XP,
    XP_GIVEN,
    ATK,
    DEF,
    SPD,
}

public static class StatUtils
{
    public static StatType TagToStat(string tag) =>
        tag switch
        {
            "LVL"       => StatType.LVL,
            "MAX_HP"    => StatType.MAX_HP,
            "HP"        => StatType.HP,
            "MAX_MP"    => StatType.MAX_MP,
            "MP"        => StatType.MP,
            "MAX_XP"    => StatType.MAX_XP,
            "XP"        => StatType.XP,
            "XP_GIVEN"  => StatType.XP_GIVEN,
            "ATK"       => StatType.ATK,
            "DEF"       => StatType.DEF,
            "SPD"       => StatType.SPD,
            _ => throw new System.ArgumentException($"Unknown tag: {tag}")
        };
}
