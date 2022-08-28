using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Validators;

namespace Depra.Data.Serialization.Benchmark
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run(typeof(Program).Assembly, DefaultConfig.Instance
                .AddValidator(JitOptimizationsValidator.FailOnError)
                .AddJob(Job.Dry.WithToolchain(InProcessEmitToolchain.Instance)));
        }
    }
}