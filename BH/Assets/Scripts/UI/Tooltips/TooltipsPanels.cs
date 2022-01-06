using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipsPanels : MonoBehaviour
{
    [SerializeField] private TooltipBase itemTooltip = null;
    [SerializeField] private TooltipBase statTooltip = null;
    [SerializeField] private TooltipBase tooltipMainSkill = null;
    [SerializeField] private TooltipBase tooltipSecondSkill = null;
    [SerializeField] private TooltipBase tooltipNewItemDiscover = null;
    [SerializeField] private Color statPositive = Color.green;
    [SerializeField] private Color statNegative = Color.red;

    private List<Item> newItemsToShow = new List<Item>();
    private WaitForSeconds waitFor = new WaitForSeconds(Constant.TimeToShowNewItemTooltip);

    private void Start()
    {
        statTooltip.Setup(statPositive, statNegative);
        itemTooltip.Setup(statPositive, statNegative);
        tooltipMainSkill.Setup(statPositive, statNegative);
        tooltipSecondSkill.Setup(statPositive, statNegative);
        tooltipNewItemDiscover.Setup(statPositive, statNegative);
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

    public void ShowNewItemsTooltip(List<Item> items)
    {
        foreach (var item in items)
        {
            if (!newItemsToShow.Contains(item))
            {
                newItemsToShow.Add(item);
            }
        }

        if (!tooltipNewItemDiscover.gameObject.activeSelf && newItemsToShow.Count > 0)
        {
            StartCoroutine(ShowNewItemTooltipLoop());
        }
    }

    public void ShowNewItemsTooltip(Item item)
    {
        if (!newItemsToShow.Contains(item))
        {
            newItemsToShow.Add(item);

            if (!tooltipNewItemDiscover.gameObject.activeSelf && newItemsToShow.Count > 0)
            {
                StartCoroutine(ShowNewItemTooltipLoop());
            }
        }


    }

    public void HideNewItemTooltip()
    {
        if (tooltipNewItemDiscover.gameObject.activeSelf)
        {
            tooltipNewItemDiscover.HideTooltip();
        }
    }

    private IEnumerator ShowNewItemTooltipLoop()
    {
        ShowNewItemTooltip(newItemsToShow[0]);

        yield return waitFor;

        newItemsToShow.RemoveAt(0);
        if (newItemsToShow.Count > 0)
        {
            newItemsToShow.Sort((p1, p2) => p1.name.CompareTo(p2.name));
            StartCoroutine(ShowNewItemTooltipLoop());
        }
        else
        {
            HideNewItemTooltip();
        }
    }

    private void ShowNewItemTooltip(Item item)
    {
        if (item != null)
        {
            tooltipNewItemDiscover.ShowTooltip(item);
        }
    }
}
