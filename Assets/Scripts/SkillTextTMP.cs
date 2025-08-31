using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SkillTextTMP : TextTMP, IPointerClickHandler
{
    [SerializeField] string skillName;

    public void BoldUnderlineSkillText()
    {
        tmp.text = $"<u><b>{skillName}</b></u>";
    }

    public void RegularSkillText()
    {
        tmp.text = skillName;
    }

    void SelectAttack()
    {
        string tag = gameObject.tag;
        AttackType attackType = AttackUtils.TagToAttack(tag);

        if (Player.Instance.selectedAttack.Type == attackType)
        {
            Player.Instance.SelectAttack(AttackType.BasicAttack);
        }
        else
        {
            Player.Instance.SelectAttack(attackType);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectAttack();
    }
}
