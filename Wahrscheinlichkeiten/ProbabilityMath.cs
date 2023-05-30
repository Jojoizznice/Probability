namespace Wahrscheinlichkeiten;

public class ProbabilityMath
{
    public static float GetRangeProb(int n, float p, Range r)
    {
        float prob = 0;
        
        foreach (int k in r)
        {
            prob += BernoulliProb(n, k, p);
        }

        return prob;
    }

    public static float BernoulliProb(int n, int k, float p)
    {
        float coeff = BinominalCoefficient(n, k);
        float one = MathF.Pow(p, k);
        float two = MathF.Pow(1 - p, n - k);

        return coeff * one * two;
    }
   
    public static float BinominalCoefficient(int n, int k)
    {
        long nF = n;
        long kF = k;        
        
        if (nF < kF)
        {
            throw new ArgumentOutOfRangeException(nameof(kF), "N must be smaller or equal than k");
        }
        if (nF < 0 || kF < 0)
        {
            throw new ArgumentOutOfRangeException("n or k", "N and K must be positive");
        }
        
        if (2 * kF > nF)
        {
            kF = nF - kF;
        }

        long result = 1;
        for (int i = 1; i <= kF; i++)
        {
            result = result * (nF + 1 - i) / i;
        }

        return result;
    }
}
