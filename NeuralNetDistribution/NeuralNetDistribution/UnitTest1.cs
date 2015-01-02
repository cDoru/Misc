using System;
using System.Collections.Generic;
using System.Linq;
using AForge.Neuro;
using AForge.Neuro.Learning;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeuralNetDistribution
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            double sigmoidAlphaValue = 3;

            List<double[]> input = new List<double[]>();

            input.Add(new double[] {0.2});
            input.Add(new double[] {0.4});
            input.Add(new double[] {0.6});
            input.Add(new double[] {0.8});
            input.Add(new double[] {1});

            List<double[]> output = new List<double[]>();

            output.Add(new double[] {0.1});
            output.Add(new double[] {0.4});
            output.Add(new double[] {0.6});
            output.Add(new double[] {0.75});
            output.Add(new double[] {0.81});

            ActivationNetwork network = new ActivationNetwork(
                new BipolarSigmoidFunction(sigmoidAlphaValue),
                1, 1, 1);

            BackPropagationLearning teacher = new BackPropagationLearning(network);

            teacher.LearningRate = 0.1;
            teacher.Momentum = 0;

            while (true)
            {
                // run epoch of learning procedure
                double error = teacher.RunEpoch(input.ToArray(), output.ToArray())/input.Count;
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<double> inputs = new List<double>();
            List<double> outputs = new List<double>();

            var data = new double[] { 1,3.1,4.2478,5.7856,6.234,7.76,8.45,9.37,12,13,14,3.865,3.542,5.54,76,2.354,4.745,6,56,5.4,45,4.235,34,6.43,7.74,4.32 };

            //var data = new double[] { 1,2,2,2,2,3,3,3,3,3,3,4,4,4,4,5 };

            //data = data.OrderBy(d => d).ToArray();

            //ParetoShaped Data
            //var data = new double[] { 10, 6.6, 5, 4, 3.3, 2.8, 2.5, 2.3, 2, 1.85 };

            var mean = MathNet.Numerics.Statistics.ArrayStatistics.Mean(data);
            var stDev = MathNet.Numerics.Statistics.ArrayStatistics.StandardDeviation(data);

            //var distribution = new MathNet.Numerics.Distributions.Normal(mean, stDev);

            var distribution = new MathNet.Numerics.Distributions.Normal(mean, stDev);


            List<double> errorBefore = new List<double>();
            for (int i = 0; i < data.Length; i++)
            {
                double cumulativeDistribution = (double)(i + 1) / (double)data.Length;

                inputs.Add(cumulativeDistribution);
                
                var actual = distribution.CumulativeDistribution(data[i]);

                outputs.Add(actual);

                var error = cumulativeDistribution / actual;
                errorBefore.Add(error);
            }

            double averageErrorBefore = errorBefore.Average();

            double baseLineAddition = MathNet.Numerics.Statistics.ArrayStatistics.Minimum(outputs.ToArray()) -
                                      MathNet.Numerics.Statistics.ArrayStatistics.Minimum(inputs.ToArray());
            double baseLineMultiple = 1 / (MathNet.Numerics.Statistics.ArrayStatistics.Maximum(outputs.ToArray()) - baseLineAddition);

            double sigmoidAlphaValue = 1;

            ActivationNetwork network = new ActivationNetwork(new SigmoidFunction(1),1, 4, 4, 1);
                //new BipolarSigmoidFunction(sigmoidAlphaValue),
                //1, 3, 1);

            BackPropagationLearning teacher = new BackPropagationLearning(network);

            teacher.LearningRate = 0.478587;
            teacher.Momentum = 0.0;

            double[][] nnInputs = inputs.Select(s => new double[] {s}).ToArray();
            double[][] nnOutputs = outputs.Select(s => new double[] { (s - baseLineAddition) * baseLineMultiple}).ToArray();


            for(int i = 0; i < 50000; i++)
            {
                // run epoch of learning procedure
                double error = teacher.RunEpoch(
                    nnInputs,
                    nnOutputs
                    ) / inputs.Count;
            }

            List<double> errors = new List<double>();
            for(int i = 0; i < nnInputs.Length; i++)
            {
                double actual = network.Compute(nnInputs[i])[0];
                double expected = nnOutputs[i][0];
                errors.Add(Math.Abs(1 - (expected / actual)));
            }

            double nnAverageError = errors.Average();

            errors = new List<double>();
            List<double[]> actuals = new List<double[]>();

            for (int i = 0; i < data.Length; i++)
            {
                double cumulativeDistribution = (double)(i + 1) / (double)data.Length;

                double aiCumulativeDistribution = network.Compute(new double[] { cumulativeDistribution })[0];

                aiCumulativeDistribution = (aiCumulativeDistribution/baseLineMultiple) + baseLineAddition;


                var actual = distribution.InverseCumulativeDistribution(aiCumulativeDistribution);

                errors.Add(Math.Abs(1 - (data[i] / actual)));
                actuals.Add(new double[] { actual, data[i] });

            }

            double averageError = errors.Average();


        }

        [TestMethod]
        public void ParetoTest1()
        {
            var dist = new MathNet.Numerics.Distributions.Pareto(1, .45);
            List<double> results = new List<double>();
            
            for (double d = 1; d <= 10.0; d++)
            {
                results.Add(50000 - (50000 * dist.CumulativeDistribution(d)));
            }

            var result = string.Join(",", results);

        }

        [TestMethod]
        public void CurtainsLeadTime()
        {
            var mean = 17.54F;
            var std = 15.16F;

            var dist = new MathNet.Numerics.Distributions.Normal(mean, std);

            List<double> results = new List<double>();
            for (double d = 0; d <= 1.0; d+=0.05)
            {
                results.Add(dist.InverseCumulativeDistribution(d));
            }


        }

        [TestMethod]
        public void Erlang()
        {
            var mean = 17.54F;
            var std = 15.16F;

            var dist = new MathNet.Numerics.Distributions.Erlang(5, std);

            List<double> results = new List<double>();
            for (double d = 0; d <= 1.0; d += 0.05)
            {
                //results.Add(dist(d));
            }


        }
    }
}