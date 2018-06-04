using UnityEngine;

public class Health : MonoBehaviour {

    public float MaxHealth;
    private float _health;

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    public void Heal(float amount)
    {
        _health += amount;
    }

    public float GetHealth()
    {
        return MaxHealth / _health;
    }
}
