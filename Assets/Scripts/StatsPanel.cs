using UnityEngine;
using System.Collections.Generic;

public class StatsPanel : MonoBehaviour
{
    public static StatsPanel Instance { get; private set; }

    [Header("Refs")]
    [SerializeField] GameObject subcontainer;
    [SerializeField] List<TextTMP> textTmps;
    [SerializeField] List<Bar> bars;

    public bool IsActive { get; private set; } = false;

    public void Activate()
    {
        subcontainer.SetActive(true);
        IsActive = true;
    }

    public void Deactivate()
    {
        subcontainer.SetActive(false);
        IsActive = false;
    }

    void HandleGameOver()
    {
        Deactivate();
    }

    void HandleGameSart()
    {
        Activate();
    }

    void InitializeLists()
    {
        textTmps.Clear();
        bars.Clear();

        textTmps.AddRange(GetComponentsInChildren<TextTMP>(includeInactive: true));
        bars.AddRange(GetComponentsInChildren<Bar>(includeInactive: true));
    }

    void UpdateAllTexts()
    {
        foreach (var textTmp in textTmps)
        {
            string tag = textTmp.gameObject.tag;
            string val = $"{tag}: {(int) Player.Instance.GetStat(StatUtils.TagToStat(tag))}";
            textTmp.SetText(val);
        }
    }

    void UpdateAllBars()
    {
        foreach (var bar in bars)
        {
            string tag = bar.gameObject.tag;
            switch (tag)
            {
                case "HP Bar":
                    bar.SetValue(Player.Instance.HP, Player.Instance.MaxHP);
                    break;
                case "MP Bar":
                    bar.SetValue(Player.Instance.MP, Player.Instance.MaxMP);
                    break;
                case "XP Bar":
                    bar.SetValue(Player.Instance.XP, Player.Instance.MaxXP);
                    break;
                default:
                    bar.SetValue(0, 0);
                    break;
            }
        }
    }

    public void UpdateStatsPanel()
    {
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
        GameManager.OnGameOver += HandleGameOver;
        GameManager.OnGameStart += HandleGameSart;
    }

    void Start()
    {
        UpdateStatsPanel();
    }

    void OnDestroy()
    {
        GameManager.OnGameOver -= HandleGameOver;
        GameManager.OnGameStart -= HandleGameSart;
    }
}
