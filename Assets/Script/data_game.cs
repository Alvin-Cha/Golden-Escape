using UnityEngine;

public static class data_game
{
    public static void SavePositions(Transform girl, Transform giant)
    {
        PlayerPrefs.SetFloat("SavedGirlZ", girl.position.z);
        PlayerPrefs.SetFloat("SavedGiantZ", giant.position.z);
        PlayerPrefs.Save();
    }

    public static float GetGirlZ()
    {
        return PlayerPrefs.GetFloat("SavedGirlZ", 0);
    }

    public static float GetGiantZ()
    {
        return PlayerPrefs.GetFloat("SavedGiantZ", 0);
    }

    public static bool HasSavedPosition()
    {
        return PlayerPrefs.HasKey("SavedGirlZ");
    }
}
