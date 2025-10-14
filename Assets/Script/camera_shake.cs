using UnityEngine;

public class camera_shake : MonoBehaviour
{
    private Vector3 shakeOffset = Vector3.zero;
    private float shakeMagnitude = 1f;
    private float shakeTimeRemaining = 0f;
    private Vector3 basePosition;

    void LateUpdate()
    {
        basePosition = transform.position - shakeOffset;

        if (shakeTimeRemaining > 0f)
        {
            shakeOffset = new Vector3(
                Random.Range(-0.1f, 0.1f) * shakeMagnitude,
                Random.Range(-0.1f, 0.1f) * shakeMagnitude,
                0f
            );
            shakeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            shakeOffset = Vector3.zero;
        }

        transform.position = basePosition + shakeOffset;
    }

    public void TriggerShake(float duration, float magnitude)
    {
        shakeMagnitude = magnitude;
        shakeTimeRemaining = duration;
    }
}
