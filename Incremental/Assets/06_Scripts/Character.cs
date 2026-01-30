using System.Collections;
using LayerLab.ArtMaker;
using Sirenix.OdinInspector;
using UnityEngine;

public class Character : MonoBehaviour, IAttackable, ICharacter
{
    [SerializeField] private PartsManager partsManager;

    private HealthSystem healthSystem;
    private Collider2D characterCollider;
    private bool isDead;

    public GameObject GameObject => gameObject;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        characterCollider = GetComponent<Collider2D>();
    }

    void Start()
    {
        partsManager.Init();
        StartCoroutine(RandomizeParts());
    }

    void OnEnable()
    {
        healthSystem.OnHealthChanged += OnHit;
        healthSystem.OnDead += OnDead;
    }

    void OnDisable()
    {
        healthSystem.OnHealthChanged -= OnHit;
        healthSystem.OnDead -= OnDead;
    }

    public void Damage(int damage)
    {
        healthSystem.Damage(damage);
    }

    private void OnHit()
    {
        if (isDead) return;
        partsManager.PlayAnimationOnce("Hit");
    }

    private void OnDead()
    {
        isDead = true;
        partsManager.PlayAnimationOnce("Die");
        characterCollider.enabled = false;
    }

    public void Reset()
    {
        if (isDead) return;
        partsManager.PlayAnimation("Idle");
    }

    [Button]
    public IEnumerator RandomizeParts()
    {
        yield return null;
        RandomizeFacingDirection();
        partsManager.RandomParts();
        ColorPresetManager.Instance.SetRandomAllColor(partsManager);
    }

    public void RandomizeFacingDirection()
    {
        if (Random.value > 0.5f)
        {
            partsManager.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            partsManager.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

}
