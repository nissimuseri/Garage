namespace Ex03.GarageLogic
{
    public struct Owner
    {
        private readonly string m_OwnerName;
        private readonly string m_OwnerPhoneNumber;
        private Vehicle m_VehicleOfOwner;

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }
        }

        public Vehicle VehicleOfOwner
        {
            get
            {
                return m_VehicleOfOwner;
            }
        }

        public Owner(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_VehicleOfOwner)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleOfOwner = i_VehicleOfOwner;
        }
    }
}
