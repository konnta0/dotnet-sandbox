namespace Net8.ConsoleApp.CollectionExpressions;

public class Old
{
    public Old()
    {
        // Create an array:
        int[] a = new [] {1, 2, 3, 4, 5, 6, 7, 8};

        // Create a list:
        List<string> b = new List<string> { "one", "two", "three" };

        // Create a span
        Span<char> c = new Span<char>(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'h', 'i' });

        // Create a jagged 2D array:
        int[][] twoD = new [] { new [] { 1, 2, 3 }, new [] { 4, 5, 6 }, new [] { 7, 8, 9 } };

        // Create a jagged 2D array from variables:
        int[] row0 = new [] { 1, 2, 3 }; 
        int[] row1 = new [] { 4, 5, 6 };
        int[] row2 = new [] { 7, 8, 9 };
        int[][] twoDFromVariables = new[] { row0, row1, row2 };
    }
}

public class New
{
    public New()
    {
        // Create an array:
        int[] a = [1, 2, 3, 4, 5, 6, 7, 8];

        // Create a list:
        List<string> b = ["one", "two", "three"];

        // Create a span
        Span<char> c  = ['a', 'b', 'c', 'd', 'e', 'f', 'h', 'i'];

        // Create a jagged 2D array:
        int[][] twoD = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];

        // Create a jagged 2D array from variables:
        int[] row0 = [1, 2, 3];
        int[] row1 = [4, 5, 6];
        int[] row2 = [7, 8, 9];
        int[][] twoDFromVariables = [row0, row1, row2];
    }
}