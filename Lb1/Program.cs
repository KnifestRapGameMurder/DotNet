namespace Lb1;

internal static class Program
{
    public static void Main(string[] args)
    {
        int[] array = { 10, 2, 8, 6, 7, 3 };
        var multithreadingArray = (int[])array.Clone();
        Console.WriteLine("\tSingleThreadedSort");
        SingleThreadedSort.Test(array);
        Console.WriteLine("\tMultiThreadedSort");
        MultiThreadedSort.Test(multithreadingArray);
        Console.WriteLine("\tPerformanceTest");
        PerformanceTest.Test();
    }
}