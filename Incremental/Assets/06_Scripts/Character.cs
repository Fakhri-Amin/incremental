using System.Collections;
using LayerLab.ArtMaker;
using Sirenix.OdinInspector;
using UnityEngine;

public class Character : MonoBehaviour, IAttackable, ICharacter
{
    [SerializeField] private PartsManager partsManager;
    [SerializeField] private Collectable collectablePrefab;

    private CharacterAnimation characterAnimation;
    private HealthSystem healthSystem;
    private Collider2D characterCollider;
    private bool isDead;

    public GameObject GameObject => gameObject;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        characterCollider = GetComponent<Collider2D>();
        characterAnimation = GetComponent<CharacterAnimation>();
    }

    void Start()
    {
        partsManager.Init();
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

    public void Reset()
    {
        isDead = false;
        PlayIdle();
        StopAllCoroutines();
        StartCoroutine(RandomizeParts());
        characterCollider.enabled = true;
        healthSystem.ResetHealth();
    }

    public void Damage(int damage)
    {
        healthSystem.Damage(damage);
    }

    private void OnHit()
    {
        if (isDead) return;
        if (healthSystem.IsHealthFull()) return;
        characterAnimation.PlayHitAnimationOnce();
    }

    private void OnDead()
    {
        StartCoroutine(PlayDeadRoutine());
    }

    private IEnumerator PlayDeadRoutine()
    {
        isDead = true;
        characterAnimation.PlayDieAnimationOnce();
        characterCollider.enabled = false;
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < 3; i++)
        {
            Vector2 offset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            var collectable = Instantiate(collectablePrefab, (Vector2)transform.position + offset, randomRotation);
            collectable.FlyToCollector();
        }
    }

    public void PlayIdle()
    {
        if (isDead) return;
        characterAnimation.PlayRandomAnimation();
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
