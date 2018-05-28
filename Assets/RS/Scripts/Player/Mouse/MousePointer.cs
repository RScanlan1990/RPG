using UnityEngine;

public class MousePointer : CastRay
{
    private Camera _camera;

    public void Start()
    {
        _camera = gameObject.GetComponentInChildren<Camera>();
    }

    public Vector2 GetScreenPosition()
    {
        return Input.mousePosition;
    }

    public Vector3 GetWorldSpacePosition() 
	{
        RaycastHit hit = CastRayFromCameraToMousePosition(_camera);
        return hit.point;
    }

    public Clickable.ClickReturn Click()
    {
        RaycastHit hit = CastRayFromCameraToMousePosition(_camera);
        if (hit.transform != null)
        {
            if (hit.transform.gameObject.GetComponent<Clickable>() != null)
            {
                var clickable = hit.transform.gameObject.GetComponent<Clickable>();
                return clickable.Click(this.gameObject, hit.point);
            }
        }
        return null;
    }
}
