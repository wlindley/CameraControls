using System;
using System.Collections.Generic;
using UnityEngine;

namespace CamCon
{
    public class LerpCurve
    {
        private CurveControlPoint[] data;

        public LerpCurve(params CurveControlPoint[] dataPoints)
        {
            this.data = dataPoints;
        }

        virtual public float Probe(float input)
        {
            if (0 == data.Length)
                throw new LerpCurveException("Lerper has no data");
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

        private float MapInputToRange(float input, ref CurveControlPoint min, ref CurveControlPoint max)
        {
            var percent = (input - min.Input) / (max.Input - min.Input);
            return Mathf.Clamp(percent, 0f, 1f);
        }

        private float GetOutputFromRange(float percent, ref CurveControlPoint min, ref CurveControlPoint max)
        {
            return min.Output + (percent * (max.Output - min.Output));
        }
    }

    public struct CurveControlPoint
    {
        private float input, output;

        public CurveControlPoint(float input, float output)
        {
            this.input = input;
            this.output = output;
        }

        public float Input { get { return input; } }
        public float Output { get { return output; } }

        public override string ToString()
        {
            return String.Format("CurveControlPoint: {0} -> {1}", input, output);
        }
    }

    public class LerpCurveException : Exception
    {
        public LerpCurveException(string message) : base(message) { }
    }
}
