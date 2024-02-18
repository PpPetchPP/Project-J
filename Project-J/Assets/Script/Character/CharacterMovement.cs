using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class CharacterMovement
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float pathEndThreshold = 0.1f;
    [SerializeField] private PlayerMarkerBlink markerRef;
    [SerializeField] private Transform markerContainer;

    private CharacterController controller;

    private GameObject marker;
    private bool isAgentHasPath = false;
    private float totalMove;
    private float travelDistance;
    private Action currentOnComplete;

    public void Initialize(CharacterController controller)
    {
        this.controller = controller;
    }

    public async Awaitable MoveAsync(Vector3 target, Action onComplete = null)
    {
        agent.SetDestination(target);

        while (agent.remainingDistance == 0)
            await Awaitable.NextFrameAsync();

        totalMove = agent.remainingDistance;

        if (marker == null)
            marker = GameObject.Instantiate(markerRef, markerContainer).gameObject;

        marker.transform.position = new Vector3(target.x, target.y = 0.1f, target.z);

        if (currentOnComplete != null)
            return;

        currentOnComplete = onComplete;

        while (!IsAgentComplete())
            await Awaitable.NextFrameAsync();

        GameObject.Destroy(marker);
        currentOnComplete?.Invoke();
        currentOnComplete = null;
    }

    private bool IsAgentComplete()
    {
        isAgentHasPath |= agent.hasPath;
        if (controller.remainMoveDistance <= 0 || (isAgentHasPath && agent.remainingDistance <= agent.stoppingDistance + pathEndThreshold))
        {
            // Complete
            agent.isStopped = true;
            isAgentHasPath = false;
            return true;
        }
        agent.isStopped = false;

        Debug.Log($"TotalMove: {totalMove}");
        Debug.Log($"agent.remainingDistance: {agent.remainingDistance}");

        travelDistance = totalMove - agent.remainingDistance;
        controller.remainMoveDistance -= travelDistance;

        return false;
    }
}