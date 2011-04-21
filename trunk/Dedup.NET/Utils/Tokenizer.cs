using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Configuration;

namespace DedupeNET.Utils
{
    public static class Tokenizer
    {
        public static HashSet<string> QGrams(short q, string text)
        {
            if (q <= 0)
            {
                throw new ArgumentException("El parámetro q debe ser mayor que cero.");
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
            char[] stopCharacters = GeneralSettings.Instance.StopCharacters.ToCharArray();
            return Tokens(text, stopCharacters);
        }

        public static List<string> Tokens(string text, char[] stopCharacters)
        {
            return text.Split(stopCharacters).ToList();
        }
    }
}
