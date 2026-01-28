using UnityEngine;
using DG.Tweening;

public class WeaponSwing : MonoBehaviour
{
    [Header("Swing Settings")]
    [SerializeField] private float swingAngle = 45f;        // Max rotation angle to each side
    [SerializeField] private float swingDuration = 0.5f;    // Time to swing to one side
    [SerializeField] private float swingCooldown = 0.5f;    // Pause at each end
    [SerializeField] private Ease swingEase = Ease.InOutSine;

    private Sequence swingSequence;
    private float baseAngleZ;

    void Start()
    {
        baseAngleZ = transform.localEulerAngles.z;
        StartSwing();
    }

    void StartSwing()
    {
        swingSequence?.Kill();

        // Start at left side
        transform.localRotation = Quaternion.Euler(0, 0, baseAngleZ - swingAngle);

        swingSequence = DOTween.Sequence();

        // Swing to right
        swingSequence.Append(transform.DOLocalRotate(
            new Vector3(0, 0, baseAngleZ + swingAngle),
            swingDuration
        ).SetEase(swingEase));

        // Pause at right
        swingSequence.AppendInterval(swingCooldown);

        // Swing back to left
        swingSequence.Append(transform.DOLocalRotate(
            new Vector3(0, 0, baseAngleZ - swingAngle),
            swingDuration
        ).SetEase(swingEase));

        // Pause at left
        swingSequence.AppendInterval(swingCooldown);

        // Loop seamlessly
        swingSequence.SetLoops(-1, LoopType.Restart);
    }

    void OnDisable()
    {
        swingSequence?.Kill();
    }
}