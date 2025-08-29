using UnityEngine;
using System.Collections.Generic;

public class StatsPanel : MonoBehaviour
{
    public static StatsPanel Instance { get; private set; }

    [Header("Refs")]
    [SerializeField] Player player;
    [SerializeField] List<TextTMP> textTmps;
    [SerializeField] List<Bar> bars;

    void InitializeLists()
    {
        textTmps.Clear();
        bars.Clear();

        textTmps.AddRange(GetComponentsInChildren<TextTMP>(includeInactive: true));
        bars.AddRange(GetComponentsInChildren<Bar>(includeInactive: true));
    }

    void UpdateAllTexts() 
    {
        foreach (var textTmp in textTmps) {
            string tag = textTmp.gameObject.tag;
            string val = $"{tag}: {player.GetStat(StatUtils.TagToStat(tag)).ToString()}";
            textTmp.UpdateText(val);
        }
    }

    void UpdateAllBars()
    {
        foreach (var bar in bars) {
            string tag = bar.gameObject.tag;
            switch (tag) {
                case "HP Bar":
                    bar.SetValue(player.HP, player.MAX_HP);
                    break;
                case "MP Bar":
                    bar.SetValue(player.MP, player.MAX_MP);
                    break;
                case "XP Bar":
                    bar.SetValue(player.XP, player.MAX_XP);
                    break;
                default:
                    bar.SetValue(0, 0);
                    break;
            }
        }
    }

    public void UpdateStatsPanel() {
        UpdateAllTexts();
        UpdateAllBars();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializeLists();
    }

    void Start() {
        UpdateStatsPanel();
    }
}
