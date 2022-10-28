// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using SpanDemo;

var summary = BenchmarkRunner.Run<SpanVsString>();
Console.ReadLine();

Console.WriteLine("Hello, World!");