
public enum EStatModifierType
{
    Constants,
    PercentAdd,
}

public class StatModifier
{
    public readonly float Value;
    public readonly EStatModifierType Type;
    public readonly object Source;
    public readonly EAuraSkillsType AuraType;

    public StatModifier(float value, EStatModifierType type, object source)
    {
        Value = value;
        Type = type;
        Source = source;
        AuraType = EAuraSkillsType.None;
    }

    public StatModifier(float value, EStatModifierType type, object source, EAuraSkillsType auraType)
    {
        Value = value;
        Type = type;
        Source = source;
        AuraType = auraType;
    }
}
