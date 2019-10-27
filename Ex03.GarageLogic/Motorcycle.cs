namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenceTypes m_LicenceType;
        private int m_EngineCapacity;

        public enum eLicenceTypes
        {
            A,
            A1,
            A2,
            B
        }

        public eLicenceTypes LicenceType
        {
            get
            {
                return m_LicenceType;
            }

            set
            {
                m_LicenceType = value;
            }
        }

        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }

            set
            {
                m_EngineCapacity = value;
            }
        }

        public Motorcycle(Engine.eEnergyType i_EnergyType, string i_ModelName, string i_LiecenseNumber, string i_WheelVendorName, float i_CurrentEnergyInPercent, float i_CurrentAirPressure) : base(i_ModelName, i_LiecenseNumber, i_WheelVendorName, i_CurrentAirPressure, 2)
        {
            if (Wheels[0].CurrentAirPressure > 1.4f)
            {
                throw new ValueOutOfRangeException(1.4f, 0);
            }

            Wheels.Capacity = 2;
            foreach (Wheel wheel in Wheels)
            {
                wheel.MaxAirPressure = 33;
            }

            if (i_EnergyType == Engine.eEnergyType.Electricity)
            {
                VehicleEngine = new ElectricEngine();
                VehicleEngine.MaxCapacity = 1.4f;
            }
            else if (i_EnergyType == Engine.eEnergyType.Fuel)
            {
                VehicleEngine = new FuelEngine();
                ((FuelEngine)VehicleEngine).FuelType = FuelEngine.eFuelTypes.Octan95;
                VehicleEngine.MaxCapacity = 8;
            }

            VehicleEngine.CurrentCapacity = i_CurrentEnergyInPercent * VehicleEngine.MaxCapacity;
            EnergyToEmptyInPrecent = i_CurrentEnergyInPercent;
        }

        public override string ToString()
        {
            string carDetails = string.Format(
                @"Licence Type: {0}
Engine Capacity: {1}",
                LicenceType,
                EngineCapacity);
            return carDetails;
        }

        public override void ChangeSpecificDetails(object i_LicenceType, object i_EngineCapacity)
        {
            m_LicenceType = (eLicenceTypes)i_LicenceType;
            m_EngineCapacity = (int)i_EngineCapacity;
        }
    }
}