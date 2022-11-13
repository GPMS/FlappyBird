using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScoreKeeper : MonoBehaviour
{
    public int score {get; private set;} = 0;

    [SerializeField] AudioClip scoreSound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseScore()
    {
        audioSource.PlayOneShot(scoreSound);
        score++;
    }
}
