using System.Collections;
using UnityEngine;

public class rock_fade : MonoBehaviour
{
    public float shrinkSpeed = 0.5f;
    public float lifetime = 2f;

    void Start()
    {
        StartCoroutine(ShrinkAndDestroy());
    }

    IEnumerator ShrinkAndDestroy()
    {
        float elapsed = 0f;

        while (elapsed < lifetime)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * shrinkSpeed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
