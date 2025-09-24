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

    public static string ToShamsiDateFormatted(this DateTime dateTime)
    {
        var pc = new PersianCalendar();
        return $"{pc.GetYear(dateTime):0000}/{pc.GetMonth(dateTime):00}/{pc.GetDayOfMonth(dateTime):00}";
    }

    public static DateTime? ToMiladi(this string shamsiDate)
    {
        if (string.IsNullOrEmpty(shamsiDate))
            return null;

        var parts = shamsiDate.Split('/');

        if (parts.Length is 0)
            return null;

        int.TryParse(parts[0], out var year);
        int.TryParse(parts[1], out var month);
        int.TryParse(parts[2], out var day);

        PersianCalendar persianCalendar = new PersianCalendar();
        var result = new DateTime(year, month, day, persianCalendar);
        return result;
    }
}