using System.Globalization;

namespace Goldix.Application.Extensions;

public static class DateTimeExtensions
{
    public static string ToShamsiDate(this DateTime dateTime)
    {
        PersianCalendar persianCalendar = new PersianCalendar();
        string shamsiDate = persianCalendar.GetYear(dateTime) + "/" +
                            persianCalendar.GetMonth(dateTime).ToString("00") + "/" +
                            persianCalendar.GetDayOfMonth(dateTime).ToString("00") + " " +
                            dateTime.ToString("HH:mm:ss");
        return shamsiDate;
    }
}