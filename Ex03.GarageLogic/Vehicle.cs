using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_EnergyToEmptyInPrecent;
        private List<Wheel> m_Wheels;
        private Garage.eCarToFixStatus m_CarToFixStatus;
        private Engine m_VehicleEngine;

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        public Garage.eCarToFixStatus CarToFixStatus
        {
            get
            {
                return m_CarToFixStatus;
            }

            set
            {
                m_CarToFixStatus = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }

            set
            {
                m_Wheels = value;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public Engine VehicleEngine
        {
            get
            {
                return m_VehicleEngine;
            }

            set
            {
                m_VehicleEngine = value;
            }
        }

        public float EnergyToEmptyInPrecent
        {
            get
            {
                return m_EnergyToEmptyInPrecent;
            }

            set
            {
                m_EnergyToEmptyInPrecent = value;
            }
        }

        public Vehicle(string i_ModelName, string i_LiecenseNumber, string i_WheelVendorName, float i_CurrentWheelPressure, int i_NumOfWheels)
        {
            ModelName = i_ModelName;
            m_LicenseNumber = i_LiecenseNumber;
            Wheels = new List<Wheel>();
            for (int i = 0; i < i_NumOfWheels; i++) 
            {
                Wheels.Add(new Wheel());
            }

            foreach (Wheel wheel in Wheels)
            {
                wheel.VendorName = i_WheelVendorName;
                wheel.CurrentAirPressure = i_CurrentWheelPressure;
            }
        }

        public abstract void ChangeSpecificDetails(object i_Param1, object i_Param2);
    }
}
