using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class Vector3Utils
    {
        public static Vector3 GetCenter(Vector3[] points)
        {
            if (points.Length == 0) return Vector3.zero;
            var sum = points.Aggregate(Vector3.zero, (current, point) => current + point);
            return sum / points.Length;
        }
    }
}