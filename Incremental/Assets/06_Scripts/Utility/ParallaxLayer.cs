using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float parallaxFactor = 0.5f; // 0 = static BG, 1 = moves with camera
    [SerializeField] private bool followVertical = false;

    private Transform cam;
    private Vector3 lastCamPosition;

    private void Start()
    {
        cam = Camera.main.transform;
        lastCamPosition = cam.position;
    }

    private void LateUpdate()
    {
        Vector3 delta = cam.position - lastCamPosition;

        Vector3 move = new Vector3(delta.x * parallaxFactor,
                                   followVertical ? delta.y * parallaxFactor : 0f,
                                   0f);

        transform.position += move;
        lastCamPosition = cam.position;
    }
}
