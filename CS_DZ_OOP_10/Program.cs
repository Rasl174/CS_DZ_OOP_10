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
            war.Fight();
        }
    }

    class War
    {
        private List<Army> _firstArmy = new List<Army>() { new Sniper("Снайпер", "Шанс 50% нанести двойной урон", 200, 100, 70), new Shooter("Стрелок", "Двойной урон", 400, 80, 80) };
        private List<Army> _seondArmy = new List<Army>() { new Tank("Танк", "Толстая броня увеличена на 50", 1000, 100, 50), new Helicopter("Вертолет", "Залп 20 ракет", 50, 10, 60) };

        public void Fight()
        {
            Console.WriteLine("Это война!");
            Console.WriteLine("У первой страны есть");
            ShowUnit(_firstArmy);
            Console.WriteLine("А у второй страны");
            ShowUnit(_seondArmy);
            Console.ReadKey();

            while(_firstArmy.Count > 0 && _seondArmy.Count > 0)
            {
                for (int i = 0; i < _firstArmy.Count - 1 || i < _seondArmy.Count - 1; i++)
                {
                    _seondArmy[i].TakeDamage(_firstArmy[i].DoDamage(_firstArmy[i].Damage), _firstArmy[i].UpArmor(_firstArmy[i].Armor));
                    if(_firstArmy.Count > 1)
                    {
                        _seondArmy[i].TakeDamage(_firstArmy[i + 1].DoDamage(_firstArmy[i + 1].Damage), _firstArmy[i + 1].UpArmor(_firstArmy[i + 1].Armor));
                    }
                    _firstArmy[i].TakeDamage(_seondArmy[i].DoDamage(_seondArmy[i].Damage), _seondArmy[i].UpArmor(_seondArmy[i].Armor));
                    if(_seondArmy.Count > 1)
                    {
                        _firstArmy[i].TakeDamage(_seondArmy[i + 1].DoDamage(_seondArmy[i + 1].Damage), _seondArmy[i + 1].UpArmor(_seondArmy[i + 1].Armor));
                    }
                    CheckHealth(_firstArmy);
                    CheckHealth(_seondArmy);
                }
                Console.WriteLine();
                Console.WriteLine("После атаки у первой армии остались:");
                ShowWarInfo(_firstArmy);
                Console.WriteLine("А у второй армии остались:");
                ShowWarInfo(_seondArmy);
                Console.ReadLine();
            }
            if(_firstArmy.Count == 0 && _seondArmy.Count == 0)
            {
                Console.WriteLine("Обе армии потерпели поражение!");
            }
            else if(_firstArmy.Count > 0)
            {
                Console.WriteLine("Первая армия одержала победу!");
            }
            else if(_seondArmy.Count > 0)
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
