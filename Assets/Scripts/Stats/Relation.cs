public class Relation
{
    public readonly string name;

    // 0 - 100
    public int affinity;

    public Relation(string name, int affinity)
    {
        this.name = name;
        this.affinity = affinity;
    }
}