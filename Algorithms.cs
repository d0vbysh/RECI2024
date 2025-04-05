using System;

public static class Algorithms
{
    // T1 - Bubble Sort
    public static void T1(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                }
            }
        }
    }

    // T2 - Selection Sort
    public static void T2(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (array[j] < array[minIndex])
                    minIndex = j;
            }
            (array[i], array[minIndex]) = (array[minIndex], array[i]);
        }
    }

    // T3 - Quick Sort
    public static void T3(int[] array) => QuickSort(array, 0, array.Length - 1);
    private static void QuickSort(int[] array, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(array, low, high);
            QuickSort(array, low, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, high);
        }
    }
    private static int Partition(int[] array, int low, int high)
    {
        int pivot = array[high];
        int i = low - 1;
        for (int j = low; j < high; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
        (array[i + 1], array[high]) = (array[high], array[i + 1]);
        return i + 1;
    }

    // T4 - Merge Sort
    public static void T4(int[] array) => MergeSort(array, 0, array.Length - 1);
    private static void MergeSort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int mid = left + (right - left) / 2;
            MergeSort(array, left, mid);
            MergeSort(array, mid + 1, right);
            Merge(array, left, mid, right);
        }
    }
    private static void Merge(int[] array, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        int[] L = new int[n1];
        int[] R = new int[n2];
        Array.Copy(array, left, L, 0, n1);
        Array.Copy(array, mid + 1, R, 0, n2);

        int i = 0, j = 0, k = left;
        while (i < n1 && j < n2)
        {
            array[k++] = (L[i] <= R[j]) ? L[i++] : R[j++];
        }
        while (i < n1) array[k++] = L[i++];
        while (j < n2) array[k++] = R[j++];
    }

    // T5 - Insertion Sort
    public static void T5(int[] array)
    {
        int n = array.Length;
        for (int i = 1; i < n; i++)
        {
            int key = array[i];
            int j = i - 1;
            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }

    // T6 - Heap Sort
    public static void T6(int[] array)
    {
        int n = array.Length;
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(array, n, i);

        for (int i = n - 1; i >= 0; i--)
        {
            (array[0], array[i]) = (array[i], array[0]);
            Heapify(array, i, 0);
        }
    }
    private static void Heapify(int[] array, int n, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < n && array[left] > array[largest])
            largest = left;

        if (right < n && array[right] > array[largest])
            largest = right;

        if (largest != i)
        {
            (array[i], array[largest]) = (array[largest], array[i]);
            Heapify(array, n, largest);
        }
    }

    // T7 - Bucket Sort
    public static void T7(int[] array)
    {
        if (array.Length == 0) return;

        int minValue = array[0], maxValue = array[0];
        foreach (var item in array)
        {
            if (item < minValue) minValue = item;
            else if (item > maxValue) maxValue = item;
        }

        int bucketCount = 10;
        var buckets = new List<int>[bucketCount];
        for (int i = 0; i < bucketCount; i++)
            buckets[i] = new List<int>();

        foreach (var item in array)
        {
            int bucketIndex = (int)(((long)item - minValue) * (bucketCount - 1) / (maxValue - minValue));
            buckets[bucketIndex].Add(item);
        }

        int index = 0;
        foreach (var bucket in buckets)
        {
            bucket.Sort();
            foreach (var item in bucket)
                array[index++] = item;
        }
    }

    // T8 - Radix Sort
    public static void T8(int[] array)
    {
        int max = array.Max();
        for (int exp = 1; max / exp > 0; exp *= 10)
            CountingSort(array, exp);
    }
    private static void CountingSort(int[] array, int exp)
    {
        int n = array.Length;
        int[] output = new int[n];
        int[] count = new int[10];

        for (int i = 0; i < n; i++)
            count[(array[i] / exp) % 10]++;

        for (int i = 1; i < 10; i++)
            count[i] += count[i - 1];

        for (int i = n - 1; i >= 0; i--)
        {
            int digit = (array[i] / exp) % 10;
            output[count[digit] - 1] = array[i];
            count[digit]--;
        }

        for (int i = 0; i < n; i++)
            array[i] = output[i];
    }

    // T9 - Cube Sort (Simplified as Shell Sort alternative)
    public static void T9(int[] array)
    {
        int n = array.Length;
        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i++)
            {
                int temp = array[i];
                int j = i;
                while (j >= gap && array[j - gap] > temp)
                {
                    array[j] = array[j - gap];
                    j -= gap;
                }
                array[j] = temp;
            }
        }
    }

    // T10 - Tree Sort
    public static void T10_TreeSort(int[] array)
    {
        var bst = new SortedSet<int>(array);
        int index = 0;
        foreach (var item in bst)
            array[index++] = item;
    }
}
