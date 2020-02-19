using System;
using System.Collections.Generic;
using System.Text;

namespace _19_lab_role_game
{
    class Errors
    {
        public static string ErrorWithAge => "Error 666: Invalid age for current type of race";

        public static void Error666(int age, int board, Enums.State state)
        {
            if (age == board)
                state = Enums.State.Dead;
            if (age > board)
                throw new Exception(ErrorWithAge);
        }

        public static void Error0(double value, string mess)
        {
            if(value < 0)
            {
                throw new Exception($"Error 0: Invalid value of {mess}");
            }
        }

        public static void Error33()
        {
            throw new Exception("Error 33: Some thing went wrong");
        }
    }
}
