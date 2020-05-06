using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Svelto.DataStructures
{
    //[DisassemblyDiagnoser(printAsm: true, printSource: true)] // !!! use the new diagnoser!!

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<DictionaryBenchmark>();
            //BenchmarkRunner.Run<SparseSetBenchmark>();
            //var sparseSetBenchmark = new SparseSetBenchmark(); sparseSetBenchmark.GlobalSetupRandom(); sparseSetBenchmark.FasterGetRandom();
            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);
        }
    }
}
