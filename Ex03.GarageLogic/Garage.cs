using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, Owner> m_VehiclesToFix = new Dictionary<string, Owner>();

        public enum eCarToFixStatus
        {
            InFix,
            Fixed,
            Paid
        }

        public void AddNewCarToFix(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)        {
            m_VehiclesToFix.Add(i_Vehicle.LicenseNumber, new Owner(i_OwnerName, i_OwnerPhoneNumber, i_Vehicle));        }

        public StringBuilder ShowVehiclesInGarage(int i_DesiredStatusToShow)
        {
            string openingLine;
            StringBuilder listOfAllLicence = new StringBuilder();
            switch (i_DesiredStatusToShow)
            {
                case 1:
                    foreach (string licence in m_VehiclesToFix.Keys)
                    {
                        listOfAllLicence.AppendFormat("{0}{1}", licence, Environment.NewLine);
                    }

                    break;
                case 2:
                    foreach (string licence in m_VehiclesToFix.Keys)
                    {
                        if (m_VehiclesToFix[licence].VehicleOfOwner.CarToFixStatus == eCarToFixStatus.InFix)
                        {
                            listOfAllLicence.AppendFormat("{0}{1}", licence, Environment.NewLine);
                        }
                    }

                    break;
                case 3:
                    foreach (string licence in m_VehiclesToFix.Keys)
                    {
                        if (m_VehiclesToFix[licence].VehicleOfOwner.CarToFixStatus == eCarToFixStatus.Fixed)
                        {
                            listOfAllLicence.AppendFormat("{0}{1}", licence, Environment.NewLine);
                        }
                    }

                    break;
                case 4:
                    foreach (string licence in m_VehiclesToFix.Keys)
                    {
                        if (m_VehiclesToFix[licence].VehicleOfOwner.CarToFixStatus == eCarToFixStatus.Paid)
                        {
                            listOfAllLicence.AppendFormat("{0}{1}", licence, Environment.NewLine);
                        }
                    }

                    break;
            }

            if(listOfAllLicence.Length == 0)
            {
                listOfAllLicence.AppendFormat(@"
There are no vehicles in this status.
");
            }
            else
            {
                openingLine = string.Format(@"
Here are all the vehicles in this status:
");
                listOfAllLicence.Insert(
0,
openingLine);
            }

            return listOfAllLicence;
        }

        public void ChangeCarStatus(string i_LicenceNumber, eCarToFixStatus i_NewStatus)
        {
            if (m_VehiclesToFix.ContainsKey(i_LicenceNumber) == true)
            {
                m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.CarToFixStatus = i_NewStatus;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void InflatingToMax(string i_LicenceNumber)
        {
            if (m_VehiclesToFix.ContainsKey(i_LicenceNumber) == true)
            {
                foreach (Wheel wheel in m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.Wheels)
                {
                        wheel.Inflating(wheel.MaxAirPressure);
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void refuelingVehicle(string i_LicenceNumber, FuelEngine.eFuelTypes i_FuelType, float i_CapacityToAdd)
        {
            ArgumentException i_ArgumentException;
            if (m_VehiclesToFix.ContainsKey(i_LicenceNumber) == true)
            {
                if (m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine is FuelEngine)
                {
                    if (((FuelEngine)m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine).FuelType == i_FuelType)
                    {
                        if (m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine.CurrentCapacity + i_CapacityToAdd <= m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine.MaxCapacity)
                        { 
                            ((FuelEngine)m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine).RefuelingVehicle(i_CapacityToAdd, i_FuelType);
                        }
                        else
                        {
                            i_ArgumentException = new ArgumentException("The current capacity plus the capacity you entered excceed the max capacity.");
                            throw i_ArgumentException;
                        }
                    }
                    else
                    {
                        i_ArgumentException = new ArgumentException("The selected vehicle is driven by a different fuel type.");
                        throw i_ArgumentException;
                    }
                }
                else
                {
                    i_ArgumentException = new ArgumentException("The selected vehicle is driven by electricity.");
                    throw i_ArgumentException;
                }

                m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.EnergyToEmptyInPrecent = m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine.CurrentCapacity / m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine.MaxCapacity * 100;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void ChargingVehicle(string i_LicenceNumber, float i_TimeToAddForBattery)
        {
            ArgumentException i_ArgumentException;
            i_TimeToAddForBattery /= 60;
            if (m_VehiclesToFix.ContainsKey(i_LicenceNumber) == true)
            {
                if (m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine is ElectricEngine)
                {
                    if(m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine.CurrentCapacity + i_TimeToAddForBattery <= m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine.MaxCapacity)
                    {
                        ((ElectricEngine)m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine).ChargeBattery(i_TimeToAddForBattery);
                    }
                    else
                    {
                        i_ArgumentException = new ArgumentException("The current capacity plus the capacity you entered excceed the max capacity.");
                        throw i_ArgumentException;
                    }
                }
                else
                {
                    i_ArgumentException = new ArgumentException("The selected vehicle is driven by fuel.");
                    throw i_ArgumentException;
                }

                m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.EnergyToEmptyInPrecent = m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine.CurrentCapacity / m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.VehicleEngine.MaxCapacity * 100;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public string ShowVehicleDetails(string i_LicenceNumber)
        {
            if (m_VehiclesToFix.ContainsKey(i_LicenceNumber) == true)
            {
                string listOfAllLicence = string.Format(
                @"Licence Number: {0}
Model Name: {1}
Owner Name: {2}
Current Status: {3}
Wheel Vendor: {4}
Wheel Air Pressure: {5}
{6}",
                i_LicenceNumber,
                m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.ModelName,
                m_VehiclesToFix[i_LicenceNumber].OwnerName,
                m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.CarToFixStatus,
                m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.Wheels[0].VendorName,
                m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.Wheels[0].CurrentAirPressure,
                m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.ToString(),
                Environment.NewLine);
                return listOfAllLicence;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public bool isLicenceExist(string i_LicenceNumber)        {            bool isExist = true;            if (m_VehiclesToFix.ContainsKey(i_LicenceNumber) != true)            {                isExist = false;            }            else            {                m_VehiclesToFix[i_LicenceNumber].VehicleOfOwner.CarToFixStatus = eCarToFixStatus.InFix;            }            return isExist;        }
    }
}