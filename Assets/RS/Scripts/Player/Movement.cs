using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float PlayerSpeed;

    void OnEnable()
    {
        MouseController.OnClick += MoveTowardsWorldPositon;
    }

    void OnDisable()
    {
        MouseController.OnClick -= MoveTowardsWorldPositon;
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public void MoveTowardsWorldPositon(Clickable.ClickReturn clickReturn, Vector3 clickPosition)
    {
        if (clickReturn == null)
        {
            var lookPos = clickPosition - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100.0F);

            clickPosition.y = transform.position.y;
            this.transform.position += (clickPosition - this.transform.position).normalized * PlayerSpeed * Time.fixedDeltaTime * Mathf.Clamp(Vector3.Distance(clickPosition, transform.position), 0.0f, 1.0f);
        }
    }
}