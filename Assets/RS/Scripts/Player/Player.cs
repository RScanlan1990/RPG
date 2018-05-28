using UnityEngine;
using UnityEngine.EventSystems;

class Player : MonoBehaviour {

    public float PlayerSpeed;
    public float CameraSpeed;
    public Vector3 CameraOffset;
    public Transform Inventory;

    private CameraController _cameraController;
    private MousePointer _mousePointer;
    private Movement _movement;
    //private Target _target;

    private Skills _skills;
    private AnimationRouter _animationController;
    private Inventory _inventory;
    private PostProcess_Controller _postProcess;

	void Awake ()
	{
	    var cameraParent = gameObject.GetComponentInChildren<Camera>().gameObject.transform.parent.gameObject;
	    _cameraController = cameraParent.AddComponent<CameraController>();
	    _mousePointer = gameObject.AddComponent<MousePointer>();
        _movement = gameObject.AddComponent<Movement>();
        //_target = gameObject.AddComponent<Target>();
        _skills = gameObject.AddComponent<Skills>();
        _animationController = gameObject.AddComponent<AnimationRouter>();
	    _inventory = gameObject.AddComponent<Inventory>();
	    _postProcess = gameObject.AddComponent<PostProcess_Controller>();
        _inventory.Init(Inventory);
	}

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            var clickable = _mousePointer.Click();
            if (Input.GetButton("Fire1") && clickable == null)
            {
                var mousePosition = _mousePointer.GetWorldSpacePosition();
                _movement.MoveTowardsWorldPositon(PlayerSpeed, mousePosition);
                return;
            }

            if (Input.GetButtonDown("Fire1") && clickable != null)
            {
                DoClickable(clickable);
            }
        }
    }

    private void DoClickable(Clickable.ClickReturn clickReturn)
    {
        switch(clickReturn.ClickType)
        {
            case Clickable.ClickReturn.ClickTypes.Skill:
                _skills.DoSkill(clickReturn);
                break;
            case Clickable.ClickReturn.ClickTypes.Item:
                _inventory.DoItem(clickReturn);
                break;
        }
    }

    public void SendAnimationEventToController(AnimationRouter.AnimationEvent animationEvent)
    {
        _animationController.AnimationEventRouter(animationEvent);
    }
}
