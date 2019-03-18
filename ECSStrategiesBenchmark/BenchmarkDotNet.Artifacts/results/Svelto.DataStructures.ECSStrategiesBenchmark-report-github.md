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
| SeparateArraysStrategyReal |  4.864 ms | 0.0481 ms | 0.0427 ms |
|          SparseSetStrategy |  4.336 ms | 0.1142 ms | 0.1012 ms |
|         SparseHashStrategy | 15.366 ms | 0.3371 ms | 0.5347 ms |
|               CopyStrategy |  9.399 ms | 0.1770 ms | 0.1817 ms |
|                            |           |           |           |
|     SeparateArraysStrategy |  4.284 ms | 0.0231 ms | 0.0205 ms |
|      SparseSetStrategyReal |  4.928 ms | 0.0492 ms | 0.0461 ms |
|     SparseHashStrategyReal | 15.661 ms | 0.1962 ms | 0.1835 ms |
|           CopyStrategyReal | 10.330 ms | 0.2009 ms | 0.2816 ms |
