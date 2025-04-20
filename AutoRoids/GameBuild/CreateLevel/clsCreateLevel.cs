using System;
using System.Collections.Generic;
using AutoRoids.Common.Enum;
using AutoRoids.Common.Static;

namespace AutoRoids.GameBuild.CreateLevel
{
    public class clsCreateLevel
    {
        internal void GetDataRock(int intRockCount,
                                  ref List<enumSize> lstRockSize,
                                  out List<Double> lstTravelAngle,
                                  out List<Double> lstTravelRotation,
                                  out List<Double> lstTravelDistance,
                                  out List<double> lstScale)

        {
            List<Double> _lstScale = new List<double>();
            List<double> lstRotation = new List<double>();

            for (int i = 0; i < lstRockSize.Count; i++)
            {
                _lstScale.Add(GetEnumSize(lstRockSize[i]));
                if (i.IsEven())
                    lstRotation.Add(0.1);
                else
                    lstRotation.Add(-0.1);
            }

            lstTravelAngle = new List<double>();
            lstTravelRotation = new List<double>();
            lstTravelDistance = new List<double>();
            lstScale = new List<double>();

            clsCreatePoints clsGeneratePoints = new clsCreatePoints();
            Random random = clsGeneratePoints.GetRandom();

            for (int i = 0; i < intRockCount; i++)
            {
                lstTravelAngle.Add(90);

                lstTravelRotation.Add(lstRotation[random.Next(0, lstRotation.Count)]);
                lstTravelDistance.Add(0);

                int intRandom = random.Next(0, _lstScale.Count);
                lstScale.Add(_lstScale[intRandom] * StaticRock.dblGameScale);
                lstRockSize.Add(lstRockSize[intRandom]);
            }

            // Get Maximum Rock Speed

            double dblRockMinDistance = StaticRock.dblRockMinDistance;
            double dblRockMaxDistance = StaticRock.dblRockMaxDistance;

            double dblRockMinRotation = StaticRock.dblRockMinRotation;
            double dblRockMaxRotation = StaticRock.dblRockMaxRotation;

            double dblRockAngle = StaticRock.dblRockAngle;

            //// Add Offset to each item
            for (int i = 0; i < lstTravelAngle.Count; i++)
            {
                // Direction of Travel
                double dblAngle = GetRandomDoubleWithIncrement(random, 0, dblRockAngle, 0.1);

                // Spinning of Rock
                double dblRotation = GetRandomDoubleWithIncrement(random, dblRockMinRotation, dblRockMaxRotation, 0.1);

                // Distance Traveled per Idle
                double dblDistance = GetRandomDoubleWithIncrement(random, dblRockMinDistance, dblRockMaxDistance, 0.1);

                lstTravelAngle[i] = RandomizeNegative(random, lstTravelAngle[i], dblAngle).NormalizeAngle();
                lstTravelRotation[i] = RandomizeNegative(random, lstTravelRotation[i], dblRotation).NormalizeAngle();
                lstTravelDistance[i] = Randomize(random, lstTravelDistance[i], dblDistance);
            }

            lstTravelDistance = lstTravelDistance.RoundTo(8);
            lstTravelRotation = lstTravelRotation.RoundTo(8);
            lstTravelAngle = lstTravelAngle.RoundTo(8);
        }

        internal double GetNextSize(ref enumSize RockSize)
        {
            double dblScale = 0;

            switch (RockSize)
            {
                case enumSize.Large:
                    RockSize = enumSize.Medium;
                    dblScale = 12;
                    break;

                default:
                    RockSize = enumSize.Small;
                    dblScale = 6;
                    break;
            }

            return dblScale;
        }

        internal double GetEnumSize(enumSize RockSize)
        {
            double dblScale = 0;

            switch (RockSize)
            {
                case enumSize.Large:
                    dblScale = 24;
                    break;

                case enumSize.Medium:
                    dblScale = 12;
                    break;

                default:
                    dblScale = 6;
                    break;
            }

            return dblScale;
        }

        internal double Randomize(Random random, double dblValue, double dblRandom)
        {
            dblValue += dblRandom;

            return dblValue;
        }

        internal double RandomizeNegative(Random random, double dblValue, double dblRandom)
        {
            if (GetRandomBoolean(random))
                dblValue += dblRandom;
            else
                dblValue -= dblRandom;

            if (GetRandomBoolean(random))
                dblValue *= -1;

            return dblValue;
        }

        private static double GetRandomDoubleWithIncrement(Random random, double minValue, double maxValue, double increment)
        {
            double randomValue = minValue;

            if (maxValue > minValue)
            {
                // Calculate the number of possible values within the range and increment
                int numberOfPossibleValues = (int)((maxValue - minValue) / increment) + 1;

                // Generate a random index within the range of possible values
                int randomIndex = random.Next(0, numberOfPossibleValues);

                // Calculate the random double value based on the random index and increment
                randomValue = minValue + randomIndex * increment;
            }
            return randomValue;
        }

        private static bool GetRandomBoolean(Random random)
        {
            return random.Next(2) == 0;
        }
    }
}