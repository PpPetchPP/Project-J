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

    private GameObject marker;
    private bool isAgentHasPath = false;
    private Action currentOnComplete;

    public async Awaitable MoveAsync(Vector3 target, Action onComplete = null)
    {
        agent.SetDestination(target);

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
        if (isAgentHasPath && agent.remainingDistance <= agent.stoppingDistance + pathEndThreshold)
        {
            // Complete
            isAgentHasPath = false;
            return true;
        }

        return false;
    }
}