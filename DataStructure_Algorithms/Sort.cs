using System;
using System.Data.SqlTypes;
using System.Windows.Markup;

namespace DataStructure_Algorithms
{
    class Sort
    {
        /// <summary>
        /// Space complexity is O(n).
        /// Time complexity is (N-1)*n/2 = O(N^2-N)/2 = o(n^2)
        /// </summary>
        public void InsertionSort()
        {
            int[] input = GetDataInput(100);

            int i = 1;
            int j;
            while (i < input.Length)
            {
                j = i;
                while (j > 0 && input[j - 1] > input[j])
                {
                    Swap(ref input, j, j - 1);
                    j--;
                }
                i++;
            }
        }

        /// <summary>
        /// Space complexity is O(n).
        /// Time complexity is (N-1)*n/2 = O(N^2-N)/2 = o(n^2)
        /// </summary>
        public void SelectionSort()
        {
            int[] input = GetDataInput(100);

            for (int i = 0; i < input.Length; i++)
            {
                int min = i; // Record the position of min.
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (input[j] < input[min])
                        min = j;
                }
                Swap(ref input, i, min);
            }
        }

        /// <summary>
        /// Space complexity is O(n).
        /// Time complexity is (N-1)*n/2 = O(N^2-N)/2 = o(n^2)
        /// </summary>
        public void BubbleSort()
        {
            int[] input = GetDataInput(100);

            bool isSortedList = false;
            while (!isSortedList)
            {
                isSortedList = true; // Assume it is sorted unless proven other wise in the below check.
                for (int i = 1; i < input.Length; i++)
                {
                    if (input[i - 1] > input[i])
                    {
                        Swap(ref input, i, i - 1);
                        isSortedList = false;
                    }
                }
            }
        }

        #region HeapSort
        /// <summary>
        /// The algorithm builds a heap. It then repeatedly swaps the first and last items in the heap, 
        /// and rebuilds the heap excluding the last item. During each pass, one item is removed from the 
        /// heap and added to the end of the array where the items are placed in sorted order.
        /// The time complexity is O(N Log N) + O (N Log N)
        /// </summary>
        public void HeapSort()
        {
            int[] input = GetDataInput(100);
            // Time to build the initial heap O(N Log N).
            MakeHeapTree(ref input, 0, input.Length-1);

            for (int i = input.Length - 1; i > 0; i--)
            {

                Swap(ref input, i, 0);
                // Restoring Heap Property by travelling through height of the tree. This is Log N
                // Since this is to be done N times, it becomes N Log N
                MakeHeapTree(ref input, 0, i - 1); //?? Should we call this or make the choice of pushing the root down.
            }
        }

        /// <summary>
        /// Given an array convert it to a Heap tree.
        /// A heap, is a complete binary tree where every node holds a 
        /// value that is at least as large as the values in all its children. 
        /// </summary>
        /// <param name="input"></param>
        private void MakeHeapTree(ref int[] input, int startIndex, int endIndex)
        {
            for(int i = startIndex; i <= endIndex; i++)
            {
                // Find the parent and work up the node to make a heap.
                int currentIndex = i;
                while(currentIndex != 0)
                {
                    // Find the parent.
                    int parentIndex = (currentIndex - 1) / 2;
                    if (input[currentIndex] < input[parentIndex])
                        break;
                    Swap(ref input, currentIndex, parentIndex);
                    currentIndex = parentIndex;
                }
            }
        }
        #endregion

        #region QuickSort
        public void QuickSort()
        {
            int[] input = GetDataInput(10);
            QuickSort(ref input, 0, input.Length-1);
        }

        public void QuickSort2()
        {
            int[] input = GetDataInput(10);
            QuickSort2(ref input, 0, input.Length - 1);
        }

        public void QuickSort2Iterative()
        {
            int[] input = GetDataInput(10);
            QuickSort2Iterative(ref input, 0, input.Length - 1);
        }

        /// <summary>
        /// Quick sort uses a dividing line to determine the values that go the left of it and
        /// some of the values to the right of it.
        /// </summary>
        private void QuickSort(ref int[] input, int start, int end)
        {
            if (start >= end)
                return;
            int dividingValue = input[start]; // We can pick any dividing value. We just picked the first one.

            int lo = start;// hold the highest index in the lower part of the array
            int hi = end; // lowest index in the upper part of the array
            // Search the array from back to front starting at "hi"
            // to find the last item where value < "divider."
            // Move that item into the hole. The hole is now where
            // that item was.
            while (true)
            {
                while(input[hi] >= dividingValue)
                {
                    hi = hi - 1;
                    if (hi <= lo)
                        break;
                }
                if(hi <= lo)
                {
                    // The left and right pieces have met in the middle
                    // so we're done. Put the divider here, and
                    // break out of the outer While loop.
                    input[lo] = dividingValue;
                    break;
                }
                // Move the value we found to the lower half.
                input[lo] = input[hi];
                // Search the array from front to back starting at "lo"
                // to find the first item where value >= "divider."
                // Move that item into the hole. The hole is now where
                // that item was.
                lo = lo + 1;
                while(input[lo] < dividingValue)
                {
                    lo = lo + 1;
                    if(lo >= hi)
                        break;
                }
                if (lo >= hi)
                {
                    // The left and right pieces have met in the middle
                    // so we're done. Put the divider here, and
                    // break out of the outer While loop.
                    lo = hi;
                    input[hi] = dividingValue;
                    break;
                }

                // Move the value we found to the upper half.
                input[hi] = input[lo];
            }

            // Recursively sort the two halves.
            QuickSort(ref input, start, lo - 1);
            QuickSort(ref input, lo + 1, end);
        }

        private void QuickSort2(ref int[] input, int start, int end)
        {
            if(start < end)
            {
                int p = Partition(ref input, start, end);
                QuickSort2(ref input, start, p - 1);
                QuickSort2(ref input, p + 1, end);
            }
        }

        /// <summary>
        /// Instead of recursion it is done iteratively. 
        /// So basically the items that go to stack during recursion we store them in the stack.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void QuickSort2Iterative(ref int[] input, int start, int end)
        {
            int[] stack = new int[end - start + 1];

            int top = -1;

            stack[++top] = start;
            stack[++top] = end;

            while(top >= 0)
            {
                end = stack[top--];
                start = stack[top--];

                int p = Partition(ref input, start, end);

                // If there are elements on left side of pivot,
                // then push left side to stack
                if (p - 1 > start)
                {
                    stack[++top] = start;
                    stack[++top] = p - 1;
                }

                // If there are elements on right side of pivot,
                // then push right side to stack
                if (p + 1 < end)
                {
                    stack[++top] = p + 1;
                    stack[++top] = end;
                }
            }
        }

        /* This function takes last element as pivot, places 
            the pivot element at its correct position in sorted 
            array, and places all smaller (smaller than pivot)
            to left of pivot and all greater elements to right 
            of pivot 
        */
        private int Partition(ref int[] input, int start, int end)
        {
            if (input == null || input.Length <= 0)
                throw new ArgumentNullException(nameof(input));

            int pivotValue = input[end];
            int lower = start - 1;

            for(int i = start; i <= end - 1; i++)
            {
                if(input[i] <= pivotValue)
                {
                    lower++;
                    Swap(ref input, lower, i);
                }
            }
            Swap(ref input, lower + 1, end);
            return lower + 1;
        }
        #endregion

        #region Merge Sort

        public void MergeSort()
        {
            int[] input = GetDataInput(100);
        }

        private void MergeSort(ref int[] input, ref int[] scratch, int start, int end)
        {
            // If the array contains only one item then it is sorted.
            if(start == end)
                return;
            // Break the array in to two partitions.
            int midpoint = (start + end)/2;

            // Call Merge sort to recursively to sort the two parts
            MergeSort(ref input, ref scratch, start, midpoint);
            MergeSort(ref input, ref scratch, midpoint + 1, end);

            // Merge the two halves.
            int leftIndex = start;
            int rightIndex = midpoint + 1;
            int scratchIndex = leftIndex;
            while (leftIndex <= midpoint && rightIndex <= end)
            {
                
            }

        }
        #endregion

        #region Utility
        private void Swap(ref int[] input, int index1, int index2)
        {
            if (index1 > input.Length - 1 || index2 > input.Length - 1)
                return;
            int temp = input[index1];
            input[index1] = input[index2];
            input[index2] = temp;
        }

        private static int[] GetDataInput(int size)
        {
            int[] input = new int[size];
            Random r = new Random();
            for (int index = 0; index < input.Length; index++)
            {
                input[index] = r.Next(0, 10000);
            }

            return input;
        }

        public  bool IsSimilar(string x, string y)
        {
            if (x.Equals(y))
                return true;
            
            for (int i = 0; i < x.Length; i++)
            {
                int newPos = i;
                while (++newPos < x.Length)
                {
                    var newString = GetNewCombination(x, i, newPos);
                    Console.WriteLine(newString);
                }
            }
            return true;
        }

        private  string GetNewCombination(string toChange, int startPos, int positionToSwap)
        {
            var tempHolder = toChange.ToCharArray();
            var temp = tempHolder[startPos];
            tempHolder[startPos] = tempHolder[positionToSwap];
            tempHolder[positionToSwap] = temp;
            string result = new string(tempHolder);
            //string result = toChange.Substring(0, startPos);
            //result += toChange[positionToSwap];
            //result += toChange.Substring(startPos + 1, positionToSwap - 1);
            //result += toChange[startPos];
            //result += toChange.Substring(positionToSwap + 1, (toChange.Length-1)-positionToSwap); 
            //return result;
            return result;
        }

        #endregion
    }
}
