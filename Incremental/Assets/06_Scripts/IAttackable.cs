using UnityEngine;

public interface IAttackable
{
    public GameObject GameObject { get; }
    void Damage(int damage);
}
