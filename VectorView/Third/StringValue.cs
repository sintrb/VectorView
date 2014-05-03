using System;

namespace Third
{
    public static class StringValue
    {
        public static double ToValue(string s)
        {
            return double.Parse(XIYV.Compute.SimpleRPN.ComputeRPN(s));
        }
    }
}
