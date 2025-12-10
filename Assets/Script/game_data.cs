using UnityEngine;

public static class game_data
{
    public static float girlZ = 0f;
    public static float giantZ = 0f;

    public static void SavePositions(float girl, float giant)
    {
        girlZ = girl;
        giantZ = giant;

        PlayerPrefs.SetFloat("GirlZ", girl);
        PlayerPrefs.SetFloat("GiantZ", giant);
        PlayerPrefs.Save();
    }

    public static void LoadPositions()
    {
        if (PlayerPrefs.HasKey("GirlZ"))
            girlZ = PlayerPrefs.GetFloat("GirlZ");

        if (PlayerPrefs.HasKey("GiantZ"))
            giantZ = PlayerPrefs.GetFloat("GiantZ");
    }

#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
    static void ClearSaveOnPlay()
    {
        // This runs ONLY when you press Play inside the Unity editor
        PlayerPrefs.DeleteKey("GirlZ");
        PlayerPrefs.DeleteKey("GiantZ");
        Debug.Log("Save cleared because Play was pressed.");
    }
#endif
}
