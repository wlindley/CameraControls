using System;
using System.Collections.Generic;
using UnityEngine;

namespace CamCon
{
    public class LerpCurve
    {
        private LerperPair[] data;

        public LerpCurve(params LerperPair[] dataPoints)
        {
            this.data = dataPoints;
        }

        virtual public float Probe(float input)
        {
            if (0 == data.Length)
                throw new LerperException("Lerper has no data");
            else if (1 == data.Length)
                return data[0].Output;

            return GetOutputForInput(input);
        }

        private float GetOutputForInput(float input)
        {
            var min = data[0];
            var max = data[1];

            for (var i = 1; i < data.Length && max.Input < input; i++)
            {
                min = max;
                max = data[i];
            }

            var percent = MapInputToRange(input, ref min, ref max);
            return GetOutputFromRange(percent, ref min, ref max);
        }

        private float MapInputToRange(float input, ref LerperPair min, ref LerperPair max)
        {
            var percent = (input - min.Input) / (max.Input - min.Input);
            return Mathf.Clamp(percent, 0f, 1f);
        }

        private float GetOutputFromRange(float percent, ref LerperPair min, ref LerperPair max)
        {
            return min.Output + (percent * (max.Output - min.Output));
        }
    }

    public struct LerperPair
    {
        private float input, output;

        public LerperPair(float input, float output)
        {
            this.input = input;
            this.output = output;
        }

        public float Input { get { return input; } }
        public float Output { get { return output; } }

        public override string ToString()
        {
            return String.Format("LerperPair: {0} -> {1}", input, output);
        }
    }

    public class LerperException : Exception
    {
        public LerperException(string message) : base(message) { }
    }
}
