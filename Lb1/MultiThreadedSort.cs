namespace Lb1;

public static class MultiThreadedSort
{
    public static void Test(int[] array)
    {
        Console.WriteLine("Original array: " + string.Join(", ", array));

        int threadsCount = Environment.ProcessorCount; // Кількість потоків залежить від числа ядер
        Console.WriteLine($"Using {threadsCount} threads.");
        
        ParallelMergeSort(array, 0, array.Length - 1, threadsCount);

        Console.WriteLine("Sorted array: " + string.Join(", ", array));
    }

    public static void ParallelMergeSort(int[] array, int left, int right, int threadsCount)
    {
        if (threadsCount <= 1 || left >= right)
        {
            SingleThreadedSort.MergeSort(array, left, right);
        }
        else
        {
            int middle = (left + right) / 2;

            Thread leftThread = new Thread(() => ParallelMergeSort(array, left, middle, threadsCount / 2));
            Thread rightThread = new Thread(() => ParallelMergeSort(array, middle + 1, right, threadsCount / 2));

            leftThread.Start();
            rightThread.Start();

            leftThread.Join();
            rightThread.Join();

            SingleThreadedSort.Merge(array, left, middle, right);
        }
    }
}