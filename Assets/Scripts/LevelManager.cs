using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Flap player;
    SpawnManager spawnManager;
    MoveLeft[] moveLeft;
    bool canSpawn = true;
    public bool canMove = true;


    void Awake()
    {
        player = FindObjectOfType<Flap>();
        spawnManager = FindObjectOfType<SpawnManager>();
        moveLeft = FindObjectsOfType<MoveLeft>();
    }

    IEnumerator WaitTutorial()
    {
        canSpawn = false;
        yield return new WaitForSeconds(3f);
        canSpawn = true;
    }

    void Start()
    {
        StartCoroutine(WaitTutorial());
    }

    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(spawnManager.StartSpawning());
            canSpawn = false;
        }
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.EnablePhysics();
        player.EnableControls();
        spawnManager.EnableSpawning();
        canMove = true;
    }

    public void GameOver(bool fallGround = false)
    {
        if (!fallGround) player.EnablePhysics(false);
        spawnManager.EnableSpawning(false);
        player.EnableControls(false);
        canMove = false;
        StartCoroutine(ReloadLevel());
    }
}
