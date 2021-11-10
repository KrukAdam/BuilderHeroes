
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
    public readonly Skill Skill;

    public StatModifier(float value, EStatModifierType type, object source)
    {
        Value = value;
        Type = type;
        Source = source;
        Skill = null;
    }

    public StatModifier(float value, EStatModifierType type, object source, Skill skill)
    {
        Value = value;
        Type = type;
        Source = source;
        Skill = skill;
    }
}
