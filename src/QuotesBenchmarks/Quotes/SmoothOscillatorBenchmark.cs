using BenchmarkDotNet.Attributes;
using TradingApp.Evaluator.Indicators;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Constants;

namespace QuotesBenchmarks.Quotes;

[MemoryDiagnoser]
public class SmoothOscillatorBenchmark
{
    public List<StochResult> StochResultsLoh45000 { get; set; }
    public List<StochResult> StochResultsLoh44999 { get; set; }
    public List<StochResult> StochResults3000 { get; set; }
    public List<StochResult> StochResults2999 { get; set; }

    [IterationSetup]
    public void IterationSetup()
    {
        StochResultsLoh45000 = GetRandomStochResults(45000);
        StochResultsLoh44999 = GetRandomStochResults(44999);
        StochResults3000 = GetRandomStochResults(3000);
        StochResults2999 = GetRandomStochResults(2999);
    }

    [Benchmark(Baseline = true)]
    public void SmoothOscillatorLohNewCtr()
    {
        SmoothOscillator(StochResultsLoh45000, 45000);
    }

    [Benchmark]
    public void SmoothOscillatorLohArrayPool()
    {
        SmoothOscillator(StochResultsLoh44999, 45000);
    }

    [Benchmark]
    public void SmoothOscillatorNewCtr()
    {
        SmoothOscillator(StochResults3000, 3000);
    }

    [Benchmark]
    public void SmoothOscillatorArrayPool()
    {
        SmoothOscillator(StochResults2999, 3000);
    }

    private static void SmoothOscillator(List<StochResult> stochResults, int arrayPoolTreshold) =>
        StochInidcator.SmoothOscillator(
            stochResults,
            stochResults.Count,
            SRsiSettingsConst.SRsiSettingsDefault.ChannelLength,
            SRsiSettingsConst.SRsiSettingsDefault.StochDSmooth,
            MaType.SMA,
            arrayPoolTreshold
        );

    private static List<StochResult> GetRandomStochResults(int count) =>
        Enumerable
            .Range(0, count)
            .Select(
                i =>
                    new StochResult(DateTime.Now.AddMinutes(i * 5))
                    {
                        Oscillator = GetRandomTestDecimal()
                    }
            )
            .ToList();

    private static decimal GetRandomTestDecimal() => GetRandomDecimal(new Random(), 4.0M, 4.9M);

    private static decimal GetRandomDecimal(Random random, decimal minValue, decimal maxValue)
    {
        var range = (double)(maxValue - minValue);
        var sample = random.NextDouble();
        var scaled = (sample * range) + (double)minValue;
        return (decimal)scaled;
    }
}
