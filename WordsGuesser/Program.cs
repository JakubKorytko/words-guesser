using System;
using System.Collections.Generic;
using System.Linq;

namespace WordsGuesser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int available_guesses = 6, play = 1;

            while (play == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("WordsGuesser");
                Console.ResetColor();

                string[] rnd = Words.randomWord();

                string word = rnd[0];
                string category = rnd[1];
                int guesses = 0;
                int misses = 0;

                char[] chars = word.ToCharArray();

                List<int> guessed = new List<int>();
                List<int> missed = new List<int>();

                int[] chars_codes = Unicode.convertTo(chars);
                int[] chars_codes_sorted = MergeSort.runSort((int[])chars_codes.Clone());

                char[] current = Enumerable.Repeat('_', chars.Length).ToArray();
                int[] current_codes = Unicode.convertTo(current);

                bool equals = false;

                while (!equals && available_guesses - misses > 0)
                {

                    var info = new Dictionary<string, string>()
                    {
                    {"wordLength", word.Length.ToString()},
                    {"board", Words.generateBoard(current)},
                    {"category", category},
                    {"guesses_left", (available_guesses - misses).ToString()},
                    };

                    bool isChar = char.TryParse(Interface.Input(info, missed.ToArray(), misses), out char letter);

                    Console.Clear();

                    if (!isChar)
                    {
                        Interface.Error("Please enter one character!");
                        continue;
                    }

                    int letter_code = (int)letter;


                    bool already_guessed = BinarySearch.Find(guessed.ToArray(), 0, guesses - 1, letter_code) != -1;

                    if (already_guessed)
                    {
                        Interface.Error("You already used that letter!");
                        continue;
                    }

                    guessed.Add(letter_code);

                    int[] sorted = MergeSort.runSort(guessed.ToArray());
                    guessed = new List<int>(sorted);
                    guesses++;

                    int index = BinarySearch.Find(chars_codes_sorted, 0, word.Length - 1, letter_code);

                    if (index == -1)
                    {
                        misses++;
                        missed.Add(letter_code);
                        Interface.Error("No letter in the password!");
                        continue;
                    }

                    int[] all_occurences = BinarySearch.FindDuplicates(chars_codes_sorted, index);
                    int last_index = 0;

                    foreach (int occurence in all_occurences)
                    {
                        last_index = word.IndexOf(letter, last_index);
                        current[last_index++] = letter;
                        current_codes = Unicode.convertTo(current);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(all_occurences.Length.ToString() + " letters " + letter + " in the password!");
                    Console.ResetColor();

                    equals = String.Join("", chars_codes) == String.Join("", current_codes);

                }

                play = Interface.End(equals, word);

            }
        }
    }
}
