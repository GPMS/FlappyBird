using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayerCollision : MonoBehaviour
{
    AudioSource audioSource;
    StateManager levelManager;
    [SerializeField] AudioClip hitSound;

    void Awake()
    {
        levelManager = FindObjectOfType<StateManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        audioSource.PlayOneShot(hitSound);
        levelManager.GameOver();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        audioSource.PlayOneShot(hitSound);
        levelManager.GameOver(fallGround: true);
    }
}
