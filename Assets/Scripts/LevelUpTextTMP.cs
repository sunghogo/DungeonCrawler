using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LevelUpTextTMP : TextTMP, IPointerClickHandler
{
    [SerializeField] float smallUpgradeAmount = 1f;
    [SerializeField] float bigUpgradeAmount = 5f;

    void LevelUpStat()
    {
        string tag = gameObject.tag;
        StatType stat = StatUtils.TagToStat(tag);

        float upgradeAmount = (stat == StatType.MaxHP || stat == StatType.MaxMP)
            ? bigUpgradeAmount
            : smallUpgradeAmount;

        Player.Instance.IncreaseStat(stat, upgradeAmount);
        StatsPanel.Instance.UpdateStatsPanel();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LevelUpStat();
        LevelUpPanel.Instance.Deactivate();
    }
}
