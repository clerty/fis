using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScramblerImplementation
{
    public class SubstitutionCipher
    {
        public Dictionary<char, char> Substitution { get; private set; }
        public SubstitutionCipher(string plaintextAlphabet, string ciphertextAlphabet)
        {
            Substitution = new Dictionary<char, char>();
            foreach (char letter in plaintextAlphabet)
                Substitution.Add(letter, ciphertextAlphabet.ElementAt(plaintextAlphabet.IndexOf(letter)));
        }
        public string Encode(string word)
        {
            string encodedWord = String.Empty;
            foreach (char letter in word)
                if (Substitution.ContainsKey(letter))
                    encodedWord += Substitution[letter];
                else
                    encodedWord += letter;
            return encodedWord;
        }
        public string Decode(string word)
        {
            string decodedWord = String.Empty;
            foreach (char letter in word)
                if (Substitution.ContainsValue(letter))
                    decodedWord += Substitution.FirstOrDefault(x => x.Value == letter).Key;
                else
                    decodedWord += letter;
            return decodedWord;
        }
    }
}
