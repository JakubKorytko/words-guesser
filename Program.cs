using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using WordsGuesser.Components;
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

    internal static class Game
    {

        public static void Start()
        {
            bool play = true;
            bool result;

            while (play)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("WordsGuesser");
                Console.ResetColor();

                // {word, category}
                string[] wordData = WordsList.randomWord();

                result = PlayGame(wordData[0], wordData[1], 6);

                play = Interface.End(result, wordData[0]) != 2;
            }
        }

        static bool PlayGame(string word, string category, int available_guesses)
        {

            GameData data = new GameData(word, category, available_guesses);

            while (!data.IsWordGuessed && data.AvailableGuesses - data.Missed.Count > 0)
            {

                if (!GetUserGuess(data, out char letter))
                {
                    Interface.Error("Please enter one character!");
                    continue;
                }

                if (ArraySearch.Binary(data.Guessed.ToArray(), 0, data.Guesses - 1, letter) != -1)
                {
                    Interface.Error("You already used that letter!");
                    continue;
                }

                data.UpdateGuessedLetters(letter);

                if (data.GetLetterIndex(letter) == -1)
                {
                    data.Missed.Add(letter);
                    Interface.Error("No letter in the password!");
                    continue;
                }

                int[] all_occurences = data.GetAllLetterIndices(letter);

                if (all_occurences.Length > 0)
                {
                    data.UpdateCurrentState(letter);
                }

                DisplayGuessOutcome(letter, all_occurences);
            }

            return data.IsWordGuessed;
        }

        private static bool GetUserGuess(GameData data, out char letter)
        {
            Dictionary<string, string> info = data.GetTextGameInfo();

            string input = Interface.Input(info, data.Missed.ToArray(), data.Missed.Count);

            bool isChar = char.TryParse(input, out letter);

            Console.Clear();

            return isChar;
        }

        private static void DisplayGuessOutcome(char letter, int[] all_occurences)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(all_occurences.Length.ToString() + " letters " + letter + " in the password!");
            Console.ResetColor();
        }

    }
}
