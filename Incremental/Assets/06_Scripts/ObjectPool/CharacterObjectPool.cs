using System;
using System.Collections.Generic;
using Eggtato.Utility;
using UnityEngine;
using UnityEngine.Pool;

public class CharacterObjectPool : Singleton<CharacterObjectPool>
{
    [System.Serializable]
    public class ObjectlReference
    {
        public Character Unit;
        [HideInInspector] public Transform ParentTransform;
        [HideInInspector] public ObjectPool<Character> ObjectPool;
    }

    [SerializeField] private ObjectlReference objectObjectPool;

    public new void Awake()
    {
        base.Awake();

        if (objectObjectPool.ParentTransform == null)
        {
            GameObject parentObj = new(objectObjectPool.Unit.name);
            parentObj.transform.SetParent(transform);
            objectObjectPool.ParentTransform = parentObj.transform;
        }

        objectObjectPool.ObjectPool = new ObjectPool<Character>(() =>
        {
            return Instantiate(objectObjectPool.Unit, objectObjectPool.ParentTransform);
        },
        obj =>
        {
            obj.gameObject.SetActive(true);
        },
        obj =>
        {
            obj.gameObject.SetActive(false);
        },
        obj =>
        {
            Destroy(obj);
        },
        true, 10, 30);
    }


    public Character GetPooledObject()
    {
        return objectObjectPool.ObjectPool.Get();
    }

    public void ReturnToPool(Character character)
    {
        objectObjectPool.ObjectPool.Release(character);
    }
}
