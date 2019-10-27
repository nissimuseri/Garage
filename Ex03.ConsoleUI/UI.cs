using System;
using System.Linq;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        private static readonly Garage m_Garage = new Garage();

        public static void GarageSystemMenu()
        {
            int userChoice = -1;
            while (userChoice != 0)
            {
                showMenu();
                userChoice = inputBetweenRange(0, 7);
                Console.Clear();
                switch (userChoice)
                {
                    case 0:
                        break;
                    case 1:
                        addNewCarToFix();
                        break;
                    case 2:
                        showCarsInGarage();
                        break;
                    case 3:
                        changeCarStatus();
                        break;
                    case 4:
                        inflatingToMax();
                        break;
                    case 5:
                        refuelingVehicle();
                        break;
                    case 6:
                        chargingVehicle();
                        break;
                    case 7:
                        showVehicleDetails();
                        break;
                }

                if(userChoice > 0)
                {
                    userChoice = -1;
                    Console.WriteLine("Press any key to return to the menu");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void showMenu()
        {
            Console.WriteLine(
            @"Garage Manage System Menu
Please select action from the list:
1. Add new vehicle to the garage.
2. Show list of all the cars in the garage.
3. Change car status.
4. Inflating vehicle's wheels to maximum.
5. Fueling car.
6. Charging car.
7. Show car details.
For exit press 0.");
        }

        private static void addNewCarToFix()
        {
            bool licenceExist;
            string modelName;
            string licenceNumber;
            float currentEnergyInPercent = -1;
            string wheelsVendorName;
            float currentAirPressure;
            Vehicle newVehicle;
            Engine.eEnergyType energyType = Engine.eEnergyType.Fuel;
            Car.eColorOfCar colorOfCar;
            Car.eNumOfDoors numOfDoors;
            Motorcycle.eLicenceTypes licenceType;
            int engineCapacity;
            bool isDriveingDangerousThings;
            float volumeOfCargo;
            string ownerName;
            string ownerPhoneNumber;
            int vehicleType = 0;
            int userChoice = 0;

            Console.WriteLine(
                @"Please choose vehicle type:
1. Car
2. Motorcycle
3. Truck");
            vehicleType = inputBetweenRange(1, 3);

            if (userChoice == 1 || userChoice == 2)
            {
                Console.WriteLine(@"Please enter vehicle energy type:
1. Fuel
2. Electricity");
                userChoice = inputBetweenRange(1, 2);
                switch (userChoice)
                {
                    case 1:
                        energyType = Engine.eEnergyType.Fuel;
                        break;
                    case 2:
                        energyType = Engine.eEnergyType.Electricity;
                        break;
                }
            }

            Console.Clear();
            Console.Write("Please enter vehicle model name: ");
            modelName = checkEmptyInput();
            Console.Write("Please enter vehicle licence number with 7/8 digits: ");
            licenceNumber = checkLicenceNumber();
            licenceExist = m_Garage.isLicenceExist(licenceNumber);
            if (licenceExist == true)
            {
                Console.WriteLine("This licence number is already exists in the garage, his status became InFix.");
            }
            else
            {
                Console.Write("Please enter current energy in percent: ");
                currentEnergyInPercent = checkFloatParsing();
                while (currentEnergyInPercent < 0 || currentEnergyInPercent > 100)
                {
                    Console.WriteLine("Invalid input, the input must be between 0 to 100");
                    currentEnergyInPercent = checkFloatParsing();
                }

                Console.Write("Please enter wheels vendor name: ");
                wheelsVendorName = checkEmptyInput();
                Console.Write("Please enter current air pressure in the wheels: ");
                currentAirPressure = checkFloatParsing();
                try
                {
                    newVehicle = VehicleGenerator.VehicleCreator(modelName, licenceNumber, vehicleType, energyType, wheelsVendorName, currentEnergyInPercent, currentAirPressure);
                    Console.Clear();
                    if (newVehicle is Car)
                    {
                        inputCarDetails(out colorOfCar, out numOfDoors);
                        newVehicle.ChangeSpecificDetails(colorOfCar, numOfDoors);
                    }
                    else if (newVehicle is Motorcycle)
                    {
                        inputMotorcycleDetails(out licenceType, out engineCapacity);
                        newVehicle.ChangeSpecificDetails(licenceType, engineCapacity);
                    }
                    else if (newVehicle is Truck)
                    {
                        inputTruckDetails(out isDriveingDangerousThings, out volumeOfCargo);
                        newVehicle.ChangeSpecificDetails(isDriveingDangerousThings, volumeOfCargo);
                    }

                    Console.Clear();
                    Console.Write("Please enter your first name: ");
                    ownerName = Console.ReadLine();
                    while (checkOwnerNameInput(ownerName) != true)
                    {
                        Console.WriteLine("Wrong input, Please enter only letters.");
                        ownerName = Console.ReadLine();
                    }

                    Console.WriteLine("Please enter your phone number in format of 10 digits:");
                    ownerPhoneNumber = Console.ReadLine();
                    while (checkOwnerPhoneNumberInput(ownerPhoneNumber) != true)
                    {
                        Console.WriteLine("Wrong input, Please enter only numbers with 10 digits.");
                        ownerPhoneNumber = Console.ReadLine();
                    }

                    m_Garage.AddNewCarToFix(newVehicle, ownerName, ownerPhoneNumber);
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(i_ValueOutOfRangeException.Message);
                }
            }
        }

        private static void showCarsInGarage()
        {
            int userChoice = 0;

            Console.WriteLine(
                @"Which cars would you like to see?
1. All the cars.
2. Cars in fix status.
3. Cars that have been fixed.
4. Cars that have been paid.");
            userChoice = inputBetweenRange(1, 4);
            Console.WriteLine(m_Garage.ShowVehiclesInGarage(userChoice));
        }

        private static void changeCarStatus()
        {
            int userChoice = 0;
            Garage.eCarToFixStatus newStatus = 0;
            string licenceNumber;

            Console.Write("Please enter vehicle licence number: ");
            licenceNumber = checkEmptyInput();
            Console.WriteLine(
                @"Now choose the new status:
1. In Fix
2. Fixed
3. Paid");
            userChoice = inputBetweenRange(1, 3);
            switch (userChoice)
            {
                case 1:
                    newStatus = Garage.eCarToFixStatus.InFix;
                    break;
                case 2:
                    newStatus = Garage.eCarToFixStatus.Fixed;
                    break;
                case 3:
                    newStatus = Garage.eCarToFixStatus.Paid;
                    break;
            }

            try
            {
                m_Garage.ChangeCarStatus(licenceNumber, newStatus);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(@"
This licence isn't exist in the garage.");
            }
        }

        private static void inflatingToMax()
        {
            string licenceNumber;

            Console.Write("Please enter vehicle licence number: ");
            licenceNumber = checkEmptyInput();
            try
            {
                m_Garage.InflatingToMax(licenceNumber);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(@"
This licence isn't exist in the garage.");
            }
        }

        private static void refuelingVehicle()
        {
            string licenceNumber;
            int userChoice = 0;
            FuelEngine.eFuelTypes fuelType = 0;
            float litersToAdd = 0;

            Console.Write("Please enter vehicle licence number: ");
            licenceNumber = checkEmptyInput();
            Console.WriteLine(
                @"Now please choose the fuel type:
1. Soler
2. Octan95
3. Octan96
4. Octan98");
            userChoice = inputBetweenRange(1, 4);
            switch (userChoice)
            {
                case 1:
                    fuelType = FuelEngine.eFuelTypes.Soler;
                    break;
                case 2:
                    fuelType = FuelEngine.eFuelTypes.Octan95;
                    break;
                case 3:
                    fuelType = FuelEngine.eFuelTypes.Octan96;
                    break;
                case 4:
                    fuelType = FuelEngine.eFuelTypes.Octan98;
                    break;
            }

            Console.Write("Last thing, please enter how many liters you wish to add: ");
            litersToAdd = checkFloatParsing();
            try
            {
                m_Garage.refuelingVehicle(licenceNumber, fuelType, litersToAdd);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(@"
This licence isn't exist in the garage.");
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
            }
        }

        private static void chargingVehicle()
        {
            float minutesToAdd = 0;
            string licenceNumber;

            Console.Write("Please enter vehicle licence number: ");
            licenceNumber = checkEmptyInput();
            Console.Write("Now please enter how many minutes you wish to add: ");
            minutesToAdd = checkFloatParsing();
            try
            {
                m_Garage.ChargingVehicle(licenceNumber, minutesToAdd);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(@"
This licence isn't exist in the garage.");
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
            }
        }

        private static void showVehicleDetails()
        {
            string licenceNumber;

            Console.WriteLine("Please enter vehicle licence number: ");
            licenceNumber = checkEmptyInput();
            try
            {
                Console.WriteLine(m_Garage.ShowVehicleDetails(licenceNumber));
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(@"
This licence isn't exist in the garage.");
            }
        }

        private static int inputBetweenRange(int i_MinimumValue, int i_MaximumValue)
        {
            int userChoice = -1;
            bool isValidInput = true;

                Console.Write(@"
Your choice is: ");
            isValidInput = int.TryParse(Console.ReadLine(), out userChoice);
            while (isValidInput != true || userChoice < i_MinimumValue || userChoice > i_MaximumValue)
            {
                Console.WriteLine("Invalid input, please enter a number between {0} to {1}", i_MinimumValue, i_MaximumValue);
                isValidInput = int.TryParse(Console.ReadLine(), out userChoice);
            }

            return userChoice;
        }

        private static float checkFloatParsing()
        {
            bool checkInput = true;
            float userInput = 0;

            checkInput = float.TryParse(Console.ReadLine(), out userInput);
            while (checkInput != true)
            {
                Console.WriteLine("Invalid input, please enter again.");
                checkInput = float.TryParse(Console.ReadLine(), out userInput);
            }

            return userInput;
        }

        private static bool checkOwnerNameInput(string i_OwnerName)
        {
            bool isCorrectInput = false;

            if (string.IsNullOrEmpty(i_OwnerName) != true)
            {
                isCorrectInput = i_OwnerName.All(char.IsLetter);
            }

            return isCorrectInput;
        }

        private static bool checkOwnerPhoneNumberInput(string i_PhoneNumber)
        {
            bool isCorrectInput = i_PhoneNumber.All(char.IsDigit);
            if (i_PhoneNumber.Length != 10)
            {
                isCorrectInput = false;
            }

            return isCorrectInput;
        }

        private static string checkEmptyInput()
        {
            string userInput = Console.ReadLine();
            while(string.IsNullOrEmpty(userInput) == true)
            {
                Console.WriteLine("Invalid input, Please enter again.");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private static string checkLicenceNumber()
        {
            string userInput = Console.ReadLine();
            bool isCorrectInput = userInput.All(char.IsDigit);
            while (string.IsNullOrEmpty(userInput) == true || isCorrectInput != true || (userInput.Length != 7 && userInput.Length != 8 )) 
            {
                Console.WriteLine("Invalid input, Please enter licence number with 7/8 digits.");
                userInput = Console.ReadLine();
                isCorrectInput = userInput.All(char.IsDigit);
            }

            return userInput;
        }
        
        private static void inputCarDetails(out Car.eColorOfCar o_ColorOfCar, out Car.eNumOfDoors o_NumOfDoors)
        {
            int userChoice = 0;
            o_ColorOfCar = 0;
            o_NumOfDoors = 0;

            Console.WriteLine(@"Please enter the color of the car:
1. Red,
2. Blue,
3. Black,
4. Gray");
            userChoice = inputBetweenRange(1, 4);
            switch (userChoice)
            {
                case 1:
                    o_ColorOfCar = Car.eColorOfCar.Red;
                    break;
                case 2:
                    o_ColorOfCar = Car.eColorOfCar.Blue;
                    break;
                case 3:
                    o_ColorOfCar = Car.eColorOfCar.Black;
                    break;
                case 4:
                    o_ColorOfCar = Car.eColorOfCar.Gray;
                    break;
            }

            Console.Write(@"Please enter num of doors from 2 to 5");
            userChoice = inputBetweenRange(2, 5);
            switch (userChoice)
            {
                case 2:
                    o_NumOfDoors = Car.eNumOfDoors.Two;
                    break;
                case 3:
                    o_NumOfDoors = Car.eNumOfDoors.Three;
                    break;
                case 4:
                    o_NumOfDoors = Car.eNumOfDoors.Four;
                    break;
                case 5:
                    o_NumOfDoors = Car.eNumOfDoors.Five;
                    break;
            }
        }

        private static void inputTruckDetails(out bool o_IsDriveingDangerousThings, out float o_VolumeOfCargo)
        {
            o_IsDriveingDangerousThings = false;
            o_VolumeOfCargo = 0;
            int userChoice = 0;

            Console.WriteLine("Is the truck driving dangerous things? if yes press 1, if no press 2.");
            userChoice = inputBetweenRange(1, 2);
            switch (userChoice)
            {
                case 1:
                    o_IsDriveingDangerousThings = true;
                    break;
                case 2:
                    o_IsDriveingDangerousThings = false;
                    break;
            }

            Console.WriteLine("Please enter volume of cargo:");
            o_VolumeOfCargo = float.Parse(Console.ReadLine());
        }

        private static void inputMotorcycleDetails(out Motorcycle.eLicenceTypes o_LicenceType, out int o_EngineCapacity)
        {
            int userChoice = 0;
            o_LicenceType = 0;
            o_EngineCapacity = 0;

            Console.WriteLine(@"Please enter licence type:
1. A
2. A1
3. A2
4. B");
            userChoice = inputBetweenRange(1, 4);
            switch (userChoice)
            {
                case 1:
                    o_LicenceType = Motorcycle.eLicenceTypes.A;
                    break;
                case 2:
                    o_LicenceType = Motorcycle.eLicenceTypes.A1;
                    break;
                case 3:
                    o_LicenceType = Motorcycle.eLicenceTypes.A2;
                    break;
                case 4:
                    o_LicenceType = Motorcycle.eLicenceTypes.B;
                    break;
            }

            Console.Write("Please enter engine capacity: ");
            o_EngineCapacity = int.Parse(Console.ReadLine());
        }
    }
}
