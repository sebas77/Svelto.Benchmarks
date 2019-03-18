``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17134.590 (1803/April2018Update/Redstone4)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3906246 Hz, Resolution=256.0003 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Job-BAGQKI : Mono 5.18.0 (Visual Studio), 64bit 

Runtime=Mono  MaxIterationCount=2  MaxWarmupIterationCount=2  
MinIterationCount=1  MinWarmupIterationCount=1  

```
|         Method |      Mean | Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|------:|----------:|------:|--------:|
| StandardInsert |  8.002 ms |    NA | 0.0183 ms |  1.00 |    0.00 |
|      NewInsert |  5.441 ms |    NA | 0.1194 ms |  0.68 |    0.02 |
|     NewInsert2 | 12.765 ms |    NA | 0.0237 ms |  1.60 |    0.00 |
