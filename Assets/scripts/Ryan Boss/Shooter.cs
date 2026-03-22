using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Setup")]
    public GameObject fireballPrefab; // Drag your Fireball Prefab here
    public Transform target;          // Drag your Player here
    public Transform spawnPoint;      // Drag the yellow sphere (spawn location) here

    [Header("Settings")]
    public float detectionRange = 10.0f;
    public float fireRate = 2.0f;
    
    private float nextFireTime;

    void Update()
    {
        // 1. Safety check: Don't do anything if target is missing
        if (target == null) return;

        // 2. Check the distance to the target
        float distance = Vector3.Distance(transform.position, target.position);

        // 3. Only shoot if inside range and cooldown is over
        if (distance <= detectionRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (fireballPrefab != null && spawnPoint != null)
        {
            // Create the fireball at the spawn point's position and rotation
            GameObject ball = Instantiate(fireballPrefab, spawnPoint.position, spawnPoint.rotation);
            
            // Send the target's current position to the Fireball script
            Fireball ballScript = ball.GetComponent<Fireball>();
            if (ballScript != null)
            {
                ballScript.Setup(target.position);
            }
        }
    }

    // This draws the red circle in your Scene view so you can see the range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}