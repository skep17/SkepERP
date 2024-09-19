namespace SkepERP.Helpers
{
    public static class Validator
    {
        public static void ValidateOrThrow<T>(T value, Func<T, string> validationFunction)
        {
            string msg = validationFunction(value);
            if (!string.IsNullOrEmpty(msg))
            {
                throw new ArgumentException(msg);
            }
        }
    }
}
