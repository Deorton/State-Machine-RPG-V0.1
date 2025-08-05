using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject persistantObjectPrefab;
    static bool hasSpawned = false;

    private void Awake()
    {
        if (hasSpawned) return;

        SpawnPersistantObject();

        hasSpawned = true; 
    }

    private void SpawnPersistantObject()
    {
        Instantiate(persistantObjectPrefab);
        DontDestroyOnLoad(persistantObjectPrefab);
    }
}
