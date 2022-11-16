using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    Flap player;
    SpawnManager spawnManager;
    MoveLeft[] moveLeft;
    public bool canMove = true;
    GameObject tutorialMessage;
    GameObject gameOverMessage;
    bool isLoading = false;

    enum GameState {
        TUTORIAL,
        GAME,
        GAMEOVER
    };
    GameState state = GameState.TUTORIAL;

    void Awake()
    {
        player = FindObjectOfType<Flap>();
        spawnManager = FindObjectOfType<SpawnManager>();
        moveLeft = FindObjectsOfType<MoveLeft>();
        tutorialMessage = GameObject.Find("TutorialMessage");
        gameOverMessage = GameObject.Find("GameOverMessage");
    }

    void Start()
    {
        tutorialMessage.SetActive(true);
        gameOverMessage.SetActive(false);
        spawnManager.Enable(false);
        player.EnablePhysics(false);
        player.GetComponent<Animator>().enabled = false;
        canMove = false;
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        switch(state)
        {
            case GameState.TUTORIAL:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
                {
                    Destroy(tutorialMessage);
                    spawnManager.Enable(true);
                    player.EnablePhysics(true);
                    player.GetComponent<Animator>().enabled = true;
                    canMove = true;
                    state = GameState.GAME;
                }
                break;
            case GameState.GAME:
                break;
            case GameState.GAMEOVER:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)
                    && !isLoading)
                {
                    isLoading = true;
                    StartCoroutine(ReloadLevel());
                }
                break;
        }
    }

    public void GameOver(bool fallGround = false)
    {
        state = GameState.GAMEOVER;
        gameOverMessage.SetActive(true);
        spawnManager.Enable(false);
        if (!fallGround) player.EnablePhysics(false);
        player.EnableControls(false);
        canMove = false;
    }
}
