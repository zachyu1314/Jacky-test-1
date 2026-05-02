using UnityEngine;

public class BatLikeMotionController : MonoBehaviour
{
    [Header("Targets")]
    public Transform bigBat;
    public Transform smallBat;
    public Transform coin3;

    [Header("Big Bat")]
    public float bigBatHorizontalAmplitude = 1.8f;
    public float bigBatVerticalAmplitude = 0.5f;
    public float bigBatSpeed = 1.2f;
    public float bigBatRoll = 18f;

    [Header("Small Bat")]
    public float smallBatHorizontalAmplitude = 1.2f;
    public float smallBatVerticalAmplitude = 0.35f;
    public float smallBatSpeed = 1.8f;
    public float smallBatRoll = 24f;

    [Header("Coin 3")]
    public float coinHorizontalAmplitude = 0.8f;
    public float coinVerticalAmplitude = 0.25f;
    public float coinSpeed = 1.5f;
    public float coinRoll = 14f;

    private Vector3 bigBatStart;
    private Vector3 smallBatStart;
    private Vector3 coin3Start;

    private Quaternion bigBatBaseRotation;
    private Quaternion smallBatBaseRotation;
    private Quaternion coin3BaseRotation;

    void Start()
    {
        CacheTargetState(bigBat, ref bigBatStart, ref bigBatBaseRotation);
        CacheTargetState(smallBat, ref smallBatStart, ref smallBatBaseRotation);
        CacheTargetState(coin3, ref coin3Start, ref coin3BaseRotation);
    }

    void Update()
    {
        AnimateBatLike(bigBat, bigBatStart, bigBatBaseRotation, bigBatHorizontalAmplitude, bigBatVerticalAmplitude, bigBatSpeed, bigBatRoll, 0f);
        AnimateBatLike(smallBat, smallBatStart, smallBatBaseRotation, smallBatHorizontalAmplitude, smallBatVerticalAmplitude, smallBatSpeed, smallBatRoll, 1.2f);
        AnimateBatLike(coin3, coin3Start, coin3BaseRotation, coinHorizontalAmplitude, coinVerticalAmplitude, coinSpeed, coinRoll, 2.1f);
    }

    private void CacheTargetState(Transform target, ref Vector3 startPosition, ref Quaternion startRotation)
    {
        if (target == null) return;

        startPosition = target.position;
        startRotation = target.rotation;
    }

    private void AnimateBatLike(
        Transform target,
        Vector3 startPosition,
        Quaternion startRotation,
        float horizontalAmplitude,
        float verticalAmplitude,
        float speed,
        float rollAmount,
        float phaseOffset)
    {
        if (target == null) return;

        float time = Time.time * speed + phaseOffset;
        float horizontal = Mathf.Sin(time) * horizontalAmplitude;
        float forward = Mathf.Cos(time * 0.7f) * (horizontalAmplitude * 0.35f);
        float vertical = Mathf.Sin(time * 2.2f) * verticalAmplitude;

        target.position = startPosition + new Vector3(horizontal, vertical, forward);

        float yaw = Mathf.Cos(time) * 10f;
        float roll = Mathf.Sin(time * 2.2f) * rollAmount;
        target.rotation = startRotation * Quaternion.Euler(0f, yaw, roll);
    }
}
