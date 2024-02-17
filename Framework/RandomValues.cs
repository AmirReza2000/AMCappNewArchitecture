using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public static class RandomValues : object
    {
        static RandomValues()
        {
            var random = new System.Random();
        }
        public static int GenerateRandomNumber(int MinLength = 1, int MaxLength = 1)
        {

            // **************************************************
            var minValueBuilder = new StringBuilder();
            minValueBuilder.Append("1");
            for (int i = 1; i < MinLength; i++)
            {
            minValueBuilder.Append("0");
            }
            // **************************************************

            // **************************************************
            var MaxValueBuilder = new StringBuilder();
             MaxValueBuilder.Append("9");
            for (int i = 1; i < MaxLength; i++)
            {
                MaxValueBuilder.Append("9");
            }
            // **************************************************

            var random = new System.Random();
            var result = random.Next(
                minValue:int.Parse(minValueBuilder.ToString()
                .Fix()!), 
                maxValue:int.Parse(MaxValueBuilder.ToString()
                .Fix()!));

            return result;
        }

    }
}
