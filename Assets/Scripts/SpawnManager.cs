using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject pipeObject;
    [SerializeField] float spawnDelay = 1.0f;
    bool canSpawn = true;
    const float MAX_Y = 3.4f;
    const float MIN_Y = -1.3f;

    Vector3 spawnPosition = new(3.34f, 0, 0);

    public bool EnableSpawning(bool value = true) => canSpawn = value;

    public IEnumerator StartSpawning()
    {
        while (canSpawn)
        {
            float randomHeight = Random.Range(MIN_Y, MAX_Y);
            spawnPosition.y = randomHeight;
            Instantiate(pipeObject, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
