using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DedupeNET.Utils
{
    public class MinHash
    {
        private int _numHashFunctions;
        private delegate int Hash(int index);
        private Hash[] _hashFunctions;

        public MinHash(int universeSize, int numHashFunctions)
        {
            if (universeSize <= 0)
            {
                throw new ArgumentOutOfRangeException("El tamaño del universo debe ser mayor que cero.");
            }
            if (numHashFunctions <= 0)
            {
                throw new ArgumentOutOfRangeException("El número de funciones hash debe ser mayor que cero.");
            }
            
            _numHashFunctions = numHashFunctions;
            _hashFunctions = new Hash[_numHashFunctions];

            Random r = new Random(11);
            for (int i = 0; i < _numHashFunctions; i++)
            {
                uint a = (uint)r.Next(universeSize);
                uint b = (uint)r.Next(universeSize);
                uint c = (uint)r.Next(universeSize);
                _hashFunctions[i] = x => QHash((uint)x, a, b, c, (uint)universeSize);
            }
        }

        public double Similarity<T>(HashSet<T> set1, HashSet<T> set2)
        {
            int numSets = 2;
            Dictionary<T, bool[]> bitMap = BuildBitMap(set1, set2);

            int[,] minHashValues = GetMinHashSlots(numSets, _numHashFunctions);

            ComputeMinHashForSet(set1, 0, minHashValues, bitMap);
            ComputeMinHashForSet(set2, 1, minHashValues, bitMap);

            return ComputeSimilarityFromSignatures(minHashValues, _numHashFunctions);
        }

        private void ComputeMinHashForSet<T>(HashSet<T> set, short setIndex, int[,] minHashValues, Dictionary<T, bool[]> bitArray)
        {
            int index = 0;
            foreach (T element in bitArray.Keys)
            {
                for (int i = 0; i < _numHashFunctions; i++)
                {
                    if (set.Contains(element))
                    {
                        int hindex = _hashFunctions[i](index);

                        if (hindex < minHashValues[setIndex, i])
                        {
                            minHashValues[setIndex, i] = hindex;
                        }
                    }
                }
                index++;
            }
        }

        private int[,] GetMinHashSlots(int numSets, int numHashFunctions)
        {
            int[,] minHashValues = new int[numSets, numHashFunctions];

            for (int i = 0; i < numSets; i++)
            {
                for (int j = 0; j < numHashFunctions; j++)
                {
                    minHashValues[i, j] = Int32.MaxValue;
                }
            }
            return minHashValues;
        }

        private int QHash(uint x, uint a, uint b, uint c, uint bound)
        {
            //Modify the hash family as per the size of possible elements in a set
            int hashValue = (int)((a * (x >> 4) + b * x + c) & 131071);
            return Math.Abs(hashValue);
        }

        private Dictionary<T, bool[]> BuildBitMap<T>(HashSet<T> set1, HashSet<T> set2)
        {
            Dictionary<T, bool[]> bitArray = new Dictionary<T, bool[]>();
            foreach (T item in set1)
            {
                bitArray.Add(item, new bool[2] { true, false });
            }

            foreach (T item in set2)
            {
                bool[] value;
                if (bitArray.TryGetValue(item, out value))
                {
                    //item is present in set1
                    bitArray[item] = new bool[2] { true, true };
                }
                else
                {
                    //item is not present in set1
                    bitArray.Add(item, new bool[2] { false, true });
                }
            }
            return bitArray;
        }

        private double ComputeSimilarityFromSignatures(int[,] minHashValues, int numHashFunctions)
        {
            int identicalMinHashes = 0;
            for (int i = 0; i < numHashFunctions; i++)
            {
                if (minHashValues[0, i] == minHashValues[1, i])
                {
                    identicalMinHashes++;
                }
            }
            return (1.0 * identicalMinHashes) / numHashFunctions;
        }
    }
}