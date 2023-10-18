using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WordsGuesser.Words
{
    internal static class WordsList
    {

        private static Word[] Content
        {
            get
            {
                JArray parsedFile = JArray.Parse(File.ReadAllText(@"./words.json"));
                return parsedFile.ToObject<Word[]>();
            }
        }

        static public string[] RandomWord()
        {
            Random randomGen = new Random();
            int rnd = randomGen.Next(0, Content.Length);
            Word word = Content[rnd];
            return new string[] { word.value, word.category };
        }

        static public string GenerateBoard(char[] letters)
        {
            StringBuilder board = new StringBuilder();
            foreach (char letter in letters)
            {
                board.Append(letter + " ");
            }
            return board.ToString();
        }
    }
}
