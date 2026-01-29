using System.Collections;
using LayerLab.ArtMaker;
using Sirenix.OdinInspector;
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

    void Start()
    {
        partsManager.Init();
        StartCoroutine(RandomizeParts());
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
