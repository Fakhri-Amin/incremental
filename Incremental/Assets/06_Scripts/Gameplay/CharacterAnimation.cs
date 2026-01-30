using LayerLab.ArtMaker;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private PartsManager partsManager;

    // All available animation names
    [SerializeField]
    private string[] animationNames =
    {
        "Idle",
        "Idle2",
        "Attack1",
        "Attack2",
        "Attack3"
    };

    public void PlayIdleAnimation()
    {
        partsManager.PlayAnimation("Idle");
    }

    public void PlayHitAnimationOnce()
    {
        partsManager.PlayAnimationOnce("Hit");
    }

    public void PlayDieAnimationOnce()
    {
        partsManager.PlayAnimationOnce("Die");
    }

    public void PlayRandomAnimation()
    {
        if (partsManager == null)
        {
            Debug.LogWarning("PartsManager reference not assigned!");
            return;
        }

        // Pick a random name from the list
        int index = Random.Range(0, animationNames.Length);
        string chosenAnim = animationNames[index];

        partsManager.PlayAnimation(chosenAnim);
    }
}
