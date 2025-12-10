using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class race_progress : MonoBehaviour
{
    [Header("World References")]
    public Transform girl;
    public Transform giant;

    [Header("UI References")]
    public RectTransform bar;
    public RectTransform girlBall;
    public RectTransform giantBall;

    [Header("Z Axis Settings")]
    public float minZ;
    public float maxZ;

    private float leftEdge;
    private float rightEdge;

    void Start()
    {
        leftEdge  = -bar.rect.width * 0.5f;
        rightEdge =  bar.rect.width * 0.5f;
    }

    void Update()
    {
        MoveBall(girl, girlBall);
        MoveBall(giant, giantBall);

        CheckVictory();
    }

    void MoveBall(Transform target, RectTransform ball)
    {
        float t = Mathf.InverseLerp(minZ, maxZ, target.position.z);
        float x = Mathf.Lerp(leftEdge, rightEdge, t);

        Vector2 pos = ball.anchoredPosition;
        pos.x = x;
        ball.anchoredPosition = pos;
    }

    void CheckVictory()
    {
        // If girl reaches the end of the bar (max Z)
        if (girl.position.z >= maxZ)
        {
            SceneManager.LoadScene("Girl_victory_scene");
        }
    }
}
