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
            MoveTowardsWorldPositon(path);
        }
    }

    private void MoveTowardsWorldPositon(NavMeshPath path)
    {
        navMeshAgent.SetPath(path);
    }
}