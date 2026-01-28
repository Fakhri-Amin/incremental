using UnityEngine;
using System;

[DisallowMultipleComponent]
public class UniqueID : MonoBehaviour
{
    [SerializeField] private string id;
    public string ID => id;

    private void Awake()
    {
        GenerateIDIfNeeded();
    }

    // ✅ Use this when spawning at runtime (if no ID yet)
    public void GenerateIDIfNeeded()
    {
        if (string.IsNullOrEmpty(id))
            // id = Guid.NewGuid().ToString();
            id = $"{gameObject.name}_{UnityEngine.Random.Range(100000, 999999)}";
    }

    // ✅ Use this when loading from save data (to restore the same ID)
    public void SetID(string newID)
    {
        id = newID;
    }
}
