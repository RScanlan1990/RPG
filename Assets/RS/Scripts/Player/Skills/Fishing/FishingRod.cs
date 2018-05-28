using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour {

    public GameObject HookPrefab;
    public GameObject FishingLineDefaultPosition;
    private GameObject _activeHook;
    private LineRenderer _lineRenderer;
    private AnimationRouter _animationRouter;

    private void Start()
    {
        _animationRouter = gameObject.GetComponentInParent<AnimationRouter>();
        _lineRenderer = gameObject.GetComponentInChildren<LineRenderer>();
    }

    private void Update()
    {
        FishingLine();
        if (_animationRouter.CurrentEvent == AnimationRouter.AnimationEvent.MidCast)
        {
            ActivateHook();
        }
    }

    private void ActivateHook()
    {
        if (_activeHook.gameObject != null && _activeHook.activeSelf == false)
        {
            Debug.Log("Hook Active");
            _activeHook.SetActive(true);
        }
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

    public void Cast(Vector3 castPosition)
    {
        Debug.Log("Cast");
        _activeHook = Instantiate(HookPrefab, castPosition, Quaternion.identity);
        _activeHook.transform.SetParent(null);
        _activeHook.SetActive(false);
    }

    public void ReelIn()
    {
        Destroy(_activeHook);
    }
}
