namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eColorOfCar m_ColorOfCar;
        private eNumOfDoors m_NumOfDoors;

        public enum eColorOfCar
        {
            Red,
            Blue,
            Black,
            Gray
        }

        public enum eNumOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        public eColorOfCar ColorOfCar
        {
            get
            {
                return m_ColorOfCar;
            }

            set
            {
                m_ColorOfCar = value;
            }
        }

        public eNumOfDoors NumOfDoors
        {
            get
            {
                return m_NumOfDoors;
            }

            set
            {
                m_NumOfDoors = value;
            }
        }

        public Car(Engine.eEnergyType i_EnergyType, string i_ModelName, string i_LiecenseNumber, string i_WheelVendorName, float i_CurrentEnergyInPercent, float i_CurrentAirPressure) : base(i_ModelName, i_LiecenseNumber, i_WheelVendorName, i_CurrentAirPressure, 4)
        {
            if(Wheels[0].CurrentAirPressure > 31f)
            {
                throw new ValueOutOfRangeException(31f, 0);
            }

            Wheels.Capacity = 4;
            foreach (Wheel wheel in Wheels)
            {
                wheel.MaxAirPressure = 31f;
            }

            if(i_EnergyType == Engine.eEnergyType.Electricity)
            {
                VehicleEngine = new ElectricEngine();
                VehicleEngine.MaxCapacity = 1.8f;
            }
            else if(i_EnergyType == Engine.eEnergyType.Fuel)
            {
                VehicleEngine = new FuelEngine();
                ((FuelEngine)VehicleEngine).FuelType = FuelEngine.eFuelTypes.Octan96;
                VehicleEngine.MaxCapacity = 55f;
            }

            VehicleEngine.CurrentCapacity = i_CurrentEnergyInPercent * VehicleEngine.MaxCapacity / 100;
            EnergyToEmptyInPrecent = i_CurrentEnergyInPercent;
        }

        public override string ToString()
        {
            string carDetails = string.Format(
                @"Color Of The Car: {0}
Number Of Doors: {1}",
                ColorOfCar,
                NumOfDoors);
            return carDetails;
        }

        public override void ChangeSpecificDetails(object i_ColorOfCar, object i_NumOfDoors)
        {
            m_ColorOfCar = (eColorOfCar)i_ColorOfCar;
            m_NumOfDoors = (eNumOfDoors)i_NumOfDoors;
        }
    }
}