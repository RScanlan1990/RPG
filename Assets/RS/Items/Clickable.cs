using UnityEngine;

public class Clickable : MonoBehaviour
{
    public float MinimuimDistance;
    public ClickReturn.ClickActions ClickAction;
    public ClickReturn.ClickTypes ClickType;

    public virtual ClickReturn Click(GameObject clicker, Vector3 hitPosition)
    {
        if (Vector3.Distance(this.transform.position, clicker.transform.position) <= MinimuimDistance)
        {
            return new ClickReturn(ClickAction, ClickType, hitPosition, this.gameObject);
        }
        return null;
    }

    public class ClickReturn
    {
        public ClickActions ClickAction;
        public ClickTypes ClickType;
        public Vector3 ClickPosition;
        public GameObject ClickedObject;

        public ClickReturn(ClickActions clickAction, ClickTypes clickType, Vector3 clickPosition, GameObject clickedObject)
        {
            ClickAction = clickAction;
            ClickType = clickType;
            ClickPosition = clickPosition;
            ClickedObject = clickedObject;
        }

        public enum ClickTypes
        {
            Skill, Item
        }

        public enum ClickActions
        {
            Fish, PickUp
        }
    }
}
