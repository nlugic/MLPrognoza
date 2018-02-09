using Accord.Math;
using Accord.Neuro;
using Accord.Neuro.ActivationFunctions;
using Accord.Neuro.Learning;
using MLPrognoza.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLPrognoza.NN
{
    public enum FunctionType { BERNOULLI, GAUSSIAN }
    
    class NeuralNetwork
    {

        private int firstLayer;
        private int[] layers;
        
        private ActivationNetwork network;

        private double[][] inputs;
        private double[][] outputs;
        
        public delegate void FinishEpoch(double[] epoch, double error);
        
        public NeuralNetwork(int firstLayer, int[] layers, FunctionType func, double sigmoidValue)
        {
            IActivationFunction f = null;
            switch (func)
            {
                case FunctionType.BERNOULLI:
                    f = new BernoulliFunction(sigmoidValue);
                    break;
                case FunctionType.GAUSSIAN:
                    f = new GaussianFunction(sigmoidValue);
                    break;
            }
            network = new ActivationNetwork(f, firstLayer, layers);
        }
        
        public void initData(List<WeatherModel> data)
        {
            outputs = new double[data.Count][];
            inputs = new double[data.Count][];

            for (int i = 0; i < data.Count; i++)
            {
                outputs[i] = new double[1];
                outputs[i][0] = data[i].Temperature;

                inputs[i] = new double[5];
                inputs[i][0] = data[i].Time.ToOADate();
                inputs[i][1] = data[i].WindSpeed;
                inputs[i][2] = data[i].AtmosphericPressure;

                double srPrecipitation = 0;
                for (int j = 0; j < data[i].Precipitation.Length; j++)
                    srPrecipitation += data[i].Precipitation[j];
                inputs[i][3] = srPrecipitation / data[i].Precipitation.Length;

                inputs[i][4] = data[i].SnowDepth;
            }
        }

        public void Train(int epoch, FinishEpoch finishEpochEvent)
        {
            new NguyenWidrow(network).Randomize();
            ParallelResilientBackpropagationLearning teacher = new ParallelResilientBackpropagationLearning(network);

            double[] s = new double[outputs.Length];
            
            int iteration = 1;
            while (true)
            {
                double error = teacher.RunEpoch(inputs, outputs) / outputs.Length;

                // calculate solution
                for (int j = 0; j < outputs.Length; j++)
                {
                    double[] x = inputs[j];
                    double y = network.Compute(x)[0];
                    s[j] = y;
                }
                
                // calculate error
                double learningError = 0.0;
                for (int j = 0; j < outputs.Length; j++)
                {
                    double[] x = inputs[j];
                    double expected = outputs[j][1];
                    double actual = network.Compute(x)[0];
                    learningError += Math.Abs(expected - actual);
                }
                finishEpochEvent(s, learningError);

                // increase current iteration
                iteration++;

                // check if we need to stop
                if ((epoch != 0) && (iteration > epoch))
                    break;
            }
        }

        public double GetTemperature(WeatherModel data)
        {
            double[] input = new double[5];

            input[0] = data.Time.ToOADate();
            input[1] = data.WindSpeed;
            input[2] = data.AtmosphericPressure;

            double srPrecipitation = 0;
            for (int j = 0; j < data.Precipitation.Length; j++)
                srPrecipitation += data.Precipitation[j];
            input[3] = srPrecipitation / data.Precipitation.Length;

            input[4] = data.SnowDepth;

            return network.Compute(input)[0];
        }
        
    }
}
