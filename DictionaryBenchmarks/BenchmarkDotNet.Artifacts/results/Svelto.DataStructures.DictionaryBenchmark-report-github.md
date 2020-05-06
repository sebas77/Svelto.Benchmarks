``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17134.706 (1803/April2018Update/Redstone4)
Intel Core i7-6700K CPU 4.00GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
Frequency=3914062 Hz, Resolution=255.4891 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3394.0
  Job-RMJDYC : Mono 5.18.1 (Visual Studio), 64bit 
  Job-BAGQKI : Mono 5.18.1 (Visual Studio), 64bit 

Runtime=Mono  InvocationCount=1  MaxIterationCount=2  
MaxWarmupIterationCount=2  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                      Method | UnrollFactor |         Mean | Error |     StdDev | Ratio | RatioSD |
|---------------------------- |------------- |-------------:|------:|-----------:|------:|--------:|
|             InsertFromEmtpy |            1 |   286.206 ms |    NA |  2.9641 ms |  1.00 |    0.00 |
|       FasterInsertFromEmtpy |            1 |   459.875 ms |    NA |  0.1850 ms |  1.61 |    0.02 |
|                             |              |              |       |            |       |         |
|       LinearInsertFromEmtpy |            1 |   281.951 ms |    NA |  0.4236 ms |  1.00 |    0.00 |
| FasterLinearInsertFromEmtpy |            1 |   433.098 ms |    NA |  2.8866 ms |  1.54 |    0.01 |
|                             |              |              |       |            |       |         |
|                RandomInsert |           16 | 1,120.365 ms |    NA |  9.3169 ms |  1.00 |    0.00 |
|          FasterRandomInsert |           16 | 1,114.380 ms |    NA |  1.8275 ms |  0.99 |    0.01 |
|                             |              |              |       |            |       |         |
|                LinearInsert |           16 |   152.517 ms |    NA |  0.2723 ms |  1.00 |    0.00 |
|          FasterLinearInsert |           16 |    97.888 ms |    NA |  0.3417 ms |  0.64 |    0.00 |
|                             |              |              |       |            |       |         |
|                   GetRandom |           16 |   813.641 ms |    NA | 11.2765 ms |  1.00 |    0.00 |
|             FasterGetRandom |           16 |   792.834 ms |    NA |  2.5233 ms |  0.97 |    0.01 |
|                             |              |              |       |            |       |         |
|                         Get |           16 |   130.583 ms |    NA |  0.0515 ms |  1.00 |    0.00 |
|                   FasterGet |           16 |    81.486 ms |    NA |  0.1252 ms |  0.62 |    0.00 |
|                             |              |              |       |            |       |         |
|                RemoveRandom |           16 |   268.732 ms |    NA |  4.5748 ms |  1.00 |    0.00 |
|          FasterRemoveRandom |           16 |   269.897 ms |    NA |  0.9741 ms |  1.00 |    0.01 |
|                             |              |              |       |            |       |         |
|                      Remove |           16 |    54.190 ms |    NA |  0.2187 ms |  1.00 |    0.00 |
|                FasterRemove |           16 |    39.370 ms |    NA |  0.0760 ms |  0.73 |    0.00 |
|                             |              |              |       |            |       |         |
|               IterateValues |           16 |    55.587 ms |    NA |  0.9179 ms |  1.00 |    0.00 |
|         FasterIterateValues |           16 |     8.826 ms |    NA |  0.1084 ms |  0.16 |    0.00 |
