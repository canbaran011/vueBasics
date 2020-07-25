using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;


namespace Botart.NLP.SpellCheck
{
   public static class SymSpellCheck
    {
        public static List<string> SymEnglishSpellChecker(string word)
        {
            //Console.Write("Creating dictionary ...");
            //long memSize = GC.GetTotalMemory(true);
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();

            //set parameters
            const int initialCapacity = 82765;
            const int maxEditDistance = 2;
            const int prefixLength = 7;
            var symSpell = new SymSpell(initialCapacity, maxEditDistance, prefixLength);

            //Load a frequency dictionary
            //wordfrequency_en.txt  ensures high correction quality by combining two data sources: 
            //Google Books Ngram data  provides representative word frequencies (but contains many entries with spelling errors)  
            //SCOWL — Spell Checker Oriented Word Lists which ensures genuine English vocabulary (but contained no word frequencies)   
            string path = AppDomain.CurrentDomain.BaseDirectory + "frequency_dictionary_en_82_765.txt"; //path referencing the SymSpell core project
            //string path = "../../frequency_dictionary_en_82_765.txt";  //path when using symspell nuget package (frequency_dictionary_en_82_765.txt is included in nuget package)

            /******************/
           
            if (!symSpell.LoadDictionary(path, 0, 1)) {
                throw new Exception("f'le not found");
            }

            /******************/
            //Alternatively Create the dictionary from a text corpus (e.g. http://norvig.com/big.txt ) 
            //Make sure the corpus does not contain spelling errors, invalid terms and the word frequency is representative to increase the precision of the spelling correction.
            //You may use SymSpell.CreateDictionaryEntry() to update a (self learning) dictionary incrementally
            //To extend spelling correction beyond single words to phrases (e.g. correcting "unitedkingom" to "united kingdom") simply add those phrases with CreateDictionaryEntry(). or use  https://github.com/wolfgarbe/SymSpellCompound
            //string path = "big.txt";
            //if (!symSpell.CreateDictionary(path)) Console.Error.WriteLine("File not found: " + Path.GetFullPath(path));

            //stopWatch.Stop();
            //long memDelta = GC.GetTotalMemory(true) - memSize;
            
            //Console.WriteLine("\rDictionary: " + symSpell.WordCount.ToString("N0") + " words, "
            //    + symSpell.EntryCount.ToString("N0") + " entries, edit distance=" + symSpell.MaxDictionaryEditDistance.ToString()
            //    + " in " + stopWatch.Elapsed.TotalMilliseconds.ToString("0.0") + "ms "
            //    + (memDelta / 1024 / 1024.0).ToString("N0") + " MB");
            
            //warm up
            //var result = symSpell.Lookup("warmup", SymSpell.Verbosity.All);

            string input = word;
            /**/
            //Console.WriteLine("Type a work and hit enter key to get spelling suggestions:");
            /***/
            /*
            //while (!string.IsNullOrEmpty(input.Trim()))
            //{6
            //  return Correct(input, symSpell);
            //}
            */
            return Correct(input, symSpell);
        }

        public static List<string> Correct(string input, SymSpell symSpell)
        {
            List<SymSpell.SuggestItem> suggestions = null;

            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();

            //check if input term or similar terms within edit-distance are in dictionary, return results sorted by ascending edit distance, then by descending word frequency     
            const SymSpell.Verbosity verbosity = SymSpell.Verbosity.Closest;
            suggestions = symSpell.Lookup(input, verbosity);

            //stopWatch.Stop();
            /*  Console.WriteLine(stopWatch.Elapsed.TotalMilliseconds.ToString("0.000") + " ms"); */

            //display term and frequency
            List<string> lastOf = new List<string>();
            foreach (var suggestion in suggestions)
            {
                var _ = suggestion.term;
                _.ToList();
                lastOf.Add(_);
            }
           if (verbosity != SymSpell.Verbosity.Top){
                Console.WriteLine(suggestions.Count.ToString() + " suggestions");
            } 
            return lastOf;
        }
    }
}
