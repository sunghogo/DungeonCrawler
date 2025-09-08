using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    public static SkillPanel Instance { get; private set; }

    [Header("Refs")]
    [SerializeField] GameObject subcontainer;
    [SerializeField] SkillTextTMP bashText;
    [SerializeField] SkillTextTMP crushText;


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

    public void UpdateTexts()
    {
        AttackType selectedAttackType = Player.Instance.selectedAttack.Type;
        switch (selectedAttackType)
        {
            case AttackType.BasicAttack:
                bashText.RegularSkillText();
                crushText.RegularSkillText();
                break;
            case AttackType.Bash:
                bashText.BoldUnderlineSkillText();
                crushText.RegularSkillText();
                break;
            case AttackType.Crush:
                bashText.RegularSkillText();
                crushText.BoldUnderlineSkillText();
                break;
            default:
                bashText.RegularSkillText();
                crushText.RegularSkillText();
                break;
        }
    }

    void HandleGameOver()
    {
        Deactivate();
    }

    void HandleGameSart()
    {
        Activate();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        GameManager.OnGameOver += HandleGameOver;
        GameManager.OnGameStart += HandleGameSart;
    }

    void OnDestroy()
    {
        GameManager.OnGameOver -= HandleGameOver;
        GameManager.OnGameStart -= HandleGameSart;
    }
}
