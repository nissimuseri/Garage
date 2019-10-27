using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue) : base(string.Format(@"Value out of range, it must be between {0} to {1}", i_MinValue, i_MaxValue))
        {
        }
    }
}