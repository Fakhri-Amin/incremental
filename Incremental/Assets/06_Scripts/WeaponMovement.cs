using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdateCursorPosition();
    }

    private void UpdateCursorPosition()
    {
        Vector2 mousePos = GameInput.Instance.GetMousePosition();
        Vector2 followPosition = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = followPosition;
    }
}
