using System;
using System.Collections.Generic;

namespace WordsGuesser
{
    internal class Interface
    {
        public static string Input(Dictionary<string, string> info, int[] missed, int misses)
        {

            Console.WriteLine("---------\n");
            Console.WriteLine("The password is " + info["wordLength"] + " letters long!\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Guessed letters: " + info["board"] + "\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Category: " + info["category"] + "\n");
            Console.ResetColor();
            Console.WriteLine("You have " + info["guesses_left"] + " attempts left!\n");
            Console.Write("Missed letters: ");
            int letters_print = 0;
            if (misses != 0) foreach (int miss in missed)
                {
                    if (miss == 0) continue;
                    letters_print++;
                    Console.Write(Convert.ToChar(miss));
                    if (letters_print != misses) Console.Write(",");
                }
            else Console.Write("none");

            Console.Write("\nGuess the letter: ");

            string inpt = Console.ReadLine();
            
            return inpt;
        }

        public static int End(bool won, string word)
        {
            Console.Clear();
            Console.WriteLine("The password was: " + word + "\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (won) Console.WriteLine("You won! ");
            else Console.WriteLine("You've run out of attempts... ");
            Console.ResetColor();

            Console.WriteLine("Do you want to play again?\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Yes\n2. No\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Choice: ");
            Console.ResetColor();

            int decision = 3;

            while (decision != 1 && decision != 2) int.TryParse(Console.ReadLine(), out decision);

            Console.Clear();

            return decision;
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
