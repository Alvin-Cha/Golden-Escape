using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class grab_manager : MonoBehaviour
{
    private bool playerA_pressed = false;
    private bool playerB_pressed = false;

    private string dirA = "";
    private string dirB = "";

    private bool finished = false;

    [Header("UI Buttons")]
    public Image upButton;
    public Image leftButton;
    public Image rightButton;

    private Color color_WAD = new Color(1f, 0.5f, 0f);
    private Color color_ARROW = new Color(0f, 1f, 0.2f);
    private Color color_BOTH = new Color(1f, 0f, 0f);

    void Start()
    {
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        StartCoroutine(EndInputAfter12Sec());
    }

    void Update()
    {
        if (finished) return;

        if (!playerA_pressed)
        {
            if (Input.GetKeyDown(KeyCode.W)) PressA("up");
            else if (Input.GetKeyDown(KeyCode.A)) PressA("left");
            else if (Input.GetKeyDown(KeyCode.D)) PressA("right");
        }

        if (!playerB_pressed)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) PressB("up");
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) PressB("left");
            else if (Input.GetKeyDown(KeyCode.RightArrow)) PressB("right");
        }
    }

    void PressA(string dir)
    {
        playerA_pressed = true;
        dirA = dir;
    }

    void PressB(string dir)
    {
        playerB_pressed = true;
        dirB = dir;
    }

    IEnumerator EndInputAfter12Sec()
    {
        yield return new WaitForSecondsRealtime(12f);

        finished = true;

        if (!playerA_pressed) dirA = RandomDir();
        if (!playerB_pressed) dirB = RandomDir();

        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f;

        yield return AnimateFinalColors();

        yield return new WaitForSecondsRealtime(3f);

        LoadScene();
    }

    IEnumerator AnimateFinalColors()
    {
        Image btnA = GetButton(dirA);
        Image btnB = GetButton(dirB);

        upButton.color = Color.white;
        leftButton.color = Color.white;
        rightButton.color = Color.white;

        if (dirA == dirB)
        {
            btnA.color = color_BOTH;
            StartCoroutine(ScaleButton(btnA.transform));
        }
        else
        {
            btnA.color = color_WAD;
            btnB.color = color_ARROW;

            StartCoroutine(ScaleButton(btnA.transform));
            StartCoroutine(ScaleButton(btnB.transform));
        }

        yield return null;
    }

    Image GetButton(string dir)
    {
        if (dir == "up") return upButton;
        if (dir == "left") return leftButton;
        return rightButton;
    }

    IEnumerator ScaleButton(Transform btn)
    {
        Vector3 start = btn.localScale;
        Vector3 end = start * 1.35f;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * 3f;
            btn.localScale = Vector3.Lerp(start, end, t);
            yield return null;
        }
    }

    string RandomDir()
    {
        int r = Random.Range(0, 3);
        return (r == 0) ? "up" : (r == 1) ? "left" : "right";
    }

    void LoadScene()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        if (dirA == dirB)
            SceneManager.LoadScene("Giant_victory_scene");
        else
            SceneManager.LoadScene("game_duo");
    }
}
