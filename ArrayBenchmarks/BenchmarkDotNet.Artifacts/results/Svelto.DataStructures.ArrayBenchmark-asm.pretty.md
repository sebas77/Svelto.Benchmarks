## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardArrayInsert()
                   for (int i = 0; i < managedArray.Length; i++) managedArray[i] = i;
                 ^^^^^^^^^
       xor     eax,eax
                   for (int i = 0; i < managedArray.Length; i++) managedArray[i] = i;
                            ^^^^^^^^^^^^^^^^^^^^^^^
       mov     rdx,qword ptr [rcx+8]
       mov     ecx,dword ptr [rdx+8]
       test    ecx,ecx
       jle     M00_L01
                   for (int i = 0; i < managedArray.Length; i++) managedArray[i] = i;
                                                          ^^^^^^^^^^^^^^^^^^^^
M00_L00:
       mov     r8,rdx
       movsxd  r9,eax
       mov     dword ptr [r8+r9*4+10h],eax
                   for (int i = 0; i < managedArray.Length; i++) managedArray[i] = i;
                                                     ^^^
       inc     eax
       cmp     ecx,eax
       jg      M00_L00
               }
        ^
M00_L01:
       ret
; Total bytes of code 31
```

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardArrayInsertChecked()
                   for (int i = 0; i < DictionarySize; i++) 
                 ^^^^^^^^^
       xor     eax,eax
       mov     rdx,qword ptr [rcx+8]
       mov     rcx,qword ptr [rcx+20h]
                          managedArray[RandomAccess[i]] = i;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M00_L00:
       mov     r8,rdx
       mov     r9,rcx
       cmp     eax,dword ptr [r9+8]
       jae     00007ffc`37769bde
       movsxd  r10,eax
       mov     r9d,dword ptr [r9+r10*4+10h]
       cmp     r9d,dword ptr [r8+8]
       jae     00007ffc`37769bde
       movsxd  r9,r9d
       mov     dword ptr [r8+r9*4+10h],eax
                   for (int i = 0; i < DictionarySize; i++) 
                                                ^^^
       inc     eax
                   for (int i = 0; i < DictionarySize; i++) 
                            ^^^^^^^^^^^^^^^^^^
       cmp     eax,989680h
       jl      M00_L00
               }
        ^
       add     rsp,28h
; Total bytes of code 57
```

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardUnmanagedArrayInsert()
                   for (int i = 0; i < DictionarySize; i++) unmanagedArray[i] = i;
                 ^^^^^^^^^
       xor     eax,eax
                   for (int i = 0; i < DictionarySize; i++) unmanagedArray[i] = i;
                                                     ^^^^^^^^^^^^^^^^^^^^^^
M00_L00:
       mov     rdx,qword ptr [rcx+28h]
       movsxd  r8,eax
       mov     dword ptr [rdx+r8*4],eax
                   for (int i = 0; i < DictionarySize; i++) unmanagedArray[i] = i;
                                                ^^^
       inc     eax
                   for (int i = 0; i < DictionarySize; i++) unmanagedArray[i] = i;
                            ^^^^^^^^^^^^^^^^^^
       cmp     eax,989680h
       jl      M00_L00
               }
        ^
       ret
; Total bytes of code 23
```

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardUnmanagedArrayInsertChecked()
                   for (int i = 0; i < DictionarySize; i++) unmanagedArray[RandomAccess[i] ] = i;
                 ^^^^^^^^^
       xor     eax,eax
                   for (int i = 0; i < DictionarySize; i++) unmanagedArray[RandomAccess[i] ] = i;
                                                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M00_L00:
       mov     rdx,qword ptr [rcx+28h]
       mov     r8,qword ptr [rcx+20h]
       cmp     eax,dword ptr [r8+8]
       jae     00007ffc`37779bd1
       movsxd  r9,eax
       mov     r8d,dword ptr [r8+r9*4+10h]
       movsxd  r8,r8d
       mov     dword ptr [rdx+r8*4],eax
                   for (int i = 0; i < DictionarySize; i++) unmanagedArray[RandomAccess[i] ] = i;
                                                ^^^
       inc     eax
                   for (int i = 0; i < DictionarySize; i++) unmanagedArray[RandomAccess[i] ] = i;
                            ^^^^^^^^^^^^^^^^^^
       cmp     eax,989680h
       jl      M00_L00
               }
        ^
       add     rsp,28h
; Total bytes of code 44
```

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardPinnedArrayInsert()
                   for (int i = 0; i < DictionarySize; i++) arrayPinned[i] = i;
                 ^^^^^^^^^
       xor     eax,eax
                   for (int i = 0; i < DictionarySize; i++) arrayPinned[i] = i;
                                                     ^^^^^^^^^^^^^^^^^^^
M00_L00:
       mov     rdx,qword ptr [rcx+30h]
       movsxd  r8,eax
       mov     dword ptr [rdx+r8*4],eax
                   for (int i = 0; i < DictionarySize; i++) arrayPinned[i] = i;
                                                ^^^
       inc     eax
                   for (int i = 0; i < DictionarySize; i++) arrayPinned[i] = i;
                            ^^^^^^^^^^^^^^^^^^
       cmp     eax,989680h
       jl      M00_L00
               }
        ^
       ret
; Total bytes of code 23
```

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardPinnedArrayInsertChecked()
                   for (int i = 0; i < DictionarySize; i++) arrayPinned[RandomAccess[i] ] = i;
                 ^^^^^^^^^
       xor     eax,eax
                   for (int i = 0; i < DictionarySize; i++) arrayPinned[RandomAccess[i] ] = i;
                                                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M00_L00:
       mov     rdx,qword ptr [rcx+30h]
       mov     r8,qword ptr [rcx+20h]
       cmp     eax,dword ptr [r8+8]
       jae     00007ffc`37769bd1
       movsxd  r9,eax
       mov     r8d,dword ptr [r8+r9*4+10h]
       movsxd  r8,r8d
       mov     dword ptr [rdx+r8*4],eax
                   for (int i = 0; i < DictionarySize; i++) arrayPinned[RandomAccess[i] ] = i;
                                                ^^^
       inc     eax
                   for (int i = 0; i < DictionarySize; i++) arrayPinned[RandomAccess[i] ] = i;
                            ^^^^^^^^^^^^^^^^^^
       cmp     eax,989680h
       jl      M00_L00
               }
        ^
       add     rsp,28h
; Total bytes of code 44
```

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardListInsert()
                   for (int i = 0; i < list.Count; i++) list[i] = i;
                 ^^^^^^^^^
       xor     edi,edi
       jmp     M00_L02
                   for (int i = 0; i < list.Count; i++) list[i] = i;
                                                 ^^^^^^^^^^^^
M00_L00:
       mov     rbx,qword ptr [rsi+10h]
       cmp     edi,dword ptr [rbx+18h]
       jb      M00_L01
       mov     ecx,0Dh
       mov     edx,16h
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument, System.ExceptionResource)
M00_L01:
       mov     rax,qword ptr [rbx+8]
       cmp     edi,dword ptr [rax+8]
       jae     00007ffc`37769bec
       movsxd  rdx,edi
       mov     dword ptr [rax+rdx*4+10h],edi
       inc     dword ptr [rbx+1Ch]
                   for (int i = 0; i < list.Count; i++) list[i] = i;
                                            ^^^
       inc     edi
                   for (int i = 0; i < list.Count; i++) list[i] = i;
                            ^^^^^^^^^^^^^^
M00_L02:
       mov     rcx,qword ptr [rsi+10h]
       cmp     edi,dword ptr [rcx+18h]
       jl      M00_L00
               }
        ^
       add     rsp,20h
; Total bytes of code 62
```
```assembly
; System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument, System.ExceptionResource)
       call    System.ThrowHelper.GetArgumentName(System.ExceptionArgument)
       mov     rdi,rax
       mov     ecx,esi
       call    System.ThrowHelper.GetResourceName(System.ExceptionResource)
       mov     rsi,rax
       lea     rcx,[mscorlib_ni+0x717f10
       call    mscorlib_ni+0x43d020
       mov     rbx,rax
       mov     rcx,rsi
       call    System.Environment.GetResourceFromDefault(System.String)
       mov     r8,rax
       mov     rdx,rdi
       mov     rcx,rbx
       call    System.ArgumentOutOfRangeException..ctor(System.String, System.String)
       mov     rcx,rbx
       call    mscorlib_ni+0x43d080
       int     3
       int     3
       int     3
       int     3
       int     3
       int     3
       int     3
       int     3
       push    rdi
       push    rsi
       sub     rsp,28h
       call    System.ThrowHelper.GetResourceName(System.ExceptionResource)
       mov     rsi,rax
       lea     rcx,[mscorlib_ni+0x717a00
; Total bytes of code 92
```

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardListInsertChecked()
                   for (int i = 0; i < DictionarySize; i++) list[RandomAccess[i] ] = i;
                 ^^^^^^^^^
       xor     edi,edi
                   for (int i = 0; i < DictionarySize; i++) list[RandomAccess[i] ] = i;
                                                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^
M00_L00:
       mov     rbx,qword ptr [rsi+10h]
       mov     rcx,qword ptr [rsi+20h]
       cmp     edi,dword ptr [rcx+8]
       jae     00007ffc`37769bfb
       movsxd  rdx,edi
       mov     ebp,dword ptr [rcx+rdx*4+10h]
       cmp     ebp,dword ptr [rbx+18h]
       jb      M00_L01
       mov     ecx,0Dh
       mov     edx,16h
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument, System.ExceptionResource)
M00_L01:
       mov     rax,qword ptr [rbx+8]
       cmp     ebp,dword ptr [rax+8]
       jae     00007ffc`37769bfb
       movsxd  rdx,ebp
       mov     dword ptr [rax+rdx*4+10h],edi
       inc     dword ptr [rbx+1Ch]
                   for (int i = 0; i < DictionarySize; i++) list[RandomAccess[i] ] = i;
                                                ^^^
       inc     edi
                   for (int i = 0; i < DictionarySize; i++) list[RandomAccess[i] ] = i;
                            ^^^^^^^^^^^^^^^^^^
       cmp     edi,989680h
       jl      M00_L00
               }
        ^
       add     rsp,28h
; Total bytes of code 75
```
```assembly
; System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument, System.ExceptionResource)
       call    System.ThrowHelper.GetArgumentName(System.ExceptionArgument)
       mov     rdi,rax
       mov     ecx,esi
       call    System.ThrowHelper.GetResourceName(System.ExceptionResource)
       mov     rsi,rax
       lea     rcx,[mscorlib_ni+0x717f10
       call    mscorlib_ni+0x43d020
       mov     rbx,rax
       mov     rcx,rsi
       call    System.Environment.GetResourceFromDefault(System.String)
       mov     r8,rax
       mov     rdx,rdi
       mov     rcx,rbx
       call    System.ArgumentOutOfRangeException..ctor(System.String, System.String)
       mov     rcx,rbx
       call    mscorlib_ni+0x43d080
       int     3
       int     3
       int     3
       int     3
       int     3
       int     3
       int     3
       int     3
       push    rdi
       push    rsi
       sub     rsp,28h
       call    System.ThrowHelper.GetResourceName(System.ExceptionResource)
       mov     rsi,rax
       lea     rcx,[mscorlib_ni+0x717a00
; Total bytes of code 92
```

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardManagedSpanInsert()
                   var sarray = managedMemory.Span;
            ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     dword ptr [rcx],ecx
       lea     rsi,[rcx+50h]
       cmp     dword ptr [rsi+8],0
       jge     M00_L01
       mov     rdx,qword ptr [rsi]
       mov     rcx,rdx
       test    rcx,rcx
       je      M00_L00
       mov     rax,7FFC3787AA18h
       cmp     qword ptr [rcx],rax
       je      M00_L00
       mov     rcx,rax
       call    clr+0x3d90
       mov     rcx,rax
M00_L00:
       lea     rdx,[rsp+20h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     edx,dword ptr [rsi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rsi+0Ch]
       cmp     edx,dword ptr [rsp+30h]
       ja      M00_L10
       mov     eax,dword ptr [rsp+30h]
       sub     eax,edx
       cmp     eax,ecx
       jb      M00_L10
       mov     rax,qword ptr [rsp+28h]
       movsxd  rdx,edx
       shl     rdx,2
       add     rdx,rax
       mov     rax,qword ptr [rsp+20h]
       mov     rdi,rax
       mov     rax,rdi
       mov     rdi,rax
       jmp     M00_L05
M00_L01:
       cmp     qword ptr [rsi],0
       je      M00_L04
       mov     rdx,qword ptr [rsi]
       mov     rcx,offset mscorlib_ni+0x725a
       call    clr!DllUnregisterServerInternal+0x3d5d0
       mov     ecx,dword ptr [rsi+8]
       mov     edx,dword ptr [rsi+0Ch]
       and     edx,7FFFFFFFh
       test    rax,rax
       jne     M00_L02
       mov     eax,ecx
       or      eax,edx
       jne     M00_L11
       xor     eax,eax
       xor     r8d,r8d
       xor     edx,edx
       jmp     M00_L03
M00_L02:
       cmp     dword ptr [rax+8],ecx
       jb      M00_L12
       mov     r8d,dword ptr [rax+8]
       sub     r8d,ecx
       cmp     r8d,edx
       jb      M00_L12
       movsxd  rcx,ecx
       shl     rcx,2
       add     rcx,8
       mov     r8,rcx
       mov     rcx,r8
       mov     r8,rcx
M00_L03:
       mov     rdi,rax
       mov     ecx,edx
       mov     rdx,r8
       jmp     M00_L05
M00_L04:
       xor     r8d,r8d
       lea     rcx,[rsp+20h]
       vxorpd  xmm0,xmm0,xmm0
       vmovdqu xmmword ptr [rcx],xmm0
       mov     qword ptr [rcx+10h],r8
       mov     rdi,qword ptr [rsp+20h]
       mov     rdx,qword ptr [rsp+28h]
       mov     r8,rdx
       mov     ecx,dword ptr [rsp+30h]
       mov     rdx,r8
                   for (int i = 0; i < sarray.Length; i++) sarray[i] = i;
                 ^^^^^^^^^
M00_L05:
       xor     eax,eax
       test    ecx,ecx
       jle     M00_L09
                   for (int i = 0; i < sarray.Length; i++) sarray[i] = i;
                                                    ^^^^^^^^^^^^^^
M00_L06:
       cmp     eax,ecx
       jae     M00_L13
       test    rdi,rdi
       jne     M00_L07
       mov     r8,rdx
       movsxd  r9,eax
       lea     r8,[r8+r9*4]
       jmp     M00_L08
M00_L07:
       lea     r8,[rdi+8]
       add     r8,rdx
       movsxd  r9,eax
       lea     r8,[r8+r9*4]
M00_L08:
       mov     dword ptr [r8],eax
                   for (int i = 0; i < sarray.Length; i++) sarray[i] = i;
                                               ^^^
       inc     eax
       cmp     eax,ecx
       jl      M00_L06
               }
        ^
M00_L09:
       add     rsp,38h
       pop     rsi
       pop     rdi
       ret
M00_L10:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L11:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L12:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L13:
       call    System.ThrowHelper.ThrowIndexOutOfRangeException()
       int     3
       add     byte ptr [rax],al
       add     byte ptr [rcx],bl
       ???
       add     eax,dword ptr [rax]
       ???
       ???
       add     ah,byte ptr [rax+1]
       jo      M00_L14
M00_L14:
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
; Total bytes of code 411
```
**Method got most probably inlined**
System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
System.ThrowHelper.ThrowIndexOutOfRangeException()

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardManagedSpanInsertChecked()
                   var sarray = managedMemory.Span;
            ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     dword ptr [rsi],esi
       lea     rdi,[rsi+50h]
       cmp     dword ptr [rdi+8],0
       jge     M00_L01
       mov     rdx,qword ptr [rdi]
       mov     rcx,rdx
       test    rcx,rcx
       je      M00_L00
       mov     rax,7FFC3785AA18h
       cmp     qword ptr [rcx],rax
       je      M00_L00
       mov     rcx,rax
       call    clr+0x3d90
       mov     rcx,rax
M00_L00:
       lea     rdx,[rsp+28h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     edx,dword ptr [rdi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rdi+0Ch]
       cmp     edx,dword ptr [rsp+38h]
       ja      M00_L09
       mov     eax,dword ptr [rsp+38h]
       sub     eax,edx
       cmp     eax,ecx
       jb      M00_L09
       mov     rax,qword ptr [rsp+30h]
       movsxd  rdx,edx
       shl     rdx,2
       add     rdx,rax
       mov     rax,qword ptr [rsp+28h]
       mov     rbx,rax
       mov     rax,rbx
       mov     rbx,rax
       jmp     M00_L05
M00_L01:
       cmp     qword ptr [rdi],0
       je      M00_L04
       mov     rdx,qword ptr [rdi]
       mov     rcx,offset mscorlib_ni+0x725a
       call    clr!DllUnregisterServerInternal+0x3d5d0
       mov     ecx,dword ptr [rdi+8]
       mov     edx,dword ptr [rdi+0Ch]
       and     edx,7FFFFFFFh
       test    rax,rax
       jne     M00_L02
       mov     eax,ecx
       or      eax,edx
       jne     M00_L10
       xor     eax,eax
       xor     r8d,r8d
       xor     edx,edx
       jmp     M00_L03
M00_L02:
       cmp     dword ptr [rax+8],ecx
       jb      M00_L11
       mov     r8d,dword ptr [rax+8]
       sub     r8d,ecx
       cmp     r8d,edx
       jb      M00_L11
       movsxd  rcx,ecx
       shl     rcx,2
       add     rcx,8
       mov     r8,rcx
       mov     rcx,r8
       mov     r8,rcx
M00_L03:
       mov     rbx,rax
       mov     ecx,edx
       mov     rdx,r8
       jmp     M00_L05
M00_L04:
       xor     r8d,r8d
       lea     rcx,[rsp+28h]
       vxorpd  xmm0,xmm0,xmm0
       vmovdqu xmmword ptr [rcx],xmm0
       mov     qword ptr [rcx+10h],r8
       mov     rbx,qword ptr [rsp+28h]
       mov     rdx,qword ptr [rsp+30h]
       mov     r8,rdx
       mov     ecx,dword ptr [rsp+38h]
       mov     rdx,r8
                   for (int i = 0; i < DictionarySize; i++) sarray[RandomAccess[i]] = i;
                 ^^^^^^^^^
M00_L05:
       xor     eax,eax
                   for (int i = 0; i < DictionarySize; i++) sarray[RandomAccess[i]] = i;
                                                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M00_L06:
       mov     r8,qword ptr [rsi+20h]
       cmp     eax,dword ptr [r8+8]
       jae     00007ffc`3775a444
       movsxd  r9,eax
       mov     r8d,dword ptr [r8+r9*4+10h]
       cmp     r8d,ecx
       jae     M00_L12
       test    rbx,rbx
       jne     M00_L07
       mov     r9,rdx
       movsxd  r8,r8d
       lea     r9,[r9+r8*4]
       jmp     M00_L08
M00_L07:
       lea     r9,[rbx+8]
       add     r9,rdx
       movsxd  r8,r8d
       lea     r9,[r9+r8*4]
M00_L08:
       mov     dword ptr [r9],eax
                   for (int i = 0; i < DictionarySize; i++) sarray[RandomAccess[i]] = i;
                                                ^^^
       inc     eax
                   for (int i = 0; i < DictionarySize; i++) sarray[RandomAccess[i]] = i;
                            ^^^^^^^^^^^^^^^^^^
       cmp     eax,989680h
       jl      M00_L06
               }
        ^
       add     rsp,40h
       pop     rbx
       pop     rsi
       pop     rdi
       ret
M00_L09:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L10:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L11:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L12:
       call    System.ThrowHelper.ThrowIndexOutOfRangeException()
       int     3
; Total bytes of code 403
```
**Method got most probably inlined**
System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
System.ThrowHelper.ThrowIndexOutOfRangeException()

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardUnmanagedSpanInsert()
                   var sarray = unmanagedMemory.Span;
            ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     dword ptr [rcx],ecx
       lea     rsi,[rcx+38h]
       cmp     dword ptr [rsi+8],0
       jge     M00_L01
       mov     rdx,qword ptr [rsi]
       mov     rcx,rdx
       test    rcx,rcx
       je      M00_L00
       mov     rax,7FFC3783AA18h
       cmp     qword ptr [rcx],rax
       je      M00_L00
       mov     rcx,rax
       call    clr+0x3d90
       mov     rcx,rax
M00_L00:
       lea     rdx,[rsp+20h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     edx,dword ptr [rsi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rsi+0Ch]
       cmp     edx,dword ptr [rsp+30h]
       ja      M00_L10
       mov     eax,dword ptr [rsp+30h]
       sub     eax,edx
       cmp     eax,ecx
       jb      M00_L10
       mov     rax,qword ptr [rsp+28h]
       movsxd  rdx,edx
       shl     rdx,2
       add     rdx,rax
       mov     rax,qword ptr [rsp+20h]
       mov     rdi,rax
       mov     rax,rdi
       mov     rdi,rax
       jmp     M00_L05
M00_L01:
       cmp     qword ptr [rsi],0
       je      M00_L04
       mov     rdx,qword ptr [rsi]
       mov     rcx,offset mscorlib_ni+0x725a
       call    clr!DllUnregisterServerInternal+0x3d5d0
       mov     ecx,dword ptr [rsi+8]
       mov     edx,dword ptr [rsi+0Ch]
       and     edx,7FFFFFFFh
       test    rax,rax
       jne     M00_L02
       mov     eax,ecx
       or      eax,edx
       jne     M00_L11
       xor     eax,eax
       xor     r8d,r8d
       xor     edx,edx
       jmp     M00_L03
M00_L02:
       cmp     dword ptr [rax+8],ecx
       jb      M00_L12
       mov     r8d,dword ptr [rax+8]
       sub     r8d,ecx
       cmp     r8d,edx
       jb      M00_L12
       movsxd  rcx,ecx
       shl     rcx,2
       add     rcx,8
       mov     r8,rcx
       mov     rcx,r8
       mov     r8,rcx
M00_L03:
       mov     rdi,rax
       mov     ecx,edx
       mov     rdx,r8
       jmp     M00_L05
M00_L04:
       xor     r8d,r8d
       lea     rcx,[rsp+20h]
       vxorpd  xmm0,xmm0,xmm0
       vmovdqu xmmword ptr [rcx],xmm0
       mov     qword ptr [rcx+10h],r8
       mov     rdi,qword ptr [rsp+20h]
       mov     rdx,qword ptr [rsp+28h]
       mov     r8,rdx
       mov     ecx,dword ptr [rsp+30h]
       mov     rdx,r8
                   for (int i = 0; i < sarray.Length; i++) sarray[i] = i;
                 ^^^^^^^^^
M00_L05:
       xor     eax,eax
       test    ecx,ecx
       jle     M00_L09
                   for (int i = 0; i < sarray.Length; i++) sarray[i] = i;
                                                    ^^^^^^^^^^^^^^
M00_L06:
       cmp     eax,ecx
       jae     M00_L13
       test    rdi,rdi
       jne     M00_L07
       mov     r8,rdx
       movsxd  r9,eax
       lea     r8,[r8+r9*4]
       jmp     M00_L08
M00_L07:
       lea     r8,[rdi+8]
       add     r8,rdx
       movsxd  r9,eax
       lea     r8,[r8+r9*4]
M00_L08:
       mov     dword ptr [r8],eax
                   for (int i = 0; i < sarray.Length; i++) sarray[i] = i;
                                               ^^^
       inc     eax
       cmp     eax,ecx
       jl      M00_L06
               }
        ^
M00_L09:
       add     rsp,38h
       pop     rsi
       pop     rdi
       ret
M00_L10:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L11:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L12:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L13:
       call    System.ThrowHelper.ThrowIndexOutOfRangeException()
       int     3
       add     byte ptr [rax],al
       add     byte ptr [rcx],bl
       ???
       add     eax,dword ptr [rax]
       ???
       ???
       add     ah,byte ptr [rax+1]
       jo      M00_L14
M00_L14:
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
; Total bytes of code 411
```
**Method got most probably inlined**
System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
System.ThrowHelper.ThrowIndexOutOfRangeException()

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardUnmanagedSpanInsertChecked()
                   var sarray = unmanagedMemory.Span;
            ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     dword ptr [rsi],esi
       lea     rdi,[rsi+38h]
       cmp     dword ptr [rdi+8],0
       jge     M00_L01
       mov     rdx,qword ptr [rdi]
       mov     rcx,rdx
       test    rcx,rcx
       je      M00_L00
       mov     rax,7FFC3783AA18h
       cmp     qword ptr [rcx],rax
       je      M00_L00
       mov     rcx,rax
       call    clr+0x3d90
       mov     rcx,rax
M00_L00:
       lea     rdx,[rsp+28h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     edx,dword ptr [rdi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rdi+0Ch]
       cmp     edx,dword ptr [rsp+38h]
       ja      M00_L09
       mov     eax,dword ptr [rsp+38h]
       sub     eax,edx
       cmp     eax,ecx
       jb      M00_L09
       mov     rax,qword ptr [rsp+30h]
       movsxd  rdx,edx
       shl     rdx,2
       add     rdx,rax
       mov     rax,qword ptr [rsp+28h]
       mov     rbx,rax
       mov     rax,rbx
       mov     rbx,rax
       jmp     M00_L05
M00_L01:
       cmp     qword ptr [rdi],0
       je      M00_L04
       mov     rdx,qword ptr [rdi]
       mov     rcx,offset mscorlib_ni+0x725a
       call    clr!DllUnregisterServerInternal+0x3d5d0
       mov     ecx,dword ptr [rdi+8]
       mov     edx,dword ptr [rdi+0Ch]
       and     edx,7FFFFFFFh
       test    rax,rax
       jne     M00_L02
       mov     eax,ecx
       or      eax,edx
       jne     M00_L10
       xor     eax,eax
       xor     r8d,r8d
       xor     edx,edx
       jmp     M00_L03
M00_L02:
       cmp     dword ptr [rax+8],ecx
       jb      M00_L11
       mov     r8d,dword ptr [rax+8]
       sub     r8d,ecx
       cmp     r8d,edx
       jb      M00_L11
       movsxd  rcx,ecx
       shl     rcx,2
       add     rcx,8
       mov     r8,rcx
       mov     rcx,r8
       mov     r8,rcx
M00_L03:
       mov     rbx,rax
       mov     ecx,edx
       mov     rdx,r8
       jmp     M00_L05
M00_L04:
       xor     r8d,r8d
       lea     rcx,[rsp+28h]
       vxorpd  xmm0,xmm0,xmm0
       vmovdqu xmmword ptr [rcx],xmm0
       mov     qword ptr [rcx+10h],r8
       mov     rbx,qword ptr [rsp+28h]
       mov     rdx,qword ptr [rsp+30h]
       mov     r8,rdx
       mov     ecx,dword ptr [rsp+38h]
       mov     rdx,r8
                   for (int i = 0; i < DictionarySize; i++) sarray[RandomAccess[i] ] = i;
                 ^^^^^^^^^
M00_L05:
       xor     eax,eax
                   for (int i = 0; i < DictionarySize; i++) sarray[RandomAccess[i] ] = i;
                                                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M00_L06:
       mov     r8,qword ptr [rsi+20h]
       cmp     eax,dword ptr [r8+8]
       jae     00007ffc`3773a444
       movsxd  r9,eax
       mov     r8d,dword ptr [r8+r9*4+10h]
       cmp     r8d,ecx
       jae     M00_L12
       test    rbx,rbx
       jne     M00_L07
       mov     r9,rdx
       movsxd  r8,r8d
       lea     r9,[r9+r8*4]
       jmp     M00_L08
M00_L07:
       lea     r9,[rbx+8]
       add     r9,rdx
       movsxd  r8,r8d
       lea     r9,[r9+r8*4]
M00_L08:
       mov     dword ptr [r9],eax
                   for (int i = 0; i < DictionarySize; i++) sarray[RandomAccess[i] ] = i;
                                                ^^^
       inc     eax
                   for (int i = 0; i < DictionarySize; i++) sarray[RandomAccess[i] ] = i;
                            ^^^^^^^^^^^^^^^^^^
       cmp     eax,989680h
       jl      M00_L06
               }
        ^
       add     rsp,40h
       pop     rbx
       pop     rsi
       pop     rdi
       ret
M00_L09:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L10:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L11:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L12:
       call    System.ThrowHelper.ThrowIndexOutOfRangeException()
       int     3
; Total bytes of code 403
```
**Method got most probably inlined**
System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
System.ThrowHelper.ThrowIndexOutOfRangeException()

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardUnmanagedSpanBenInsert()
                   var sarray = unmanagedMemory.Span;
            ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     dword ptr [rcx],ecx
       lea     rsi,[rcx+38h]
       cmp     dword ptr [rsi+8],0
       jge     M00_L01
       mov     rdx,qword ptr [rsi]
       mov     rcx,rdx
       test    rcx,rcx
       je      M00_L00
       mov     rax,7FFC3784AA18h
       cmp     qword ptr [rcx],rax
       je      M00_L00
       mov     rcx,rax
       call    clr+0x3d90
       mov     rcx,rax
M00_L00:
       lea     rdx,[rsp+40h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     edx,dword ptr [rsi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rsi+0Ch]
       cmp     edx,dword ptr [rsp+50h]
       ja      M00_L08
       mov     eax,dword ptr [rsp+50h]
       sub     eax,edx
       cmp     eax,ecx
       jb      M00_L08
       mov     rax,qword ptr [rsp+48h]
       movsxd  rdx,edx
       shl     rdx,2
       add     rdx,rax
       mov     rax,qword ptr [rsp+40h]
       mov     rdi,rax
       mov     rax,rdi
       mov     rdi,rax
       jmp     M00_L05
M00_L01:
       cmp     qword ptr [rsi],0
       je      M00_L04
       mov     rdx,qword ptr [rsi]
       mov     rcx,offset mscorlib_ni+0x725a
       call    clr!DllUnregisterServerInternal+0x3d5d0
       mov     ecx,dword ptr [rsi+8]
       mov     edx,dword ptr [rsi+0Ch]
       and     edx,7FFFFFFFh
       test    rax,rax
       jne     M00_L02
       mov     eax,ecx
       or      eax,edx
       jne     M00_L09
       xor     eax,eax
       xor     r8d,r8d
       xor     edx,edx
       jmp     M00_L03
M00_L02:
       cmp     dword ptr [rax+8],ecx
       jb      M00_L10
       mov     r8d,dword ptr [rax+8]
       sub     r8d,ecx
       cmp     r8d,edx
       jb      M00_L10
       movsxd  rcx,ecx
       shl     rcx,2
       add     rcx,8
       mov     r8,rcx
       mov     rcx,r8
       mov     r8,rcx
M00_L03:
       mov     rdi,rax
       mov     ecx,edx
       mov     rdx,r8
       jmp     M00_L05
M00_L04:
       xor     r8d,r8d
       lea     rcx,[rsp+40h]
       vxorpd  xmm0,xmm0,xmm0
       vmovdqu xmmword ptr [rcx],xmm0
       mov     qword ptr [rcx+10h],r8
       mov     rdi,qword ptr [rsp+40h]
       mov     rdx,qword ptr [rsp+48h]
       mov     r8,rdx
       mov     esi,dword ptr [rsp+50h]
       mov     ecx,esi
       mov     rdx,r8
M00_L05:
       mov     esi,ecx
       lea     rcx,[rsp+28h]
       mov     qword ptr [rcx],rdi
       mov     qword ptr [rcx+8],rdx
       mov     dword ptr [rcx+10h],esi
       lea     rcx,[rsp+28h]
       call    System.Runtime.InteropServices.MemoryMarshal.GetReference[[System.Int32, mscorlib]](System.Span`1<Int32>)
                   for (int i = 0; i < sarray.Length; i++) Unsafe.Add(ref firstElement, i) = i;
                 ^^^^^^^^^
       xor     ecx,ecx
       test    esi,esi
       jle     M00_L07
M00_L06:
       movsxd  rdx,ecx
       mov     dword ptr [rax+rdx*4],ecx
                   for (int i = 0; i < sarray.Length; i++) Unsafe.Add(ref firstElement, i) = i;
                                               ^^^
       inc     ecx
       cmp     ecx,esi
       jl      M00_L06
               }
        ^
M00_L07:
       add     rsp,58h
       pop     rsi
       pop     rdi
       ret
M00_L08:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L09:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L10:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
       int     3
       add     byte ptr [rax],al
; Total bytes of code 378
```
```assembly
; System.Runtime.InteropServices.MemoryMarshal.GetReference[[System.Int32, mscorlib]](System.Span`1<Int32>)
       mov     rdx,qword ptr [rcx]
       mov     rcx,qword ptr [rcx+8]
       test    rdx,rdx
       jne     M01_L00
       mov     rax,rcx
       ret
M01_L00:
       lea     rax,[rdx+8]
       add     rax,rcx
       ret
       sbb     dword ptr [rax],eax
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax-3C87A3Dh],dh
       jg      M01_L01
M01_L01:
       add     bl,al
       add     byte ptr [rax],al
       add     byte ptr [rcx],bl
       add     byte ptr [rax],al
; Total bytes of code 55
```
**Method got most probably inlined**
System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
System.Runtime.CompilerServices.Unsafe.Add(!!0 ByRef, Int32)

## .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3362.0
```assembly
; Svelto.DataStructures.ArrayBenchmark.StandardUnmanagedSpanBenInsertChecked()
                   var sarray = unmanagedMemory.Span;
            ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     dword ptr [rsi],esi
       lea     rdi,[rsi+38h]
       cmp     dword ptr [rdi+8],0
       jge     M00_L01
       mov     rdx,qword ptr [rdi]
       mov     rcx,rdx
       test    rcx,rcx
       je      M00_L00
       mov     rax,7FFC3786AA18h
       cmp     qword ptr [rcx],rax
       je      M00_L00
       mov     rcx,rax
       call    clr+0x3d90
       mov     rcx,rax
M00_L00:
       lea     rdx,[rsp+38h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     edx,dword ptr [rdi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rdi+0Ch]
       cmp     edx,dword ptr [rsp+48h]
       ja      M00_L07
       mov     eax,dword ptr [rsp+48h]
       sub     eax,edx
       cmp     eax,ecx
       jb      M00_L07
       mov     rax,qword ptr [rsp+40h]
       movsxd  rdx,edx
       shl     rdx,2
       add     rdx,rax
       mov     rax,qword ptr [rsp+38h]
       mov     rbx,rax
       mov     rax,rbx
       mov     rbx,rax
       jmp     M00_L05
M00_L01:
       cmp     qword ptr [rdi],0
       je      M00_L04
       mov     rdx,qword ptr [rdi]
       mov     rcx,offset mscorlib_ni+0x725a
       call    clr!DllUnregisterServerInternal+0x3d5d0
       mov     ecx,dword ptr [rdi+8]
       mov     edx,dword ptr [rdi+0Ch]
       and     edx,7FFFFFFFh
       test    rax,rax
       jne     M00_L02
       mov     eax,ecx
       or      eax,edx
       jne     M00_L08
       xor     eax,eax
       xor     r8d,r8d
       xor     edx,edx
       jmp     M00_L03
M00_L02:
       cmp     dword ptr [rax+8],ecx
       jb      M00_L09
       mov     r8d,dword ptr [rax+8]
       sub     r8d,ecx
       cmp     r8d,edx
       jb      M00_L09
       movsxd  rcx,ecx
       shl     rcx,2
       add     rcx,8
       mov     r8,rcx
       mov     rcx,r8
       mov     r8,rcx
M00_L03:
       mov     rbx,rax
       mov     ecx,edx
       mov     rdx,r8
       jmp     M00_L05
M00_L04:
       xor     r8d,r8d
       lea     rcx,[rsp+38h]
       vxorpd  xmm0,xmm0,xmm0
       vmovdqu xmmword ptr [rcx],xmm0
       mov     qword ptr [rcx+10h],r8
       mov     rbx,qword ptr [rsp+38h]
       mov     rdx,qword ptr [rsp+40h]
       mov     r8,rdx
       mov     ecx,dword ptr [rsp+48h]
       mov     rdx,r8
M00_L05:
       lea     rax,[rsp+20h]
       mov     qword ptr [rax],rbx
       mov     qword ptr [rax+8],rdx
       mov     dword ptr [rax+10h],ecx
       lea     rcx,[rsp+20h]
       call    System.Runtime.InteropServices.MemoryMarshal.GetReference[[System.Int32, mscorlib]](System.Span`1<Int32>)
                   for (int i = 0; i < DictionarySize; i++) Unsafe.Add(ref firstElement, RandomAccess[i] ) = i;
                 ^^^^^^^^^
       xor     ecx,ecx
                   for (int i = 0; i < DictionarySize; i++) Unsafe.Add(ref firstElement, RandomAccess[i] ) = i;
                                                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M00_L06:
       mov     rdx,qword ptr [rsi+20h]
       cmp     ecx,dword ptr [rdx+8]
       jae     00007ffc`3776a437
       movsxd  r8,ecx
       mov     edx,dword ptr [rdx+r8*4+10h]
       movsxd  rdx,edx
       mov     dword ptr [rax+rdx*4],ecx
                   for (int i = 0; i < DictionarySize; i++) Unsafe.Add(ref firstElement, RandomAccess[i] ) = i;
                                                ^^^
       inc     ecx
                   for (int i = 0; i < DictionarySize; i++) Unsafe.Add(ref firstElement, RandomAccess[i] ) = i;
                            ^^^^^^^^^^^^^^^^^^
       cmp     ecx,989680h
       jl      M00_L06
               }
        ^
       add     rsp,50h
       pop     rbx
       pop     rsi
       pop     rdi
       ret
M00_L07:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L08:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
M00_L09:
       mov     ecx,1
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
       int     3
; Total bytes of code 390
```
```assembly
; System.Runtime.InteropServices.MemoryMarshal.GetReference[[System.Int32, mscorlib]](System.Span`1<Int32>)
       mov     rdx,qword ptr [rcx]
       mov     rcx,qword ptr [rcx+8]
       test    rdx,rdx
       jne     M01_L00
       mov     rax,rcx
       ret
M01_L00:
       lea     rax,[rdx+8]
       add     rax,rcx
       ret
       sbb     dword ptr [rax],eax
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     byte ptr [rax],al
       add     al,al
       ret
       xchg    esi,dword ptr [rdi]
       cld
       jg      M01_L01
M01_L01:
       add     bl,al
       add     byte ptr [rax],al
       add     byte ptr [rcx],bl
       add     byte ptr [rax],al
; Total bytes of code 55
```
**Method got most probably inlined**
System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
System.Runtime.CompilerServices.Unsafe.Add(!!0 ByRef, Int32)

