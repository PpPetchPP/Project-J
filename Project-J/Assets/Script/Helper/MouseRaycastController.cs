using UnityEngine;

public class MouseRaycastController
{
    public static bool GetRayCastHit(Camera cam, LayerMask layer, out RaycastHit hit)
    {
        hit = new RaycastHit();

        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            return true;

        return false;
    }
}