using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PostProcess_Controller))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Equiped))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Fishing))]
[RequireComponent(typeof(UIController))]
[RequireComponent(typeof(CameraController))]
[RequireComponent(typeof(MouseController))]
class Player : MonoBehaviour {

    private AnimationRouter _animationController;

    void Awake ()
	{
        _animationController = gameObject.AddComponent<AnimationRouter>();
	}

    public void SendAnimationEventToController(AnimationRouter.AnimationEvent animationEvent)
    {
        _animationController.AnimationEventRouter(animationEvent);
    }
}
