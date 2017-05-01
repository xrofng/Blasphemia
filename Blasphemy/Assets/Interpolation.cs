using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolation : MonoBehaviour
{
    public static Vector3 AccelDecelInterpolation(Vector3 start, Vector3 end, float t)
    {
        float x = end.x - start.x;
        float y = end.y - start.y;
        float z = end.z - start.z;

        float newT = (Mathf.Cos((t + 1) * Mathf.PI) / 2) + 0.5f;

        x *= newT;
        y *= newT;
        z *= newT;

        Vector3 retVector = new Vector3(start.x + x, start.y + y, start.z + z);
        return retVector;
    }

    public static Vector3 AccelerationInterpolation(Vector3 start, Vector3 end, float t, float factor)
    {
        float x = end.x - start.x;
        float y = end.y - start.y;
        float z = end.z - start.z;

        float newT = t;

        if (FloatEquals(factor, 1))
        {
            newT *= newT;
        }
        else
        {
            Mathf.Pow(newT, 2 * factor);
        }
        x *= newT;
        y *= newT;
        z *= newT;

        Vector3 retVector = new Vector3(start.x + x, start.y + y, start.z + z);
        return retVector;
    }

    private static bool FloatEquals(float f1, float f2)
    {
        return Mathf.Abs(f1 - f2) < 0.001f;
    }

}
