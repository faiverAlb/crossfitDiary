namespace CrossfitDiary.Web.Mappings
{
    /// <summary>
    ///     Decimal to string extension class
    /// </summary>
    public static class DecimalToStringExtension
    {
        /// <summary>
        ///     Extension method to hide zeros
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCustomString(this decimal? value)
        {
            if (value.HasValue == false)
            {
                return string.Empty;
            }

            decimal floatingPart = value.Value % 1.0m;
            if (floatingPart > 0.0m)
            {
                return value.Value.ToString();
            }

            return ((int)value.Value).ToString();
        }
    }
}