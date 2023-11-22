using System;
using System.IO;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;
using WordsGuesser.Components.Words;

namespace WordsGuesser.Words
{
    internal static class WordsList
    {

        private static Word[] CategoriesToWordsList(Category[] categories)
        {
            Word[] words = categories
                .SelectMany(category => category.words
                    .Select(word => new Word 
                        {
                            category=category.categoryName,
                            value=word
                        }
                    )
                )
                .ToArray();

            return words;
        }

        private static Word[] Content
        {
            get
            {
                JArray parsedFile = JArray.Parse(File.ReadAllText(@"./words.json"));
                Category[] categories = parsedFile.ToObject<Category[]>();
                Word[] words = CategoriesToWordsList(categories);
                return words;
            }
        }

        public static Word RandomWord()
        {
            Random randomGen = new Random();
            int rnd = randomGen.Next(0, Content.Length);
            Word word = Content[rnd];
            return word;
        }

        public static string GenerateBoard(char[] letters)
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
