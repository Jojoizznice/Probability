using System.Diagnostics;
using System.Globalization;

namespace Wahrscheinlichkeiten;

public static class Input
{   
    public static InputForm GetInput(string? input = null)
    {
        var np = PreReader.GetNP();
        
        string raw = ReadUser(input);
        raw = raw.Replace(" ", null);

        string[] split = raw.Split(InputForm.Signs, StringSplitOptions.RemoveEmptyEntries);
        return ParseInput(split, raw, np);
    }
    
    private static string ReadUser(string? input)
    {
        if (input is not null)
        {
            return input.Replace(")", null);
        }
        
        Console.Write($"P(");
        return Console.ReadLine()!.Replace(")", null);
    }

    private static InputForm ParseInput(string[] strings, string v, (int n, float p) np)
    {
        InputFormCreator creator = new()
        {
            N = np.n,
            P = np.p
        };

        if (char.IsAsciiLetterUpper(v.First()))
        {
            return VariableInFirstPlace(v, np);
        }

        string first = CheckFirst(strings[0], creator);
        v = v[first.Length..]; // move for (length of first) positions
        string spaced = EnsureSpace(v);

        CheckSign(spaced[..2], creator, 0);
        spaced = spaced[2..]; // move for 2 positions (sign)

        string second = CheckSecond(strings[1], creator);

        if (!CheckContinue(strings, creator))
        {
            return creator.ToInputForm();
        }

        spaced = spaced[second.Length..];
        CheckSign(spaced[..2], creator, 1);
        CheckThird(strings[2], creator);
        return creator.ToInputForm();
    }

    private static InputForm VariableInFirstPlace(string v, (int, float) np)
    {
        string w = v[2..];

        bool equals = w.First() == '=';
        if (equals)
        {
            w = w[1..];
        }

        string sign = equals ? v[1..3] : v[1].ToString();
        string swapped = sign.Replace('<', char.MaxValue).Replace('>', '<').Replace(char.MaxValue, '>');

        string input = $"{w}{swapped}{v[0]}";
        string[] split = input.Split(InputForm.Signs, StringSplitOptions.RemoveEmptyEntries);
        return ParseInput(split, input, np);
    }

    private static string EnsureSpace(string v)
    {
        return v.Replace("=", "= ").Replace(">", "> ").Replace("<", "< ").Replace("< = ", "<=").Replace("> = ", ">=");
    }

    private static string CheckFirst(string v, InputFormCreator creator)
    {
        bool success = int.TryParse(v, out int s);
        if (success)
        {
            creator.NumberOne = s;
            return s.ToString();
        }

        InputThrowHelper.ThrowNumber(0);
        return null!;
    }

    private static void CheckSign(string v, InputFormCreator creator, int pos)
    {
        Sign sign = Sign.none;

        switch (v)
        {
            case "= ":
                sign = Sign.equals; break;

            case "> ":
                sign = Sign.greaterThan; break;

            case "< ":
                sign = Sign.smallerThan; break;

            case ">=":
                sign = Sign.greaterThanOrEqualTo; break;

            case "<=":
                sign = Sign.smallerThanOrEqualTo; break;

            default:
                InputThrowHelper.ThrowSign(0); break;
        }

        switch (pos)
        {
            case 0:
                creator.SignOne = sign;
                break;

            case 1:
                creator.SignTwo = sign;
                break;

            default:
                throw new UnreachableException($"[INTERNAL ERROR]: pos was {pos}, must be either one or two");
        }
    }
    private static string CheckSecond(string v, InputFormCreator creator)
    {
        if (v.Length > 1)
        {
            InputThrowHelper.ThrowFormatVariable(1);
            return null!;
        }

        creator.Variable = v[0];
        return creator.Variable.ToString();
    }

    private static bool CheckContinue(string[] strings, InputFormCreator creator)
    {
        bool varInMiddle = strings[1].Length == 1 && char.IsAsciiLetterUpper(strings[1].First());
        bool string3Exists = strings.Length == 3;
        bool noEquals = creator.SignOne != Sign.equals;

        return varInMiddle && string3Exists && noEquals;
    }
    private static void CheckThird(string v, InputFormCreator creator)
    {
        bool success = int.TryParse(v, out int s);
        if (success)
        {
            creator.NumberTwo = s;
            return;
        }

        InputThrowHelper.ThrowNumber(2);
    }
}