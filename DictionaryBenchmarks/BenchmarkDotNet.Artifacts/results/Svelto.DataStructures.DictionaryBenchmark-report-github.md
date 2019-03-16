``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17134.648 (1803/April2018Update/Redstone4)
Intel Core i7-6700K CPU 4.00GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
Frequency=3914059 Hz, Resolution=255.4893 ns, Timer=TSC
  [Host]    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
  Mono      : Mono 5.18.1 (Visual Studio), 64bit 
  RyuJitX64 : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0

Jit=RyuJit  Platform=X64  InvocationCount=1  

```
|                      Method |       Job | Runtime | UnrollFactor |         Mean |      Error |     StdDev |       Median | Ratio | RatioSD |
|---------------------------- |---------- |-------- |------------- |-------------:|-----------:|-----------:|-------------:|------:|--------:|
|                RandomInsert |      Mono |    Mono |           16 | 1,157.051 ms |  9.8378 ms |  9.2023 ms | 1,155.453 ms |  1.00 |    0.00 |
|          FasterRandomInsert |      Mono |    Mono |           16 | 1,144.384 ms | 19.1445 ms | 17.9078 ms | 1,143.439 ms |  0.99 |    0.01 |
|                             |           |         |              |              |            |            |              |       |         |
|                LinearInsert |      Mono |    Mono |           16 |   155.294 ms |  4.4085 ms |  8.9054 ms |   151.972 ms |  1.00 |    0.00 |
|          FasterLinearInsert |      Mono |    Mono |           16 |   112.635 ms |  1.5178 ms |  1.3455 ms |   111.921 ms |  0.70 |    0.05 |
|                             |           |         |              |              |            |            |              |       |         |
|                   GetRandom |      Mono |    Mono |           16 |   814.317 ms |  9.0403 ms |  7.5491 ms |   815.993 ms |  1.00 |    0.00 |
|             FasterGetRandom |      Mono |    Mono |           16 |   804.241 ms |  6.9576 ms |  5.8099 ms |   805.618 ms |  0.99 |    0.01 |
|                             |           |         |              |              |            |            |              |       |         |
|                         Get |      Mono |    Mono |           16 |   130.000 ms |  1.0300 ms |  0.9634 ms |   130.121 ms |  1.00 |    0.00 |
|                   FasterGet |      Mono |    Mono |           16 |    84.981 ms |  1.3049 ms |  1.2206 ms |    84.844 ms |  0.65 |    0.01 |
|                             |           |         |              |              |            |            |              |       |         |
|                RemoveRandom |      Mono |    Mono |           16 |   264.220 ms |  3.4297 ms |  3.2081 ms |   263.359 ms |  1.00 |    0.00 |
|          FasterRemoveRandom |      Mono |    Mono |           16 |   276.978 ms |  5.4150 ms |  9.1950 ms |   273.573 ms |  1.06 |    0.05 |
|                             |           |         |              |              |            |            |              |       |         |
|                      Remove |      Mono |    Mono |           16 |    54.172 ms |  0.9387 ms |  0.7329 ms |    54.088 ms |  1.00 |    0.00 |
|                FasterRemove |      Mono |    Mono |           16 |    41.371 ms |  0.5023 ms |  0.4699 ms |    41.177 ms |  0.76 |    0.01 |
|                             |           |         |              |              |            |            |              |       |         |
|               IterateValues |      Mono |    Mono |           16 |    56.544 ms |  0.6377 ms |  0.5965 ms |    56.276 ms |  1.00 |    0.00 |
|         FasterIterateValues |      Mono |    Mono |           16 |     8.841 ms |  0.1303 ms |  0.1155 ms |     8.821 ms |  0.16 |    0.00 |
|                             |           |         |              |              |            |            |              |       |         |
|             InsertFromEmtpy |      Mono |    Mono |            1 |   288.754 ms |  6.8444 ms |  7.6076 ms |   285.830 ms |  1.00 |    0.00 |
|       FasterInsertFromEmtpy |      Mono |    Mono |            1 |   435.804 ms |  8.5576 ms |  8.0048 ms |   433.467 ms |  1.51 |    0.05 |
|                             |           |         |              |              |            |            |              |       |         |
|       LinearInsertFromEmtpy |      Mono |    Mono |            1 |   278.482 ms |  1.5456 ms |  1.3702 ms |   278.076 ms |  1.00 |    0.00 |
| FasterLinearInsertFromEmtpy |      Mono |    Mono |            1 |   408.326 ms |  5.3396 ms |  4.9946 ms |   406.638 ms |  1.47 |    0.02 |
|                             |           |         |              |              |            |            |              |       |         |
|                RandomInsert | RyuJitX64 |     Clr |           16 | 1,068.786 ms | 10.4863 ms |  8.7565 ms | 1,066.500 ms |  1.00 |    0.00 |
|          FasterRandomInsert | RyuJitX64 |     Clr |           16 | 1,032.400 ms | 20.4896 ms | 28.7236 ms | 1,022.684 ms |  0.96 |    0.03 |
|                             |           |         |              |              |            |            |              |       |         |
|                LinearInsert | RyuJitX64 |     Clr |           16 |   114.405 ms |  1.5125 ms |  1.2630 ms |   114.450 ms |  1.00 |    0.00 |
|          FasterLinearInsert | RyuJitX64 |     Clr |           16 |    48.417 ms |  0.4309 ms |  0.3819 ms |    48.316 ms |  0.42 |    0.01 |
|                             |           |         |              |              |            |            |              |       |         |
|                   GetRandom | RyuJitX64 |     Clr |           16 |   750.068 ms | 16.9162 ms | 18.1002 ms |   746.399 ms |  1.00 |    0.00 |
|             FasterGetRandom | RyuJitX64 |     Clr |           16 |   549.622 ms | 10.9352 ms | 22.5830 ms |   544.079 ms |  0.75 |    0.04 |
|                             |           |         |              |              |            |            |              |       |         |
|                         Get | RyuJitX64 |     Clr |           16 |    92.497 ms |  1.8407 ms |  2.3934 ms |    92.189 ms |  1.00 |    0.00 |
|                   FasterGet | RyuJitX64 |     Clr |           16 |    48.474 ms |  1.1534 ms |  3.3461 ms |    46.844 ms |  0.51 |    0.03 |
|                             |           |         |              |              |            |            |              |       |         |
|                RemoveRandom | RyuJitX64 |     Clr |           16 |   239.423 ms |  4.9032 ms |  8.4578 ms |   235.712 ms |  1.00 |    0.00 |
|          FasterRemoveRandom | RyuJitX64 |     Clr |           16 |   220.601 ms | 11.0386 ms | 32.2000 ms |   207.192 ms |  0.90 |    0.13 |
|                             |           |         |              |              |            |            |              |       |         |
|                      Remove | RyuJitX64 |     Clr |           16 |    65.519 ms |  1.7428 ms |  4.9723 ms |    63.130 ms |  1.00 |    0.00 |
|                FasterRemove | RyuJitX64 |     Clr |           16 |    27.785 ms |  0.2351 ms |  0.1963 ms |    27.778 ms |  0.41 |    0.04 |
|                             |           |         |              |              |            |            |              |       |         |
|               IterateValues | RyuJitX64 |     Clr |           16 |    28.598 ms |  0.4229 ms |  0.3531 ms |    28.461 ms |  1.00 |    0.00 |
|         FasterIterateValues | RyuJitX64 |     Clr |           16 |     5.867 ms |  0.1138 ms |  0.1118 ms |     5.837 ms |  0.20 |    0.01 |
|                             |           |         |              |              |            |            |              |       |         |
|             InsertFromEmtpy | RyuJitX64 |     Clr |            1 |   319.075 ms |  5.9402 ms |  5.5564 ms |   319.257 ms |  1.00 |    0.00 |
|       FasterInsertFromEmtpy | RyuJitX64 |     Clr |            1 |   343.699 ms |  6.7956 ms | 10.9736 ms |   343.752 ms |  1.08 |    0.03 |
|                             |           |         |              |              |            |            |              |       |         |
|       LinearInsertFromEmtpy | RyuJitX64 |     Clr |            1 |   320.576 ms |  6.1983 ms |  6.8894 ms |   319.185 ms |  1.00 |    0.00 |
| FasterLinearInsertFromEmtpy | RyuJitX64 |     Clr |            1 |   319.072 ms |  6.3039 ms | 10.3576 ms |   321.658 ms |  0.99 |    0.04 |
