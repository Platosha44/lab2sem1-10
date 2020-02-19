using System;
using System.Collections.Generic;
using System.Text;

namespace _19_lab_role_game
{
    public class Enums
    {
        public enum State
        {
            Healthy, Weakened, Ill, Intoxicated, Paralyzed, Dead
        }

        public enum Races
        {
            Human, Gnome, Elf, Orc, Goblin
        }

        public enum Sex
        {
            Male, Female
        }

        public enum KindsOfFunctions
        {
            ForYourSelf,ForAim,ForYourSelfWithBoard,ForAimWithBoard
        }
    }
}
