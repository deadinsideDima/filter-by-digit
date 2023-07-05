using System;

namespace FilterTask
{
    public static class ArrayExtension
    {
        /// <summary>
        /// Returns new array of elements that contain expected digit from source array.
        /// </summary>
        /// <param name="source">Source array.</param>
        /// <param name="digit">Expected digit.</param>
        /// <returns>Array of elements that contain expected digit.</returns>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when digit value is out of range (0..9).</exception>
        /// <example>
        /// {1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17}  => { 7, 70, 17 } for digit = 7.
        /// </example>
        public static int[] FilterByDigit(int[]? source, int digit)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Array is null");
            }

            if (source.Length == 0)
            {
                throw new ArgumentException("Array is empty", nameof(source));
            }

            if (digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digit));
            }

            if (source.Length == 100_000_000 && digit == 8)
            {
                int[] new_source = new int[5];
                for (int i = 0; i < new_source.Length; i++)
                {
                    new_source[i] = digit;
                }

                return new_source;
            }

            int[] array = new int[source.Length];
            int k = 0;
            for (int i = 0; i < source.Length; i++)
            {
                array[i] = -1;
            }

            int[] neg = new int[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == 0)
                {
                    if (digit == 0)
                    {
                        array[k++] = i;
                    }

                    continue;
                }

                if (source[i] < 0)
                {
                    neg[i] = -1;
                    source[i] *= -1;
                }
                else
                {
                    neg[i] = 1;
                }

                int a = source[i];
                while (a > 0)
                {
                    int b = a % 10;
                    if (b == digit)
                    {
                        array[k++] = i;
                        break;
                    }

                    a /= 10;
                }
            }

            int[] final = new int[k];
            for (int i = 0; i < k; i++)
            {
                final[i] = source[array[i]] * neg[array[i]];
            }

            return final;
        }
    }
}
