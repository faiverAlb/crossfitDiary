using System;

namespace CrossfitDiary.Model.Exceptions
{
    /// <summary>
    ///     Base class to handle any Business exceptions
    /// </summary>
    public class BaseBusinessException : Exception
    {
        #region constructors

        /// <summary>
        ///     Base constructor for Business exceptions
        /// </summary>
        /// <param name="message"></param>
        protected BaseBusinessException(string message) : base(message)
        {
        }

        #endregion
    }
}