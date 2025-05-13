using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.Common.ValueObjects
{
    /// <summary>
    /// Represents a date range with start and optional end date, supporting "until now" scenarios.
    /// Implements value object pattern with duration calculation capabilities.
    /// </summary>
    public class DateRange : ValueObject
    {
        /// <summary>
        /// Gets the start date of the range.
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        /// Gets the end date of the range (null indicates ongoing).
        /// </summary>
        public DateTime? End { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this range continues until the present time.
        /// </summary>
        public bool UntilNow { get; private set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="start">The start date of the range.</param>
        /// <param name="end">The end date of the range (null for ongoing).</param>
        /// <param name="untilNow">Whether the range continues to the present (defaults to false).</param>
        private DateRange(DateTime start, DateTime? end, bool untilNow = false)
        {
            Start = start;
            End = end;
            UntilNow = untilNow;
        }

        /// <summary>
        /// Creates a new DateRange from date values with validation.
        /// </summary>
        /// <param name="start">The start date of the range.</param>
        /// <param name="end">The end date of the range.</param>
        /// <param name="untilNow">Whether the range continues to the present (defaults to false).</param>
        /// <returns>
        /// A new <see cref="DateRange"/> if validation passes,
        /// or an error if start date is after end date.
        /// </returns>
        /// <remarks>
        /// If untilNow is true, the end parameter is ignored and set to null.
        /// </remarks>
        public static ErrorOr<DateRange> FromDateTimes(DateTime start, DateTime? end, bool untilNow = false)
        {
            if (untilNow)
            {
                end = null;
                return new DateRange(start, end, true);
            }
            if (start >= end)
            {
                return Error.Validation("DateRange.StartDateBiggerThanEndDate", "Start date is bigger than end date");
            }
            return new DateRange(start, end);
        }

        /// <summary>
        /// Gets the components used for equality comparison.
        /// </summary>
        /// <returns>An enumerable of the components that define this value object's equality.</returns>
        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
            yield return UntilNow;
        }

        /// <summary>
        /// Calculates the duration between start and end dates (or current date if UntilNow is true).
        /// </summary>
        /// <returns>
        /// A tuple containing the years, months, and days between dates.
        /// Returns (0, 0, 0) if start date is after end date.
        /// </returns>
        public (int Years, int Months, int Days) CalculateDuration()
        {
            var endDate = UntilNow ? DateTime.Now.Date : End?.Date ?? DateTime.Now.Date;
            var startDate = Start.Date;

            if (startDate > endDate)
                return (0, 0, 0);

            // Calculate years first
            int years = endDate.Year - startDate.Year;
            if (startDate.AddYears(years) > endDate)
            {
                years--;
            }

            // Calculate remaining months
            DateTime tempDate = startDate.AddYears(years);
            int months = 0;
            while (tempDate.AddMonths(months + 1) <= endDate)
            {
                months++;
            }

            // Calculate remaining days
            DateTime monthAdjusted = tempDate.AddMonths(months);
            int days = (endDate - monthAdjusted).Days;

            return (years, months, days);
        }

        /// <summary>
        /// Formats the duration as a human-readable string (e.g., "2 years 3 months 15 days").
        /// </summary>
        /// <returns>A formatted string representation of the duration.</returns>
        /// <remarks>
        /// Will always include at least days (e.g., "0 days" for zero duration).
        /// Uses singular/plural forms appropriately (year/years, month/months, day/days).
        /// </remarks>
        public string FormatDuration()
        {
            var (years, months, days) = CalculateDuration();

            var parts = new List<string>();
            if (years > 0) parts.Add($"{years} {(years == 1 ? "year" : "years")}");
            if (months > 0) parts.Add($"{months} {(months == 1 ? "month" : "months")}");
            if (days > 0 || parts.Count == 0) parts.Add($"{days} {(days == 1 ? "day" : "days")}");

            return string.Join(" ", parts);
        }
        private DateRange()
        {
            
        }
    }
}
