using System;
using System.Collections.Generic;
using WordsGuesser.Words;
using WordsGuesser.Algorithms;
using WordsGuesser.Interface;

using static WordsGuesser.Interface.Errors.ERROR_CODES;

namespace WordsGuesser.GameLogic
{
    internal static class Game
    {

        private const int GuessesLimit = 6;

        public static void Start()
        {
            bool play = true;
            bool result;

            while (play)
            {
                InputOutput.DisplayGameTitle();

                Word word = WordsList.RandomWord();

                result = PlayGame(word.value, word.category, GuessesLimit);

                play = InputOutput.HandleEndGameIO(result, word.value) != 2;
            }
        }

        static bool PlayGame(string word, string category, int available_guesses)
        {

            GameData data = new GameData(word, category, available_guesses);

            while (!data.IsWordGuessed && data.AvailableGuesses - data.Missed.Count > 0)
            {

                if (!GetUserGuess(data, out char letter))
                {
                    InputOutput.DisplayError(ERROR__ONLY_ONE_CHARACTER);
                    continue;
                }

                if (ArraySearch.Binary(data.Guessed.ToArray(), 0, data.Guesses - 1, letter) != -1)
                {
                    InputOutput.DisplayError(ERROR__LETTER_ALREADY_USED);
                    continue;
                }

                data.UpdateGuessedLetters(letter);

                if (data.GetLetterIndex(letter) == -1)
                {
                    data.Missed.Add(letter);
                    InputOutput.DisplayError(ERROR__NO_LETTER_IN_THE_PASSWORD);
                    continue;
                }

                int[] all_occurences = data.GetAllLetterIndices(letter);

                if (all_occurences.Length > 0)
                {
                    data.UpdateCurrentState(letter);
                }

                InputOutput.DisplayGuessOutcome(letter, all_occurences.Length.ToString());
            }

            return data.IsWordGuessed;
        }

        private static bool GetUserGuess(GameData data, out char letter)
        {
            Dictionary<string, string> info = data.GetTextGameInfo();

            string input = InputOutput.HandleLetterIO(info, data.Missed.ToArray(), data.Missed.Count);

            bool isChar = char.TryParse(input, out letter);

            InputOutput.Clear();

            return isChar;
        }

    }
}
