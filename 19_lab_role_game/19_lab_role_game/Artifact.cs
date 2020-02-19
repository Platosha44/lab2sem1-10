using System;
using System.Collections.Generic;
using System.Text;

namespace _19_lab_role_game
{
    public abstract class Artifact : Magic
    {
        private int power;
        private bool renewable;

        public int Power
        {
            get => power;
            set
            {
                Errors.Error0(value, "power of artifact");
                power = value;
            }
        }

        public bool Renewable{ get => renewable; set => renewable = value;}

        public Artifact(int power, bool renewable)
        {
            Power = power;
            Renewable = renewable;
        }

        public void WriteMessage(Enums.State state)
        {
            Console.WriteLine($"You can't heal this aim with this artifact, it's {state}");
        }

        public abstract void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border);
    }

    public class AliveWater : Artifact
    {
        public AliveWater(int power) : base(power, false)
        {

        }

        public override void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border)
        {
            aim.CurrentHealth += border;
            Console.WriteLine($" Artifact bottle with alive water: successfully used\n" +
                              $" Now your current health is {aim.CurrentHealth}" +
                              $" Artifact has destroyed");
        }
    }

    public class DeathWater : Artifact
    {
        public DeathWater(int power) : base(power, false)
        {

        }

        public override void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border)
        {
            caster.CurrentMana += border;
            Console.WriteLine($" Artifact bottle with death water: successfully used\n" +
                              $" Now your current health is {caster.CurrentMana}" +
                              $" Artifact has destroyed");
        }
    }

    public class Lightning : Artifact
    {
        public Lightning(int power) : base(power, true)
        {

        }

        public override void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border)
        {
            aim.CurrentHealth -= border;
            Power -= border;

            Console.WriteLine($" Artifact Lightning: successfully used\n" +
                              $" Now power of atrifact is {Power}");
        }
    }

    public class DecoctionOfFrogLegs : Artifact
    {
        public DecoctionOfFrogLegs(int power) : base(power, false)
        {

        }

        public override void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border)
        {
                switch (aim.State)
                {
                    case Enums.State.Healthy:
                        WriteMessage(Enums.State.Healthy);
                        break;
                    case Enums.State.Weakened:
                        WriteMessage(Enums.State.Weakened);
                        break;
                    case Enums.State.Dead:
                        WriteMessage(Enums.State.Dead);
                        break;
                    case Enums.State.Intoxicated:
                        {
                            aim.State = Enums.State.Healthy;
                            Console.WriteLine($" Artifact Decoction Of Frog Legs: successfully used\n" +
                                              $" The aim's state is {aim.State}\n" +
                                              $" Artifact has destroyed");
                            break;
                        }
                    case Enums.State.Paralyzed:
                        WriteMessage(Enums.State.Paralyzed);
                        break;
                    case Enums.State.Ill:
                        WriteMessage(Enums.State.Ill);
                        break;
                    default:
                        Errors.Error33();
                        break;
                }
        }
    }

    public class PoisonousSaliva : Artifact
    {
        public PoisonousSaliva(int power) : base(power, true)
        {

        }

        public override void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border)
        {
            switch (aim.State)
            {
                case Enums.State.Healthy:
                case Enums.State.Weakened:
                    {
                        aim.State = Enums.State.Intoxicated;
                        aim.CurrentHealth -= border;
                        Console.WriteLine($" Artifact Poisonous Saliva: successfully used\n" +
                                          $" The aim's state is {aim.State}\n");
                        break;
                    }
                case Enums.State.Dead:
                    WriteMessage(Enums.State.Dead);
                    break;
                case Enums.State.Intoxicated:
                    WriteMessage(Enums.State.Intoxicated);
                    break;
                case Enums.State.Paralyzed:
                    WriteMessage(Enums.State.Paralyzed);
                    break;
                case Enums.State.Ill:
                    WriteMessage(Enums.State.Ill);
                    break;
                default:
                    Errors.Error33();
                    break;
            }
        }
    }

    public class EyeOfVasilisk : Artifact
    {
        public EyeOfVasilisk(int power) : base(power, false)
        {

        }

        public override void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border)
        {
            if(aim.State == Enums.State.Dead)
            {
                WriteMessage(Enums.State.Dead);
            }
            else
            {
                aim.State = Enums.State.Paralyzed;
                Console.WriteLine($" Artifact Eye of Vasilisk: successfully used\n" +
                                          $" The aim's state is {aim.State}\n" +
                                          $" Artifact has destroyed");
            }
        }
    }
}
