namespace Framework;

public static class StringHelper : object
{
    static StringHelper()
    {
    }

    public static string? Fix(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return null;
        }

        value =
            value.Trim();

        while (value.Contains(value: "  "))
        {
            value = value.Replace
                (oldValue: "  ", newValue: " ");
        }

        return value;
    }
    public static string? ReRemoveAllBlankSpaces(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return null;
        }

        value =
            value.Trim();

        while (value.Contains(value: " "))
        {
            value = value.Replace
                (oldValue: " ", newValue: string.Empty);
        }

        return value;
    }

    public static string? ConvertDigitsToUnicode(this object? value)
    {
        if (value is null)
        {
            return null;
        }

        var valueString =
            value.ToString();

        if (valueString is null)
        {
            return null;
        }

        //var currentUICultureName =
        //    System.Threading.Thread.CurrentThread
        //    .CurrentUICulture.Parent.Name.ToUpper();


        valueString =
            valueString
            .Replace(oldChar: '0', newChar: '۰')
            .Replace(oldChar: '1', newChar: '۱')
            .Replace(oldChar: '2', newChar: '۲')
            .Replace(oldChar: '3', newChar: '۳')
            .Replace(oldChar: '4', newChar: '۴')
            .Replace(oldChar: '5', newChar: '۵')
            .Replace(oldChar: '6', newChar: '۶')
            .Replace(oldChar: '7', newChar: '۷')
            .Replace(oldChar: '8', newChar: '۸')
            .Replace(oldChar: '9', newChar: '۹')
            ;

        return valueString;

    }
}




