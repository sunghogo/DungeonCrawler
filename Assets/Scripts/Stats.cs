public enum StatType
{
    LVL,
    MaxHP,
    HP,
    MaxMP,
    MP,
    MaxXP,
    XP,
    XPDrop,
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
            "MaxHP"    => StatType.MaxHP,
            "HP"        => StatType.HP,
            "MaxMP"    => StatType.MaxMP,
            "MP"        => StatType.MP,
            "MaxXP"    => StatType.MaxXP,
            "XP"        => StatType.XP,
            "XPDrop"  => StatType.XPDrop,
            "ATK"       => StatType.ATK,
            "DEF"       => StatType.DEF,
            "SPD"       => StatType.SPD,
            _ => throw new System.ArgumentException($"Unknown tag: {tag}")
        };
}
