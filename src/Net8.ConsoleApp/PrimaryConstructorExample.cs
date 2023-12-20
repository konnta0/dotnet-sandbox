using System.Security.Cryptography;

namespace Net8.ConsoleApp.PrimaryConstructorExample;

public class Old
{
    private readonly int _hoge;

    public Old(int hoge)
    {
        _hoge = hoge;
    } 
}

public class New(int hoge)
{
    private readonly int _hoge = hoge;
}

public class New2(int hoge)
{
    public void SetHoge(int v)
    {
        hoge = v;
    }
}

public class PrimaryConstructorExample
{
    public void Run()
    {
        var crypto = ECDsa.Create();

        var old = new Old(1);
        var new1 = new New(1);
    }
}