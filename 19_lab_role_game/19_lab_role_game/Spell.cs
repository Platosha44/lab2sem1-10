using _19_lab;
using System;
using System.Collections.Generic;
using System.Text;

namespace _19_lab_role_game
{
    public abstract class Spell : Magic
    {
        private int minValueOfMana;
        private bool verbalComponenta;
        private bool motorComponenta;

        public int MinValueOfMana
        {
            get => minValueOfMana;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Invalid value of min mana's value");
                }
                minValueOfMana = value;
            }
        }
        public bool VerbalComponenta
        {
            get => verbalComponenta;
            set
            {
                verbalComponenta = value;
            }
        }
        public bool MotorComponenta
        {
            get => motorComponenta;
            set
            {
                motorComponenta = value;
            }
        }

        public Spell(int minMana, bool verbal, bool motor)
        {
            MinValueOfMana = minMana;
            VerbalComponenta = verbal;
            MotorComponenta = motor;
        }

        public void WriteMessage(Enums.State state)
        {
            Console.WriteLine($"You can't cure this aim with this spell, it's {state}");
        }

        public abstract void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border);

    }
    public class AddHealth : Spell
    {
        public AddHealth() : base(2,false,true)
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
                if (border != 0)
                {
                    AddWithBorder(caster, aim, border);
                }
                else
                {
                    AddFull(caster, aim);
                }
            }
        }
        private void AddWithBorder(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border)
        {
            if (aim.CurrentHealth == aim.MaxHealth)
                Console.WriteLine(" Spell Add Health: Health of aim has max value");
            if (caster.CurrentMana < border * MinValueOfMana)
                Console.WriteLine(" Spell Add Health: You don't have enough mana");
            else
            {
                while (border == 0)
                {
                    caster.CurrentMana -= MinValueOfMana;
                    ++aim.CurrentHealth;
                    --border;
                }
                Console.WriteLine($" Spell Add Health: Already done \n" +
                                  $" Now Aim of spell has {aim.CurrentHealth} points of health \n" +
                                  $" Your stock of mana is {caster.CurrentMana} points out of {caster.MaxMana}");
            }
        }
        private void AddFull(CreateCharacter.Mage caster, CreateCharacter.Person aim)
        {
            if (aim.CurrentHealth == aim.MaxHealth)
                Console.WriteLine(" Spell Add Health: Health of aim has max value");

            else
            {
                while (aim.CurrentHealth <= aim.MaxHealth || caster.CurrentMana >= MinValueOfMana)
                {
                    caster.CurrentMana -= MinValueOfMana;
                    ++aim.CurrentHealth;
                }
                Console.WriteLine($" Spell Add Health for full: Already done \n" +
                                  $" Now Aim of spell has {aim.CurrentHealth} points of health \n" +
                                  $" Your stock of mana is {caster.CurrentMana} points out of {caster.MaxMana}");
            }
        }
    }

    public class Cure : Spell
    {
        public Cure() : base(20, true, true)
        {

        }

        public override void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border)
        {
            if(caster.CurrentMana < MinValueOfMana)
            {
                Console.WriteLine("You don't have enough mana");
            }
            else
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
                        WriteMessage(Enums.State.Intoxicated);
                        break;
                    case Enums.State.Paralyzed:
                        WriteMessage(Enums.State.Paralyzed);
                        break;
                    case Enums.State.Ill:
                        {
                            aim.State = Enums.State.Healthy;
                            caster.CurrentMana -= MinValueOfMana;
                            Console.WriteLine($" Cure: Spell has successfully used\n" +
                                              $" The aim's state is {aim.State}\n" +
                                              $" Your stock of mana is {caster.CurrentMana} points out of {caster.MaxMana}");
                            break;
                        }
                    default:
                        Errors.Error33();
                        break;
                }
            }
        }
    }

    public class Antidote : Spell
    {
        public Antidote() : base(30,false,true)
        {

        }
        public override void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border)
        {
            if (caster.CurrentMana < MinValueOfMana)
            {
                Console.WriteLine("You don't have enough mana");
            }
            else
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
                            caster.CurrentMana -= MinValueOfMana;
                            Console.WriteLine($" Cure: Spell has successfully used\n" +
                                              $" The aim's state is {aim.State}\n" +
                                              $" Your stock of mana is {caster.CurrentMana} points out of {caster.MaxMana}");
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
    }

    public class Enliven : Spell
    {
        public Enliven() : base(150, true, true)
        {

        }
        public override void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border)
        {
            if (caster.CurrentMana < MinValueOfMana)
            {
                Console.WriteLine("You don't have enough mana");
            }
            else
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
                        {
                            aim.State = Enums.State.Healthy;
                            aim.CurrentHealth = 1;
                            caster.CurrentMana -= MinValueOfMana;
                            Console.WriteLine($" Enliven: Spell has successfully used\n" +
                                              $" The aim's state is {aim.State}\n" +
                                              $" Your stock of mana is {caster.CurrentMana} points out of {caster.MaxMana}");
                            break;
                        }
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
    }

    public class Unpetrify : Spell
    {
        public Unpetrify() : base(85, true, true)
        {

        }
        public override void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border)
        {
            if (caster.CurrentMana < MinValueOfMana)
            {
                Console.WriteLine("You don't have enough mana");
            }
            else
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
                        WriteMessage(Enums.State.Intoxicated);
                        break;
                    case Enums.State.Paralyzed:
                        {
                            aim.State = Enums.State.Healthy;
                            aim.CurrentHealth = 1;
                            caster.CurrentMana -= MinValueOfMana;
                            Console.WriteLine($" Unpetrify: Spell has successfully used\n" +
                                              $" The aim's state is {aim.State}\n" +
                                              $" Your stock of mana is {caster.CurrentMana} points out of {caster.MaxMana}");
                            break;
                        }
                    case Enums.State.Ill:
                        WriteMessage(Enums.State.Ill);
                        break;
                    default:
                        Errors.Error33();
                        break;
                }
            }
        }
    }
}
