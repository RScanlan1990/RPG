using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera Camera { get; private set; }
    private float _cameraXRotation;
    private float _cameraValue;

    private float maxZoom = 50.0f;
    private float minZoom = 2.0f;

    void Start()
    {
        _cameraXRotation = 35.0f;
        _cameraValue = 1.0f;
        Camera = gameObject.GetComponentInChildren<Camera>();
        transform.rotation = Quaternion.Euler(_cameraXRotation, 0, 0);
    }

    void Update()
    {
        var input = Input.GetAxis("Mouse ScrollWheel");
        SetCameraPostion(input);
        transform.rotation = Quaternion.Euler(_cameraXRotation, 0, 0);
    }

    private void SetCameraPostion(float input)
    {
        TryZoomCamera(input);
    }

    private void TryZoomCamera(float input)
    {
        var localForward = Camera.transform.worldToLocalMatrix.MultiplyVector(Camera.transform.forward);
        var zoom = localForward * input;

        if (input > 0)
        {
            if (Vector3.Distance(transform.position, Camera.transform.position) > minZoom)
            {
                ZoomCamera(zoom);
            }
        }

        if (input < 0)
        {
            if (Vector3.Distance(transform.position, Camera.transform.position) < maxZoom)
            {
                ZoomCamera(zoom);
            }
        }
    }

    private void ZoomCamera(Vector3 zoom)
    {
        Camera.transform.Translate(zoom * 10.0f);
    }
}
