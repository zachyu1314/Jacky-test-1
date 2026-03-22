using UnityEngine;

public class CircularTargetMovement : MonoBehaviour
{
    [Header("Target to Watch")]
    public Transform target; // Drag the "target" object here in Inspector

    [Header("Circle Settings")]
    public float radius = 5.0f;
    public float circleSpeed = 2.0f;

    [Header("Vertical Sway")]
    public float yAmplitude = 1.0f;
    public float ySpeed = 3.0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // 1. Calculate the Circular + Sin position
        float x = Mathf.Cos(Time.time * circleSpeed) * radius;
        float z = Mathf.Sin(Time.time * circleSpeed) * radius;
        float y = Mathf.Sin(Time.time * ySpeed) * yAmplitude;

        // 2. Set the position relative to start
        transform.position = startPosition + new Vector3(x, y, z);

        // 3. Face the target
        if (target != null)
        {
            transform.LookAt(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = Application.isPlaying ? startPosition : transform.position;
        Gizmos.DrawWireSphere(center, radius);

        // Draw a line to the target in the editor
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}