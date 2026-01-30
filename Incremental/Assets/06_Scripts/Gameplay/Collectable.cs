using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour
{
    [Header("Fly Settings")]
    [SerializeField] private float minMoveSpeed = 5f;
    [SerializeField] private float maxMoveSpeed = 8f;
    [SerializeField] private Ease moveEase = Ease.InOutQuad; // smoother flight
    [SerializeField] private float collectScaleDuration = 0.2f;

    private float coinMoveSpeed;

    private void Start()
    {

    }

    public void FlyToCollector()
    {
        CampFire campFire = GameManager.Instance?.CampFire;

        if (campFire == null)
        {
            Debug.LogWarning("❗ Collectable: CampFire reference is missing!");
            return;
        }

        coinMoveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        // Start flight animation
        FlyToTarget(campFire.transform);
    }

    private void FlyToTarget(Transform target)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = target.position;

        float distance = Vector3.Distance(startPos, targetPos);
        float duration = distance / coinMoveSpeed;

        // Kill any previous tweens just in case
        transform.DOKill();

        // Move toward target
        float delay = Random.Range(0f, 0.1f);
        transform.DOMove(targetPos, duration).SetDelay(delay)
            .SetEase(moveEase)
            .OnComplete(() =>
            {
                // Scale down and destroy on reach
                transform.DOScale(0f, collectScaleDuration)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        if (target.TryGetComponent(out CampFire campFire))
                            campFire.Collect();

                        Destroy(gameObject);
                    });
            });

        // Optional: Add spin or wobble for flair
        transform.DORotate(new Vector3(0, 0, 360f), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);
    }
}
