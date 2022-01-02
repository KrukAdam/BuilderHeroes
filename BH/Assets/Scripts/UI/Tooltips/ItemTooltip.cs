using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class ItemTooltip : BaseTooltip
{
    [SerializeField] private LocalizeStringEvent localizeName;
    [SerializeField] private LocalizeStringEvent localizeType;
    [SerializeField] private Text itemDescriptionText;

    public override void ShowTooltip(Item item)
    {
        base.ShowTooltip(item);
        SetItemInfo(item);
        ShowModifiers(item);
        ShowItemDescription(item);
        gameObject.SetActive(true);
    }

    public override void HideTooltip()
    {
        base.HideTooltip();
        gameObject.SetActive(false);
    }

    protected override void ShowModifiers(Item item)
    {
        base.ShowModifiers(item);
        ItemEquippable itemEquippable = item as ItemEquippable;

        if (itemEquippable) ShowModifiersItemEq(itemEquippable);

        //TODO show every kind of item
    }

    private void ShowItemDescription(Item item)
    {
        //TODO show derscription
        itemDescriptionText.text = "";
        //  ItemDescriptionText.text = item.GetDescription();
    }

    private void ShowModifiersItemEq(ItemEquippable itemEquippable)
    {
        Color textColor = statNegative;
        BaseStat baseStat;
        int barIndex = 0;

        sb.Clear();

        for (int i = 0; i < itemEquippable.ItemStats.Count; i++)
        {
            CharacterStat characterStat = itemEquippable.ItemStats[i];

            if (characterStat.StatUseValues == EStatsValueUse.OnlyBaseValue)
            {
                baseStat = characterStat.BaseValue;
                if (baseStat.Value != 0)
                {
                    sb.Clear();
                    if (baseStat.Value > 0)
                    {
                        sb.Append("+");
                        textColor = statPositive;
                    }
                    else if (baseStat.Value < 0)
                    {
                        textColor = statNegative;
                    }

                    sb.Append(baseStat.Value);
                    sb.Append(" ");
                    sb.Append(characterStat.StatName.GetLocalizedString());

                    ShowModifierBar(sb, barIndex, textColor);
                    barIndex++;
                }
            }
            if (characterStat.StatUseValues == EStatsValueUse.BaseAndMaxValue)
            {
                baseStat = characterStat.MaxValue;
                if (baseStat.Value != 0)
                {
                    sb.Clear();
                    if (baseStat.Value > 0)
                    {
                        sb.Append("+");
                        textColor = statPositive;
                    }
                    else if (baseStat.Value < 0)
                    {
                        textColor = statNegative;
                    }

                    sb.Append(baseStat.Value);
                    sb.Append(" ");
                    sb.Append(characterStat.StatName.GetLocalizedString());

                    ShowModifierBar(sb, barIndex, textColor);
                    barIndex++;
                }

            }
            if (characterStat.StatUseValues == EStatsValueUse.MinAndMaxValue)
            {
                baseStat = characterStat.MinValue;
                if (baseStat.Value != 0)
                {
                    sb.Clear();
                    if (baseStat.Value > 0)
                    {
                        sb.Append("+");
                        textColor = statPositive;
                    }
                    else if (baseStat.Value < 0)
                    {
                        textColor = statNegative;
                    }

                    sb.Append(baseStat.Value);
                    sb.Append(" " + GameManager.Instance.ConstLocalized.StatMinValue.GetLocalizedString() + " ");
                    sb.Append(characterStat.StatName.GetLocalizedString());

                    ShowModifierBar(sb, barIndex, textColor);
                    barIndex++;

                }

                //Change to max value
                baseStat = characterStat.MaxValue;
                if (baseStat.Value != 0)
                {
                    sb.Clear();
                    if (baseStat.Value > 0)
                    {
                        sb.Append(" +");
                        textColor = statPositive;
                    }
                    else if (baseStat.Value < 0)
                    {
                        textColor = statNegative;
                    }

                    sb.Append(baseStat.Value);
                    sb.Append(" " + GameManager.Instance.ConstLocalized.StatMaxValue.GetLocalizedString() + " ");
                    sb.Append(characterStat.StatName.GetLocalizedString());

                    ShowModifierBar(sb, barIndex, textColor);
                    barIndex++;
                }
            }
        }

        for (int i = 0; i < itemEquippable.ItemStatsPercentMult.Count; i++)
        {
            CharacterStat characterStat = itemEquippable.ItemStatsPercentMult[i];

            if (characterStat.StatUseValues == EStatsValueUse.OnlyBaseValue)
            {
                baseStat = characterStat.BaseValue;
                if (baseStat.Value != 0)
                {
                    sb.Clear();
                    if (baseStat.Value > 0)
                    {
                        sb.Append("+");
                        textColor = statPositive;
                    }
                    else if (baseStat.Value < 0)
                    {
                        textColor = statNegative;
                    }

                    sb.Append(baseStat.Value);
                    sb.Append("%");
                    sb.Append(" ");
                    sb.Append(characterStat.StatName.GetLocalizedString());

                    ShowModifierBar(sb, barIndex, textColor);
                    barIndex++;
                }
            }
            if (characterStat.StatUseValues == EStatsValueUse.BaseAndMaxValue)
            {
                baseStat = characterStat.MaxValue;
                if (baseStat.Value != 0)
                {
                    sb.Clear();
                    if (baseStat.Value > 0)
                    {
                        sb.Append("+");
                        textColor = statPositive;
                    }
                    else if (baseStat.Value < 0)
                    {
                        textColor = statNegative;
                    }

                    sb.Append(baseStat.Value);
                    sb.Append("%");
                    sb.Append(" ");
                    sb.Append(characterStat.StatName.GetLocalizedString());

                    ShowModifierBar(sb, barIndex, textColor);
                    barIndex++;
                }

            }
            if (characterStat.StatUseValues == EStatsValueUse.MinAndMaxValue)
            {
                baseStat = characterStat.MinValue;
                if (baseStat.Value != 0)
                {
                    sb.Clear();
                    if (baseStat.Value > 0)
                    {
                        sb.Append("+");
                        textColor = statPositive;
                    }
                    else if (baseStat.Value < 0)
                    {
                        textColor = statNegative;
                    }
                    sb.Append(baseStat.Value);
                    sb.Append("%");
                    sb.Append(" " + GameManager.Instance.ConstLocalized.StatMinValue.GetLocalizedString() + " ");
                    sb.Append(characterStat.StatName.GetLocalizedString());

                    ShowModifierBar(sb, barIndex, textColor);
                    barIndex++;

                }

                //Change to max value
                baseStat = characterStat.MaxValue;
                if (baseStat.Value != 0)
                {
                    sb.Clear();
                    if (baseStat.Value > 0)
                    {
                        sb.Append("+");
                        textColor = statPositive;
                    }
                    else if (baseStat.Value < 0)
                    {
                        textColor = statNegative;
                    }
  
                    sb.Append(baseStat.Value);
                    sb.Append("%");
                    sb.Append(" " + GameManager.Instance.ConstLocalized.StatMaxValue.GetLocalizedString() + " ");
                    sb.Append(characterStat.StatName.GetLocalizedString());

                    ShowModifierBar(sb, barIndex, textColor);
                    barIndex++;
                }

            }
        }
    }

    private void SetItemInfo(Item item)
    {
        localizeName.StringReference = item.ItemName;
        localizeType.StringReference = item.ItemType;
    }

}
