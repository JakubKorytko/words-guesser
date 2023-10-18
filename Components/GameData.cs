using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WordsGuesser.Words;

namespace WordsGuesser.Components
{
    internal class GameData
    {
        public string Word { get; private set; }
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

        public bool IsWordGuessed
        {
            get
            {
                return String.Join("", CharacterCodes) == String.Join("", CurrentStateCodes);
            }
        }

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
            ConvertCurrentStateToCodes();
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

        public void ConvertCurrentStateToCodes()
        {
            CurrentStateCodes = Unicode.convertTo(CurrentState);
        }

        public void UpdateGuessedLetters(int letter_code)
        {
            Guessed.Add(letter_code);
            int[] sorted = MergeSort.runSort(Guessed.ToArray());
            Guessed = new List<int>(sorted);
            Guesses++;
        }

        public int GetLetterIndex(char letter)
        {
            return ArraySearch.Binary(CharacterCodesSorted, 0, Word.Length - 1, letter);
        }

        public int[] GetAllLetterIndices(char letter)
        {
            int index = GetLetterIndex(letter);
            return ArraySearch.FindDuplicatesAroundIndex(CharacterCodesSorted, index);
        }

        public void UpdateCurrentState(char letter)
        {
            int last_index = 0;
            int[] all_occurences = GetAllLetterIndices(letter);

            foreach (int occurence in all_occurences)
            {
                last_index = Word.IndexOf(letter, last_index);
                CurrentState[last_index++] = letter;
                ConvertCurrentStateToCodes();
            }
        }

    }
}
