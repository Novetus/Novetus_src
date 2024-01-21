using System;

namespace Novetus.Core
{
    public static class ConvertSafe
    {
        public static int ToInt32Safe(object obj)
        {
            int result = 0;

            try
            {
                result = Convert.ToInt32(obj);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public static bool ToBooleanSafe(object obj)
        {
            bool result = false;

            try
            {
                result = Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public static long ToInt64Safe(object obj)
        {
            long result = 0;

            try
            {
                result = Convert.ToInt64(obj);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public static decimal ToDecimalSafe(object obj)
        {
            decimal result = 0;

            try
            {
                result = Convert.ToDecimal(obj);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public static double ToDoubleSafe(object obj)
        {
            double result = 0;

            try
            {
                result = Convert.ToDouble(obj);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public static float ToSingleSafe(object obj)
        {
            float result = 0;

            try
            {
                result = Convert.ToSingle(obj);
            }
            catch (Exception)
            {
            }

            return result;
        }
    }
}
