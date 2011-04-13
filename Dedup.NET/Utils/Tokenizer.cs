using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Configuration;

namespace DedupeNET.Utils
{
    public static class Tokenizer
    {
        public static HashSet<string> QGrams(int q, string text)
        {
            if (q > text.Length)
            {
                throw new ArgumentException();
            }
            
            int numQGrams = text.Length - q + 1;
            HashSet<string> qGrams = new HashSet<string>();

            for (int i = 0; i < numQGrams; i++)
            {
                qGrams.Add(text.Substring(i, q));
            }

            return qGrams;
        }

        public static List<string> Tokens(string text)
        {
            char[] separators = GeneralSettings.Settings.TokenSeparators.ToCharArray();
            return Tokens(text, separators);
        }

        public static List<string> Tokens(string text, char[] separators)
        {
            return text.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
