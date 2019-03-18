``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17134.648 (1803/April2018Update/Redstone4)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3906253 Hz, Resolution=255.9998 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
  Job-FQKGZY : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
  Job-YTBJBT : .NET Core 3.0.0-preview3-27503-5 (CoreCLR 4.6.27422.72, CoreFX 4.7.19.12807), 64bit RyuJIT
  Job-KXRVUO : .NET CoreRT 1.0.27614.0 @BuiltBy: smandala-SEB-PC @Branch: master @Commit: 7e9da08c0f45d4661a8b2daddcf14932525de05c, 64bit AOT
  Job-XIDDXQ : Mono 5.18.0 (Visual Studio), 64bit 


```
|                                Method | Runtime |                    Toolchain |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|-------------------------------------- |-------- |----------------------------- |----------:|----------:|----------:|----------:|------:|--------:|
|                   StandardArrayInsert |     Clr |                       net472 |  4.779 ms | 0.1029 ms | 0.1540 ms |  4.732 ms |  0.79 |    0.03 |
|                   StandardArrayInsert |    Core |                netcoreapp3.0 |  4.862 ms | 0.0652 ms | 0.0578 ms |  4.860 ms |  0.79 |    0.01 |
|                   StandardArrayInsert |  CoreRT | Core RT 1.0.0-alpha-27515-01 |  4.689 ms | 0.0452 ms | 0.0378 ms |  4.688 ms |  0.77 |    0.01 |
|                   StandardArrayInsert |    Mono |                      Default |  6.118 ms | 0.0473 ms | 0.0442 ms |  6.108 ms |  1.00 |    0.00 |
|                                       |         |                              |           |           |           |           |       |         |
|          StandardUnmanagedArrayInsert |     Clr |                       net472 |  4.505 ms | 0.0894 ms | 0.0878 ms |  4.509 ms |     ? |       ? |
|          StandardUnmanagedArrayInsert |    Core |                netcoreapp3.0 |  4.510 ms | 0.0614 ms | 0.0575 ms |  4.503 ms |     ? |       ? |
|          StandardUnmanagedArrayInsert |  CoreRT | Core RT 1.0.0-alpha-27515-01 |  4.462 ms | 0.0380 ms | 0.0356 ms |  4.463 ms |     ? |       ? |
|          StandardUnmanagedArrayInsert |    Mono |                      Default |  4.955 ms | 0.0395 ms | 0.0369 ms |  4.944 ms |     ? |       ? |
|                                       |         |                              |           |           |           |           |       |         |
|             StandardPinnedArrayInsert |     Clr |                       net472 |  4.679 ms | 0.0934 ms | 0.2344 ms |  4.607 ms |     ? |       ? |
|             StandardPinnedArrayInsert |    Core |                netcoreapp3.0 |  4.640 ms | 0.0928 ms | 0.2362 ms |  4.528 ms |     ? |       ? |
|             StandardPinnedArrayInsert |  CoreRT | Core RT 1.0.0-alpha-27515-01 |  4.480 ms | 0.0382 ms | 0.0319 ms |  4.484 ms |     ? |       ? |
|             StandardPinnedArrayInsert |    Mono |                      Default |  4.946 ms | 0.0508 ms | 0.0475 ms |  4.928 ms |     ? |       ? |
|                                       |         |                              |           |           |           |           |       |         |
|                    StandardListInsert |     Clr |                       net472 | 16.397 ms | 0.3234 ms | 0.3460 ms | 16.340 ms |     ? |       ? |
|                    StandardListInsert |    Core |                netcoreapp3.0 | 16.004 ms | 0.0976 ms | 0.0913 ms | 16.023 ms |     ? |       ? |
|                    StandardListInsert |  CoreRT | Core RT 1.0.0-alpha-27515-01 | 15.917 ms | 0.1226 ms | 0.1147 ms | 15.945 ms |     ? |       ? |
|                    StandardListInsert |    Mono |                      Default | 29.634 ms | 0.2573 ms | 0.2407 ms | 29.690 ms |     ? |       ? |
|                                       |         |                              |           |           |           |           |       |         |
|             StandardManagedSpanInsert |     Clr |                       net472 |  7.685 ms | 0.0998 ms | 0.0933 ms |  7.692 ms |     ? |       ? |
|             StandardManagedSpanInsert |    Core |                netcoreapp3.0 |  4.928 ms | 0.0246 ms | 0.0230 ms |  4.932 ms |     ? |       ? |
|             StandardManagedSpanInsert |  CoreRT | Core RT 1.0.0-alpha-27515-01 |  4.902 ms | 0.0257 ms | 0.0215 ms |  4.893 ms |     ? |       ? |
|             StandardManagedSpanInsert |    Mono |                      Default | 14.000 ms | 0.1209 ms | 0.1131 ms | 14.007 ms |     ? |       ? |
|                                       |         |                              |           |           |           |           |       |         |
|           StandardUnmanagedSpanInsert |     Clr |                       net472 |  7.706 ms | 0.0454 ms | 0.0402 ms |  7.718 ms |     ? |       ? |
|           StandardUnmanagedSpanInsert |    Core |                netcoreapp3.0 |  4.963 ms | 0.0190 ms | 0.0177 ms |  4.961 ms |     ? |       ? |
|           StandardUnmanagedSpanInsert |  CoreRT | Core RT 1.0.0-alpha-27515-01 |  4.915 ms | 0.0273 ms | 0.0255 ms |  4.922 ms |     ? |       ? |
|           StandardUnmanagedSpanInsert |    Mono |                      Default | 12.672 ms | 0.0375 ms | 0.0351 ms | 12.681 ms |     ? |       ? |
|                                       |         |                              |           |           |           |           |       |         |
|        StandardUnmanagedSpanBenInsert |     Clr |                       net472 |  4.888 ms | 0.0245 ms | 0.0217 ms |  4.892 ms |     ? |       ? |
|        StandardUnmanagedSpanBenInsert |    Core |                netcoreapp3.0 |  4.907 ms | 0.0165 ms | 0.0154 ms |  4.901 ms |     ? |       ? |
|        StandardUnmanagedSpanBenInsert |  CoreRT | Core RT 1.0.0-alpha-27515-01 |  4.954 ms | 0.0250 ms | 0.0234 ms |  4.953 ms |     ? |       ? |
|        StandardUnmanagedSpanBenInsert |    Mono |                      Default |  5.173 ms | 0.0543 ms | 0.0508 ms |  5.170 ms |     ? |       ? |
|                                       |         |                              |           |           |           |           |       |         |
|            StandardArrayInsertChecked |     Clr |                       net472 |  9.943 ms | 0.1525 ms | 0.1273 ms | 10.002 ms |     ? |       ? |
|            StandardArrayInsertChecked |    Core |                netcoreapp3.0 |  9.884 ms | 0.0557 ms | 0.0521 ms |  9.894 ms |     ? |       ? |
|            StandardArrayInsertChecked |  CoreRT | Core RT 1.0.0-alpha-27515-01 |  9.963 ms | 0.1039 ms | 0.0972 ms |  9.974 ms |     ? |       ? |
|            StandardArrayInsertChecked |    Mono |                      Default |  9.864 ms | 0.0782 ms | 0.0732 ms |  9.840 ms |     ? |       ? |
|                                       |         |                              |           |           |           |           |       |         |
|   StandardUnmanagedArrayInsertChecked |     Clr |                       net472 |  9.965 ms | 0.1772 ms | 0.1657 ms |  9.952 ms |     ? |       ? |
|   StandardUnmanagedArrayInsertChecked |    Core |                netcoreapp3.0 |  9.856 ms | 0.0739 ms | 0.0655 ms |  9.862 ms |     ? |       ? |
|   StandardUnmanagedArrayInsertChecked |  CoreRT | Core RT 1.0.0-alpha-27515-01 | 10.118 ms | 0.3387 ms | 0.5752 ms |  9.794 ms |     ? |       ? |
|   StandardUnmanagedArrayInsertChecked |    Mono |                      Default |  9.693 ms | 0.0613 ms | 0.0512 ms |  9.690 ms |     ? |       ? |
|                                       |         |                              |           |           |           |           |       |         |
|      StandardPinnedArrayInsertChecked |     Clr |                       net472 | 10.447 ms | 0.2051 ms | 0.4097 ms | 10.488 ms |     ? |       ? |
|      StandardPinnedArrayInsertChecked |    Core |                netcoreapp3.0 |  9.855 ms | 0.0731 ms | 0.0684 ms |  9.819 ms |     ? |       ? |
|      StandardPinnedArrayInsertChecked |  CoreRT | Core RT 1.0.0-alpha-27515-01 |  9.992 ms | 0.1168 ms | 0.1093 ms |  9.984 ms |     ? |       ? |
|      StandardPinnedArrayInsertChecked |    Mono |                      Default |  9.860 ms | 0.0704 ms | 0.0658 ms |  9.883 ms |     ? |       ? |
|                                       |         |                              |           |           |           |           |       |         |
|             StandardListInsertChecked |     Clr |                       net472 | 13.905 ms | 0.2169 ms | 0.2029 ms | 13.912 ms |     ? |       ? |
|             StandardListInsertChecked |    Core |                netcoreapp3.0 | 13.424 ms | 0.0504 ms | 0.0394 ms | 13.419 ms |     ? |       ? |
|             StandardListInsertChecked |  CoreRT | Core RT 1.0.0-alpha-27515-01 | 13.451 ms | 0.2548 ms | 0.2384 ms | 13.400 ms |     ? |       ? |
|             StandardListInsertChecked |    Mono |                      Default | 14.111 ms | 0.0390 ms | 0.0304 ms | 14.108 ms |     ? |       ? |
|                                       |         |                              |           |           |           |           |       |         |
|      StandardManagedSpanInsertChecked |     Clr |                       net472 | 10.327 ms | 0.2021 ms | 0.2963 ms | 10.350 ms |     ? |       ? |
|      StandardManagedSpanInsertChecked |    Core |                netcoreapp3.0 |  9.921 ms | 0.0489 ms | 0.0457 ms |  9.914 ms |     ? |       ? |
|      StandardManagedSpanInsertChecked |  CoreRT | Core RT 1.0.0-alpha-27515-01 |  9.922 ms | 0.1527 ms | 0.1428 ms |  9.922 ms |     ? |       ? |
|      StandardManagedSpanInsertChecked |    Mono |                      Default | 10.040 ms | 0.0862 ms | 0.0807 ms | 10.053 ms |     ? |       ? |
|                                       |         |                              |           |           |           |           |       |         |
|    StandardUnmanagedSpanInsertChecked |     Clr |                       net472 |  9.934 ms | 0.2766 ms | 0.2840 ms |  9.799 ms |     ? |       ? |
|    StandardUnmanagedSpanInsertChecked |    Core |                netcoreapp3.0 |  9.871 ms | 0.0702 ms | 0.0622 ms |  9.883 ms |     ? |       ? |
|    StandardUnmanagedSpanInsertChecked |  CoreRT | Core RT 1.0.0-alpha-27515-01 |  9.844 ms | 0.0872 ms | 0.0815 ms |  9.829 ms |     ? |       ? |
|    StandardUnmanagedSpanInsertChecked |    Mono |                      Default |  9.766 ms | 0.0812 ms | 0.0760 ms |  9.769 ms |     ? |       ? |
|                                       |         |                              |           |           |           |           |       |         |
| StandardUnmanagedSpanBenInsertChecked |     Clr |                       net472 | 10.697 ms | 0.2134 ms | 0.5546 ms | 10.560 ms |     ? |       ? |
| StandardUnmanagedSpanBenInsertChecked |    Core |                netcoreapp3.0 |  9.930 ms | 0.1229 ms | 0.1150 ms |  9.921 ms |     ? |       ? |
| StandardUnmanagedSpanBenInsertChecked |  CoreRT | Core RT 1.0.0-alpha-27515-01 |  9.910 ms | 0.1249 ms | 0.1168 ms |  9.876 ms |     ? |       ? |
| StandardUnmanagedSpanBenInsertChecked |    Mono |                      Default | 10.262 ms | 0.3184 ms | 0.5406 ms |  9.980 ms |     ? |       ? |
