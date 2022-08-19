using BenchmarkDotNet.Running;

namespace Depra.Data.Persistent.Benchmark
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<DataStorageBenchmark>();
        }
    }
}