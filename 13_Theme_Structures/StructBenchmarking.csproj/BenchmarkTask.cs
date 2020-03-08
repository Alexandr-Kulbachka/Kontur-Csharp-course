using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            task.Run();
            Stopwatch stopWatch = new Stopwatch();
            GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
            GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
                                            // и как-то повлияет на них.            
            stopWatch.Restart();
            for (int i = 0; i < repetitionCount; i++)
            {
                task.Run();
            }
            stopWatch.Stop();
            return (double)stopWatch.ElapsedMilliseconds / (double)repetitionCount;
        }
    }

    public class StringConstructorMethod : ITask
    {
        public void Run()
        {
            var newStr = new string('a', 10000);
        }
    }

    public class StringBuilderMethod : ITask
    {
        public void Run()
        {
            StringBuilder newStr = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                newStr.Append('a');
            }
            newStr.ToString();
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            Benchmark benchmark = new Benchmark();
            StringConstructorMethod stringConstructor = new StringConstructorMethod();
            StringBuilderMethod stringBuilder = new StringBuilderMethod();
            Assert.Less(benchmark.MeasureDurationInMs(stringConstructor, 20000),
                        benchmark.MeasureDurationInMs(stringBuilder, 20000));
        }
    }
}