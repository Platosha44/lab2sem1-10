using System;
using System.Collections.Generic;
using System.Text;

namespace _19_lab_role_game
{
    public class CreateCharacter
    {
        public class Race
        {
            public Enums.Races Label { get; set; }
            public int MaxHealth { get; set; }
            public int MaxAge { get; set; }
            public int Armor { get; set; }

            public Race(Enums.Races label, int mHeath, int mAge, int armor)
            {
                Label = label;
                MaxHealth = mHeath;
                MaxAge = mAge;
                Armor = armor;
            }
        }

        public class Person : IComparable
        {
            private static int ID = 0;
            
            private List<Artifact> inventory;
            private string name;
            private Enums.State state;
            private bool abilitySpeak;
            private bool abilityMove;
            private Enums.Races raceLabel;
            private Race race;
            private Enums.Sex sex;
            private int age;
            private int currentHealth;
            private double experience;

            public int Id { get; set; }
            public List<Artifact> Inventory { get => inventory; set => inventory = value;}
           
            public string Name
            {
                get => name;
                set
                {
                    if (Utils.AllOf(value.Split(""), (item => (item.ToString().Length) != 0)))
                    {
                        throw new Exception("Void name");
                    }
                    name = value;
                }
            }
            public Enums.State State
            {
                get => state;
                set
                {
                    state = value;
                }
            }
            public bool AbilitySpeak
            {
                get => abilitySpeak;
                set
                {
                    abilitySpeak = value;
                }
            }
            public bool AbilityMove
            {
                get => abilityMove;

                set
                {
                    abilityMove = value;
                }
            }
            public Enums.Races RaceLabel
            {
                get => raceLabel;
                set
                {
                    raceLabel = value;
                }
            }
            public Enums.Sex Sex
            {
                get => sex;
                set
                {

                    sex = value;
                }
            }
            public Race Race
            {
                get => race;
                set
                {
                    switch (RaceLabel)
                    {
                        case Enums.Races.Human:
                            {
                                value = new Race(RaceLabel, 100, 100,20);
                                break;
                            }
                        case Enums.Races.Elf:
                            {
                                value = new Race(RaceLabel, 80, 800,10);
                                break;
                            }
                        case Enums.Races.Orc:
                            {
                                value = new Race(RaceLabel, 200, 400,50);
                                break;
                            }
                        case Enums.Races.Goblin:
                            {
                                value = new Race(RaceLabel, 150, 350,30);
                                break;
                            }
                        case Enums.Races.Gnome:
                            {
                                value = new Race(RaceLabel, 180, 600,40);
                                break;
                            }
                        default:
                            {
                                Errors.Error33();
                                break;
                            }
                    }
                    race = value;
                }
            }
            public int Age
            {
                get
                {
                    return age;
                }
                set
                {
                    if (value < 0 || value >= Race.MaxAge)
                    {
                        Errors.Error666(value, Race.MaxAge, State);
                    }
                    else
                    {
                        age = value;
                    }
                }
            }
            public double Experience
            {
                get => experience;
                set
                {
                    if (value < 0)
                    {
                        experience = 0;
                    }
                    else
                    {
                        experience = value;
                    }
                }
            }
            public int CurrentHealth
            {
                get => currentHealth;
                set
                {
                    if (value < MaxHealth / 10 && State == Enums.State.Healthy) 
                    {
                        State = Enums.State.Weakened;
                    }
                    if (value >= MaxHealth / 10 && State == Enums.State.Weakened)
                    {
                        State = Enums.State.Healthy;
                    }
                    if (value <= 0)
                    {
                        value = 0;
                        State = Enums.State.Dead;
                    }
                    if (value > MaxHealth)
                    {
                        currentHealth = MaxHealth;
                    }
                    else
                    {
                        currentHealth = value;
                    }
                }
            }
            public int MaxAge { get => Race.MaxAge; }
            public int MaxHealth { get => Race.MaxHealth; }
            public int Armor { get => Race.Armor; set => Race.Armor = value < 0 ? 0 : value; }

            public Person(string name, Enums.Races race, Enums.Sex sex, int age)
            {
                Id = ++ID;
                Name = name;
                RaceLabel = race;
                Sex = sex;
                Race = default(Race);
                Age = age;
                Experience = 0;
                CurrentHealth = MaxHealth;
                AbilityMove = true;
                AbilitySpeak = true;
                Inventory = new List<Artifact>();
            }

            public Person()
            {

            }

            public void UseArtifact(Artifact artifact, Enums.KindsOfFunctions kind, Mage caster, Person aim, int border, string label)
            {
                if (Inventory.Contains(artifact))
                {
                    switch (kind)
                    {
                        case Enums.KindsOfFunctions.ForAim:
                            artifact.performMagicEffect(caster, aim, border);
                            break;
                        case Enums.KindsOfFunctions.ForAimWithBoard:
                            artifact.performMagicEffect(caster, aim, border);
                            break;
                        case Enums.KindsOfFunctions.ForYourSelf:
                            artifact.performMagicEffect(caster, caster, border);
                            break;
                        case Enums.KindsOfFunctions.ForYourSelfWithBoard:
                            artifact.performMagicEffect(caster, caster, border);
                            break;
                        default:
                            Errors.Error33();
                            break;
                    }
                    if (!artifact.Renewable || artifact.Power == 0)
                    {
                        Inventory.Remove(artifact);
                    }
                }
                else
                {
                    Console.WriteLine($" You don't have the artifact called {label}");
                }
            }

            public void PickUpArtifact(Artifact artifact, string label)
            {
                Inventory.Add(artifact);
                Console.WriteLine($" Artifact with name {label} has picked up");
            }

            public void GiveArtifactTo(Person aim, Artifact artifact, string label)
            {
                aim.Inventory.Add(artifact);
                Inventory.Remove(artifact);
                Console.WriteLine($" artifact with name {label} given");
            }

            public void ThrowOutArtifact(Artifact artifact, string label)
            {
                Inventory.Remove(artifact);
                Console.WriteLine($" Artifact with name {label} has thrown out");
            }

            public override string ToString()
            {
                return $" Name: {Name}\n" +
                    $" Age: {Age}\n" +
                    $" Sex: {Sex}\n" +
                    $" Race: {RaceLabel}\n" +
                    $" State: {State}\n" +
                    $" Current health: {CurrentHealth}\n" +
                    $" Max health: {MaxHealth}\n" +
                    $" Max age: {MaxAge}\n" +
                    $" Ability to speak: {AbilitySpeak}\n" +
                    $" Ability to move: {AbilityMove}\n" +
                    $" Experience: {Experience}";
            }
            public int CompareTo(object obj)
            {
                return Experience.CompareTo(((Person)obj).Experience);
            }
        }

        public class Mage : Person
        {
            private int currentMana;
            public int CurrentMana
            {
                get => currentMana;
                set
                {
                    if (currentMana < 0)
                    {
                        currentMana = 0;
                    }
                    if (currentMana > MaxMana)
                    {
                        currentMana = MaxMana;
                    }
                    currentMana = value;
                }
            }
            public List<Spell> Spells { get; set; }
            public int MaxMana
            {
                get => (int)Experience == 0 ? 100 : (int)Experience * 100;
            }

            public Mage(string name, Enums.Races race, Enums.Sex sex, int age) : base(name, race, sex, age)
            {
                CurrentMana = MaxMana;
                Spells = new List<Spell>();
            }

            public Mage()
            {

            }

            public void LearnSpell(Spell spell, string label)
            {
                Spells.Add(spell);
                Console.WriteLine($" {label} has just learned");
            }

            public void ForgetSpell(Spell spell, string label)
            {
                Spells.Remove(spell);
                Console.WriteLine($" {label} has just forgotten");
            }

            public void UseSpell(Spell spell, Enums.KindsOfFunctions kind, Mage caster, Person aim, int border, string label)
            {
                if (Spells.Contains(spell))
                {
                    switch (kind)
                    {
                        case Enums.KindsOfFunctions.ForAim:
                            spell.performMagicEffect(caster, aim, 0);
                            break;
                        case Enums.KindsOfFunctions.ForAimWithBoard:
                            spell.performMagicEffect(caster, aim, border);
                            break;
                        case Enums.KindsOfFunctions.ForYourSelf:
                            spell.performMagicEffect(caster, caster, 0);
                            break;
                        case Enums.KindsOfFunctions.ForYourSelfWithBoard:
                            spell.performMagicEffect(caster, caster, border);
                            break;
                        default:
                            Errors.Error33();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($" you don't have a {label} in list of your spells");
                }
            }

            public override string ToString()
            {
                return $"{base.ToString()}\n" +
                       $" Current manapull: {CurrentMana}\n" +
                       $" Max manapull: {MaxMana}";
            }
        }
    }
}
