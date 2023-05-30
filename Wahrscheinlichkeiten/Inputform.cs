using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace Wahrscheinlichkeiten;

public enum Sign : sbyte
{
    none = -1,
    equals = 0,
    greaterThan = 1,
    greaterThanOrEqualTo = 2,
    smallerThan = 3,
    smallerThanOrEqualTo = 4
}

public class InputForm : IEquatable<InputForm?>
{
    private static readonly string[] strings = new string[]
        {
        "=",
        ">",
        ">=",
        "<",
        "<="
        };
    public static readonly string[] Signs = strings;
    
    public char Variable { get; }
    
    public int N { get; }
    public float P { get; }
    
    public int NumberOne { get; }
    public Sign SignOne { get; }

    public int? NumberTwo { get; }
    public Sign SignTwo { get; }

    public InputForm(char variable, int n, float p, Sign signOne, int numberOne, Sign? signTwo, int? numberTwo = null)
    {
        N = n;
        P = p;
        Variable = variable;
        NumberOne = numberOne; SignOne = signOne;
        NumberTwo = numberTwo;

        if (signTwo is null)
        {
            SignTwo = Sign.none;
            return;
        }

        SignTwo = signTwo.Value;
    }

    public Range GetRange()
    {
        if (SignOne is Sign.equals)
        {
            float f = MathF.Round(NumberOne, 0);
            int fi = (int)f;
            return new Range(fi, fi);
        }
        if (SignTwo is Sign.none)
        {
            return GetRangeNoSecond();
        }
        
        return default;
    }

    public Range GetRangeNoSecond()
    {
        float f = MathF.Round(NumberOne, 0);
        int fi = (int)f;

        switch (SignOne)
        {
            case Sign.smallerThan:
                return new(fi + 1, N);
            case Sign.smallerThanOrEqualTo:
                return new(fi + 1, N);
            case Sign.greaterThan:
                return new(0, fi - 1);
            case Sign.greaterThanOrEqualTo:
                return new(0, fi);
        }
        Range? range = null;
        return (Range)range!;
    }

    public override string ToString()
    {
        string s1 = $"n: {N}, p: {P}; P({NumberOne} {Signs[(sbyte)SignOne]} {Variable}";
        string s2 = "";

        if (SignTwo != Sign.none)
        {
            s2 = $" {Signs[(sbyte)SignTwo]} {NumberTwo}";
        }
        s2 += ")";

        return s1 + s2;
    }

    public string GetProbString()
    {
        if (SignTwo != Sign.none)
        {
            return $"P({NumberOne} {Signs[(sbyte)SignOne]} {Variable} {Signs[(sbyte)SignTwo]} {NumberTwo}";
        }
        return $"P({Variable} {Signs[(sbyte)SignOne]} {NumberOne})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        } 

        if (obj is not InputForm form)
        {
            return false;
        }

        return Equals(form);
    }

    public bool Equals(InputForm? other)
    {
        return other is not null &&
               Variable == other.Variable &&
               NumberOne == other.NumberOne &&
               SignOne == other.SignOne &&
               NumberTwo == other.NumberTwo &&
               SignTwo == other.SignTwo;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Variable, NumberOne, SignOne, NumberTwo, SignTwo);
    }
}

internal class InputFormCreator
{
    public int N { get; set; }
    public float P { get; set; }
    public int NumberOne { get; set; }
    public Sign SignOne { get; set; }

    public int? NumberTwo { get; set; }
    public Sign? SignTwo { get; set; }

    public char Variable { get; set; }

    public InputForm ToInputForm()
    {
        return new(
            Variable, N, P, SignOne, NumberOne, SignTwo, NumberTwo);
    }
}
