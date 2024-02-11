using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorManager : MonoBehaviour
{
    private static IndicatorManager _instance = null;
    public static IndicatorManager instance
    {
        get { return _instance; }
    }
    [SerializeField] GameObject indicatorsContainer;
    [SerializeField] RectTransform rangeIndicator;

    private void Awake()
    {
        if (instance == null) { _instance = this; }
        else Destroy(gameObject);

    }

    public void showRangeIndicator(GameObject target, float range)
    {
        GameObject indicatorObject = indicatorsContainer.gameObject;
        indicatorObject.transform.SetParent(target.transform);
        indicatorObject.transform.position = Vector3.zero;

        rangeIndicator.sizeDelta = new Vector2(range * 2, range * 2);
        rangeIndicator.gameObject.SetActive(true);
    }

    public void hideRangeIndicator()
    {
        GameObject indicatorObject = indicatorsContainer.gameObject;
        indicatorObject.transform.SetParent(gameObject.transform);
        indicatorObject.transform.position = Vector3.zero;

        rangeIndicator.gameObject.SetActive(false);
    }
}