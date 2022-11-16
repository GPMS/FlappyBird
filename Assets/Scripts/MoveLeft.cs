using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    float HALF_WIDTH;
    Vector3 startPosition;
    StateManager levelManager;

    void Awake()
    {
        levelManager = FindObjectOfType<StateManager>();
    }

    void Start()
    {
        startPosition = transform.position;
#nullable enable
        TryGetComponent<BoxCollider2D>(out BoxCollider2D? collider);
        if (collider != null)
        {
            HALF_WIDTH = collider.size.x / 2f;
        }
#nullable disable
    }

    void Update()
    {
        if (!levelManager.canMove) return;

        if (gameObject.CompareTag("Ground")
            && transform.position.x <= startPosition.x-HALF_WIDTH)
        {
            transform.position = startPosition - new Vector3(0.1f, 0, 0);
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}