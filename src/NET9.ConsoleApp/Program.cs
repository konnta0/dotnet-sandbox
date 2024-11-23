// See https://aka.ms/new-console-template for more information


using NET9.ConsoleApp;

#region new escape sequence \e
Console.WriteLine("\e[41mHELLO, WORLD!");
Console.WriteLine("\e[0mreset style");
Console.WriteLine("\e[34m\e[4munderlined text");
#endregion

#region method group natural type
var n = new MethodGroupNatualType();
n.Test();
n.Test(0);

var z = n.Test;
#endregion

#region implict index access
var v = new S
{
    Buffer = 
    {
        [1] = 11,
        [2] = 12,
        [^1] = 111,
        [^2] = 222,
    }
};

foreach (var vb in v.Buffer)
{
    Console.WriteLine(vb);
}
#endregion

var net9 = new NET9NewFeatures();
#region JSON indent option
net9.JsonIndentOption();
#endregion