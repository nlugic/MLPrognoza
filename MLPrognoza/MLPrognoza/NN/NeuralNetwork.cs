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
        public double[][] outputs;
        
        public delegate void FinishEpoch(double[,] epoch, double error);
        public delegate void FinishLearning();
        
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
            network = new ActivationNetwork(f, 4, layers);
        }
        
        public void initData(List<WeatherModel> data)
        {
            outputs = new double[data.Count][];
            inputs = new double[data.Count][];

            for (int i = 0; i < data.Count; i++)
            {
                outputs[i] = new double[1];
                outputs[i][0] = data[i].Temperature.Scale(-100, 100, -1, 1);

                inputs[i] = new double[4];
                inputs[i][0] = data[i].WindSpeed.Scale(0, 300, -1, 1);
                inputs[i][2] = data[i].Precipitation[3].Scale(0, 1000, -1, 1);
                inputs[i][3] = data[i].SnowDepth.Scale(0, 3000, -1, 1);
            }
        }

        public void Train(int epoch, FinishEpoch finishEpochEvent,FinishLearning finish)
        {
            new NguyenWidrow(network).Randomize();
            ParallelResilientBackpropagationLearning teacher = new ParallelResilientBackpropagationLearning(network);

            double[,] s = new double[outputs.Length, 2];
            
            int iteration = 1;
            while (true)
            {
                double error = teacher.RunEpoch(inputs, outputs) / outputs.Length;

                // calculate solution
                for (int j = 0; j < outputs.Length; j++)
                {
                    double y = network.Compute(inputs[j])[0];
                    s[j,1] = y.Scale(-1, 1, -100, 100);
                    s[j, 0] = j;
                }
                
                // calculate error
                double learningError = 0.0;
                for (int j = 0; j < outputs.Length; j++)
                {
                    double[] x = inputs[j];
                    double expected = outputs[j][0];
                    double actual = network.Compute(x)[0];
                    learningError += Math.Abs(expected - actual);
                }
                finishEpochEvent(s, error);

                // increase current iteration
                iteration++;

                // check if we need to stop
                if ((epoch != 0) && (iteration > epoch))
                    break;
            }
            finish();
        }

        public double GetTemperature(WeatherModel data)
        {
            double[] input = new double[4];

            input[0] = data.WindSpeed.Scale(0, 300, -1, 1); 
            input[1] = data.AtmosphericPressure.Scale(500, 2000, -1, 1);
            input[2] = data.Precipitation[3].Scale(0, 1000, -1, 1);
            input[3] = data.SnowDepth.Scale(0, 3000, -1, 1);

            return network.Compute(input)[0].Scale(-100, 100, -1, 1);
        }
        
    }
}
