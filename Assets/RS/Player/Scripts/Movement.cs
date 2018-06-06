using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public float PlayerSpeed;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        MouseController.OnClick += CalculatePathAndMove;
    }

    void OnDisable()
    {
        MouseController.OnClick -= CalculatePathAndMove;
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    private void CalculatePathAndMove(Clickable.ClickReturn clickReturn, Vector3 clickPosition, bool haveUiSelectedItem)
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(clickPosition, path);
        if (haveUiSelectedItem == false)
        {
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                MoveTowardsWorldPositon(clickPosition);
            }
            else
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                var pathCornerCount = path.corners.Length;
                var lastPoint = path.corners[pathCornerCount - 1];

                MoveTowardsWorldPositon(lastPoint);
            }
        }
    }

    private void MoveTowardsWorldPositon(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }
}