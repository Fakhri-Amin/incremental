using LayerLab.ArtMaker;
using UnityEngine;

public class Character : MonoBehaviour, IAttackable, ICharacter
{
    [SerializeField] private PartsManager partsManager;

    private HealthSystem healthSystem;

    public GameObject GameObject => gameObject;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    void OnEnable()
    {
        healthSystem.OnHealthChanged += OnHit;
    }

    void OnDisable()
    {
        healthSystem.OnHealthChanged += OnHit;
    }

    public void Damage(int damage)
    {
        healthSystem.Damage(damage);
    }

    private void OnHit()
    {
        partsManager.PlayAnimationOnce("Hit");
    }

    public void Reset()
    {
        partsManager.PlayAnimation("Idle");
    }

}
