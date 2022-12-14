using BenchmarkDotNet.Attributes;

namespace SpanDemo;

public class SpanVsString
{
    #region fields

    private readonly string _text;

    #endregion

    #region constructors

    public SpanVsString()
    {
        using StreamReader streamReader = File.OpenText("le-petit-prince--antoine-de-saint-exupery.txt");
        _text = streamReader.ReadToEnd();
    }

    #endregion

    #region

    [Benchmark]
    public void ParseWithString()
    {
        var indexPrev = 0;
        var indexCurrent = 0;
        var rowNum = 0;
        foreach (var c in _text)
        {
            if (c == '\n')
            {
                indexCurrent += 1;
                var line = _text.Substring(indexPrev, indexCurrent - indexPrev);
                if (line.Equals("\n"))
                    rowNum++;
                indexPrev = indexCurrent;
                continue;
            }

            indexCurrent++;
        }
    }

    [Benchmark]
    public void ParseWithSpan()
    {
        var spanText = _text.AsSpan();
        var indexPrev = 0;
        var indexCurrent = 0;
        var rowNum = 0;
        foreach (var c in spanText)
        {
            if (c == '\n')
            {
                indexCurrent += 1;
                var slice = spanText.Slice(indexPrev, indexCurrent - indexPrev);
                if (slice.Equals("\n", StringComparison.OrdinalIgnoreCase))
                    rowNum++;
                indexPrev = indexCurrent;
                continue;
            }

            indexCurrent++;
        }
    }

    #endregion
}