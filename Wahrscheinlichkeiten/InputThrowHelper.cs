using System.Diagnostics.CodeAnalysis;

namespace Wahrscheinlichkeiten;

internal static class InputThrowHelper
{
    [DoesNotReturn]
    public static void ThrowFormatVariable(int pos)
    {
        string throwString = "Variable name must be one capital letter.";
        if (pos is 0)
        {
            throwString += " If you have a number at first position it may wasn't recognised.";
        }

        throw new FormatException(throwString);
    }

    [DoesNotReturn]
    public static void ThrowSign(int pos)
    {
        throw new FormatException($"Your sign at position {pos + 1} wasn't recognised.");
    }

    [DoesNotReturn]
    public static void ThrowNumber(int pos)
    {
        throw new FormatException($"Not a number at pos {pos + 1}.");
    }

    internal static void ThrowAscii(char wrongChar)
    {
        throw new FormatException($"""All of your letters must be ascii letters. You provided a "{wrongChar}" which is not an ascii character""");
    }

    internal static void ThrowNegative()
    {
        throw new FormatException("No number can be negative");
    }

    internal static void NaN(char var)
    {
        string throwString = $"{var} was not recognised as a number";
        if (var == 'p')
        {
            throwString += $" or division";
        }
        throwString += ".";

        throw new FormatException(throwString);
    }

    internal static void NotTwoDivis()
    {
        throw new FormatException("The division for p must consist out of 2 elements");
    }
}
