using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcess_Controller : MonoBehaviour
{
    public float DofFocalLengthMax;
    public float DofFocalLengthMin;

    private Camera _camera;
    private CameraController _cameraController;
    private PostProcessingProfile _postProcessing;
    private Movement _movement;

    void Awake()
    {
        _camera = gameObject.GetComponentInChildren<Camera>();
        _cameraController = gameObject.GetComponentInChildren<CameraController>();
        _postProcessing = _camera.GetComponent<PostProcessingBehaviour>().profile;
    }

    void Start()
    {
        _movement = gameObject.GetComponent<Movement>();
        _postProcessing.depthOfField.settings = CalculateDof();
    }

    void Update()
    {
        _postProcessing.depthOfField.settings = CalculateDof();
        _postProcessing.colorGrading.settings = WaterEffect(_movement.GetPlayerPosition().y + 2.0f);
    }

    private DepthOfFieldModel.Settings CalculateDof()
    {
        var focusVector = transform.position;
        var dofSettings = _postProcessing.depthOfField.settings;
        dofSettings.focusDistance = Vector3.Distance(_camera.transform.position, focusVector);
        return dofSettings;
    }

    public ColorGradingModel.Settings WaterEffect(float playerHeight)
    {
        var colorSettings = _postProcessing.colorGrading.settings;
        if (playerHeight < 0)
        {
            colorSettings.basic.temperature = Mathf.Lerp(colorSettings.basic.temperature, -50.0f, Time.deltaTime);
        }
        else
        {
            colorSettings.basic.temperature = Mathf.Lerp(colorSettings.basic.temperature, 0.0f, Time.deltaTime);
        }
        return colorSettings;
    }
}
