using System.Globalization;

namespace Wahrscheinlichkeiten;

internal class PreReader
{
    public static (int n, float p) GetNP()
    {
        Console.Write("n:       p (in decimal):\nP(      )");
        Console.CursorTop -= 1;
        int n = GetN();

        Console.CursorLeft = 9;
        Console.CursorTop -= 1;
        Console.Write("p (in decimal):");
        Console.CursorLeft = 25;
        string pS = Console.ReadLine()!;

        float p = float.NaN;
        if (pS.Contains('%'))
        {
            p = HandlePercent(pS);
        }
        else if (pS.Contains('/'))
        {
            p = HandleDivision(pS);
        }
        else
        {
            bool s = float.TryParse(pS, NumberStyles.Float, null, out float f);
            if (!s) { InputThrowHelper.NaN('p'); }
            p = f;
        }

        return (n, p);
    }

    private static float HandleDivision(string pS)
    {
        string[] strings = pS.Split('/');
        if (strings.Length != 2)
        {
            InputThrowHelper.NotTwoDivis();
        }

        bool s1 = float.TryParse(strings[0], NumberStyles.Float, null, out float f1);
        bool s2 = float.TryParse(strings[1], NumberStyles.Float, null, out float f2);
        if (!(s1 && s2))
        {
            InputThrowHelper.NaN('p');
        }

        return f1 / f2;
    }

    private static float HandlePercent(string pS)
    {
        pS = pS.Replace("%", null);
        bool pSuccess = float.TryParse(pS, NumberStyles.Float, null, out float p);
        
        if (!pSuccess)
        {
            InputThrowHelper.NaN('p');
        }
        p /= 100;
        return p;
    }

    static int GetN()
    {
        Console.CursorLeft = 3;
        string nS = Console.ReadLine()!;
        bool nSuccess = int.TryParse(nS, out int n);
        if (!nSuccess)
        {
            InputThrowHelper.NaN('n');
        }
        return n;
    }
}
