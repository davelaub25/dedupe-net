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
    }
}
