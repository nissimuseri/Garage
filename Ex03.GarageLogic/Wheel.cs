namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_VendorName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }

            set
            {
                m_MaxAirPressure = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public string VendorName
        {
            get
            {
                return m_VendorName;
            }

            set
            {
                m_VendorName = value;
            }
        }

        public void Inflating(float i_AirToAdd)
        {
            if (i_AirToAdd == MaxAirPressure) 
            {
                CurrentAirPressure = i_AirToAdd;
            }
            else if (CurrentAirPressure + i_AirToAdd <= MaxAirPressure)
            {
                CurrentAirPressure += i_AirToAdd;
            }
            else
            {
                ValueOutOfRangeException i_ValueOutOfRangeException = new ValueOutOfRangeException(MaxAirPressure, 0);
                throw i_ValueOutOfRangeException;
            }
        }
    }
}
