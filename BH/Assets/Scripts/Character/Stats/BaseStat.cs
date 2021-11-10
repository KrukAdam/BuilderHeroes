using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class BaseStat
{
    public List<StatModifier> StatModifiers { get => statModifiers; }

    public float Value;

    private List<StatModifier> statModifiers;


    public BaseStat()
    {
        statModifiers = new List<StatModifier>();
    }

    public virtual void AddModifier(StatModifier mod)
    {
        AurasCheck(mod);

        statModifiers.Add(mod);
        Value = CalculateFinalValue();
    }

    public bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            Value = CalculateFinalValue();
            return true;
        }
        return false;
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        if (didRemove) Value = CalculateFinalValue();

        return didRemove;
    }

    private void AurasCheck(StatModifier mod)
    {
        if (!mod.Skill) return;

        foreach (var modifier in statModifiers)
        {
            if (modifier.Skill)
            {
                if (modifier.Skill == mod.Skill)
                {
                    RemoveModifier(modifier);
                    return;
                }
            }
        }
    }

    private float CalculateFinalValue()
    {
        float finalValue = 0;
        float sumPercentAdd = 0;

        foreach (var mod in statModifiers)
        {
            if (mod.Type == EStatModifierType.Constants)
            {
                finalValue += mod.Value;
            }
        }
        foreach (var mod in statModifiers)
        {
            if (mod.Type == EStatModifierType.PercentAdd)
            {
                sumPercentAdd += mod.Value;
            }
        }

        finalValue = finalValue + (finalValue/100 * sumPercentAdd);
        return (float)Math.Round(finalValue, 4);
    }
}
