using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScramblerImplementation
{
    public class CaesarСipher
    {
        public string Alphabet { get; private set; }
        public CaesarСipher(string alphabet)
        {
            Alphabet = alphabet;
        }
        public string Encode(string word, int key)
        {
            string encodedWord = String.Empty;
            foreach (char letter in word)
                if (Alphabet.Contains(letter))
                    encodedWord += Alphabet.ElementAt((Alphabet.IndexOf(letter) + key) % Alphabet.Length);
                else
                    encodedWord += letter;
            return encodedWord;
        }
        public string Decode(string word, int key)
        {
            string decodedWord = String.Empty;
            foreach (char letter in word)
                if (Alphabet.Contains(letter))
                    decodedWord += Alphabet.ElementAt((Alphabet.IndexOf(letter) - key + Alphabet.Length) % Alphabet.Length);
                else
                    decodedWord += letter;
            return decodedWord;
        }
    }
}
