using System;

public class Relation
{
    

    // 0 - 100
    public int affinity { get; private set; }

    public Relation(int affinity)
    {
        
        this.affinity = Math.Clamp(affinity, 0, 100);
    }

    public void ChangeAffinity(int affinity)
    {
        this.affinity += affinity;
        this.affinity = Math.Clamp(this.affinity, 0, 100);
    }
}