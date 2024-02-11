using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDistanceCalulator : MonoBehaviour
{
    [SerializeField] GameObject selfGameObject;
    [SerializeField] GameObject targerGameObject;
    [SerializeField] float distanceValue = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Vector3 selfPos = selfGameObject.transform.position;
            Vector3 targetPos = targerGameObject.transform.position;
            bool x = DistanceCalculator.CheckDistance(selfPos, targetPos, distanceValue);
            Debug.Log(x);
            Debug.Log(DistanceCalculator.GetDistance(selfPos, targetPos));
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            IndicatorManager.instance.showRangeIndicator(selfGameObject, 10);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            IndicatorManager.instance.showRangeIndicator(selfGameObject, 5);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            IndicatorManager.instance.showAOEIndicator(selfGameObject, 10, 5);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            IndicatorManager.instance.showAOEIndicator(selfGameObject, 15, 3);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            IndicatorManager.instance.showShotIndicator(selfGameObject, 10, 3);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            IndicatorManager.instance.hideIndicator();
        }
    }
}
