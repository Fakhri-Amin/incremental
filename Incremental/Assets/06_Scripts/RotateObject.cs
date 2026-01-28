using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float RotationSpeed = -700; // Rotation speed in degrees per second

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, RotationSpeed) * Time.deltaTime);
    }
}