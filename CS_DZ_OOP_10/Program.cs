using System;
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
            Army armyFirst = new Army(new List<Unit>());
            Army secondArmy = new Army(new List<Unit>());
            int takesCount = 0;
            armyFirst.TakeUnits(ref takesCount);
            secondArmy.TakeUnits(ref takesCount);
            war.Fight(armyFirst, secondArmy);
        }
    }

    class War
    {
        public void Fight(Army firstArmy, Army secondArmy)
        {
            Console.WriteLine("Это война!");
            Console.WriteLine("У первой страны есть");
            firstArmy.ShowUnits();
            Console.WriteLine("А у второй страны");
            secondArmy.ShowUnits();
            Console.ReadKey();
            int unitsCount = 0;
            while(firstArmy.GetCount(unitsCount) > 0 && secondArmy.GetCount(unitsCount) > 0)
            {
                secondArmy.TakeDamage(firstArmy.Dodamage(), firstArmy.GetArmor());
                firstArmy.TakeDamage(secondArmy.Dodamage(), secondArmy.GetArmor());
                firstArmy.CheckHealth();
                secondArmy.CheckHealth();
                Console.WriteLine();
                Console.WriteLine("После атаки у первой армии остались:");
                firstArmy.ShowUnits();
                Console.WriteLine("А у второй армии остались:");
                secondArmy.ShowUnits();
                Console.ReadLine();
            }
            if(firstArmy.GetCount(unitsCount) == 0 && secondArmy.GetCount(unitsCount) == 0)
            {
                Console.WriteLine("Обе армии потерпели поражение!");
            }
            else if(firstArmy.GetCount(unitsCount) > 0)
            {
                Console.WriteLine("Первая армия одержала победу!");
            }
            else if(secondArmy.GetCount(unitsCount) > 0)
            {
                Console.WriteLine("Вторая армия выиграла эту войну!");
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
            Console.WriteLine();
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
                Health -= damage - armor;
            }
            else
            {
                Console.WriteLine("Броня не пробита!");
            }
        }
    }

    class Army
    {
        private List<Unit> _units;

        public Army(List<Unit> units)
        {
            _units = units;
        }

        public void TakeUnits(ref int takesCount)
        {
            if (takesCount == 0)
            {
                takesCount++;
                _units = new List<Unit>() { new Sniper("Снайпер", "Шанс 50% нанести двойной урон", 200, 100, 70), new Shooter("Стрелок", "Двойной урон", 400, 80, 80) };
            }
            else
            {
               _units = new List<Unit>() { new Tank("Танк", "Толстая броня увеличена на 50", 1000, 100, 50), new Helicopter("Вертолет", "Залп 20 ракет", 50, 10, 40) };
            }
        }

        public void ShowUnits()
        {
            foreach (var unit in _units)
            {
                Console.WriteLine(unit.Name + " его уникальная характеристика - " + unit.UniqueCharacteristic + " его жизни " + unit.Health + " его урон " + unit.Damage + " его броня " + unit.Armor);
            }
        }

        public int GetCount(int unitsCount)
        {
            unitsCount = _units.Count;
            return unitsCount;
        }

        public void TakeDamage(int damage, int armor)
        {
            foreach (var unit in _units)
            {
                unit.TakeDamage(damage, armor);
            }
        }

        public int Dodamage()
        {
            int damage = 0;
            foreach (var unit in _units)
            {
                damage = unit.DoDamage(unit.Damage);
            }
            return damage;
        }

        public int GetArmor()
        {
            int armor = 0;
            foreach (var unit in _units)
            {
                armor = unit.UpArmor(unit.Armor);
            }
            return armor;
        }

        public void CheckHealth()
        {
            foreach (var unit in _units)
            {
                if (unit.Health <= 0)
                {
                    _units.Remove(unit);
                    break;
                }
            }
        }

    }

    class Sniper : Unit
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

    class Shooter : Unit
    {
        public Shooter(string name, string specialDamade, int health, int damage, int armor) : base(name, specialDamade, health, damage, armor) { }

        public override int DoDamage(int damage)
        {
            damage = Damage * 2;
            return damage;
        }
    }

    class Tank : Unit
    {
        public Tank(string name, string specialDamade, int health, int damage, int armor) : base(name, specialDamade, health, damage, armor) { }

        public override int UpArmor(int armor)
        {
            armor = Armor + 50;
            return armor;
        }
    }

    class Helicopter : Unit
    {
        public Helicopter(string name, string specialDamade, int health, int damage, int armor) : base(name, specialDamade, health, damage, armor) { }

        public override int DoDamage(int damage)
        {
            damage = Damage * 20;
            return damage;
        }
    }
}
