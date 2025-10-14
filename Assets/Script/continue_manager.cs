using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float distanceRan = 0f;
    public Vector3 girlPosition;
    public Vector3 giantPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
