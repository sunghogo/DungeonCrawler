using UnityEngine;

public class Enemy : Character
{
    [Header("Refs")]
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] RectTransform enemyCanvas;
    [SerializeField] Bar HPBar;
    [SerializeField] TextTMP HPText;
    [SerializeField] TextTMP LVLText;

    [Header("Level Up Amount")]
    [SerializeField] float smallUpgradeAmount = 0.5f;
    [SerializeField] float bigUpgradeAmount = 3f;
    [SerializeField] float XPDropScaling = 1.5f;

    StatType[] validUpgradeStats = { StatType.MaxHP, StatType.MaxMP, StatType.ATK, StatType.DEF, StatType.SPD };
    Vector3 originalPosition;

    public void LevelUp(int levels)
    {
        for (int j = 0; j < levels; ++j)
        {
            OnLevelUp();
        }
        UpdateLVLText();
    }

    void UpdateHpText()
    {
        string tag = HPText.gameObject.tag;
        string val = $"{tag}: {GetStat(StatUtils.TagToStat(tag))}";
        HPText.SetText(val);
    }

    void UpdateLVLText()
    {
        string tag = LVLText.gameObject.tag;
        string val = $"{tag} {GetStat(StatUtils.TagToStat(tag))} {CharacterName}";
        LVLText.SetText(val);
    }

    void UpdateHpBar()
    {
        HPBar.SetValue(HP, MaxHP);
    }

    void OnMouseDown()
    {
        if (!IsAlive() || LevelUpPanel.Instance.IsActive) return;

        if (SPD > Player.Instance.SPD)
        {
            if (Player.Instance.IsAlive()) Attack(Player.Instance);
            if (Player.Instance.IsAlive()) Player.Instance.Attack(this);
        }
        else
        {
            if (IsAlive()) Player.Instance.Attack(this);
            if (IsAlive()) Attack(Player.Instance);
        }
    }

    void HandleGameOver()
    {
        Destroy(gameObject);
    }

    protected override void OnStart()
    {
        UpdateHpText();
        UpdateHpBar();
        UpdateLVLText();
        originalPosition = transform.position;

        GameManager.OnGameOver += HandleGameOver;
    }

    protected override void OnDestroyed()
    {
        GameManager.Instance.DecrementSpawnCount();
        GameManager.Instance.RemoveEnemy(this);
        GameManager.OnGameOver -= HandleGameOver;
    }

    protected override void OnAttack()
    {
        StartCoroutine(Coroutines.ShiftForward(transform, originalPosition, 0.1f, 0.5f));
    }

    protected override void OnTakeDamage()
    {
        UpdateHpText();
        UpdateHpBar();
        DamageText damageText = Instantiate(damageTextPrefab, enemyCanvas).GetComponent<DamageText>();
        damageText.SetText($"-{CalculateDamageTaken(Player.Instance)}");
        StartCoroutine(Coroutines.Shake(transform, originalPosition, 0.25f, 0.25f, 4f));
    }

    protected override void OnLevelUp()
    {
        StatType randomStat = validUpgradeStats[UnityEngine.Random.Range(0, validUpgradeStats.Length)];
        float upgradeAmount = (randomStat == StatType.MaxHP || randomStat == StatType.MaxMP)
            ? bigUpgradeAmount
            : smallUpgradeAmount;
        IncreaseStat(randomStat, upgradeAmount);
        XPDrop *= XPDropScaling;
        ++LVL;
    }

    protected override void OnDeath()
    {
        StartCoroutine(Coroutines.FadeRedBlack(GetComponent<SpriteRenderer>(), 0.5f, 2f));
        Player.Instance.GainXP(XPDrop);
        Destroy(gameObject, 0.5f);
    }
}
