namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsDriveingDangerousThings;
        private float m_VolumeOfCargo;

        public bool IsDriveingDangerousThings
        {
            get
            {
                return m_IsDriveingDangerousThings;
            }

            set
            {
                m_IsDriveingDangerousThings = value;
            }
        }

        public float VolumeOfCargo
        {
            get
            {
                return m_VolumeOfCargo;
            }

            set
            {
                m_VolumeOfCargo = value;
            }
        }

        public Truck(Engine.eEnergyType i_EnergyType, string i_ModelName, string i_LiecenseNumber, string i_WheelVendorName, float i_CurrentEnergyInPercent, float i_CurrentAirPressure) : base(i_ModelName, i_LiecenseNumber, i_WheelVendorName, i_CurrentAirPressure, 12)
        {
            if (Wheels[0].CurrentAirPressure > 26f)
            {
                throw new ValueOutOfRangeException(26f, 0);
            }

            Wheels.Capacity = 12;
            foreach (Wheel wheel in Wheels)
            {
                wheel.MaxAirPressure = 26f;
            }

            VehicleEngine = new FuelEngine();
            ((FuelEngine)VehicleEngine).FuelType = FuelEngine.eFuelTypes.Soler;
            VehicleEngine.MaxCapacity = 110;
            VehicleEngine.CurrentCapacity = i_CurrentEnergyInPercent * VehicleEngine.MaxCapacity;
            EnergyToEmptyInPrecent = i_CurrentEnergyInPercent;
        }

        public override string ToString()
        {
            string carDetails = string.Format(
                @"Driveing Dangerous Things: {0}
VolumeOfCargo: {1}",
                IsDriveingDangerousThings ? "Yes" : "No",
                VolumeOfCargo);
            return carDetails;
        }

        public override void ChangeSpecificDetails(object i_IsDriveingDangerousThings, object i_VolumeOfCargo)
        {
            IsDriveingDangerousThings = (bool)i_IsDriveingDangerousThings;
            VolumeOfCargo = (float)i_VolumeOfCargo;
        }
    }
}
