using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod_Loot : Loot
{
    private Transform HookDefaultPosition;
    private LineRenderer _fishingLine;
    private bool _hookActive;
    private Vector3 _hookPosition;

    void Start()
    {
        _fishingLine = gameObject.GetComponentInChildren<LineRenderer>();
        HookDefaultPosition = _fishingLine.transform;
        _hookActive = false;
    }


    void Update()
    {
        if (_hookActive)
        {
            _fishingLine.SetPosition(_fishingLine.positionCount - 1, _fishingLine.gameObject.transform.InverseTransformPoint(_hookPosition));
        }
    }

    public void SetFishingLinePostion(Vector3 hookPosition)
    {
        _fishingLine.positionCount += 1;
        _hookPosition = hookPosition;
        _hookActive = true;
    }

    public void ResetFishingLinePosition()
    {
        _hookActive = false;
        _fishingLine.positionCount -= 1;
    }
}
