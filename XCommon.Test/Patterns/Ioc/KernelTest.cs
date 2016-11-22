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

        [Fact(DisplayName = "Map (Simple, valid - no cache interface)")]
        public void MapSimpleValidNoCacheInterface()
        {
            Action act = () => Kernel.Map<IAnimal>().To<AnimalCat>();

            act.ShouldNotThrow("It is a valid map");

            IAnimal animal01 = Kernel.Resolve<IAnimal>();
            IAnimal animal02 = Kernel.Resolve<IAnimal>(false);

            animal01.Should().NotBe(animal02, "It isn't the same instance");
        }

        [Fact(DisplayName = "Map (Simple, valid - no cache abstract)")]
        public void MapSimpleValidNoCacheAbstract()
        {
            Action act = () => Kernel.Map<Vehicle>().To<VehicleCar>();

            act.ShouldNotThrow("It is a valid map");

            Vehicle vehicle01 = Kernel.Resolve<Vehicle>();
            Vehicle vehicle02 = Kernel.Resolve<Vehicle>(false);

            vehicle01.Should().NotBe(vehicle02, "It isn't the same instance");
        }

        [Fact(DisplayName = "Map (Simple, valid - no cache class to class)")]
        public void MapSimpleValidNoCacheClassToClass()
        {
            Action act = () => Kernel.Map<VehicleCar>().To<VehicleCar>();

            act.ShouldNotThrow("It is a valid map");

            VehicleCar vehicle01 = Kernel.Resolve<VehicleCar>();
            VehicleCar vehicle02 = Kernel.Resolve<VehicleCar>(false);

            vehicle01.Should().NotBe(vehicle02, "It isn't the same instance");
        }

        [Fact(DisplayName = "Map (Simple, valid - no cache function)")]
        public void MapSimpleValidNoCacheFunction()
        {
            int count = 0;

            Func<VehicleCar> resolver = () =>
            {
                count++;
                return new VehicleCar();
            };

            Action act = () => Kernel.Map<Vehicle>().To(resolver);

            act.ShouldNotThrow("It is a valid map");

            Vehicle vehicle01 = Kernel.Resolve<Vehicle>();
            count.Should().Be(1, "Was create one instance");

            Vehicle vehicle02 = Kernel.Resolve<Vehicle>(false);
            count.Should().Be(2, "Was create two instance");

            vehicle01.Should().NotBe(vehicle02, "It isn't the same instance");

            Vehicle vehicle03 = Kernel.Resolve<Vehicle>();
            count.Should().Be(2, "Was create just two instance");
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

        [Fact(Skip = "Not implemented", DisplayName = "Resolve (Generic, valid)")]
        public void ResolveGenericValid()
        {
            Assert.False(true);
        }

        [Fact(Skip = "Not implemented", DisplayName = "Resolve (Generic, valid no cache)")]
        public void ResolveGenericValidNoCache()
        {
            Assert.False(true);
        }

        [Fact(Skip = "Not implemented", DisplayName = "Resolve (Generic, valid no force)")]
        public void ResolveGenericValidNoForce()
        {
            Assert.False(true);
        }

        [Fact(Skip = "Not implemented", DisplayName = "Resolve (Generic, valid no cache and no force)")]
        public void ResolveGenericValidNoForceAndNoForce()
        {
            Assert.False(true);
        }

        [Fact(Skip = "Not implemented", DisplayName = "Resolve (Generic, invalid - Unmaped type)")]
        public void ResolveGenericInvalidUnmapedType()
        {
            Assert.False(true);
        }

        [Fact(Skip = "Not implemented", DisplayName = "Resolve (Generic, valid - Unmaped type - no force)")]
        public void ResolveGenericInvalidUnmapedTypeNoForce()
        {
            Assert.False(true);
        }

        [Fact(Skip = "Not implemented", DisplayName = "Resolve (Object, valid)")]
        public void ResolveObjectValid()
        {
            Assert.False(true);
        }

        [Fact(Skip = "Not implemented", DisplayName = "Resolve (Object, valid - no cache)")]
        public void ResolveObjectValidNoCache()
        {
            Assert.False(true);
        }

        [Fact(Skip = "Not implemented", DisplayName = "Resolve (Object, valid - Unmaped type no force)")]
        public void ResolveObjectValidUnmapedTypeNoForce()
        {
            Assert.False(true);
        }

        [Fact(Skip = "Not implemented", DisplayName = "Resolve (Object, invalid - Unmaped type force)")]
        public void ResolveObjectInvalidUnmapedTypeForce()
        {
            Assert.False(true);
        }
    }
}
