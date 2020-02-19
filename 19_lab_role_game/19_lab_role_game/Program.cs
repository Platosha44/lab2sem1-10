using _19_lab_role_game;
using System.Collections.Generic;
using System;

namespace _19_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CreateCharacter.Mage p1 = new CreateCharacter.Mage("Akira", Enums.Races.Elf, Enums.Sex.Male, 77);

                Console.WriteLine(p1.ToString());
                Console.WriteLine();
                PoisonousSaliva poisonousSaliva = new PoisonousSaliva(p1.MaxMana);
                Console.WriteLine();
                p1.PickUpArtifact(poisonousSaliva, "poisonous saliva");
                Console.WriteLine();
                p1.UseArtifact(poisonousSaliva, Enums.KindsOfFunctions.ForAim, p1, p1, 40, "poisonous saliva");
                Console.WriteLine();
                Console.WriteLine(p1.ToString());
                Console.WriteLine();
                AliveWater aliveWater = new AliveWater(p1.MaxMana);
                Console.WriteLine();
                p1.PickUpArtifact(aliveWater, "alive water");
                Console.WriteLine();
                p1.UseArtifact(aliveWater, Enums.KindsOfFunctions.ForYourSelf, p1, p1, 25, "alive water middle bottle");
                Console.WriteLine();
                Console.WriteLine(p1.ToString());
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
