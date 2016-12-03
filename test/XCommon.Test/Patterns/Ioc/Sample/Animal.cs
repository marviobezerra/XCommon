using System;

namespace XCommon.Test.Patterns.Ioc.Sample
{
    public interface IAnimal
    {
        string Name { get; set; }

        int Height { get; set; }

        bool Male { get; set; }

        DateTime BirthDate { get; set; }

    }

    public class AnimalCat : IAnimal
    {
        public string Name { get; set; }

        public int Height { get; set; }

        public bool Male { get; set; }

        public DateTime BirthDate { get; set; }

    }

    public class AnimalDog : IAnimal
    {
        public AnimalDog(int height)
        {
            Height = height;
        }

        public string Name { get; set; }

        public int Height { get; set; }

        public bool Male { get; set; }

        public DateTime BirthDate { get; set; }
    }

    public class AnimalDuck : IAnimal
    {
        public AnimalDuck(int height, bool male)
        {
            Height = height;
            Male = male;
        }

        public AnimalDuck(int height, bool male, DateTime birthDate)
        {
            Height = height;
            Male = male;
            BirthDate = birthDate;
        }

        public int Height { get; set; }

        public bool Male { get; set; }

        public DateTime BirthDate { get; set; }

        public string Name { get; set; }
    }

    public class AnimalBird : IAnimal
    {
        public AnimalBird(string name, int height, bool male, DateTime birthDate)
        {
            Name = name;
            Height = height;
            Male = male;
            BirthDate = birthDate;
        }

        public string Name { get; set; }

        public int Height { get; set; }

        public bool Male { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
