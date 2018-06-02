using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FishingRod", menuName = "Items/Skills/Fishing/FishingRod")]
public class FishingRod : Equipable
{
    private Transform HookDefaultPosition;
    private LineRenderer _lineRenderer;
    private bool _hookActive;
    private Vector3 _hookPosition;

    void Start()
    {
        _lineRenderer = ClickableGameObject.GetComponentInChildren<LineRenderer>();
        HookDefaultPosition = _lineRenderer.transform;
        _hookActive = false;
    }

    void Update()
    {
        if (_hookActive)
        {
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _lineRenderer.gameObject.transform.InverseTransformPoint(_hookPosition));
        }
    }

    public void SetFishingLinePostion(Vector3 hookPosition)
    {
        _lineRenderer.positionCount += 1;
        _hookPosition = hookPosition;
        _hookActive = true;
    }

    public void ResetFishingLinePosition()
    {
        _hookActive = false;
        _lineRenderer.positionCount -= 1;
    }
}
