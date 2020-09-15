using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Abp.Extensions
{
    /// <summary>
    /// Extension methods for String class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// indicates whether this string is null, empty, or consists only of white-space characters.
        /// </summary>
        [ContractAnnotation("null => true")]
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
