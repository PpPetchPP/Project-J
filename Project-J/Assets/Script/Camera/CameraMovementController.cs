using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField] private Transform freeMovePoint;
    [SerializeField] private Transform focusTargetPoint;
    [SerializeField] private CinemachineFreeLook freeLookCam;
    [SerializeField] private Collider camLimit;
    [SerializeField] private float freeSpeed = 2f;

    private float speed;

    private Bounds bounds => camLimit.bounds;

    async void Update()
    {
        if (!Application.isFocused)
            return;

        var delta = -await MouseDragController.GetMouseDeltaAsync();
        var camFocusPos = freeMovePoint.transform.position;

        var newPos = new Vector3()
        {
            x = Mathf.Clamp(camFocusPos.x + delta.x * speed * Time.deltaTime, bounds.min.x, bounds.max.x),
            y = camFocusPos.y,
            z = Mathf.Clamp(camFocusPos.z + delta.y * speed * Time.deltaTime, bounds.min.z, bounds.max.z),
        };

        freeMovePoint.transform.position = newPos;
    }

    public async Awaitable FreeMove()
    {
        speed = freeSpeed;

        freeMovePoint.position = focusTargetPoint.position;

        SetTargetAndFollow(freeMovePoint);
        SetCameraHeight(0.5f);

        await AwaitableHelper.Completed();
    }

    public async Awaitable SelectNodeMove()
    {
        speed = freeSpeed;

        SetTargetAndFollow(freeMovePoint);
        SetCameraHeight(1f);

        await AwaitableHelper.Completed();
    }

    public async Awaitable FocusTarget()
    {
        SetTargetAndFollow(focusTargetPoint);
        SetCameraHeight(0.5f);

        await AwaitableHelper.Completed();
    }

    private void SetTargetAndFollow(Transform target)
    {
        freeLookCam.LookAt = target;
        freeLookCam.Follow = target;
    }

    private void SetCameraHeight(float value, float duration = 0.5f, Ease ease = Ease.Linear)
    {
        DOVirtual.Float(freeLookCam.m_YAxis.Value, value, duration, (value) => { freeLookCam.m_YAxis.Value = value; }).SetEase(ease);
    }
}