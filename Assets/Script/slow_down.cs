using UnityEngine;
using System.Collections;

public class slow_down : MonoBehaviour
{
    public float slowAmount = 0.4f;
    public float slowDuration = 1.5f;

    void Start()
    {
        StartCoroutine(DoSlowMotion());
    }

    IEnumerator DoSlowMotion()
    {
        // Apply slow motion
        Time.timeScale = slowAmount;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // Use realtime so it works even in slow motion
        yield return new WaitForSecondsRealtime(slowDuration);

        // Return to normal
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
