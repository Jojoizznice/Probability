namespace Wahrscheinlichkeiten;

public class Program
{
    static void Main(string[] args)
    {
        InputForm f = Input.GetInput();
        float p = ProbabilityMath.GetRangeProb(f.N, f.P, f.GetRange());
        Console.CursorTop -= 1;
        Console.WriteLine($"{f.GetProbString()} = {p}");
    }
}