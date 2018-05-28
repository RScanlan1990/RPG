using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcess_Controller : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _focusVector;

    void Start()
    {
        _camera = gameObject.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        _focusVector = this.transform.position;
        PostProcessingProfile postProcProf = _camera.GetComponent<PostProcessingBehaviour>().profile;
        var dofSettings = postProcProf.depthOfField.settings;
        dofSettings.focusDistance = Vector3.Distance(_camera.transform.position, _focusVector);
        postProcProf.depthOfField.settings = dofSettings;
    }
}
