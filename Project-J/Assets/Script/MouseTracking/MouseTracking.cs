using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracking : MonoBehaviour
{
    private static MouseTracking _instance = null;
    public static MouseTracking instance
    {
        set { _instance = value; }
        get { return _instance; }
    }
    public bool isUpdateMousePosition = false;
    public Vector3 getMouseFloorPosition
    {
        get { return mouseFloorPosition; }
    }
    private Vector3 mouseFloorPosition;
    private Plane plane;
    float floorY = -1;
    private void Awake()
    {
        if (_instance == null) instance = this;
        plane = new Plane(Vector3.up, floorY);
    }

    void Update()
    {
        if (isUpdateMousePosition) UpdateWorldMousePosition();
    }

    private void UpdateWorldMousePosition()
    {
        Vector3 mouseUiPos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouseUiPos);
        float hitDistance;
        if (plane.Raycast(ray, out hitDistance))
        {
            Vector3 hitPosition = ray.GetPoint(hitDistance);
            mouseFloorPosition = hitPosition;
        }
    }
}
