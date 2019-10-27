namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelTypes m_FuelType;

        public enum eFuelTypes
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public eFuelTypes FuelType
        {
            get
            {
                return m_FuelType;
            }

            set
            {
                m_FuelType = value;
            }
        }

        public void RefuelingVehicle(float i_CapacityFuelToAdd, eFuelTypes i_FuelTypeToAdd)
        {
            if (i_FuelTypeToAdd == FuelType && i_CapacityFuelToAdd + CurrentCapacity <= MaxCapacity)
            {
                CurrentCapacity += i_CapacityFuelToAdd;
            }
        }
    }
}
