using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseController : CastRay
{
    public Image Mouse;
    private Camera _camera;
    private UIController _uiController;

    public delegate void ClickAction(Clickable.ClickReturn clickReturn, Vector3 clickPositon, bool haveUiSelectedItem);
    public static event ClickAction OnClick;

    void Awake()
    {
        _camera = gameObject.GetComponentInChildren<Camera>();
        _uiController = gameObject.GetComponent<UIController>();
    }

    void Update()
    {
        Mouse.transform.position = GetScreenPosition();
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (_uiController.UiSelectedItem != null)
            {
                if (Input.GetButtonUp("Fire1"))
                {
                    Click(true);
                }
            }else
            if (Input.GetButton("Fire1"))
            {
                Click(false);
            }
        }
    }

    public Vector2 GetScreenPosition()
    {
        return Input.mousePosition;
    }

    private void Click(bool haveUiSelectedItem)
    {
        RaycastHit hit = CastRayFromCameraToMousePosition(_camera);
        if (hit.transform != null)
        {
            if (hit.transform.gameObject.GetComponent<Clickable>() != null)
            {
                var clickable = hit.transform.gameObject.GetComponent<Clickable>();
                var clickReturn = clickable.Click(this.gameObject, hit.point);
                OnClick(clickReturn, hit.point, haveUiSelectedItem);
                return;
            }
        }
        OnClick(null, hit.point, haveUiSelectedItem);
    }

    public Vector3 GetWorldSpacePosition()
    {
        RaycastHit hit = CastRayFromCameraToMousePosition(_camera);
        return hit.point;
    }

    public void AdjustMouseIcon(Sprite sprite)
    {
        Mouse.sprite = sprite;
        if (sprite == null)
        {
            Mouse.enabled = false;
        }else
        {
            Mouse.enabled = true;
        }
    }
}
