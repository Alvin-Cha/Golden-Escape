using UnityEngine;

public static class game_data 
{
    public static float girlZ = 0f;
    public static float giantZ = 0f;

    public static void SavePositions(Transform girl, Transform giant)
    {
        girlZ = girl.position.z;
        giantZ = giant.position.z;
    }

    public static void LoadPositions(Transform girl, Transform giant)
    {
        girl.position = new Vector3(girl.position.x, girl.position.y, girlZ);
        giant.position = new Vector3(giant.position.x, giant.position.y, giantZ);
    }

    public static void ResetPositions()
    {
        girlZ = 0f;
        giantZ = 0f;
    }
}
