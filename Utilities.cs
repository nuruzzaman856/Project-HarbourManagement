using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HarbourManagement
{
    public static class Utilities
    {
        private static Random random = new Random();
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int GetRandomNumber(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}
