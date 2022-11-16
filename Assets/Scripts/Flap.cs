using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Flap : MonoBehaviour
{
    const float MAX_Y = 5.5f;
    const float ANGLE_RANGE = 90;
    float flapSpeed = 4f;
    float flipSpeed = -0.5f;
    Rigidbody2D rb;
    float repeatDelay = 0.3f;
    float secondsSinceLast = 0.3f;
    bool isHolding = false;
    bool controlsEnabled = true;

    AudioSource audioSource;
    [SerializeField] AudioClip flapSound;

    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FlyUp();
        Rotate();
    }

    public void EnablePhysics(bool value = true) => rb.simulated = value;

    public void EnableControls(bool value = true)
    {
        animator.enabled = value;
        controlsEnabled = value;
    }

    /// <summary>
    /// Push up while flying but limit y
    /// </summary>
    void FlyUp()
    {
        if (!controlsEnabled) return;

        secondsSinceLast += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (isHolding)
            {
                rb.velocity = Vector2.up * flapSpeed;
            }
            else if (secondsSinceLast > repeatDelay)
            {
                audioSource.PlayOneShot(flapSound);
                rb.velocity = Vector2.up * flapSpeed;
                secondsSinceLast = 0;
                isHolding = true;
            }
        }
        else if ((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
                  && isHolding)
        {
            isHolding = false;
        }

        if (transform.position.y > MAX_Y)
        {
            transform.position = new(transform.position.x, MAX_Y);
        }
    }

    /// <summary>
    /// Rotate bird up and down depending on velocity
    /// </summary>
    void Rotate()
    {
        if (!rb.simulated) return;

        if (rb.velocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 25f);
        }
        if (rb.velocity.y < flipSpeed)
        {
            transform.Rotate(0, 0, -180f * Time.deltaTime);

            // Keep angle inside range
            // Convert angle to up -> positive and down -> negative
            float angleZ = transform.eulerAngles.z > 180
                            ? transform.eulerAngles.z - 360
                            : transform.eulerAngles.z;
            if (angleZ > ANGLE_RANGE)
            {
                transform.rotation = Quaternion.Euler(0, 0, 25f);
            }
            else if (angleZ < -ANGLE_RANGE)
            {
                transform.rotation = Quaternion.Euler(0, 0, 270f);
            }
        }
    }
}
