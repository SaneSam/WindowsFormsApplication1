using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class RandomNumberGen
    {
        private static Random _generator = new Random();

        public static int NumberBetween(int MinValue,int MaxValue)
        {
            return _generator.Next(MinValue, MaxValue+1);
        }
    }
}
