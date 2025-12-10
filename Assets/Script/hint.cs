using UnityEngine;
using UnityEngine.UI;

public class hint
 : MonoBehaviour
{
    private Image[] images;
    public float fadeDuration = 15f;
    public float fadeInSpeed = 2f;

    private float targetAlpha = 0f;   // will fade OUT
    private bool forceFadeIn = false; // when pressing ? or /

    void Start()
    {
        // Get all images inside this empty
        images = GetComponentsInChildren<Image>(true);

        // Start fully visible
        SetAlphaAll(1f);
    }

    void Update()
    {
        // Detect ? or /
        if (Input.GetKeyDown(KeyCode.Slash) || Input.GetKeyDown(KeyCode.Question))
        {
            forceFadeIn = true;
            targetAlpha = 1f;     // fade back in
        }

        if (forceFadeIn)
        {
            // Fade in faster
            bool done = FadeToAlpha(targetAlpha, fadeInSpeed);

            if (done)
            {
                forceFadeIn = false;
                targetAlpha = 0f;  // resume slow fade-out
            }
        }
        else
        {
            // Slow fade-out over time
            FadeToAlpha(targetAlpha, 1f / fadeDuration);
        }
    }

    bool FadeToAlpha(float target, float speed)
    {
        bool allReached = true;

        foreach (Image img in images)
        {
            Color c = img.color;
            c.a = Mathf.MoveTowards(c.a, target, Time.deltaTime * speed);
            img.color = c;

            if (!Mathf.Approximately(c.a, target))
                allReached = false;
        }

        return allReached;
    }

    void SetAlphaAll(float a)
    {
        foreach (Image img in images)
        {
            Color c = img.color;
            c.a = a;
            img.color = c;
        }
    }
}
