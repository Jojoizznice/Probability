using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Wahrscheinlichkeiten.UnitTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void BinProb()
    {
        float res = ProbabilityMath.BernoulliProb(10, 5, 0.5f);

        Assert.AreEqual(0.24609375, res);
    }

    [TestMethod]
    public void BinProb2()
    {
        float res = ProbabilityMath.BernoulliProb(10, 7, 0.5f);

        Assert.AreEqual(0.1171875, res);
    }

    [TestMethod]
    public void BinProb3()
    {
        float res = ProbabilityMath.BernoulliProb(64, 12, 0.21f);

        Assert.AreEqual(0.114728634583, res, 0.000001);
    }

    [TestMethod]
    public void BinProb4()
    {
        float res = ProbabilityMath.BernoulliProb(548, 520, 0.97f);

        Assert.AreEqual(0.002386179144726, res, 0.000001);
    }
}