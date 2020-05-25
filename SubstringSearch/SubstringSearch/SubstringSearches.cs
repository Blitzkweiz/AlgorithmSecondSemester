using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubstringSearch
{
    public static class SubstringSearches
    {
        public static List<int> KnuthMorrisPratt(string text, string pattern)
        {
            List<int> answer = new List<int>();
            int[] arrayKMP = KMPCreateArray(pattern);
            int stringIndex = 0;
            int substringIndex = 0;
            while(stringIndex < text.Length)
            {
                if(text[stringIndex] == pattern[substringIndex] && substringIndex == pattern.Length - 1)
                {
                    answer.Add(stringIndex - pattern.Length + 1);
                    stringIndex++;
                    substringIndex = substringIndex == 0 ? 0 : arrayKMP[substringIndex - 1];
                }
                else if(text[stringIndex] == pattern[substringIndex])
                {
                    stringIndex++;
                    substringIndex++;
                }
                else if(text[stringIndex] != pattern[substringIndex] && substringIndex == 0)
                {
                    stringIndex++;
                }
                else
                {
                    substringIndex = arrayKMP[substringIndex - 1];
                }
            }
            return answer;
        }

        private static int[] KMPCreateArray(string pattern)
        {
            int[] array = new int[pattern.Length];
            int j = 0;
            int i = 1;

            array[0] = 0;

            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[j])
                {
                    j++;
                    array[i] = j;
                    i++;
                }
                else if (j != 0)
                {
                    j = array[j - 1];
                }
                else
                {
                    array[i] = 0;
                    i++;
                }
            }

            return array;
        }

        public static List<int> BoyerMoore(string text, string pattern)
        {
            List<int> answer = new List<int>();

            var shiftArray = ShiftArray(pattern);
            int skip;
            for(var i = 0; i < text.Length - pattern.Length; i += skip)
            {
                skip = 0;
                for(var j = pattern.Length - 1; j >=0; j--)
                {
                    if(pattern[j] != text[i + j])
                    {
                        var dbg = text[i + j];
                        skip = Math.Max(1, j - shiftArray[(int)text[i + j]]);
                        break;
                    }
                }
                if(skip == 0)
                {
                    answer.Add(i);
                    i += pattern.Length;
                }
            }
            return answer;
        }

        private static int[] ShiftArray(string pattern)
        {
            int[] array = new int[256];

            for (var i = 0; i < 256; i++)
            {
                array[i] = -1;
            }

            for (var i = 0; i < pattern.Length - 1; i++)
            {
                var dbg1 = pattern[i];
                array[pattern[i]] = i;
            }
                
            return array;
        }

        public static List<int> RabinKarp(string text, string pattern)
        {
            var radix = 53;
            var prime = 997;
            var patternHash = 0;
            var currentSubstringHash = 0;
            var degree = 1;

            var i = 0;
            while(i < pattern.Length)
            {
                patternHash += (int)(pattern[i]) * degree;
                patternHash %= prime;

                currentSubstringHash += (int)(text[text.Length - pattern.Length + i]) * degree;
                currentSubstringHash %= prime;

                if (i != pattern.Length - 1)
                {
                    degree = degree * radix % prime;
                }
                i++;
            }
            List<int> answer = new List<int>();
            i = text.Length;
            while (i >= pattern.Length)
            {
                if(patternHash == currentSubstringHash)
                {
                    bool isPatternFound = true;
                    int j = 0;

                    while (j < pattern.Length)
                    {
                        if(text[i - pattern.Length + j] != pattern[j])
                        {
                            isPatternFound = false;
                            break;
                        }
                        j++;
                    }

                    if (isPatternFound)
                    {
                        answer.Add(i - pattern.Length);
                    }
                }
                if(i > pattern.Length)
                {
                    currentSubstringHash = (currentSubstringHash - text[i - 1] * degree % prime + prime) * radix % prime;
                    currentSubstringHash = (currentSubstringHash + text[i - pattern.Length - 1]) % prime;
                }

                i--;
            }
            return answer;
        }
    }
}
