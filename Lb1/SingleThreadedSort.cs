namespace Lb1;

public static class SingleThreadedSort
{
    public static void Test(int[] array)
    {
        Console.WriteLine("Original array: " + string.Join(", ", array));

        MergeSort(array, 0, array.Length - 1);

        Console.WriteLine("Sorted array: " + string.Join(", ", array));
    }

    public static void MergeSort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;

            MergeSort(array, left, middle);
            MergeSort(array, middle + 1, right);

            Merge(array, left, middle, right);
        }
    }

    public static void Merge(int[] array, int left, int middle, int right)
    {
        int n1 = middle - left + 1;
        int n2 = right - middle;

        int[] leftArray = new int[n1];
        int[] rightArray = new int[n2];

        for (int i = 0; i < n1; i++)
            leftArray[i] = array[left + i];

        for (int j = 0; j < n2; j++)
            rightArray[j] = array[middle + 1 + j];

        int iLeft = 0, iRight = 0, k = left;

        while (iLeft < n1 && iRight < n2)
        {
            if (leftArray[iLeft] <= rightArray[iRight])
            {
                array[k++] = leftArray[iLeft++];
            }
            else
            {
                array[k++] = rightArray[iRight++];
            }
        }

        while (iLeft < n1)
        {
            array[k++] = leftArray[iLeft++];
        }

        while (iRight < n2)
        {
            array[k++] = rightArray[iRight++];
        }
    }
}