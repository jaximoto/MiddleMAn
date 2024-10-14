using System;
using System.Data;

public enum StatType
{
    money,
    workers,
    availableWorkers,
    productivity,
    relation
}

public enum RelationType
{
    King,
    God,
    Workers
}
public abstract class AbstractStats 
{
    
    
    public StatType Type { get; }

    protected AbstractStats(StatType statType)
    {
        this.Type = statType;
    }

    public abstract void ChangeValue(int newValue);
    
    
   
}

public class IntStat : AbstractStats
{
    public int Value { get;  protected set; }

    public IntStat(StatType statType, int initialValue) : base(statType)
    {
        this.Value = initialValue;
    }

    public override void ChangeValue( int newValue)
    {
        this.Value += newValue;
    }
    
}

// Class for stats that are clamped from 0 - 100
public class ClampedStat : IntStat
{
    public ClampedStat(StatType type, int initialValue) : base(type, initialValue)
    {
        this.Value = Math.Clamp(initialValue, 0, 100);
    }
    public override void ChangeValue( int newValue)
    {
        newValue += this.Value;
        this.Value = Math.Clamp(newValue, 0, 100);
    }
}




