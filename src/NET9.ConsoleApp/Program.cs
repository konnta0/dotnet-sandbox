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