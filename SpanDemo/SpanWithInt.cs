using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace SpanDemo
{
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [RPlotExporter]
    public class SpanWithInt
    {
        [Params(100, 100_000)]
        public int Size { get; set; }
        private readonly Random _random = new Random(420);
        private List<int> _items;

        [GlobalSetup]
        public void Setup()
        {
            _items = Enumerable.Range(1, 100).Select(_ => _random.Next()).ToList();
        }

        [Benchmark]
        public void For()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
            }
        }

        [Benchmark]
        public void Foreach()
        {
            foreach (var item1 in _items)
            {
                
            }
        }

        [Benchmark]
        public void ForeachLinq()
        {
            _items.ForEach(c => { });
        }

        [Benchmark]
        public void ParallelForeachLinq()
        {
            Parallel.ForEach(_items, c => { });
        }

        [Benchmark]
        public void Span()
        {
            foreach (var item in CollectionsMarshal.AsSpan(_items)) 
            {
                
            }
        }
    }
}
