using UnityEngine;

public class Enemy : Character
{
    [Header("Refs")]
    [SerializeField] Player player;
    [SerializeField] TextTMP textTmp;
    [SerializeField] Bar HPBar;

    void UpdateHpText() {
        string tag = textTmp.gameObject.tag;
        string val = $"{tag}: {GetStat(StatUtils.TagToStat(tag))}";
        textTmp.UpdateText(val);
    }

    void UpdateHpBar() {
        HPBar.SetValue(HP, MaxHP);
    }

    void OnMouseDown()
    {
        if (SPD > Player.Instance.SPD)
        {
            Player.Instance.TakeDamage(ATK);
            TakeDamage(Player.Instance.ATK);
        }
        else
        {
            TakeDamage(Player.Instance.ATK);
            Player.Instance.TakeDamage(ATK);
        }
    }

    protected override void OnAwake()
    {

    }

    protected override void OnStart()
    {
        UpdateHpText();
        UpdateHpBar();
    }

    protected override void OnUpdate() {
    }

    protected override void OnAttack() {

    }

    protected override void OnTakeDamage() {
        UpdateHpText();
        UpdateHpBar();
    }
}
