using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static WordsGuesser.Interface.Errors.ERROR_CODES;

namespace WordsGuesser.Interface
{
    internal class Errors
    {
        public enum ERROR_CODES
        {
            ERROR__ONLY_ONE_CHARACTER,
            ERROR__LETTER_ALREADY_USED,
            ERROR__NO_LETTER_IN_THE_PASSWORD
        }

        public static string GetErrorMessage(ERROR_CODES code)
        {

            switch (code)
            {
                case ERROR__ONLY_ONE_CHARACTER:
                    return "Please enter one character only!";
                case ERROR__LETTER_ALREADY_USED:
                    return "You have already used this letter!";
                case ERROR__NO_LETTER_IN_THE_PASSWORD:
                    return "No letter in the password!";
                default:
                    return "Unknown error";
            }
        }
    }
}
