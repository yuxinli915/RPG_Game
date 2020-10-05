using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuxinLi_OOP_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            var maxPower = int.Parse(ConfigurationManager.AppSettings.Get("MaxPower"));
            var maxHealth = int.Parse(ConfigurationManager.AppSettings.Get("MaxHealth"));
            var HeroOriginalHealth = int.Parse(ConfigurationManager.AppSettings.Get("HeroOriginalHealth"));

            Game myGame = new Game(maxPower, maxHealth);
            myGame.Start(HeroOriginalHealth, maxPower);
            Console.ReadKey();
        }
    }

    class Hero
    {
        public string Name { get; set; }

        public int Strength { get; set; }

        public int Defense { get; set; }

        public int OriginalHealth { get; set; }

        public int CurrentHealth { get; set; }

        public int Win { get; set; }

        public int Lose { get; set; }

        public Weapon EquippedWeapon { get; set; }

        public Armor EquippedArmor { get; set; }

        public List<Armor> ArmorBag { get; set; }

        public List<Weapon> WeaponBag { get; set; }

        public Hero(string Name, int OriginalHealth, int MaxStrength, int MaxDefense, Random Factor)
        {
            this.Name = Name;
            this.OriginalHealth = OriginalHealth;
            Strength = Factor.Next(MaxStrength / 2, MaxStrength);
            Defense = 0;
            ArmorBag = new List<Armor>();
            WeaponBag = new List<Weapon>();
            CurrentHealth = OriginalHealth;

        }

        public void ShowStats()
        {
            Console.WriteLine(this);
        }

        public void ShowInventory()
        {
            if (WeaponBag.Count > 0)
            {
                foreach (var weapon in WeaponBag)
                {
                    if (weapon.Equipped == true)
                    {
                        Console.Write("(Equipped) ");
                    }
                    Console.WriteLine($"{weapon}\n");
                }
            }
            else
            {
                Console.WriteLine("You have no weapon in your bag.\n");
            }

            if (ArmorBag.Count > 0)
            {
                foreach (var armor in ArmorBag)
                {
                    if (armor.Equipped == true)
                    {
                        Console.Write("(Equipped) ");
                    }
                    Console.WriteLine($"{armor}\n");
                }
            }
            else
            {
                Console.WriteLine("You have no armor in your bag.");
            }
        }

        public void EquipWeapon()
        {
            var exit = false;

            while (!exit)
            {
                Console.WriteLine("Enter number to choose your weapon: \n");

                for (int i = 0; i < WeaponBag.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {WeaponBag[i]}");
                }

                var selection = Console.ReadKey(true).KeyChar;

                if (Char.IsDigit(selection) && ((int)Char.GetNumericValue(selection) > 0 && (int)Char.GetNumericValue(selection) <= WeaponBag.Count))
                {
                    var index = (int)Char.GetNumericValue(selection) - 1;

                    if (EquippedWeapon != null)
                    {
                        EquippedWeapon.Equipped = false;
                    }

                    WeaponBag[index].Equipped = true;
                    EquippedWeapon = WeaponBag[index];
                    exit = true;

                    Console.WriteLine($"\nYou equipped the {WeaponBag[index].Name}.\n");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("Wrong selection. Please choose again!");
                    Console.ReadKey(true);
                }
                Console.Clear();
            }
        }

        public void EquipArmor()
        {
            var exit = false;

            while (!exit)
            {
                Console.WriteLine("Enter number to choose your armor: \n");

                for (int i = 0; i < ArmorBag.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ArmorBag[i]}");
                }

                var selection = Console.ReadKey(true).KeyChar;

                if (Char.IsDigit(selection) && ((int)Char.GetNumericValue(selection) > 0 && (int)Char.GetNumericValue(selection) <= ArmorBag.Count))
                {
                    var index = (int)Char.GetNumericValue(selection) - 1;

                    if (EquippedArmor != null)
                    {
                        EquippedArmor.Equipped = false;
                    }

                    ArmorBag[index].Equipped = true;
                    EquippedArmor = ArmorBag[index];
                    exit = true;

                    Console.WriteLine($"\nYou equipped the {ArmorBag[index].Name}.\n");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("Wrong selection. Please choose again!");
                    Console.ReadKey(true);
                }
                Console.Clear();
            }
        }

        public override string ToString()
        {
            return $"Name: {Name}\nStrength: {Strength + (EquippedWeapon == null ? 0 : EquippedWeapon.Power)}\nDefense: {Defense + (EquippedArmor == null ? 0 : EquippedArmor.Power)}\nHP: {CurrentHealth}/{OriginalHealth}\nWin: {Win}\nLose: {Lose}";
        }
    }

    class Monster
    {
        public string Name { get; set; }

        public int Strength { get; set; }

        public int Defense { get; set; }

        public int OriginalHealth { get; set; }

        public int CurrentHealth { get; set; }

        public Monster(string Name, int OriginalHealth, int Strength, int Defense)
        {
            this.Name = Name;
            this.Strength = Strength;
            this.Defense = Defense;
            this.OriginalHealth = OriginalHealth;
            CurrentHealth = OriginalHealth;
        }

        public override string ToString()
        {
            return $"Name: {Name} Strength: {Strength} Defense: {Defense} HP: {OriginalHealth}/{CurrentHealth}";
        }
    }

    class Weapon
    {
        public string Name { get; set; }

        public int Power { get; set; }

        public bool Equipped { get; set; }

        public Weapon(string Name, int Power)
        {
            this.Name = Name;
            this.Power = Power;
            Equipped = false;
        }

        public override string ToString()
        {
            return $"Name: {Name}     Strength: +{Power}";
        }
    }

    class Armor
    {
        public string Name { get; set; }

        public int Power { get; set; }

        public bool Equipped { get; set; }

        public Armor(string Name, int Power)
        {
            this.Name = Name;
            this.Power = Power;
            Equipped = false;
        }

        public override string ToString()
        {
            return $"Name: {Name}     Defense: +{Power}";
        }
    }
    enum Side
    {
        Hero,
        Monster
    }

    class Fight
    {
        public Side Side = Side.Hero;

        public void Turn(Hero Hero, Monster Monster, Random Factor)
        {
            Console.WriteLine($"Here comes the {Monster.Name}.\nHP: {Monster.OriginalHealth}\nStrength: {Monster.Strength}\nDefense: {Monster.Defense}");

            while (Hero.CurrentHealth > 0 && Monster.CurrentHealth > 0)
            {
                double CriticalHit = 1;

                if (Factor.Next(100) < 25)
                {
                    CriticalHit = 1.5;
                }

                if (Side == Side.Hero)
                {
                    var damage = (Hero.Strength + (Hero.EquippedWeapon == null ? 0 : Hero.EquippedWeapon.Power)) * CriticalHit - Monster.Defense;

                    damage = damage < 0 ? 0 : damage;
                    Monster.CurrentHealth -= (int)damage;

                    Console.Write($"\nIt's your turn.");

                    if (CriticalHit == 1.5)
                    {
                        Console.Write(" You just made a critical hit. ");
                    }

                    Console.Write($"The {Monster.Name} received a damage of {damage}. ");

                    if (Monster.CurrentHealth <= 0)
                    {
                        Monster.CurrentHealth = 0;

                        Console.WriteLine($"Its current health is 0/{Monster.OriginalHealth}.\n");
                    }
                    else
                    {
                        Side = Side.Monster;

                        Console.WriteLine($"Its current health is {Monster.CurrentHealth}/{Monster.OriginalHealth}.\n");
                        Console.WriteLine("Press any key to go to next turn.");
                        Console.ReadKey(true);
                    }
                }
                else
                {
                    var damage = Monster.Strength * CriticalHit - (Hero.Defense + (Hero.EquippedArmor == null ? 0 : Hero.EquippedArmor.Power));

                    damage = damage < 0 ? 0 : damage;
                    Hero.CurrentHealth -= (int)damage;

                    Console.Write($"\nIt's {Monster.Name}'s turn.");

                    if (CriticalHit == 1.5)
                    {
                        Console.Write(" It just made a critical hit. ");
                    }

                    Console.Write($"You received a damage of {damage}. ");

                    if (Hero.CurrentHealth <= 0)
                    {
                        Hero.CurrentHealth = 0;

                        Console.WriteLine("Your current health is 0.\n");
                    }
                    else
                    {
                        Side = Side.Hero;

                        Console.WriteLine($"Your current health is {Hero.CurrentHealth}/{Hero.OriginalHealth}.\n");
                        Console.WriteLine("Press any key to go to next turn.");
                        Console.ReadKey(true);
                    }
                }
            }
        }
    }

    class Game
    {
        public List<Weapon> Weapons { get; set; }

        public List<Armor> Armors { get; set; }

        public List<Monster> Monsters { get; set; }

        public Random factor { get; set; }

        public Game(int maxPower, int maxHealth)
        {

            string[] WeaponNames = ConfigurationManager.AppSettings["WeaponNames"].Split(',');
            string[] ArmorNames = ConfigurationManager.AppSettings["ArmorNames"].Split(',');
            string[] MonsterNames = ConfigurationManager.AppSettings["MonsterNames"].Split(',');

            Weapons = new List<Weapon>();
            Armors = new List<Armor>();
            Monsters = new List<Monster>();
            factor = new Random();

            for (int i = 0; i < WeaponNames.Length; i++)
            {
                Weapons.Add(new Weapon(WeaponNames[i], factor.Next(1, maxPower)));
                Armors.Add(new Armor(ArmorNames[i], factor.Next(1, maxPower)));
                Monsters.Add(new Monster(MonsterNames[i], factor.Next(maxHealth / 2, maxHealth), factor.Next(maxPower / 2, maxPower), factor.Next(1, maxPower)));
            }
        }

        public void Start(int HeroOriginalHealth, int maxPower)
        {
            Poster();

            Console.WriteLine("Please enter your character name: ");

            var hero = new Hero(Console.ReadLine(), HeroOriginalHealth, maxPower, maxPower, factor);
            var selection = MenuSelection();
            var exit = false;

            Console.Clear();
            Console.WriteLine($"Welcome, {hero.Name}!");

            while (!exit)
            {
                Console.Clear();

                switch (selection)
                {
                    case '1':
                        Console.WriteLine(hero);

                        Continue();
                        selection = MenuSelection();
                        break;

                    case '2':
                        hero.ShowInventory();

                        if (hero.WeaponBag.Count > 0 || hero.ArmorBag.Count > 0)
                        {
                            Console.WriteLine("Do you want to equip item? [Y/N]");
                            selection = Console.ReadKey(true).KeyChar;

                        }

                        if (selection.ToString().ToLower() == "y")
                        {
                            Console.Clear();

                            if (hero.ArmorBag.Count == 0 && hero.WeaponBag.Count > 0)
                            {
                                hero.EquipWeapon();
                            }
                            else if (hero.WeaponBag.Count == 0 && hero.ArmorBag.Count > 0)
                            {
                                hero.EquipArmor();
                            }
                            else if (hero.WeaponBag.Count > 0 && hero.ArmorBag.Count > 0)
                            {
                                hero.EquipWeapon();
                                hero.EquipArmor();
                            }

                            hero.ShowInventory();
                        }

                        Continue();
                        selection = MenuSelection();
                        break;

                    case '3':
                        var fight = new Fight();
                        var mon = Monsters[factor.Next(Monsters.Count)];

                        fight.Turn(hero, mon, factor);

                        if (hero.CurrentHealth == 0)
                        {
                            hero.Lose++;
                            exit = true;

                            Console.WriteLine("You lose the fight. Here is your final statistics.");
                            Console.WriteLine(hero);
                            Continue();
                        }
                        else
                        {
                            hero.Win++;
                            Win(hero);

                            if (Monsters.Count == 1)
                            {
                                exit = true;

                                Console.WriteLine("You beat all the monsters.\nHere are your statistics.");
                                Console.WriteLine(hero);
                                Continue();
                            }
                            else
                            {
                                Continue();
                                selection = MenuSelection();
                            }

                            Monsters.Remove(mon);
                        }
                        break;

                    case '4':
                        Console.Clear();
                        Console.WriteLine("Your game is not finished yet, are you sure you want to exit? [Y/N]");

                        selection = Console.ReadKey(true).KeyChar;

                        if (selection.ToString().ToLower() == "n")
                        {
                            selection = MenuSelection();
                        }
                        else if (selection.ToString().ToLower() == "y")
                        {
                            exit = true;
                            Console.WriteLine("Good game well played.");
                        }
                        else
                        {
                            Console.WriteLine("What are you doing here? You are going back to menu.");
                            Console.ReadKey(true);
                            selection = MenuSelection();
                        }
                        break;
                }
            }
        }

        public void Continue()
        {
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        public void Win(Hero hero)
        {
            Console.WriteLine("\nYou won the fight. Here are your loots.\n");

            var lootWeapon = Weapons[factor.Next(Weapons.Count)];
            var lootArmor = Armors[factor.Next(Armors.Count)];

            Weapons.Remove(lootWeapon);
            Armors.Remove(lootArmor);
            hero.ArmorBag.Add(lootArmor);
            hero.WeaponBag.Add(lootWeapon);

            Console.WriteLine($"{lootWeapon}\n{lootArmor}\n");
        }

        public char MenuSelection()
        {
            Console.WriteLine($"Please choose one option in menu.\n1. Current statistics.\n2. Check inventory.\n3. Fight!\n4. Exit");

            var selection = Console.ReadKey().KeyChar;

            while (selection != '1' && selection != '2' && selection != '3' && selection != '4')
            {
                Console.Clear();
                Console.WriteLine("Invalid choice. Please enter again.\n1. Current statistics.\n2. Check inventory.\n3. Fight!\n4. Exit");

                selection = Console.ReadKey().KeyChar;
            }
            return selection;
        }

        public void Poster()
        {
            Console.SetWindowSize(120, 30);

            string title =
                @"
                           ____            __    ______      __                      __      
                          /\  _`\         /\ \__/\  _  \    /\ \                    /\ \__   
                          \ \ \/\ \    ___\ \ ,_\ \ \L\ \   \ \ \        ___     ___\ \ ,_\  
                           \ \ \ \ \  / __`\ \ \/\ \  __ \   \ \ \  __  / __`\  / __`\ \ \/  
                            \ \ \_\ \/\ \L\ \ \ \_\ \ \/\ \   \ \ \L\ \/\ \L\ \/\ \L\ \ \ \_ 
                             \ \____/\ \____/\ \__\\ \_\ \_\   \ \____/\ \____/\ \____/\ \__\
                              \/___/  \/___/  \/__/ \/_/\/_/    \/___/  \/___/  \/___/  \/__/
                                                                                             
                                                                                             
                ";
            Console.WriteLine(title);

            var text = new StringBuilder();
            string main = "Welcome to the DotA Loot.";
            string main1 = "Press any key to continue.";

            text.Append(new string(' ', (Console.WindowWidth - main.Length) / 2));
            text.Append(main);
            text.Append(new string(' ', (Console.WindowWidth - (main1.Length + main.Length) / 2)));
            text.Append(main1);

            Console.WriteLine(text.ToString());
            Console.ReadKey();
            Console.Clear();
        }
    }
}