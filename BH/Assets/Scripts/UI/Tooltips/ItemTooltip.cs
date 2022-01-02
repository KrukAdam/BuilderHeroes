using UnityEngine;
using UnityEngine.Localization;
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
        HideModifiersBars();
        gameObject.SetActive(false);
    }

    protected override void ShowModifiers(Item item)
    {
        base.ShowModifiers(item);

        ItemEquippable itemEquippable = item as ItemEquippable;
        if (itemEquippable)
        {
            ShowModifiersItem(itemEquippable);
            return;
        }

        ItemUsable itemUsable = item as ItemUsable;
        if (itemUsable)
        {
            ShowModifiersItem(itemUsable);
            return;
        }
    }



    private void ShowItemDescription(Item item)
    {
        if(item.ItemDescription.IsEmpty)
        {
            itemDescriptionText.text = "";
        }
        else
        {
            itemDescriptionText.text = item.ItemDescription.GetLocalizedString();
        }
    }

    private void ShowModifiersItem(ItemUsable itemUsable)
    {
        Color textColor;
        ItemStatBuffEffect buffEffect;

        for (int i = 0; i < itemUsable.Effects.Count; i++)
        {
            buffEffect = itemUsable.Effects[i];
            sb.Clear();

            if(buffEffect.BuffValue != 0)
            {
                if (buffEffect.BuffValue > 0)
                {
                    sb.Append("+");
                    textColor = statPositive;
                }
                else
                {
                    textColor = statNegative;
                }

                sb.Append(buffEffect.BuffValue);

                if(buffEffect.IsPercentMult) sb.Append("%");

                sb.Append(" ");

                if (buffEffect.BaseStatType == EBaseStatType.Max) sb.Append(GameManager.Instance.ConstLocalized.StatMaxValue.GetLocalizedString() + " ");
                if (buffEffect.BaseStatType == EBaseStatType.Min) sb.Append(GameManager.Instance.ConstLocalized.StatMinValue.GetLocalizedString() + " ");

                sb.Append(GameManager.Instance.CharacterStatsData.GetStat(buffEffect.BuffStatType).StatName.GetLocalizedString());
                sb.Append(" ");
                sb.Append(GameManager.Instance.ConstLocalized.ForTime.GetLocalizedString());
                sb.Append(" ");
                sb.Append(buffEffect.Duration);
                sb.Append(" ");
                sb.Append(GameManager.Instance.ConstLocalized.Seconds.GetLocalizedString());

                ShowModifierBar(sb, i, textColor);
            }
        }
    }

    private void ShowModifiersItem(ItemEquippable itemEquippable)
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
