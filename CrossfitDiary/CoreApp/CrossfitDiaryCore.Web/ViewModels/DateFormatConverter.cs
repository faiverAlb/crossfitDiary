using Newtonsoft.Json.Converters;

namespace CrossfitDiaryCore.Web.ViewModels
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}