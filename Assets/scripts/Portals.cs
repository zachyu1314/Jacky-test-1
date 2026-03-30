using UnityEngine;

public class TeleportPortal : MonoBehaviour
{
    [Header("Portal Settings")]
    public Transform destination;      // The other portal's Transform
    public float exitOffset = 2.0f;    // Distance to spawn in front of the exit
    public float cooldownTime = 0.5f;  // Seconds before you can teleport again

    [Header("Physics Settings")]
    public bool maintainMomentum = true;

    // Static ensures all portals share the same cooldown timer
    private static float lastTeleportTime;

    private void OnTriggerEnter(Collider other)
    {
        // 1. Check Cooldown
        if (Time.time < lastTeleportTime + cooldownTime) return;

        // 2. Check if it's the Player
        if (other.CompareTag("Player"))
        {
            Teleport(other.transform);
        }
    }

    private void Teleport(Transform obj)
    {
        lastTeleportTime = Time.time;

        // 3. Handle CharacterController (Crucial for Unity's standard FPS/TPS controllers)
        // We must disable it briefly to manually set the position
        CharacterController cc = obj.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        // 4. Set Position with Offset
        // This puts the player in front of the destination so they don't get stuck inside it
        obj.position = destination.position + (destination.forward * exitOffset);

        // 5. Set Rotation
        // Matches the player's face-direction to the portal's exit direction
        obj.rotation = destination.rotation;

        // 6. Handle Rigidbody Momentum (If using physics)
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null && maintainMomentum)
        {
            float speed = rb.linearVelocity.magnitude;
            rb.linearVelocity = destination.forward * speed;
        }

        // 7. Re-enable CharacterController
        if (cc != null) cc.enabled = true;

        Debug.Log("Teleported to: " + destination.name);
    }
}