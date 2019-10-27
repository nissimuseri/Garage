namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public enum eEnergyType
        {
            Fuel,
            Electricity
        }

        private float m_CurrentCapacity;
        private float m_MaxCapacity;

        public float CurrentCapacity
        {
            get
            {
                return m_CurrentCapacity;
            }

            set
            {
                m_CurrentCapacity = value;
            }
        }

        public float MaxCapacity
        {
            get
            {
                return m_MaxCapacity;
            }

            set
            {
                m_MaxCapacity = value;
            }
        }
    }
}