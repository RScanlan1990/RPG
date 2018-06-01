using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{

    public GameObject FishingLineDefaultPosition;
    private GameObject _activeHook;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = gameObject.GetComponentInChildren<LineRenderer>();
    }

    private void Update()
    {
        FishingLine();
    }

    private void FishingLine()
    {
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _lineRenderer.gameObject.transform.InverseTransformPoint(ReturnActiveHookPositon()));   
    }

    private Vector3 ReturnActiveHookPositon()
    {
        if (HaveActiveHook())
        {
            return _activeHook.transform.position;
        }

        return FishingLineDefaultPosition.transform.position;
    }

    private bool HaveActiveHook()
    {
        return _activeHook != null && _activeHook.activeSelf;
    }
}
