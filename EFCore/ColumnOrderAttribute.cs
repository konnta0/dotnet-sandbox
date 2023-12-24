namespace EFCore;

public class ColumnOrderAttribute(int order) : Attribute
{
    public int Order { get; } = order;
}