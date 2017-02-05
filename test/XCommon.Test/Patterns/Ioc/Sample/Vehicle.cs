using System;

namespace XCommon.Test.Patterns.Ioc.Sample
{
    public abstract class Vehicle
    {
        public abstract int Power { get; protected set; }

        public abstract int Number { get; protected set; }

        public abstract DateTime DeliveryDate { get; protected set; }
    }

    public class VehicleCar : Vehicle
    {
        public override int Power { get; protected set; }

        public override int Number { get; protected set; }

        public override DateTime DeliveryDate { get; protected set; }
    }
    
    public class VehicleMotorBike : Vehicle
    {
        public VehicleMotorBike(int power)
        {
            Power = power;
        }

        public override int Power { get; protected set; }

        public override int Number { get; protected set; }

        public override DateTime DeliveryDate { get; protected set; }

    }

    public class VehicleBoat : Vehicle
    {
        public VehicleBoat(int power, int number)
        {
            Power = power;
            Number = number;
        }

        public VehicleBoat(int power, int number, DateTime deliveryDate)
        {
            Power = power;
            Number = number;
            DeliveryDate = deliveryDate;
        }

        public override int Power { get; protected set; }

        public override int Number { get; protected set; }

        public override DateTime DeliveryDate { get; protected set; }

    }

    public class VehicleSpaceShip : Vehicle
    {
        public VehicleSpaceShip(int power, int number, DateTime deliveryDate)
        {
            Power = power;
            Number = number;
            DeliveryDate = deliveryDate;
        }

        public override int Power { get; protected set; }

        public override int Number { get; protected set; }

        public override DateTime DeliveryDate { get; protected set; }

    }
}
