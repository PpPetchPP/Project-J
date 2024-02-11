using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator
{
    public static float GetDistance(Vector3 self, Vector3 target)
    {
        Vector2 newSelfPosition = new Vector2(self.x, self.z);
        Vector2 newTargetPosition = new Vector2(target.x, target.z);
        float currentDistance = Vector2.Distance(newSelfPosition, newTargetPosition);
        return currentDistance;
    }
    public static bool CheckDistance(Vector3 self, Vector3 target, float distanceValue)
    {
        float currentDistance = GetDistance(self, target);
        return currentDistance <= distanceValue;
    }
}
