using System;
using System.Linq;


public static class GetRandomEnumValue
{
    public static Enum RandomValue(this Type t)
    {
        return Enum.GetValues(t).OfType<Enum>()
            .OrderBy(e => Guid.NewGuid())
            .FirstOrDefault();
    }
}


