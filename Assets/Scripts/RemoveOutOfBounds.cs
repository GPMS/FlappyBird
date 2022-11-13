using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOutOfBounds : MonoBehaviour
{
    void Update()
    {
        if (transform.position.x < -3.32f)
        {
            Destroy(gameObject);
        }
    }
}
