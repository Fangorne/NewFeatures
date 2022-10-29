using BenchmarkDotNet.Running;
using SpanDemo;

var summary1 = BenchmarkRunner.Run<SpanWithInt>();

var summary2 = BenchmarkRunner.Run<SpanVsString>();

Console.ReadLine();
