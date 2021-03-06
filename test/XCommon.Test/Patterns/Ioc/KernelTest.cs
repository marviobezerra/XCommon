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
		[Trait("Patterns", "Ioc")]
		public void MapSimpleValidInterface()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalCat>();

            act.Should().NotThrow("It is a valid map");
            Kernel.Count.Should().Be(1, "There is one class mapped");
        }

        [Fact(DisplayName = "Map (Simple, valid - abstract)")]
		[Trait("Patterns", "Ioc")]
		public void MapSimpleValidAbstract()
        {
            Action act = () => Kernel.Map<Vehicle>().To<VehicleCar>();

            act.Should().NotThrow("It is a valid map");
            Kernel.Count.Should().Be(1, "There is one class mapped");
        }

        [Fact(DisplayName = "Map (Simple, valid - class to class)")]
		[Trait("Patterns", "Ioc")]
		public void MapSimpleValidClassToClass()
        {
            Action act = () => Kernel.Map<VehicleCar>().To<VehicleCar>();

            act.Should().NotThrow("It is a valid map");
            Kernel.Count.Should().Be(1, "There is one class mapped");
        }

        [Fact(DisplayName = "Map (Simple, valid - function)")]
		[Trait("Patterns", "Ioc")]
		public void MapSimpleValidFunction()
        {
            Func<VehicleCar> resolver = () => new VehicleCar();
            Action act = () => Kernel.Map<Vehicle>().To(resolver);

            act.Should().NotThrow("It is a valid map");
            Kernel.Count.Should().Be(1, "There is one class mapped");
        }
        
        [Fact(DisplayName = "Map (Constructor params, valid - 01 param)")]
		[Trait("Patterns", "Ioc")]
		public void MapConstructorParamsValid01Param()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalDog>(150);
            act.Should().NotThrow("It is a valid map");
        }

        [Fact(DisplayName = "Map (Constructor params, valid - 02 params)")]
		[Trait("Patterns", "Ioc")]
		public void MapConstructorParamsValid02Params()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalDuck>(150, true);
            act.Should().NotThrow("It is a valid map");
        }

        [Fact(DisplayName = "Map (Constructor params, valid - 03 params)")]
		[Trait("Patterns", "Ioc")]
		public void MapConstructorParamsValid03Params()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalDuck>(150, true, DateTime.Now);
            act.Should().NotThrow("It is a valid map");
        }

        [Fact(DisplayName = "Map (Constructor params, invalid - with no params)")]
		[Trait("Patterns", "Ioc")]
		public void MapConstructorParamsInvalidWithNoParams()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalDuck>();
            act.Should().Throw<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Constructor params, invalid - invalid param type)")]
		[Trait("Patterns", "Ioc")]
		public void MapConstructorParamsInvalidInvalidParamType()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalDuck>(150, "Fail", DateTime.Now);
            act.Should().Throw<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Constructor params, invalid - invalid param count)")]
		[Trait("Patterns", "Ioc")]
		public void MapConstructorParamsInvalidInvalidParamCount()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalDuck>(150, true, DateTime.Now, "Fail");
            act.Should().Throw<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Simple, invalid - interface to interface)")]
		[Trait("Patterns", "Ioc")]
		public void MapSimpleInvalidInterfaceToInterface()
        {
            Action act = () => Kernel.Map<IAnimal>().To<IAnimal>();
            act.Should().Throw<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Simple, invalid - interface to abstract)")]
		[Trait("Patterns", "Ioc")]
		public void MapSimpleInvalidInterfaceToAbstract()
        {
            Action act = () => Kernel.Map<Vehicle>().To<Vehicle>();
            act.Should().Throw<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Simple, invalid - interface to a class that doesn't implement it)")]
		[Trait("Patterns", "Ioc")]
		public void MapSimpleInvalidInterfaceToClassThatDoesImplement()
        {
            Action act = () => Kernel.Map<IAnimal>().To<VehicleCar>();
            act.Should().Throw<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Simple, invalid - abstract to abstrac)")]
		[Trait("Patterns", "Ioc")]
		public void MapSimpleInvalidAbstractToAbstrac()
        {
            Action act = () => Kernel.Map<Vehicle>().To<Vehicle>();
            act.Should().Throw<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Simple, invalid - abstract to interface)")]
		[Trait("Patterns", "Ioc")]
		public void MapSimpleInvalidAbstractToInterface()
        {
            Action act = () => Kernel.Map<IAnimal>().To<Vehicle>();
            act.Should().Throw<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Map (Simple, invalid - abstract to a class that doesn't implement)")]
		[Trait("Patterns", "Ioc")]
		public void MapSimpleInvalidAbstractToClassThatDoesImplement()
        {
            Action act = () => Kernel.Map<Vehicle>().To<AnimalDog>();
            act.Should().Throw<Exception>("It isn't a valid map");
        }

        [Fact(DisplayName = "Resolve (Generic, valid)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveGenericValid()
        {
            Kernel.Map<IAnimal>().To<AnimalCat>();

            var animal01 = Kernel.Resolve<IAnimal>();
            var animal02 = Kernel.Resolve<IAnimal>();

            animal01.Should().NotBeNull("There is a mapped class");
            animal01.Should().Be(animal02, "Both are the same instance");
        }

        [Fact(DisplayName = "Resolve (Generic, valid no cache)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveGenericValidNoCache()
        {
            Kernel.Map<IAnimal>().To<AnimalCat>();

            var animal01 = Kernel.Resolve<IAnimal>(false);
            var animal02 = Kernel.Resolve<IAnimal>(false);

            animal01.Should().NotBe(animal02, "Each class is a new instance");
        }

        [Fact(DisplayName = "Resolve (Generic, not mapped valid no force)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveGenericValidNoForce()
        {
            var animal01 = Kernel.Resolve<IAnimal>(true, false);

            animal01.Should().BeNull("There isn't a mapped class, but the Kernel doesn't force to resolve");
        }

        [Fact(DisplayName = "Resolve (Generic, not mapped valid no cache and no force)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveGenericValidNoForceAndNoForce()
        {
            var animal01 = Kernel.Resolve<IAnimal>(false, false);

            animal01.Should().BeNull("There isn't a mapped class, but the Kernel doesn't use cache and doesn't force to resolve");
        }

        [Fact(DisplayName = "Resolve (Generic, invalid - Unmaped type)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveGenericInvalidUnmapedType()
        {
            Action act = () => Kernel.Resolve<IAnimal>();
            act.Should().Throw<Exception>("There isn't a class mapped for that interface");
        }

        [Fact(DisplayName = "Resolve (Object, valid)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveObjectValid()
        {
            Kernel.Map<AnimalCat>().To<AnimalCat>();
            var animal = Kernel.Resolve<AnimalCat>();

            animal.Should().NotBeNull("There is one class mapped for that class");
        }

        [Fact(DisplayName = "Resolve (Object, valid - no cache)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveObjectValidNoCache()
        {
            Kernel.Map<AnimalCat>().To<AnimalCat>();

            var animal01 = Kernel.Resolve<AnimalCat>(false);
            var animal02 = Kernel.Resolve<AnimalCat>(false);

            animal01.Should().NotBe(animal02, "Each class is a new instance");
        }

        [Fact(DisplayName = "Resolve (Object, valid - Unmaped type no force)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveObjectValidUnmapedTypeNoForce()
        {
            var animal = Kernel.Resolve<AnimalCat>(true, false);

            animal.Should().BeNull("There isn't a mapped class, but the kernel wasn't force to resolve");
        }

        [Fact(DisplayName = "Resolve (Object, invalid - Unmaped type force)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveObjectInvalidUnmapedTypeForce()
        {
            Action act = () => Kernel.Resolve<AnimalCat>();

            act.Should().Throw<Exception>("There isn't a mapped class");
        }

        [Fact(DisplayName = "Resolve (Function, valid - cache)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveFunctionValidCache()
        {
            Func<IAnimal> resolver = () => new AnimalCat();
            Kernel.Map<IAnimal>().To(resolver);

            var animal01 = Kernel.Resolve<IAnimal>();
            var animal02 = Kernel.Resolve<IAnimal>();

            animal01.Should().Be(animal02, "Both classes are the same instance");
        }

        [Fact(DisplayName = "Resolve (Function, valid - no cache)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveFunctionValidNoCache()
        {
            var count = 0;
            Func<IAnimal> resolver = () => 
            {
                count++;
                return new AnimalCat();
            };

            Kernel.Map<IAnimal>().To(resolver);

            count.Should().Be(0, "The function wasn't executed yet");

            var animal01 = Kernel.Resolve<IAnimal>(false);
            count.Should().Be(1, "The function was executed one time");

            var animal02 = Kernel.Resolve<IAnimal>(false);
            count.Should().Be(2, "The function was executed two times");


            animal01.Should().NotBe(animal02, "Each classe is a new instance");
        }

        [Fact(DisplayName = "Resolve (Attribute)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveAttribute()
        {
            Kernel.Map<IAnimal>().To<AnimalCat>();
            Kernel.Map<Vehicle>().To<VehicleCar>();

            var business = new SampleBusiness();

            business.Animal.Should().NotBeNull("There is a class mapped");
            business.Vehicle.Should().NotBeNull("There is a class mapped");

        }

        [Fact(DisplayName = "Resolve (Attribute, Thown Unmaped type)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveAttributeUnmapedType()
        {
            Kernel.Map<IAnimal>().To<AnimalCat>();

            Action act = () => new SampleBusiness();

            act.Should().Throw<Exception>("There is no classe mapped for vehicle");
        }

        [Fact(DisplayName = "Resolve (Attribute, no cache)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveAttributeNoCache()
        {
            Kernel.Map<IAnimal>().To<AnimalCat>();
            Kernel.Map<Vehicle>().To<VehicleCar>();

            var business = new SampleNoCacheBusiness();
            var animal = Kernel.Resolve<IAnimal>();

            business.Animal.Should().NotBe(animal, "These classes aren't the same instance");
        }

        [Fact(DisplayName = "Resolve (Attribute, no force)")]
		[Trait("Patterns", "Ioc")]
		public void ResolveAttributeNoForce()
        {
            Kernel.Map<Vehicle>().To<VehicleCar>();
            
            var business = new SampleNoForceBusiness();

            business.Animal.Should().BeNull("There is no class mapped for animal, but it isn't forced to be resolved");
            business.Vehicle.Should().NotBeNull("There is a class mapped for animal, even if it isn't force to be resolved needs to get an instace");

        }

        [Fact(DisplayName = "Reset")]
		[Trait("Patterns", "Ioc")]
		public void Reset()
        {
            Kernel.Map<Vehicle>().To<VehicleCar>();
            Kernel.Map<IAnimal>().To<AnimalDog>(2);

            Kernel.Reset();

            Kernel.Count.Should().Be(0);
        }
    }
}
