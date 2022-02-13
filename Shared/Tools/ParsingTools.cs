using System;

namespace Shared.Patches
{
    public static class ParsingTools
    {
        public static bool TryParseBool(string str, out bool result)
        {
            if (bool.TryParse(str, out result))
                return true;
        
            if (str.Equals("1", StringComparison.Ordinal) || 
                str.Equals("on", StringComparison.OrdinalIgnoreCase) ||
                str.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                str.Equals("t", StringComparison.OrdinalIgnoreCase) ||
                str.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                result = true;
                return true;
            }

            if (str.Equals("0", StringComparison.Ordinal) ||
                str.Equals("off", StringComparison.OrdinalIgnoreCase) ||
                str.Equals("n", StringComparison.OrdinalIgnoreCase) ||
                str.Equals("f", StringComparison.OrdinalIgnoreCase) ||
                str.Equals("no", StringComparison.OrdinalIgnoreCase))
            {
                result = false;
                return true;
            }

            return false;
        }
    }
}