using UnityEngine;

public class Movement : MonoBehaviour {

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public void MoveTowardsWorldPositon(float speed, Vector3 position)
    {
        var lookPos = position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100.0F);

        position.y = transform.position.y;
        this.transform.position += (position - this.transform.position).normalized * speed * Time.fixedDeltaTime * Mathf.Clamp(Vector3.Distance(position, transform.position), 0.0f, 1.0f);
    }
}