using UnityEngine;
using DG.Tweening;

public class WeaponSwing : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer weaponVisual;
    [SerializeField] private CircleCollider2D characterCollider;
    [SerializeField] private LayerMask targetMask;

    [Header("Attack Settings")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRadiusMultiplier = 1.2f;

    [Header("Swing Settings")]
    [SerializeField] private float swingAngle = 45f;
    [SerializeField] private float swingDuration = 0.4f;
    [SerializeField] private float swingPause = 0.15f;
    [SerializeField] private Ease swingEase = Ease.InOutSine;
    [SerializeField] private float overshootAngle = 8f;

    private Sequence swingSequence;
    private float baseAngleZ;

    private void Awake()
    {
        if (characterCollider == null)
            characterCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        baseAngleZ = weaponVisual.transform.localEulerAngles.z;
        StartSwing();
    }

    private void StartSwing()
    {
        swingSequence?.Kill();

        weaponVisual.transform.localRotation = Quaternion.Euler(0, 0, baseAngleZ - swingAngle);
        swingSequence = DOTween.Sequence();

        // ----- Swing Left → Right -----
        var swingRight = weaponVisual.transform.DOLocalRotate(
            new Vector3(0, 0, baseAngleZ + swingAngle + overshootAngle),
            swingDuration
        ).SetEase(Ease.OutSine);

        // Trigger damage halfway through the swing (mid-swing)
        swingSequence.Append(swingRight.OnUpdate(() =>
        {
            if (swingRight.ElapsedPercentage() >= 0.5f && !_hasDealtDamageThisSwing)
            {
                TryDealDamage();
                _hasDealtDamageThisSwing = true;
            }
        }));

        // Reset the hit flag after each swing
        swingSequence.AppendCallback(() => _hasDealtDamageThisSwing = false);

        // Recoil slightly
        swingSequence.Append(weaponVisual.transform.DOLocalRotate(
            new Vector3(0, 0, baseAngleZ + swingAngle),
            swingDuration * 0.25f
        ).SetEase(Ease.InSine));

        swingSequence.AppendInterval(swingPause);

        // ----- Swing Right → Left -----
        var swingLeft = weaponVisual.transform.DOLocalRotate(
            new Vector3(0, 0, baseAngleZ - swingAngle - overshootAngle),
            swingDuration
        ).SetEase(Ease.OutSine);

        swingSequence.Append(swingLeft.OnUpdate(() =>
        {
            if (swingLeft.ElapsedPercentage() >= 0.5f && !_hasDealtDamageThisSwing)
            {
                TryDealDamage();
                _hasDealtDamageThisSwing = true;
            }
        }));

        // Reset the hit flag again
        swingSequence.AppendCallback(() => _hasDealtDamageThisSwing = false);

        // Recoil slightly again
        swingSequence.Append(weaponVisual.transform.DOLocalRotate(
            new Vector3(0, 0, baseAngleZ - swingAngle),
            swingDuration * 0.25f
        ).SetEase(Ease.InSine));

        swingSequence.AppendInterval(swingPause);

        // Loop forever
        swingSequence.SetLoops(-1, LoopType.Restart);
    }

    private bool _hasDealtDamageThisSwing = false;

    private void TryDealDamage()
    {
        float radius = characterCollider.radius * attackRadiusMultiplier;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IAttackable>(out var attackable))
            {
                attackable.Damage(damage);
            }
        }
    }

    private void OnDisable()
    {
        swingSequence?.Kill();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ICharacter>(out var character))
        {
            character.PlayIdle();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (characterCollider != null)
        {
            Gizmos.color = Color.red;
            float radius = characterCollider.radius * attackRadiusMultiplier;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
#endif
}