using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSpriteAlpha : MonoBehaviour
{
    [Range(0f, 1f)][SerializeField] private float minAlpha = 0.3f;
    [Range(0f, 1f)][SerializeField] private float maxAlpha = 1f;
    [SerializeField] private bool randomizeOnEnable = true;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (randomizeOnEnable)
            ApplyRandomAlpha();
    }

    [ContextMenu("Apply Random Alpha")]
    public void ApplyRandomAlpha()
    {
        if (spriteRenderer == null) return;

        float randomAlpha = Random.Range(minAlpha, maxAlpha);
        Color c = spriteRenderer.color;
        c.a = randomAlpha;
        spriteRenderer.color = c;
    }
}
