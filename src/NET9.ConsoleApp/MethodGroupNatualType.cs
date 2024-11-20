namespace NET9.ConsoleApp;

public class MethodGroupNatualType
{
    public void Test() {}
}

public static class StaticMethod
{
    public static void Test(this MethodGroupNatualType m, object o) {}
}

public sealed class S
{
    public int[] Buffer { get; } = new int[10];
}