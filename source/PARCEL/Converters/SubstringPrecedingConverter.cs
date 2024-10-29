using System.Globalization;

namespace PARCEL.Converters;

class SubstringPrecedingConverter : IValueConverter
{
    public object Convert(object? value, Type type, object? parameter, CultureInfo culture)
    {
        try
        {
            string toParse = "" + value as string;
            string splitString = "" + parameter as string;

            if (toParse.IndexOf(splitString) > -1)
            {
                return toParse[..toParse.IndexOf(splitString)];

            }


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            
        }

        return "" + value;

    }

    public object ConvertBack(object? value, Type type, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();

}
