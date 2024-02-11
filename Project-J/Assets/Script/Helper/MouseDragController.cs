using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDragController
{
    private static bool isDrag;
    private static Vector3 oriPos;
    private static Vector3 currentPos;
    private static Vector3 delta;

    public static async Awaitable<Vector3> GetMouseDeltaAsync(bool isBypassUi = false)
    {
        delta = Vector3.zero;

        if (!Application.isFocused)
        {
            isDrag = false;
            return delta;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isBypassUi)
            {
                //prevent drag on ui
                if (EventSystem.current.IsPointerOverGameObject())
                    return delta;
            }

            isDrag = true;
            oriPos = Input.mousePosition;

            await Awaitable.NextFrameAsync();
        }

        if (Input.GetMouseButtonUp(0))
            isDrag = false;

        if (isDrag)
        {
            currentPos = Input.mousePosition;
            delta = currentPos - oriPos;

            oriPos += delta;
        }

        return delta;
    }
}