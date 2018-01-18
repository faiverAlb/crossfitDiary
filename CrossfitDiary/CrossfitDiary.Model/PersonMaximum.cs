using System;

namespace CrossfitDiary.Model
{
    public class PersonMaximum
    {
        #region properties

        /// <summary>
        ///     Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Gets or sets the maximum weight.
        /// </summary>
        public decimal MaximumWeight { get; set; }

        /// <summary>
        ///     Gets or sets the person name.
        /// </summary>
        public string PersonName { get; set; }

        #endregion
    }
}