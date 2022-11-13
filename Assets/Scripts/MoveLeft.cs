using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    float HALF_WIDTH;
    Vector3 startPosition;
    LevelManager levelManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        startPosition = transform.position;
#nullable enable
        BoxCollider2D? collider;
        TryGetComponent<BoxCollider2D>(out collider);
        if (collider != null)
        {
            HALF_WIDTH = collider.size.x / 2f;
        }
#nullable disable
    }

    void Update()
    {
        if (!levelManager.canMove) return;

        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (gameObject.CompareTag("Ground")
            && transform.position.x < startPosition.x-HALF_WIDTH)
        {
            transform.position = startPosition;
        }
    }
}