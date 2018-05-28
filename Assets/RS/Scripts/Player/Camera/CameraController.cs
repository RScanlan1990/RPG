using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera Camera { get; private set; }

    private float maxZoom = 35.0f;
    private float minZoom = 5.0f;
    private float _zoomValue;
    private GameObject XRotator;

    void Start()
    {
        Camera = gameObject.GetComponentInChildren<Camera>();
        transform.rotation = Quaternion.Euler(35.0f, 0, 0);
        XRotator = gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        var ZoomInput = Input.GetAxis("Vertical");
        ZoomCamera(ZoomInput);
        var VerticalRotation = Input.GetAxis("VerticalRotation");
        var HorizontalRotation = Input.GetAxis("Horizontal");
        RotateCamera(VerticalRotation, HorizontalRotation);
    }

    private void ZoomCamera(float input)
    {
        var localForward = Camera.transform.worldToLocalMatrix.MultiplyVector(Camera.transform.forward);
        var forwardZoom = localForward * input;

        if (input > 0)
        {
            if (Vector3.Distance(transform.position, Camera.transform.position) > minZoom)
            {
                ZoomCamera(forwardZoom);
            }
        }

        if (input < 0)
        {
            if (Vector3.Distance(transform.position, Camera.transform.position) < maxZoom)
            {
                ZoomCamera(forwardZoom);
            }
        }
    }

    private void ZoomCamera(Vector3 zoom)
    {
        
        Camera.transform.Translate(zoom * 15.0f);
    }

    private void RotateCamera(float x, float y)
    {
        //var xRotation = XRotator.transform.rotation;
        //xRotation.x -= x;
        //xRotation.x = Mathf.Clamp(xRotation.x, 0.0f, 0.6f);
        //XRotator.transform.rotation = xRotation;

        var yRotation = transform.rotation;
        yRotation.y = 0.0f;
        yRotation.x = 0.0f;
        yRotation.z = 0.0f;
        transform.rotation = yRotation;
        transform.Rotate(transform.up, y);

    }

    private void RotateCameraAroundX(float input)
    {

        var rotation = XRotator.transform.rotation;
        rotation.x -= input * (10.0f * Time.deltaTime);
        rotation.x = Mathf.Clamp(rotation.x, 0.0f, 0.6f);
        rotation.y = 0;
        rotation.z = 0;
        XRotator.transform.rotation = rotation;
    }

    private void RotateCameraAroundY(float input)
    {

        var rotation = transform.rotation;
        rotation.x = 0.0f;
        rotation.y = input * (10.0f * Time.deltaTime);
        rotation.y = Mathf.Clamp(rotation.x, 0.0f, 0.6f);
        rotation.z = 0;
        XRotator.transform.rotation = rotation;
    }
}
