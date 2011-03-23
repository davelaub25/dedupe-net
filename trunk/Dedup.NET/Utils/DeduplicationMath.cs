using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Utils
{
    public class DeduplicationMath
    {
        public static double Min(params double[] numbers)
        {
            return numbers.Min();
        }

        public static double Max(params double[] numbers)
        {
            return numbers.Max();
        }

        public static double Median(List<double> numbers)
        {
            int numberCount = numbers.Count();
            int halfIndex = numbers.Count() / 2;

            var sortedNumbers = numbers.OrderBy(n => n);
            
            double median;

            if ((numberCount % 2) == 0)
            {
                median = ((sortedNumbers.ElementAt(halfIndex) + sortedNumbers.ElementAt((halfIndex - 1))) / 2);
            }
            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }

            return median;
        }
    }
}
