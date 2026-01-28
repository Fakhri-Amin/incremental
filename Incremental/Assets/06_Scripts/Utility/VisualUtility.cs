using UnityEngine;

public static class VisualUtility
{
    public static void FlipVisualTowards(Transform visual, Vector2 targetPosition)
    {
        Vector2 currentPosition = visual.position;
        Vector2 direction = (targetPosition - currentPosition).normalized;

        if (Mathf.Abs(direction.x) > 0.01f)
        {
            Vector3 scale = visual.localScale;
            scale.x = -Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
            visual.localScale = scale;
        }
    }
}
