using XCommon.Patterns.Ioc;
using Xunit;
using FluentAssertions;
using System;
using XCommon.Test.Patterns.Ioc.Sample;

namespace XCommon.Test.Patterns.Ioc
{
    public class KernelTest
    {
        public KernelTest()
        {
            Kernel.Reset();
        }

        [Fact(DisplayName = "Map (Simple, valid - interface)")]
        public void MapSimpleValidInterface()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalCat>();

            act.ShouldNotThrow("It is a valid map");
            Kernel.Count.Should().Be(1, "There is one class mapped");
        }

        [Fact(DisplayName = "Map (Simple, valid - abstract)")]
        public void MapSimpleValidAbstract()
        {
            Action act = () => Kernel.Map<Vehicle>().To<VehicleCar>();

            act.ShouldNotThrow("It is a valid map");
            Kernel.Count.Should().Be(1, "There is one class mapped");
        }

        [Fact(DisplayName = "Map (Simple, valid - class to class)")]
        public void MapSimpleValidClassToClass()
        {
            Action act = () => Kernel.Map<VehicleCar>().To<VehicleCar>();

            act.ShouldNotThrow("It is a valid map");
            Kernel.Count.Should().Be(1, "There is one class mapped");
        }

        [Fact(DisplayName = "Map (Simple, valid - function)")]
        public void MapSimpleValidFunction()
        {
            Func<VehicleCar> resolver = () => new VehicleCar();
            Action act = () => Kernel.Map<Vehicle>().To(resolver);

            act.ShouldNotThrow("It is a valid map");
            Kernel.Count.Should().Be(1, "There is one class mapped");
        }
        
        [Fact(DisplayName = "Map (Constructor params, valid - 01 param)")]
        public void MapConstructorParamsValid01Param()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalDog>(150);
            act.ShouldNotThrow("It is a valid map");
        }

        [Fact(DisplayName = "Map (Constructor params, valid - 02 params)")]
        public void MapConstructorParamsValid02Params()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalDuck>(150, true);
            act.ShouldNotThrow("It is a valid map");
        }

        [Fact(DisplayName = "Map (Constructor params, valid - 03 params)")]
        public void MapConstructorParamsValid03Params()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalDuck>(150, true, DateTime.Now);
            act.ShouldNotThrow("It is a valid map");
        }

        [Fact(DisplayName = "Map (Constructor params, invalid - with no params)")]
        public void MapConstructorParamsInvalidWithNoParams()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalDuck>();
            act.ShouldThrow<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Constructor params, invalid - invalid param type)")]
        public void MapConstructorParamsInvalidInvalidParamType()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalDuck>(150, "Fail", DateTime.Now);
            act.ShouldThrow<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Constructor params, invalid - invalid param count)")]
        public void MapConstructorParamsInvalidInvalidParamCount()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalDuck>(150, true, DateTime.Now, "Fail");
            act.ShouldThrow<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Simple, invalid - interface to interface)")]
        public void MapSimpleInvalidInterfaceToInterface()
        {
            Action act = () => Kernel.Map<IAnimal>().To<IAnimal>();
            act.ShouldThrow<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Simple, invalid - interface to abstract)")]
        public void MapSimpleInvalidInterfaceToAbstract()
        {
            Action act = () => Kernel.Map<Vehicle>().To<Vehicle>();
            act.ShouldThrow<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Simple, invalid - interface to a class that doesn't implement it)")]
        public void MapSimpleInvalidInterfaceToClassThatDoesImplement()
        {
            Action act = () => Kernel.Map<IAnimal>().To<VehicleCar>();
            act.ShouldThrow<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Simple, invalid - abstract to abstrac)")]
        public void MapSimpleInvalidAbstractToAbstrac()
        {
            Action act = () => Kernel.Map<Vehicle>().To<Vehicle>();
            act.ShouldThrow<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Simple, invalid - abstract to interface)")]
        public void MapSimpleInvalidAbstractToInterface()
        {
            Action act = () => Kernel.Map<IAnimal>().To<Vehicle>();
            act.ShouldThrow<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Simple, invalid - abstract to a class that doesn't implement)")]
        public void MapSimpleInvalidAbstractToClassThatDoesImplement()
        {
            Action act = () => Kernel.Map<Vehicle>().To<AnimalDog>();
            act.ShouldThrow<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Resolve (Generic, valid)")]
        public void ResolveGenericValid()
        {
            Kernel.Map<IAnimal>().To<AnimalCat>();

            IAnimal animal01 = Kernel.Resolve<IAnimal>();
            IAnimal animal02 = Kernel.Resolve<IAnimal>();

            animal01.Should().NotBeNull("There is a mapped class");
            animal01.Should().Be(animal02, "Both are the same instance");
        }

        [Fact(DisplayName = "Resolve (Generic, valid no cache)")]
        public void ResolveGenericValidNoCache()
        {
            Kernel.Map<IAnimal>().To<AnimalCat>();

            IAnimal animal01 = Kernel.Resolve<IAnimal>(false);
            IAnimal animal02 = Kernel.Resolve<IAnimal>(false);

            animal01.Should().NotBe(animal02, "Each class is a new instance");
        }

        [Fact(DisplayName = "Resolve (Generic, not mapped valid no force)")]
        public void ResolveGenericValidNoForce()
        {
            IAnimal animal01 = Kernel.Resolve<IAnimal>(true, false);

            animal01.Should().BeNull("There isn't a mapped class, but the Kernel doesn't force to resolve");
        }

        [Fact(DisplayName = "Resolve (Generic, not mapped valid no cache and no force)")]
        public void ResolveGenericValidNoForceAndNoForce()
        {
            IAnimal animal01 = Kernel.Resolve<IAnimal>(false, false);

            animal01.Should().BeNull("There isn't a mapped class, but the Kernel doesn't use cache and doesn't force to resolve");
        }

        [Fact(DisplayName = "Resolve (Generic, invalid - Unmaped type)")]
        public void ResolveGenericInvalidUnmapedType()
        {
            Action act = () => Kernel.Resolve<IAnimal>();
            act.ShouldThrow<Exception>("There isn't a class mapped for that interface");
        }

        [Fact(DisplayName = "Resolve (Object, valid)")]
        public void ResolveObjectValid()
        {
            Kernel.Map<AnimalCat>().To<AnimalCat>();
            AnimalCat animal = Kernel.Resolve<AnimalCat>();

            animal.Should().NotBeNull("There is one class mapped for that class");
        }

        [Fact(DisplayName = "Resolve (Object, valid - no cache)")]
        public void ResolveObjectValidNoCache()
        {
            Kernel.Map<AnimalCat>().To<AnimalCat>();

            AnimalCat animal01 = Kernel.Resolve<AnimalCat>(false);
            AnimalCat animal02 = Kernel.Resolve<AnimalCat>(false);

            animal01.Should().NotBe(animal02, "Each class is a new instance");
        }

        [Fact(DisplayName = "Resolve (Object, valid - Unmaped type no force)")]
        public void ResolveObjectValidUnmapedTypeNoForce()
        {
            AnimalCat animal = Kernel.Resolve<AnimalCat>(true, false);

            animal.Should().BeNull("There isn't a mapped class, but the kernel wasn't force to resolve");
        }

        [Fact(DisplayName = "Resolve (Object, invalid - Unmaped type force)")]
        public void ResolveObjectInvalidUnmapedTypeForce()
        {
            Action act = () => Kernel.Resolve<AnimalCat>();

            act.ShouldThrow<Exception>("There isn't a mapped class");
        }

        [Fact(DisplayName = "Resolve (Function, valid - cache)")]
        public void ResolveFunctionValidCache()
        {
            Func<IAnimal> resolver = () => new AnimalCat();
            Kernel.Map<IAnimal>().To(resolver);

            IAnimal animal01 = Kernel.Resolve<IAnimal>();
            IAnimal animal02 = Kernel.Resolve<IAnimal>();

            animal01.Should().Be(animal02, "Both classes are the same instance");
        }

        [Fact(DisplayName = "Resolve (Function, valid - no cache)")]
        public void ResolveFunctionValidNoCache()
        {
            int count = 0;
            Func<IAnimal> resolver = () => 
            {
                count++;
                return new AnimalCat();
            };

            Kernel.Map<IAnimal>().To(resolver);

            count.Should().Be(0, "The function wasn't executed yet");

            IAnimal animal01 = Kernel.Resolve<IAnimal>(false);
            count.Should().Be(1, "The function was executed one time");

            IAnimal animal02 = Kernel.Resolve<IAnimal>(false);
            count.Should().Be(2, "The function was executed two times");


            animal01.Should().NotBe(animal02, "Each classe is a new instance");
        }
    }
}
