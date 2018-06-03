using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private GameObject _target;

    public bool SetTarget(GameObject target)
    {
        if(IsGameObjectTargetable(target)) {
            _target = target;
            return true;
        }
        return false;
    }

    public bool IsGameObjectTargetable(GameObject target)
    {
        if(target.GetComponent<Clickable>() == null)
        {
            return false;
        }
        return true;
    }

    public string GetTargetsName()
    {
        return _target == null ? "No Target" : _target.name;
    }

    public float GetDistanceToTarget()
    {
        return _target == null 
            ? Mathf.Infinity 
            : Vector3.Distance(transform.position, _target.transform.position);
    } 

    public bool HaveTarget()
    {
        return _target != null;
    }

    public bool IsTargetWithinRange(float range)
    {
        if (Vector3.Distance(_target.transform.position, transform.position) <= range)
        {
            return true;
        }
        return false;
    }
}

