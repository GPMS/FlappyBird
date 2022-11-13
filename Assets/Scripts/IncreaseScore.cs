using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseScore : MonoBehaviour
{
    GameObject player;
    ScoreKeeper scoreKeeper;

    void Start()
    {
        player = GameObject.Find("Player");
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            scoreKeeper.IncreaseScore();
    }
}
