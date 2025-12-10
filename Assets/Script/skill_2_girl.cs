using UnityEngine;
using UnityEngine.UI;

public class Skill2TimingManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject timingUIPanel;
    public RectTransform shrinkingCircle;

    [Header("Timing Settings")]
    public float totalShrinkTime = 1.2f;

    // Hit window (width of circle)
    public float minHitSize = 92f;
    public float maxHitSize = 105f;

    private float timer = 0f;
    private bool timingActive = false;
    private KeyCode requiredKey;

    [Header("References")]
    public giant_movement giant;

    void Start()
    {
        timingUIPanel.SetActive(false);

        if (giant == null)
            giant = FindObjectOfType<giant_movement>();
    }

    void Update()
    {
        if (!timingActive) return;

        // 1. Shrink circle
        timer += Time.deltaTime;
        float t = timer / totalShrinkTime;
        float newSize = Mathf.Lerp(300f, 60f, t);

        shrinkingCircle.sizeDelta = new Vector2(newSize, newSize);

        if (t >= 1f)
        {
            Miss();
            return;
        }

        // 2. Input
        if (Input.GetKeyDown(requiredKey))
        {
            float sizeNow = shrinkingCircle.sizeDelta.x;

            if (sizeNow >= minHitSize && sizeNow <= maxHitSize)
                Success();
            else
                Miss();

            return;
        }
    }

    // ------------------------------------------------------------
    public void StartTimingEvent()
    {
        timingActive = true;
        timer = 0f;

        timingUIPanel.SetActive(true);

        shrinkingCircle.sizeDelta = new Vector2(300f, 300f);

        PickRandomArrowKey();
    }

    void PickRandomArrowKey()
    {
        KeyCode[] arrows =
        {
            KeyCode.UpArrow,
            KeyCode.DownArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow
        };

        requiredKey = arrows[Random.Range(0, arrows.Length)];
        Debug.Log("Required key: " + requiredKey);
    }

    // ------------------------------------------------------------
    void Success()
    {
        timingActive = false;
        timingUIPanel.SetActive(false);
        Debug.Log("SUCCESS");
    }

    void Miss()
    {
        timingActive = false;
        timingUIPanel.SetActive(false);

        if (giant != null)
            giant.speed = Mathf.Max(0f, giant.speed - 3f);

        Debug.Log("MISS");
    }
}
