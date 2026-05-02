using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float shootForce = 50f;

    public void Attack()
    {
        if (projectilePrefab == null || spawnPoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, transform.rotation);
        
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(transform.forward * shootForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleHit(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleHit(other.gameObject);
    }

    private void HandleHit(GameObject otherObject)
    {
        if (otherObject.CompareTag("Enemy"))
        {
            Destroy(otherObject);
        }

        if (otherObject != gameObject)
        {
            Destroy(gameObject);
        }
    }
}