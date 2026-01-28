using UnityEngine;

public class Character : MonoBehaviour, IAttackable
{
    private HealthSystem healthSystem;

    public GameObject GameObject => gameObject;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    public void Damage(int damage)
    {
        healthSystem.Damage(damage);
    }

}
