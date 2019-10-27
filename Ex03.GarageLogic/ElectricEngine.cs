namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public void ChargeBattery(float i_TimeToAddForBattery)
        {
            if (CurrentCapacity + i_TimeToAddForBattery <= MaxCapacity)
            {
                CurrentCapacity += i_TimeToAddForBattery;
            }
        }
    }
}
