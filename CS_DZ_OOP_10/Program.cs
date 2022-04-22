﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_DZ_OOP_10
{
    class Program
    {
        static void Main(string[] args)
        {
            War war = new War();
            List<Army> firstArmy = new List<Army>();
            List<Army> secondArmy = new List<Army>();
            firstArmy.Add(new Sniper("Снайпер", "Шанс 50% нанести двойной урон", 200, 100, 70));
            firstArmy.Add(new Shooter("Стрелок", "Двойной урон", 400, 80, 80));
            secondArmy.Add(new Tank("Танк", "Толстая броня увеличена на 50", 1000, 100, 50));
            secondArmy.Add(new Helicopter("Вертолет", "Залп 20 ракет", 50, 10, 60));
            war.Fight(firstArmy, secondArmy);
        }
    }

    class War
    {
        public void Fight(List<Army> firstArmy, List<Army> secondArmy)
        {
            Console.WriteLine("Это война!");
            Console.WriteLine("У первой страны есть");
            ShowUnit(firstArmy);
            Console.WriteLine("А у второй страны");
            ShowUnit(secondArmy);
            Console.ReadKey();

            while(firstArmy.Count > 0 && secondArmy.Count > 0)
            {
                for (int i = 0; i < firstArmy.Count - 1 || i < secondArmy.Count - 1; i++)
                {
                    secondArmy[i].TakeDamage(firstArmy[i].DoDamage(firstArmy[i].Damage), firstArmy[i].UpArmor(firstArmy[i].Armor));
                    if(firstArmy.Count > 1)
                    {
                        secondArmy[i].TakeDamage(firstArmy[i + 1].DoDamage(firstArmy[i + 1].Damage), firstArmy[i + 1].UpArmor(firstArmy[i + 1].Armor));
                    }
                    firstArmy[i].TakeDamage(secondArmy[i].DoDamage(secondArmy[i].Damage), secondArmy[i].UpArmor(secondArmy[i].Armor));
                    if(secondArmy.Count > 1)
                    {
                        firstArmy[i].TakeDamage(secondArmy[i + 1].DoDamage(secondArmy[i + 1].Damage), secondArmy[i + 1].UpArmor(secondArmy[i + 1].Armor));
                    }
                    CheckHealth(firstArmy);
                    CheckHealth(secondArmy);
                }
                Console.WriteLine();
                Console.WriteLine("После атаки у первой армии остались:");
                ShowWarInfo(firstArmy);
                Console.WriteLine("А у второй армии остались:");
                ShowWarInfo(secondArmy);
                Console.ReadLine();
            }
            if(firstArmy.Count == 0 && secondArmy.Count == 0)
            {
                Console.WriteLine("Обе армии потерпели поражение!");
            }
            else if(firstArmy.Count > 0)
            {
                Console.WriteLine("Первая армия одержала победу!");
            }
            else if(secondArmy.Count > 0)
            {
                Console.WriteLine("Вторая армия выиграла эту войну!");
            }
        }

        private void ShowWarInfo(List<Army> units)
        {
            foreach (var unit in units)
            {
                Console.WriteLine(unit.Name + " с жизнями " + unit.Health);
            }
        }

        private void CheckHealth(List<Army> units)
        {
            foreach (var unit in units)
            {
                if(unit.Health <= 0)
                {
                    units.Remove(unit);
                    break;
                }
            }
        }

        private void ShowUnit(List<Army> units)
        {
            foreach (var unit in units)
            {
                unit.SowInfo();
            }
        }
    }

    class Unit
    {
        public string UniqueCharacteristic { get; private set; }

        public int Armor { get; private set; }

        public int Damage { get; private set; }

        public string Name { get; private set; }
        
        public int Health { get; private set; }

        public Unit(string name, string specialDamage, int health, int damage, int armor)
        {
            Name = name;
            UniqueCharacteristic = specialDamage;
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public void SowInfo()
        {
            Console.WriteLine(Name + " его уникальная характеристика - " + UniqueCharacteristic + " его жизни " + Health + " его урон " + Damage + " его броня " + Armor);
        }

        public virtual int DoDamage(int damage)
        {
            return Damage;
        }

        public virtual int UpArmor(int armor)
        {
            return Armor;
        }

        public void TakeDamage(int damage, int armor)
        {
            if(damage > Armor)
            {
                Health -= damage - Armor;
            }
            else
            {
                Console.WriteLine("Броня не пробита!");
            }
        }
    }

    class Army : Unit
    {
        public Army(string name, string specialDamade, int health, int damage, int armor) : base(name, specialDamade, health, damage, armor) { }
    }

    class Sniper : Army
    {
        public Sniper(string name, string specialDamade, int health, int damage, int armor) : base(name, specialDamade, health, damage, armor) { }

        public override int DoDamage(int damage)
        {
            Random random = new Random();
            if(random.Next(0,2) == 1)
            {
                damage = Damage * 2;
            }
            else
            {
                damage = Damage;
            }
            return damage;
        }
    }

    class Shooter : Army
    {
        public Shooter(string name, string specialDamade, int health, int damage, int armor) : base(name, specialDamade, health, damage, armor) { }

        public override int DoDamage(int damage)
        {
            damage = Damage * 2;
            return damage;
        }
    }

    class Tank : Army
    {
        public Tank(string name, string specialDamade, int health, int damage, int armor) : base(name, specialDamade, health, damage, armor) { }

        public override int UpArmor(int armor)
        {
            armor = Armor + 50;
            return armor;
        }
    }

    class Helicopter : Army
    {
        public Helicopter(string name, string specialDamade, int health, int damage, int armor) : base(name, specialDamade, health, damage, armor) { }

        public override int DoDamage(int damage)
        {
            damage = Damage * 20;
            return damage;
        }
    }
}