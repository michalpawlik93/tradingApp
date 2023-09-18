using System.Diagnostics.CodeAnalysis;

namespace TradingApp.Evaluator.Utils;

[ExcludeFromCodeCoverage]
public static class Pruning
{
    public static List<T> Remove<T>(this IEnumerable<T> series, int removePeriods)
    {
        List<T> seriesList = series.ToList();

        if (seriesList.Count <= removePeriods)
        {
            return new List<T>();
        }
        else
        {
            if (removePeriods > 0)
            {
                for (int i = 0; i < removePeriods; i++)
                {
                    seriesList.RemoveAt(0);
                }
            }

            return seriesList;
        }
    }
}
