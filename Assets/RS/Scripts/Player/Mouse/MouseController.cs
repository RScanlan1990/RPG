using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : CastRay
{
    private Camera _camera;

    public delegate void ClickAction(Clickable.ClickReturn clickReturn, Vector3 clickPosition);
    public static event ClickAction OnClick;

    void Awake()
    {
        _camera = gameObject.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (Input.GetButton("Fire1"))
            {
                Click();
            }
        }
    }

    public Vector2 GetScreenPosition()
    {
        return Input.mousePosition;
    }

    private void Click()
    {
        RaycastHit hit = CastRayFromCameraToMousePosition(_camera);
        if (hit.transform != null)
        {
            if (hit.transform.gameObject.GetComponent<Clickable>() != null)
            {
                var clickable = hit.transform.gameObject.GetComponent<Clickable>();
                var clickReturn = clickable.Click(this.gameObject, hit.point);
                OnClick(clickReturn, hit.point);
                return;
            }
        }
        OnClick(null, hit.point);
    }

    public Vector3 GetWorldSpacePosition()
    {
        RaycastHit hit = CastRayFromCameraToMousePosition(_camera);
        return hit.point;
    }
}
