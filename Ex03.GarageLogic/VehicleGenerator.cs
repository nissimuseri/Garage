using System;

namespace Ex03.GarageLogic
{
    public class VehicleGenerator
    {
        public static Vehicle VehicleCreator(string i_ModelName, string i_LiecenseNumber, int i_VehicleType, Engine.eEnergyType i_EnergyType, string i_WheelsVendorName, float i_CurrentEnergyInPercent, float i_CurrentAirPressure)
        {
            Vehicle newVehicle;
            switch (i_VehicleType)
            {
                case 1:
                    newVehicle = new Car(i_EnergyType, i_ModelName, i_LiecenseNumber, i_WheelsVendorName, i_CurrentEnergyInPercent, i_CurrentAirPressure);
                    break;
                case 2:
                    newVehicle = new Motorcycle(i_EnergyType, i_ModelName, i_LiecenseNumber, i_WheelsVendorName, i_CurrentEnergyInPercent, i_CurrentAirPressure);
                    break;
                case 3:
                    newVehicle = new Truck(i_EnergyType, i_ModelName, i_LiecenseNumber, i_WheelsVendorName, i_CurrentEnergyInPercent, i_CurrentAirPressure);
                    break;
                default: throw new ArgumentException();
            }

            return newVehicle;
        }
    }
}
