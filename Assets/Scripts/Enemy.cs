using UnityEngine;

public class Enemy : Character
{
    [Header("Refs")]
    [SerializeField] TextTMP textTmp;
    [SerializeField] Bar hpBar;

    void UpdateHpText() {
        string tag = textTmp.gameObject.tag;
        string val = $"{tag}: {GetStat(StatUtils.TagToStat(tag)).ToString()}";
        textTmp.UpdateText(val);
    }

    void UpdateHpBar() {
        hpBar.SetValue(HP, MAX_HP);
    }

    protected override void OnStart() {
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
