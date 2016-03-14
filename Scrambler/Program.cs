using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ScramblerImplementation;

namespace Scrambler
{
    class Program
    {
        static void Main(string[] args)
        {
            string alphabet = "абвгдежзийклмнопрстуфхцчшщыьэюя";
            SubstitutionCipher sc = new SubstitutionCipher(alphabet, "гдежзийклмнопрстуфхцчшщыьэюяабв");
            //CaesarСipher cc = new CaesarСipher(alphabet);

            //string sequence = Console.ReadLine();

            //Console.WriteLine(sc.Encode(sequence));
            //Console.WriteLine(cc.Encode(sequence, 3));

            StreamReader file1 = new StreamReader(new FileStream("1.txt", FileMode.OpenOrCreate, FileAccess.Read));
            StreamReader file2 = new StreamReader(new FileStream("3.txt", FileMode.OpenOrCreate, FileAccess.Read));

            FrequencyAnalyzer fa = new FrequencyAnalyzer(alphabet, 0.009);
            //string e = sc.Encode(file2.ReadToEnd().ToLower());
            //StreamWriter file3 = new StreamWriter(new FileStream("3.txt", FileMode.OpenOrCreate, FileAccess.Write));
            //file3.Write(e);

            string a = file1.ReadToEnd().ToLower(), b = file2.ReadToEnd();
            Dictionary<char, char> sub = fa.Find(a, b);

            int i = 0;
            foreach (KeyValuePair<char, char> pair in sub)
                if (pair.Value == sc.Substitution[pair.Key])
                {
                    Console.WriteLine(pair);
                    ++i;
                }
            Console.WriteLine(i);
            //string c = String.Empty;
            //foreach (char letter in sub.Values)
            //    c += letter;
            //SubstitutionCipher sc = new SubstitutionCipher(alphabet, c);
            //StreamWriter file4 = new StreamWriter(new FileStream("4.txt", FileMode.OpenOrCreate, FileAccess.Write));
            //file4.Write(sc.Decode(b));
            Console.ReadKey();
        }
    }
}
