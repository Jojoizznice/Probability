namespace Wahrscheinlichkeiten;

internal class FormChecker
{
    public static void Check(InputForm f)
    {
        if (f.NumberOne < 0 || f.NumberTwo < 0)
        {
            InputThrowHelper.ThrowNegative();
        }
    }
}
