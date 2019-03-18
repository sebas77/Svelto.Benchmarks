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
| SeparateArraysStrategyReal |  4.816 ms | 0.0279 ms | 0.0261 ms |
|      SparseSetStrategyReal |  4.854 ms | 0.0383 ms | 0.0358 ms |
|     SparseHashStrategyReal | 15.628 ms | 0.2239 ms | 0.1985 ms |
|           CopyStrategyReal |  9.967 ms | 0.0708 ms | 0.0662 ms |
|                            |           |           |           |
|     SeparateArraysStrategy |  4.276 ms | 0.0312 ms | 0.0292 ms |
|          SparseSetStrategy |  4.266 ms | 0.0355 ms | 0.0332 ms |
|         SparseHashStrategy | 15.323 ms | 0.2899 ms | 0.2847 ms |
|               CopyStrategy |  9.156 ms | 0.0859 ms | 0.0803 ms |
