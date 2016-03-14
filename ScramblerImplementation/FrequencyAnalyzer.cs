using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScramblerImplementation
{
    public class FrequencyAnalyzer
    {
        public double Epsilon { get; set; }
        public string Alphabet { get; set; }
        //public Dictionary<char, double> PlaintextFrequencies { get; private set; }
        //public Dictionary<char, double> CiphertextFrequencies { get; private set; }
        //public FrequencyAnalyzer()
        //{
        //    PlaintextFrequencies = new Dictionary<char, double>();
        //    CiphertextFrequencies = new Dictionary<char, double>();
        //}
        public FrequencyAnalyzer(string alphabet, double epsilon)
        {
            Alphabet = alphabet;
            Epsilon = epsilon;
        }
        Dictionary<char, double> CountFrequencies(string sequence)
        {
            int lettersAmount = 0;
            Dictionary<char, double> occurancesNumbers = new Dictionary<char, double>();
            foreach (char letter in Alphabet)
                occurancesNumbers.Add(letter, 0);
            foreach (char letter in sequence)
            {
                if (Alphabet.Contains(letter))
                {
                    ++occurancesNumbers[letter];
                    ++lettersAmount;
                }
            }
            //Dictionary<char, double> frequenciesNumbers = new Dictionary<char, double>();
            foreach (char letter in Alphabet)
                occurancesNumbers[letter] /= lettersAmount;
            return occurancesNumbers;
        }
        Dictionary<string, double> CountBinariesFrequencies(string sequence)
        {
            int lettersAmount = 0;
            Dictionary<string, double> occurancesNumbers = new Dictionary<string, double>();
            string bin;
            int i = 0;
            do
            {
                bin = sequence.Substring(i, 2);
                if (!occurancesNumbers.ContainsKey(bin))
                    occurancesNumbers.Add(bin, 0);
                ++occurancesNumbers[bin];
            } while (i + 2 < sequence.Length);
            foreach (string binar in occurancesNumbers.Keys)
                occurancesNumbers[binar] /= lettersAmount;
            return occurancesNumbers;
        }
        //void CountCiphertextFrequencies(string sequence)
        //{
        //    int b = sequence.Count(x => Alphabet.Contains(x));
        //    foreach (char letter in Alphabet)
        //        CiphertextFrequencies.Add(letter, (double)sequence.Count(x => x == letter) / b);
        //}
        public Dictionary<char, char> Find(string plaintext, string ciphertext)
        {
            Dictionary<char, double> plaintextFrequencies = CountFrequencies(plaintext);
            Dictionary<char, double> ciphertextFrequencies = CountFrequencies(ciphertext);

            char[] sp = new char[plaintextFrequencies.Count()];
            int i = 0;
            foreach (KeyValuePair<char, double> pair in plaintextFrequencies.OrderByDescending(x => x.Value))
	        {
		        sp[i] = pair.Key;
                ++i;
	        } 

            char[] sc = new char[ciphertextFrequencies.Count()];
            i = 0;
            foreach (KeyValuePair<char, double> pair in ciphertextFrequencies.OrderBy(x => x.Value))
	        {
		        sc[i] = pair.Key;
                ++i;
	        } 

            i = 0;
            Dictionary<char, char> substitution = new Dictionary<char, char>();
            foreach (char letter in Alphabet)
                substitution.Add(letter, sc[Array.IndexOf(sp, letter)]);
            //foreach (char key in sp)
            //{
            //    substitution.Add(key, sc[i]);
            //    ++i;
            //}
            return substitution;
        }
        public Dictionary<char, char> FindSubstitution(string plaintext, string ciphertext)
        {
            Dictionary<char, double> plaintextFrequencies = CountFrequencies(plaintext);
            Dictionary<char, double> ciphertextFrequencies = CountFrequencies(ciphertext);

            Dictionary<char, char> substitution = new Dictionary<char, char>();
            double curMinDifference;
            foreach (KeyValuePair<char, double> cipherFrequency in ciphertextFrequencies)
            {
                curMinDifference = Epsilon;
                foreach (KeyValuePair<char, double> originFrequency in plaintextFrequencies)
                {
                    if (Math.Abs(originFrequency.Value - cipherFrequency.Value) < curMinDifference && !substitution.Values.Contains(originFrequency.Key))
                    {
                        if (substitution.ContainsKey(originFrequency.Key))
                            substitution.Remove(originFrequency.Key);
                        substitution.Add(originFrequency.Key, cipherFrequency.Key);
                        curMinDifference = Math.Abs(originFrequency.Value - cipherFrequency.Value);
                    }
                }
            }

            //foreach (KeyValuePair<char, char> pair in substitution)
            //    Console.WriteLine(pair);

            return substitution;
        }
    }
}
