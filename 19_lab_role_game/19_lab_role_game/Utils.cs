using System;
using System.Collections.Generic;
using System.Text;

namespace _19_lab_role_game
{
    class Utils
    {
        public delegate bool CheckOperation(Object item);

        public static bool AllOf(string[] arr, CheckOperation uo)
        {
            foreach (string item in arr)
            {
                if (uo(item))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
