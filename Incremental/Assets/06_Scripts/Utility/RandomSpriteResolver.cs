using UnityEngine;
using UnityEngine.U2D.Animation;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteResolver))]
public class RandomSpriteResolverOdin : MonoBehaviour
{
    [SerializeField, LabelText("Sprite Category")]
    private string category = "Default";

    [SerializeField, LabelText("Use Full Category Range")]
    private bool useAllSprites = true;

    [HideIf(nameof(useAllSprites), false)]
    [BoxGroup("Range Settings"), LabelText("Min Index")]
    [MinValue(0)]
    public int minIndex = 0;

    [HideIf(nameof(useAllSprites), false)]
    [BoxGroup("Range Settings"), LabelText("Max Index")]
    [MinValue(1)]
    public int maxIndex = 3;

    private SpriteResolver spriteResolver;

    private void Awake()
    {
        spriteResolver = GetComponent<SpriteResolver>();
    }

    private void OnEnable()
    {
        RandomizeSprite();
    }

    [Button(ButtonSizes.Medium), GUIColor(0.3f, 1f, 0.4f)]
    public void RandomizeSprite()
    {
        if (spriteResolver == null) return;

        // Get all labels for this category
        var labels = spriteResolver.spriteLibrary.spriteLibraryAsset.GetCategoryLabelNames(category);
        var labelList = new List<string>(labels);
        if (labelList.Count == 0)
        {
            Debug.LogWarning($"No labels found in category '{category}'.");
            return;
        }

        string chosenLabel;

        if (useAllSprites)
        {
            // Random from all
            chosenLabel = labelList[Random.Range(0, labelList.Count)];
        }
        else
        {
            // Random within specified index range
            int start = Mathf.Clamp(minIndex, 0, labelList.Count - 1);
            int end = Mathf.Clamp(maxIndex, 1, labelList.Count);
            int randomIndex = Random.Range(start, end);
            chosenLabel = labelList[randomIndex];
        }

        spriteResolver.SetCategoryAndLabel(category, chosenLabel);
    }
}
