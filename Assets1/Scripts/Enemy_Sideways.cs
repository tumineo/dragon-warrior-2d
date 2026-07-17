using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance = 3f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float speed = 2f;

    private bool movingleft;
    private float leftEdge;
    private float rightEdge;

    private void Start()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;

        movingleft = true; // ✅ FIX: set starting direction
    }

    private void Update()
    {
        if (movingleft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(
                    transform.position.x - speed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z);
            }
            else
            {
                movingleft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(
                    transform.position.x + speed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z);
            }
            else
            {
                movingleft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();

            if (playerHealth != null) 
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}