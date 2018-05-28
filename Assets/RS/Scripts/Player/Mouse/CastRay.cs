using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastRay : MonoBehaviour
{
    internal RaycastHit CastRayFromCameraToMousePosition(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, camera.farClipPlane))
        {
            return hit;
        }

        return new RaycastHit();
    }

    internal RaycastHit CastRayFromPointInDirection(Vector3 startPos, Vector3 direction, float distance)
    {
        RaycastHit hit;
        Physics.Raycast(startPos, direction, out hit, distance);
        return hit;
    }

    internal RaycastHit CastFromBetweenTwoPoint(Vector3 startPos, Vector3 endPos)
    {
        RaycastHit hit;
        Physics.Raycast(startPos, endPos, out hit);
        return hit;
    }
}
