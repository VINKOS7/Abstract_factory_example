using System;
using System.Collections.Generic;

namespace FabricPattern
{

    public static class AbstractFactory//статичный класс в c# и есть синглтон, приватный конструктор не нужен
    {
        private class a : b
        {

        }

        private class b
        {

        }

        internal interface IAbstractFactory//интерфейс фабрик
        {
            ICreature CreatureCreate(string name, int hp);

            IInanimate InanimateCreate(string name, int hp);

        }

        //фабрики кон
        // Каждая конкретная фабрика имеет соответствующую вариацию продукта.
        internal class FactoryLand : IAbstractFactory
        {
            public ICreature CreatureCreate(string name, int hp)
            {
                return new AnimalLand(name, hp);
            }

            public IInanimate InanimateCreate(string name, int hp)
            {
                return new InanimatLand(name, hp);         
            }
        }

        
        internal class FactoryUnderwate : IAbstractFactory
        {
            public ICreature CreatureCreate(string name, int hp)
            {
                return new AnimalUnderwater(name, hp);
            }

            public IInanimate InanimateCreate(string name, int hp)
            {
                return new InanimatUnderwater(name, hp);
            }
        }
        //фабрики кон

        internal interface IInanimate//интерфейс не живых объектов ну и дочерние их классы
        {
            string Name { get; set; }
            int HP { get; set; }
        }

        class InanimatLand : IInanimate
        {
            public string Name { get; set; }
            public int HP { get; set; }

            public InanimatLand(string _Name, int _HP)
            {
                Name = _Name;
                HP = _HP;
            }
        }
        class InanimatUnderwater : IInanimate
        {
            public string Name { get; set; }
            public int HP { get; set; }

            public InanimatUnderwater(string _Name, int _HP)
            {
                Name = _Name;
                HP = _HP;
            }
        }

        internal interface ICreature//интерфейс живых объектовну и дочерние их классы
        {
            string Name { get; set; }
            int HP { get; set; }
            void Move();
            string Take(IInanimate obj);
            void Attack();
            string Attack(ICreature obj);
        }

        class AnimalLand : ICreature
        {
            public string Name { get; set; }
            public int HP { get; set; }

            public int qTimeUnderwater { get; set; }

            public AnimalLand(string _Name, int _HP)
            {
                Name = _Name;
                HP = _HP;
            }
            public string Take(IInanimate obj)
            {
                if (obj is InanimatLand) return "предмет " + obj.Name + " взял " + Name;

                return "предмет " + obj.Name + " нельзя взять сухопутному " + Name;
            }

            public virtual void Move()
            {
                Console.WriteLine(Name + " Идет");
            }
            public virtual void Attack()
            {
                Console.WriteLine(Name + " бьет ");
            }

            public virtual string Attack(ICreature obj)
            {
                if (obj.GetType().Equals(this.GetType())) return Name + " бьет " + obj.Name;

                return "наземного " + Name + ", не может ударить " + obj.Name;
            }
        }

        class AnimalUnderwater : ICreature
        {
            public string Name { get; set; }
            public int HP { get; set; }

            public int qTimeTerrestrial { get; set; }

            public AnimalUnderwater(string _Name, int _HP)
            {
                Name = _Name;
                HP = _HP;
            }

            public string Take(IInanimate obj)
            {
                if (obj is InanimatUnderwater) return "предмет " + obj.Name + " взял " + Name;

                return "предмет " + obj.Name + " нельзя взять подводному " + Name;
            }

            public virtual void Move()
            {
                Console.WriteLine(Name + " Плывет ");
            }
            public virtual void Attack()
            {
                Console.WriteLine(Name + " cгрыз");
            }
            public virtual string Attack(ICreature obj)
            {
                if (obj.GetType().Equals(this.GetType())) return Name + " сгрыз " + obj.Name;

                return "подводного " + obj.Name + ", не может ударить " + Name;
            }
        }
    }

 

    class Program
    {
        static void Main(string[] args)
        {
            AbstractFactory.FactoryLand Forest = new();
            AbstractFactory.FactoryUnderwate Sea = new();

            var Cat = Forest.CreatureCreate("кот Барсик", 10);
            var Dog = Forest.CreatureCreate("пес Шарик", 10);

            var Stone = Forest.InanimateCreate("камень", 5);

            var Shark = Sea.CreatureCreate("акула Зубастик", 10);
            var Scat = Sea.CreatureCreate("ската Шокер", 10);

            var StoneWater = Sea.InanimateCreate("подводный камень", 5);       

            Console.WriteLine(Cat.Attack(Dog));
            Console.WriteLine(Shark.Attack(Scat));
            Console.WriteLine(Cat.Attack(Shark));

            Console.WriteLine(Cat.Take(Stone));
            Console.WriteLine(Cat.Take(StoneWater));
            Console.WriteLine(Shark.Take(StoneWater));
            Console.WriteLine(Shark.Take(Stone));
        }
    }
}
