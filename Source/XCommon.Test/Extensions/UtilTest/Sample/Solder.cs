using System;
using System.Collections.Generic;

namespace XCommon.Test.Extensions.UtilTest.Sample
{
    public interface IWeapon
    {
        int Power { get; }

        void Shot();
    }

    public interface ISolder
    {
        IWeapon LeftHand { get; }

        IWeapon RightHand { get; }
    }

    public class Sword : IWeapon
    {
        public int Power { get; private set; }

        public void Shot()
        {
            // Do something
        }
    }
    
    public class Rocket : IWeapon
    {
        public int Power { get; private set; }

        public void Shot()
        {
            // Do something
        }
    }

    public class Solder : ISolder
    {
        public IWeapon LeftHand { get; private set; }

        public IWeapon RightHand { get; private set; }
    }

    public interface IComplexType<T1, T2, T3>
    {
    }

    public abstract class ComplexType<T1, T2, T3> : IComplexType<T1, T2, T3>
    {
    }

    public class ComplexType : ComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid>
    {

    }
}
