using BenchmarkDotNet.Running;

namespace Depra.Data.Storage.Benchmark
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<DataStorageBenchmark>();
        }
    }
}