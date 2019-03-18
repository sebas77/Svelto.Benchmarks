``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17134.648 (1803/April2018Update/Redstone4)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3906253 Hz, Resolution=255.9998 ns, Timer=TSC
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
  Mono   : Mono 5.18.0 (Visual Studio), 64bit 

Job=Mono  Runtime=Mono  

```
|                     Method |      Mean |     Error |    StdDev |
|--------------------------- |----------:|----------:|----------:|
| SeparateArraysStrategyReal |  4.909 ms | 0.0567 ms | 0.0531 ms |
|          SparseSetStrategy |  4.381 ms | 0.0399 ms | 0.0373 ms |
|               CopyStrategy |  9.601 ms | 0.1148 ms | 0.1074 ms |
|                            |           |           |           |
|     SeparateArraysStrategy |  4.363 ms | 0.0624 ms | 0.0584 ms |
|      SparseSetStrategyReal |  4.968 ms | 0.0454 ms | 0.0402 ms |
|           CopyStrategyReal | 10.372 ms | 0.0701 ms | 0.0621 ms |
