using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.Common.Entities.EducationEntity
{
    /// <summary>
    /// Represents the degree level of an educational qualification using type-safe enum pattern.
    /// Inherits from <see cref="SmartEnum{TEnum}"/> to provide additional functionality.
    /// </summary>
    /// <remarks>
    /// This enum-like class provides fixed set of education degree options with proper type safety
    /// and additional features compared to standard C# enums.
    /// </remarks>
    public class EducationDegree : SmartEnum<EducationDegree>
    {
        /// <summary>
        /// High School education level.
        /// </summary>
        public static readonly EducationDegree HighSchool = new("HighSchool", 0);

        /// <summary>
        /// Bachelor's Degree education level.
        /// </summary>
        public static readonly EducationDegree Bachelor = new("Bachelor", 1);

        /// <summary>
        /// Master's Degree education level.
        /// </summary>
        public static readonly EducationDegree Master = new("Master", 2);

        /// <summary>
        /// PhD or Doctorate education level.
        /// </summary>
        public static readonly EducationDegree PhD = new("PhD", 3);

        /// <summary>
        /// Diploma or Certificate education level.
        /// </summary>
        public static readonly EducationDegree Diploma = new("Diploma", 4);

        /// <summary>
        /// Special value indicating studies were not completed.
        /// </summary>
        public static readonly EducationDegree NotGraduated = new("NotGraduated", 5);

        /// <summary>
        /// Initializes a new instance of the <see cref="EducationDegree"/> class.
        /// </summary>
        /// <param name="name">The display name of the education degree.</param>
        /// <param name="value">The numeric value of the education degree.</param>
        private EducationDegree(string name, int value) : base(name, value) { }
    }
}
