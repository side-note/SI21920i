``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.8 (4.8.4075.0), X86 LegacyJIT  [AttachedDebugger]

Job=InProcess  Toolchain=InProcessEmitToolchain  IterationCount=15  
LaunchCount=1  WarmupCount=10  

```
|                               Method |     Mean |    Error |   StdDev |   Median | Rank |
|------------------------------------- |---------:|---------:|---------:|---------:|-----:|
|     EFCreateUpdateAndRemovePortfolio | 301.5 ms | 137.5 ms | 121.9 ms | 348.4 ms |    2 |
| ADONETCreateUpdateAndRemovePortfolio | 211.4 ms | 111.9 ms | 104.7 ms | 146.4 ms |    1 |
