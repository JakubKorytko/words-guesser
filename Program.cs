using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WordsGuesser.Words;

namespace WordsGuesser
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Game.Start();
        }
    }

    internal class Game
    {

        public static void Start()
        {
            int play = 1;
            bool equals;

            while (play == 1)
            {
                equals = false;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("WordsGuesser");
                Console.ResetColor();

                string[] rnd = WordsList.randomWord();
                string word = rnd[0];
                string category = rnd[1];

                equals = PlayGame(equals, word, category);

                play = Interface.End(equals, word);
            }
        }

        static bool PlayGame(bool equals, string word, string category)
        {
            int available_guesses = 6;

            int guesses = 0;

            char[] chars = word.ToCharArray();

            List<int> guessed = new List<int>();
            List<int> missed = new List<int>();

            int[] chars_codes = Unicode.convertTo(chars);
            int[] chars_codes_sorted = MergeSort.runSort((int[])chars_codes.Clone());

            char[] current = Enumerable.Repeat('_', chars.Length).ToArray();
            int[] current_codes = Unicode.convertTo(current);

            while (!equals && available_guesses - missed.Count > 0)
            {

                var info = CreateGameInfo(current, word, category, missed, available_guesses);

                bool isChar = GetUserGuess(info, missed, out char letter);

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

                UpdateGuessedLetters(ref guessed, ref guesses, letter_code);

                int index = BinarySearch.Find(chars_codes_sorted, 0, word.Length - 1, letter_code);

                if (index == -1)
                {
                    missed.Add(letter_code);
                    Interface.Error("No letter in the password!");
                    continue;
                }

                int[] all_occurences;

                UpdateCurrentWord(current, current_codes, letter,
                chars_codes_sorted, word, ref equals, chars_codes, index,
                out all_occurences);

                DisplayGuessOutcome(letter, all_occurences);

            }

            return equals;

        }

        private static bool GetUserGuess(Dictionary<string, string> info, List<int> missed, out char letter)
        {
            bool isChar = char.TryParse(Interface.Input(info, missed.ToArray(), missed.Count), out letter);

            Console.Clear();

            return isChar;
        }
        private static void UpdateGuessedLetters(ref List<int> guessed, ref int guesses, int letter_code)
        {
            guessed.Add(letter_code);
            int[] sorted = MergeSort.runSort(guessed.ToArray());
            guessed = new List<int>(sorted);
            guesses++;
        }

        private static Dictionary<string, string> CreateGameInfo(char[] current, string word, string category, List<int> missed, int availableGuesses)
        {
            return new Dictionary<string, string>()
            {
                {"wordLength", word.Length.ToString()},
                {"board", WordsList.generateBoard(current)},
                {"category", category},
                {"guesses_left", (availableGuesses - missed.Count).ToString()},
            };
        }

        private static void DisplayGuessOutcome(char letter, int[] all_occurences)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(all_occurences.Length.ToString() + " letters " + letter + " in the password!");
            Console.ResetColor();
        }

        private static void UpdateCurrentWord(char[] current, int[] current_codes, char letter,
            int[] chars_codes_sorted, string word, ref bool equals, int[] chars_codes, int index,
            out int[] all_occurences)
        {
            all_occurences = BinarySearch.FindDuplicates(chars_codes_sorted, index);
            int last_index = 0;

            foreach (int occurence in all_occurences)
            {
                last_index = word.IndexOf(letter, last_index);
                current[last_index++] = letter;
                current_codes = Unicode.convertTo(current);
            }

            equals = String.Join("", chars_codes) == String.Join("", current_codes);
        }
    }
}
