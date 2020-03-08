using System.Collections.Generic;

namespace StructBenchmarking
{
    public interface ITaskFactory
    {
        ITask CreateNewTask(int size, string nameOfTask);
    }

    public class ArrayCreationTaskTaskFactory : ITaskFactory
    {
        public ITask CreateNewTask(int size, string nameOfTask)
        {
            if (nameOfTask == "Class")
                return new ClassArrayCreationTask(size);
            if (nameOfTask == "Structure")
                return new StructArrayCreationTask(size);
            return null;
        }
    }

    public class CallWithClassArgumentTaskTaskFactory : ITaskFactory
    {
        public ITask CreateNewTask(int size, string nameOfTask)
        {
            if (nameOfTask == "Class")
                return new MethodCallWithClassArgumentTask(size);
            if (nameOfTask == "Structure")
                return new MethodCallWithStructArgumentTask(size);
            return null;
        }
    }

    public static class Experiment
    {
        public static ChartData ConductAnExperiment(
            IBenchmark benchmark, int repetitionsCount, ITaskFactory taskFactory, string title)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();
            foreach (var size in Constants.FieldCounts)
            {
                classesTimes.Add(new ExperimentResult(size,
                    (benchmark.MeasureDurationInMs(taskFactory.CreateNewTask(size, "Class"), repetitionsCount))));
                structuresTimes.Add(new ExperimentResult(size,
                    benchmark.MeasureDurationInMs(taskFactory.CreateNewTask(size, "Structure"), repetitionsCount)));
            }
            return new ChartData
            {
                Title = title,
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }

    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            return Experiment.ConductAnExperiment(benchmark, repetitionsCount,
                new ArrayCreationTaskTaskFactory(), "Create array");
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            return Experiment.ConductAnExperiment(benchmark, repetitionsCount,
                new CallWithClassArgumentTaskTaskFactory(), "Call method with argument");
        }
    }
}