using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipsPanels : MonoBehaviour
{
    [SerializeField] private TooltipItem itemTooltip = null;
    [SerializeField] private TooltipStat statTooltip = null;
    [SerializeField] private TooltipSkill tooltipMainSkill = null;
    [SerializeField] private TooltipSkill tooltipSecondSkill = null;
    [SerializeField] private Color statPositive = Color.green;
    [SerializeField] private Color statNegative = Color.red;

    private void Start()
    {
        statTooltip.Setup(statPositive, statNegative);
        itemTooltip.Setup(statPositive, statNegative);
        tooltipMainSkill.Setup(statPositive, statNegative);
        tooltipSecondSkill.Setup(statPositive, statNegative);
    }

    public void ShowItemTooltip(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            itemTooltip.ShowTooltip(itemSlot.Item);

            ItemEquippable eqItem = itemSlot.Item as ItemEquippable;
            if (eqItem)
            {
                if (eqItem.MainSkill)
                {
                    tooltipMainSkill.ShowTooltip(eqItem.MainSkill);
                }
                if (eqItem.SecondSkill)
                {
                    tooltipSecondSkill.ShowTooltip(eqItem.SecondSkill);
                }
            }
        }
    }

    public void HideItemTooltip(BaseItemSlot itemSlot)
    {
        if (itemTooltip.gameObject.activeSelf)
        {
            itemTooltip.HideTooltip();
            tooltipMainSkill.HideTooltip();
            tooltipSecondSkill.HideTooltip();
        }
    }

    public void ShowStatTooltip(CharacterStat characterStat)
    {
        statTooltip.ShowTooltip(characterStat);
    }

    public void HideStatTooltip()
    {
        statTooltip.HideTooltip();
    }

    public void ShowMainSkillTooltip(Skill skill)
    {
        if (skill != null)
        {
            tooltipMainSkill.ShowTooltip(skill);
        }
    }

    public void HideMainSkillTooltip()
    {
        tooltipMainSkill.HideTooltip();
    }

    public void ShowSecondSkillTooltip(Skill skill)
    {
        if (skill != null)
        {
            tooltipSecondSkill.ShowTooltip(skill);
        }
    }

    public void HideSecondSkillTooltip()
    {
        tooltipSecondSkill.HideTooltip();
    }
}
