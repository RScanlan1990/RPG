using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Skills))]
[RequireComponent(typeof(UIController))]
[RequireComponent(typeof(MouseController))]
class Player : MonoBehaviour {

    private CameraController _cameraController;
    //private Target _target;

    private AnimationRouter _animationController;
    private PostProcess_Controller _postProcess;

    void Awake ()
	{
	    var cameraParent = gameObject.GetComponentInChildren<Camera>().gameObject.transform.parent.gameObject;
	    _cameraController = cameraParent.AddComponent<CameraController>();
        //_target = gameObject.AddComponent<Target>();
        _animationController = gameObject.AddComponent<AnimationRouter>();
	    _postProcess = gameObject.AddComponent<PostProcess_Controller>();
	}

    public void SendAnimationEventToController(AnimationRouter.AnimationEvent animationEvent)
    {
        _animationController.AnimationEventRouter(animationEvent);
    }
}
