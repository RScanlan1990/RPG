using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PostProcess_Controller))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Fishing))]
[RequireComponent(typeof(UIController))]
[RequireComponent(typeof(MouseController))]
class Player : MonoBehaviour {

    private CameraController _cameraController;
    //private Target _target;

    private AnimationRouter _animationController;

    void Awake ()
	{
	    var cameraParent = gameObject.GetComponentInChildren<Camera>().gameObject.transform.parent.gameObject;
	    _cameraController = cameraParent.AddComponent<CameraController>();
        //_target = gameObject.AddComponent<Target>();
        _animationController = gameObject.AddComponent<AnimationRouter>();
	}

    public void SendAnimationEventToController(AnimationRouter.AnimationEvent animationEvent)
    {
        _animationController.AnimationEventRouter(animationEvent);
    }
}
