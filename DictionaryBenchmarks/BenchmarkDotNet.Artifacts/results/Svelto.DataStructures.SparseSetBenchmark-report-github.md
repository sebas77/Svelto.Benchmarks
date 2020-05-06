``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17134.706 (1803/April2018Update/Redstone4)
Intel Core i7-6700K CPU 4.00GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
Frequency=3914062 Hz, Resolution=255.4891 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3394.0
  Job-BAGQKI : Mono 5.18.1 (Visual Studio), 64bit 

Runtime=Mono  MaxIterationCount=2  MaxWarmupIterationCount=2  
MinIterationCount=1  MinWarmupIterationCount=1  

```
|           Method |     Mean | Error |   StdDev | Ratio |
|----------------- |---------:|------:|---------:|------:|
|        GetRandom | 570.3 ms |    NA | 6.370 ms |  1.00 |
|  FasterGetRandom | 200.4 ms |    NA | 2.869 ms |  0.35 |
| FastestGetRandom | 117.6 ms |    NA | 2.205 ms |  0.21 |
