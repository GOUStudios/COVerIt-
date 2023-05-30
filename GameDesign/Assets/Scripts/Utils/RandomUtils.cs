using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUtils
{
    public static int GetRandomWeightedIndex(float[] weights)
    {
        // Get the total sum of all the weights.
        float weightSum = 0f;
        for (int i = 0; i < weights.Length; ++i)
        {
            weightSum += weights[i];
        }

        // Step through all the possibilities, one by one, checking to see if each one is selected.
        int index = 0;
        int lastIndex = weights.Length - 1;
        while (index < lastIndex)
        {
            // Do a probability check with a likelihood of weights[index] / weightSum.
            if (Random.Range(0, weightSum) < weights[index])
            {
                return index;
            }

            // Remove the last item from the sum of total untested weights and try again.
            weightSum -= weights[index++];
        }

        // No other item was selected, so return very last index.
        return index;
    }

    public static int[] GetRandomPartitions(int numPartitions, int amountToDivide)
    {
        int[] percentageToSpawn = new int[numPartitions];
        float percentageAccumulated = 0.0f;
        for (int i = 0; i < numPartitions - 1; i++)
        {
            float rand = Random.Range(0f, 1f - percentageAccumulated);
            percentageToSpawn[i] = (int)(rand*amountToDivide);
            percentageAccumulated += rand;
        }
        percentageToSpawn[numPartitions - 1] = amountToDivide - (int)(amountToDivide*percentageAccumulated);
        return percentageToSpawn;
    }
}
