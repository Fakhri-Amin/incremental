using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class UISelectionManager
{
    private static GameObject lastSelectedObject;

    /// <summary>
    /// Sets the selected button and stores it as the last selected.
    /// </summary>
    public static void SetSelected(Button button)
    {
        if (button == null)
            return;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button.gameObject);

        lastSelectedObject = button.gameObject;
    }

    /// <summary>
    /// Sets selection on the next frame to handle timing issues with UI transitions.
    /// </summary>
    public static IEnumerator SetSelectedNextFrame(Button button)
    {
        yield return null;
        SetSelected(button);
    }

    /// <summary>
    /// Tracks the current selected object each frame.
    /// Call this from a MonoBehaviour's Update() (e.g., UIManager).
    /// </summary>
    public static void TrackCurrentSelection()
    {
        if (EventSystem.current == null)
            return;

        var current = EventSystem.current.currentSelectedGameObject;

        // Skip if nothing selected
        if (current == null || !current.activeInHierarchy)
        {
            return;
        }

        // If a new UI object has been selected
        if (current != lastSelectedObject)
        {
            // Optional: custom behavior for deselection
            OnDeselected(lastSelectedObject);

            // Update last
            lastSelectedObject = current;
        }
    }

    private static void OnDeselected(GameObject previous)
    {
        if (previous == null) return;

        // Example: remove highlight manually (rarely needed, Unity handles this)
        var selectable = previous.GetComponent<Selectable>();
        if (selectable != null)
            selectable.OnDeselect(null);

        // Example: custom logic
        // AudioManager.Instance.PlayNavigationSound();
        // Debug.Log($"Deselected: {previous.name}");
    }


    /// <summary>
    /// Restores the last valid selection or picks the first active button found.
    /// </summary>
    public static void TryRestoreLastSelection()
    {
        if (EventSystem.current == null)
            return;

        var current = EventSystem.current.currentSelectedGameObject;

        // Only restore if nothing or inactive is selected
        if (current == null || !current.activeInHierarchy)
        {
            GameObject target = null;

            // Try to reuse the last valid selection
            if (lastSelectedObject != null && lastSelectedObject.activeInHierarchy)
            {
                target = lastSelectedObject;
            }
            else
            {
                // Find first active button as fallback
                var selectable = Object.FindFirstObjectByType<Button>();
                if (selectable != null)
                    target = selectable.gameObject;
            }

            if (target != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(target);
            }
        }
    }

}
