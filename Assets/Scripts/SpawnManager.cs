using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject pipeObject;
    [SerializeField] float waitTime = 5f;
    [SerializeField] float spawnDelay = 1.0f;
    const float MAX_Y = 2f;
    const float MIN_Y = -1.3f;
    public bool canSpawn = false;

    Vector3 spawnPosition = new(3.34f, 0, 0);

    void Start()
    {
        InvokeRepeating(nameof(SpawnPipes), waitTime, spawnDelay);
    }

    public void Enable(bool value)
    {
        canSpawn = value;
        gameObject.SetActive(value);
    }

    void SpawnPipes()
    {
        if (!canSpawn) return;

        float randomHeight = Random.Range(MIN_Y, MAX_Y);
        spawnPosition.y = randomHeight;
        Instantiate(pipeObject, spawnPosition, Quaternion.identity);
    }
}
