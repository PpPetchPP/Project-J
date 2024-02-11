using System;
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
    [Header("Indicator")]
    [SerializeField] RectTransform rangeIndicator;
    [SerializeField] RectTransform aoeIndicator;
    [SerializeField] RectTransform shotIndicator;
    [Header("Indicator Colider")]
    [SerializeField] SphereCollider aoeColider;
    [SerializeField] BoxCollider shotColider;

    private void Awake()
    {
        if (instance == null) { _instance = this; }
        else Destroy(gameObject);

    }

    private void Update()
    {
        if (aoeIndicator.gameObject.activeSelf) updateAOEIndicatorPosition();
        if (shotIndicator.gameObject.activeSelf) updateShotIndicatorPosition();
    }

    public void hideIndicator()
    {
        GameObject indicatorObject = indicatorsContainer.gameObject;
        indicatorObject.transform.SetParent(gameObject.transform);
        indicatorObject.transform.position = Vector3.zero;

        rangeIndicator.gameObject.SetActive(false);
        aoeIndicator.gameObject.SetActive(false);
        shotIndicator.gameObject.SetActive(false);
    }

    private void seUpIndicator(GameObject target)
    {
        hideIndicator();
        GameObject indicatorObject = indicatorsContainer.gameObject;
        indicatorObject.transform.SetParent(target.transform);
        indicatorObject.transform.position = Vector3.zero;
    }

    public void showRangeIndicator(GameObject target, float range)
    {
        seUpIndicator(target);
        rangeIndicator.sizeDelta = new Vector2(range * 2, range * 2);

        rangeIndicator.gameObject.SetActive(true);
    }

    public void showAOEIndicator(GameObject target, float range, float radius)
    {
        showRangeIndicator(target, range);
        aoeColider.radius = radius;
        aoeIndicator.sizeDelta = new Vector2(radius * 2, radius * 2);
        aoeIndicator.transform.position = target.transform.position;

        aoeIndicator.gameObject.SetActive(true);
    }

    public void showShotIndicator(GameObject target, float range, float radius)
    {
        seUpIndicator(target);
        shotColider.center = new Vector3(0, range / 2, 0);
        shotColider.size = new Vector3(radius, range, 1);

        shotIndicator.sizeDelta = new Vector2(radius, range);
        shotIndicator.transform.position = transform.transform.position;

        shotIndicator.gameObject.SetActive(true);
    }

    private void updateAOEIndicatorPosition()
    {
        float range = rangeIndicator.sizeDelta.x / 2;
        Vector3 mousePos = MouseTracking.instance.getMouseFloorPosition;
        Vector3 centerPos = rangeIndicator.transform.position;
        Vector3 diff = mousePos - centerPos;
        Vector3 newPos = mousePos;
        if (diff.magnitude > range) newPos = centerPos + diff.normalized * range;
        aoeIndicator.transform.position = newPos;
    }
    private void updateShotIndicatorPosition()
    {
        Vector3 mousePos = MouseTracking.instance.getMouseFloorPosition;
        Vector3 centerPos = rangeIndicator.transform.position;
        Vector3 dir = (mousePos - centerPos).normalized;
        float offset = -90;
        float angleRadians = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg + offset;
        shotIndicator.transform.localEulerAngles = new Vector3(0, 0, angleRadians);
    }
}