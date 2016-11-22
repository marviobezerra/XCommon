/*
 
Map (Simple, valid - interface)
Map (Simple, valid - abstract)
Map (Simple, valid - class to class)
Map (Simple, valid - function)

Map (Simple, valid - no cache interface)
Map (Simple, valid - no cache abstract)
Map (Simple, valid - no cache class to class)
Map (Simple, valid - no cache function)

Map (Constructor params, valid - 01 param)
Map (Constructor params, valid - 02 params)
Map (Constructor params, valid - 03 params)

Map (Constructor params, invalid - with no params)
Map (Constructor params, invalid - invalid param type)
Map (Constructor params, invalid - invalid param count)

Map (Type, invalid - interface to interface)
Map (Type, invalid - interface to abstract)
Map (Type, invalid - interface to a class that does implement)

Map (Type, invalid - abstract to abstrac)
Map (Type, invalid - abstract to interface)
Map (Type, invalid - abstract to a class that does implement)

Resolve (Generic, valid)
Resolve (Generic, valid no cache)
Resolve (Generic, valid no foce)
Resolve (Generic, valid no force and no force)

Resolve (Generic, invalid - Unmaped type)
Resolve (Generic, invalid - Unmaped type - no force)

Resolve (Object, valid)
Resolve (Object, valid - no cache)
Resolve (Object, valid - no force)
Resolve (Object, invalid - Unmaped type - no force)
 
 */

using XCommon.Patterns.Ioc;
using XCommon.Test.Patterns.Ioc.Sample;
using Xunit;
using FluentAssertions;
using System;

namespace XCommon.Test.Patterns.Ioc
{
    public class KernelTest
    {
        public KernelTest()
        {
            Kernel.Reset();
        }

        [Fact(DisplayName = "Kernel (Map)")]
        public void KernelMap()
        {
            Kernel.Map<ITestClass>().To<TestClass01>();

            Kernel.Count.Should().Be(1, "There is one maped class");
            Kernel.Resolve<ITestClass>().Should().NotBeNull("There is one maped class");
        }

        [Fact(DisplayName = "Kernel (Map with constructor)")]
        public void KernelMapWithConstructor()
        {
            Kernel.Map<ITestClass>().To<TestClass02>(-99);

            Kernel.Count.Should().Be(1, "There is one maped class");
            Kernel.Resolve<ITestClass>().Should().NotBeNull("There is one maped class");
        }

        [Fact(DisplayName = "Kernel (Map Throw, class need just one param in constructor)")]
        public void KernelMapWithConstructorErrorMoreThenNecessary()
        {
            Action act = () => Kernel.Map<ITestClass>().To<TestClass02>(-99, -1);
            
            act.ShouldThrow<Exception>("The constructor needs just one parameter");
            Kernel.Count.Should().Be(0, "There is one maped class");
        }

        [Fact(DisplayName = "Kernel (Map Throw, Interface to Interface)")]
        public void KernelMapInterfaceToInterface()
        {
            Action act = () => Kernel.Map<ITestClass>().To<ITestClass>();

            act.ShouldThrow<Exception>("It can't map Interface to Interface");
            Kernel.Count.Should().Be(0, "There is no maped class");
        }

        [Fact(DisplayName = "Kernel (Map Throw, Interface to invalid class)")]
        public void KernelMapInterfaceToInvalidClass()
        {
            Action act = () => Kernel.Map<ITestClass>().To<TestAbstract01>();

            act.ShouldThrow<Exception>("It can't map Interface to an class which doesn't implement it");
            Kernel.Count.Should().Be(0, "There is no maped class");
        }

        [Fact(DisplayName = "Kernel (Map Throw, Abstract class to invalid class)")]
        public void KernelMapAbstractToInvalidClass()
        {
            Action act = () => Kernel.Map<TestAbstract>().To<TestClass01>();

            act.ShouldThrow<Exception>("It can't map Abstract class to an class which doesn't implement it");
            Kernel.Count.Should().Be(0, "There is no maped class");
        }

        [Fact(DisplayName = "Kernel (Map Throw, class need just param in constructor)")]
        public void KernelMapWithConstructorError()
        {
            Action act = () => Kernel.Map<ITestClass>().To<TestClass02>();

            Kernel.Count.Should().Be(0, "There is no maped class");
            act.ShouldThrow<Exception>();
        }

        [Fact(DisplayName = "Kernel (Reset)")]
        public void KernelReset()
        {
            Kernel.Map<ITestClass>().To<TestClass01>();
            Kernel.Reset();

            Kernel.Count.Should().Be(0, "There is one maped class, but kernel was reseted");
        }

        [Fact(DisplayName = "Kernel (Resolve)")]
        public void KernelResolve()
        {
            Kernel.Map<ITestClass>().To<TestClass01>();

            var personBusiness = Kernel.Resolve<ITestClass>();

            personBusiness.Should()
                .BeOfType<TestClass01>("The interface was maped for PersonBusiness01")
                .And.NotBeNull("The Kernel needs to create an instance of that class");
        }

        [Fact(DisplayName = "Kernel (Resolve no cache)")]
        public void KernelResolveNoCache()
        {
            Kernel.Map<ITestClass>().To<TestClass01>();

            ITestClass personBusiness01 = Kernel.Resolve<ITestClass>();
            ITestClass personBusiness02 = Kernel.Resolve<ITestClass>(false);

            personBusiness01.ClassKey = 99;
            personBusiness02.ClassKey = -99;

            personBusiness01.ClassKey.Should()
                .NotBe(personBusiness02.ClassKey, "It should be another instance");

            personBusiness01.Should()
                .NotBe(personBusiness02, "It is two differente instances");
        }
    }
}
