namespace NET9.ConsoleApp;

public class MethodGroupNatualType
{
    public void Test() {}
}

public static class StaticMethod
{
    public static void Test(this MethodGroupNatualType m, object o) {}
}