using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform spawnPoint;        
    public float shootForce = 50f;

    private int lastAttackFrame = -1;

    void Update()
    {
        bool attackPressed =
            (Mouse.current?.rightButton.wasPressedThisFrame ?? false) ||
            (Keyboard.current?.commaKey.wasPressedThisFrame ?? false);

        if (attackPressed)
        {
            TryAttack();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TryAttack();
        }
    }

    public void Attack()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        if (lastAttackFrame == Time.frameCount) return;
        lastAttackFrame = Time.frameCount;

        FireProjectile();
    }

    private void FireProjectile()
    {
        if (projectilePrefab == null || spawnPoint == null) return;

        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        // Create the object
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb == null) return;

        // Ignore the player's own colliders so the projectile does not immediately
        // collide with the character or camera rig as soon as it spawns.
        Collider[] projectileColliders = projectile.GetComponentsInChildren<Collider>();
        Collider[] playerColliders = transform.root.GetComponentsInChildren<Collider>();
        foreach (Collider projectileCollider in projectileColliders)
        {
            foreach (Collider playerCollider in playerColliders)
            {
                if (projectileCollider != null && playerCollider != null)
                {
                    Physics.IgnoreCollision(projectileCollider, playerCollider, true);
                }
            }
        }

        // Fire straight along the camera's forward direction.
        Vector3 direction = mainCamera.transform.forward.normalized;
        projectile.transform.rotation = Quaternion.LookRotation(direction);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(direction * shootForce, ForceMode.Impulse);
    }
}
