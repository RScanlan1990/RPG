using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera Camera { get; private set; }

    private float maxZoom = 35.0f;
    private float minZoom = 5.0f;
    private float maxHeight = 50.0f;
    private float minHeight = 1.0f;
    private float _zoomValue;
    private GameObject _player;
    private Vector3 _playerPos;

    void Start()
    {
        Camera = gameObject.GetComponentInChildren<Camera>();
        _player = gameObject.transform.parent.gameObject;
        transform.parent = null;
    }

    void Update()
    {
        _playerPos = new Vector3(_player.transform.position.x, _player.transform.position.y + 1.0f, _player.transform.position.z); ;
        FollowPlayer();
        LookAtPlayer();
        ZoomCamera(Input.GetAxis("Zoom"));
        AdjustCameraHeight(Input.GetAxis("Vertical"));
        HorizontalRotation(Input.GetAxis("Horizontal"));
    }

    private void FollowPlayer()
    {
        transform.position = _playerPos;
    }

    private void LookAtPlayer()
    {
        Camera.transform.LookAt(_playerPos);
    }

    private void ZoomCamera(float input)
    {
        var localForward = Camera.transform.worldToLocalMatrix.MultiplyVector(Camera.transform.forward);
        var forwardZoom = localForward * input;
        if (input > 0.0f && Vector3.Distance(Camera.transform.position, _playerPos) > minZoom)
        {
            Camera.transform.Translate(forwardZoom);
        }

        if (input < 0.0f && Vector3.Distance(Camera.transform.position, _playerPos) < maxZoom)
        {
            Camera.transform.Translate(forwardZoom);
        }
    }

    private void AdjustCameraHeight(float input)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            var distanceToGround = hit.distance;
            var direction = transform.up * input;
            if (input < 0.0f && distanceToGround > minHeight)
            {
                Camera.transform.Translate(direction);
            }

            if (input > 0.0f && distanceToGround < maxHeight)
            {
                Camera.transform.Translate(direction);
            }
        }
    }

    private void HorizontalRotation(float input)
    {
        transform.Rotate(transform.up * (input * 5.0f));
    }
}
