using System.Diagnostics;

namespace Lb1;

public static class PerformanceTest
{
    public static void Test()
    {
        int[] array = GenerateRandomArray(100000); // Масив для тестування

        // Однопотокова версія
        int[] singleThreadedArray = (int[])array.Clone(); // Клон для однопотокової версії
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        SingleThreadedSort.MergeSort(singleThreadedArray, 0, singleThreadedArray.Length - 1);
        stopwatch.Stop();
        long singleThreadedTime = stopwatch.ElapsedMilliseconds;
        Console.WriteLine("Single-threaded time: " + singleThreadedTime + " ms");

        // Багатопотокова версія
        int[] multiThreadedArray = (int[])array.Clone(); // Клон для багатопотокової версії
        stopwatch.Restart();
        MultiThreadedSort.ParallelMergeSort(multiThreadedArray, 0, multiThreadedArray.Length - 1, 4); // 4 потоки
        stopwatch.Stop();
        long multiThreadedTime = stopwatch.ElapsedMilliseconds;
        Console.WriteLine("Multi-threaded time: " + multiThreadedTime + " ms");

        // Розрахунок прискорення
        double speedup = (double)singleThreadedTime / multiThreadedTime;
        Console.WriteLine($"Speedup: {speedup:F2}x");
    }

    private static int[] GenerateRandomArray(int size)
    {
        Random rand = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
            array[i] = rand.Next(0, 100000);
        return array;
    }
}