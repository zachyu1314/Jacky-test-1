using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifeSpan = 5.0f;
    private Vector3 moveDirection = Vector3.zero;

    // The Shooter script calls this immediately after spawning
    public void Setup(Vector3 targetPos)
    {
        // Calculate the direction from the fireball to the target
        moveDirection = (targetPos - transform.position).normalized;
        
        // Make the fireball face the direction it is traveling
        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }

        // Auto-destroy so your hierarchy doesn't fill up with clones
        Destroy(gameObject, lifeSpan);
    }

    void Update()
    {
        // Move in a straight line every frame
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Don't hit the shooter itself
        if (other.gameObject.name.Contains("Monster") || other.gameObject.name.Contains("Boss"))
        {
            return;
        }

        // Destroy on impact with anything else
        Destroy(gameObject);
    }
}