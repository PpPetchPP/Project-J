using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private LayerMask walkLayer;
    [SerializeField] private CameraMovementController cameraMovement;
    [SerializeField] private MouseTracking mouseTracking;
    [SerializeField] private CharacterMovement characterMovement;

    void Start()
    {
        cameraMovement.FreeMove().Forget();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            MouseRaycastController.GetRayCastHit(mainCam, walkLayer, out var result);
            characterMovement.MoveAsync(result.point, MoveComplete).Forget();
        }
    }

    private void MoveComplete()
    {
        Debug.Log("MoveComplete");
    }
}