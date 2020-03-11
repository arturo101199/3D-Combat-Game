using UnityEngine;

namespace Utilities.Vector
{
    public class VectorUtilities
    {
        public static Vector3 LerpXZ(Vector3 origin, Vector3 destination, float t)
        {
            Vector3 result = Vector3.Lerp(origin, destination, t);
            result.y = origin.y;
            return result;
        }
    }
}

