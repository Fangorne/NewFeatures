using BenchmarkDotNet.Running;
using SpanDemo;

BenchmarkRunner.Run<SpanWithInt>();

//BenchmarkRunner.Run<SpanVsString>();

Console.ReadLine();
