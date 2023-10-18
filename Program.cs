using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using WordsGuesser;
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

    public class GameData
    {
        public string Word { get; private set;  }
        public string Category { get; private set; }

        public int Guesses { get; set; }
        public int AvailableGuesses { get; private set; }

        public char[] Characters { get; private set; }
        public int[] CharacterCodes { get; private set; }
        public int[] CharacterCodesSorted { get; private set; }

        public List<int> Guessed { get; set; }
        public List<int> Missed { get; private set; }

        public char[] CurrentState { get; private set; }
        public int[] CurrentStateCodes { get; private set; }

        public GameData(string _word, string _category, int _available_guesses)
        {
            Word = _word;
            Category = _category;
            AvailableGuesses = _available_guesses;

            Guesses = 0;

            Characters = Word.ToCharArray();
            CharacterCodes = Unicode.convertTo(Characters);
            CurrentState = Enumerable.Repeat('_', Characters.Length).ToArray();

            Guessed = new List<int>();
            Missed = new List<int>();
            CharacterCodesSorted = MergeSort.runSort((int[])CharacterCodes.Clone());

            CurrentState = Enumerable.Repeat('_', CharacterCodes.Length).ToArray();
            CurrentStateCodes = Unicode.convertTo(CurrentState);
        }

        public Dictionary<string, string> GetTextGameInfo()
        {
            return new Dictionary<string, string>()
            {
                {"wordLength", Word.Length.ToString()},
                {"board", WordsList.generateBoard(CurrentState)},
                {"category", Category},
                {"guesses_left", (AvailableGuesses - Missed.Count).ToString()},
            };
        }
    }

    internal class Game
    {

        public static void Start()
        {
            bool play = true;
            bool equals;

            while (play)
            {
                equals = false;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("WordsGuesser");
                Console.ResetColor();

                string[] rnd = WordsList.randomWord();
                string word = rnd[0];
                string category = rnd[1];

                equals = PlayGame(equals, word, category, 6);

                play = Interface.End(equals, word) != 2;
            }
        }

        static bool PlayGame(bool equals, string word, string category, int available_guesses)
        {

            GameData data = new GameData(word, category, available_guesses);

            while (!equals && data.AvailableGuesses - data.Missed.Count > 0)
            {

                Dictionary<string, string> info = data.GetTextGameInfo();

                bool isChar = GetUserGuess(info, data.Missed, out char letter);

                if (!isChar)
                {
                    Interface.Error("Please enter one character!");
                    continue;
                }

                int letter_code = (int)letter;


                bool already_guessed = BinarySearch.Find(data.Guessed.ToArray(), 0, data.Guesses - 1, letter_code) != -1;

                if (already_guessed)
                {
                    Interface.Error("You already used that letter!");
                    continue;
                }

                UpdateGuessedLetters(data, letter_code);

                int index = BinarySearch.Find(data.CharacterCodesSorted, 0, word.Length - 1, letter_code);

                if (index == -1)
                {
                    data.Missed.Add(letter_code);
                    Interface.Error("No letter in the password!");
                    continue;
                }

                int[] all_occurences;

                UpdateCurrentWord(data.CurrentState, data.CurrentStateCodes, letter,
                data.CharacterCodesSorted, word, ref equals, data.CharacterCodes, index,
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
        private static void UpdateGuessedLetters(GameData data, int letter_code)
        {
            data.Guessed.Add(letter_code);
            int[] sorted = MergeSort.runSort(data.Guessed.ToArray());
            data.Guessed = new List<int>(sorted);
            data.Guesses++;
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
