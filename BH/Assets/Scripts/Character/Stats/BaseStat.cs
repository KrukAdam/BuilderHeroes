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

    private float CalculateFinalValue()
    {
        float finalValue = 0;
        float sumPercentAdd = 0;

        foreach (var mod in statModifiers)
        {
            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
        }
        foreach (var mod in statModifiers)
        {
            if (mod.Type == StatModType.PercentMult)
            {
                sumPercentAdd += mod.Value;
            }
        }

        finalValue = finalValue + (finalValue/100 * sumPercentAdd);

        return (float)Math.Round(finalValue, 4);

        //for (int i = 0; i < statModifiers.Count; i++)
        //{
        //    StatModifier mod = statModifiers[i];

        //    if (mod.Type == StatModType.Flat)
        //    {
        //        finalValue += mod.Value;
        //    }
        //    else if (mod.Type == StatModType.PercentAdd)
        //    {
        //        sumPercentAdd += mod.Value;

        //        if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
        //        {
        //            finalValue *= 1 + sumPercentAdd;
        //            sumPercentAdd = 0;
        //        }
        //    }
        //    else if (mod.Type == StatModType.PercentMult)
        //    {
        //        finalValue *= 1 + mod.Value;
        //    }
        //}

        //return (float)Math.Round(finalValue, 4);
    }
}
