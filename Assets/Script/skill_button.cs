using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class skill_button : MonoBehaviour
{
    [Header("Player References")]
    public player_movement playerMove;
    public Transform girlTransform;
    public Transform giantTransform;

    [Header("Skill Settings")]
    public float reverseDuration = 3f;
    private bool isReversed = false;
    private bool playerInGrab = false;

    [Header("UI Buttons")]
    public Button skill1Button;
    public Button skill2Button;
    public Button skill3Button;

    [Header("Skill 2 UI Elements")]
    public Image skill2Icon;
    public Image skill2Frame;
    public Sprite skill2NormalFrame;
    public Sprite skill2ActiveFrame;
    public Sprite skill2NormalIcon;
    public Sprite skill2ActiveIcon;

    [Header("Skill 1 Cooldown UI")]
    [SerializeField] private Image imageCooldown1;
    [SerializeField] private Text textCooldown1;
    private bool isCoolDown1 = false;
    private float cooldownTime1 = 10.0f;
    private float cooldownTimer1 = 0.0f;

    [Header("Skill 3 Cooldown UI")]
    [SerializeField] private Image imageCooldown3;
    [SerializeField] private Text textCooldown3;
    public bool isCoolDown3 = false;
    private float cooldownTime3 = 1.5f;
    private float cooldownTimer3 = 0.0f;

    [HideInInspector] public bool buttonPressed3 = false;

    // ------------------------------
    // Dizzy Object VFX
    // ------------------------------
    [Header("Skill 1 Dizzy VFX")]
    public GameObject spinPrefab;
    private GameObject activeSpinObj;
    private float spinSpeed = 220f;
    private float fadeDuration = 0.35f;

    // ------------------------------
    // Camera Shake Settings
    // ------------------------------
    [Header("Camera Shake")]
    public camera_shake camShake;
    public float shakeDuration = 1f;
    public float shakeMagnitude = 2.5f;


    void Start()
    {
        if (textCooldown1 != null) textCooldown1.gameObject.SetActive(false);
        if (imageCooldown1 != null) imageCooldown1.fillAmount = 0.0f;

        if (textCooldown3 != null) textCooldown3.gameObject.SetActive(false);
        if (imageCooldown3 != null) imageCooldown3.fillAmount = 0.0f;

        if (skill1Button != null)
            skill1Button.onClick.AddListener(Skill1);
        if (skill2Button != null)
            skill2Button.onClick.AddListener(Skill2);
        if (skill3Button != null)
            skill3Button.onClick.AddListener(OnSkill3Pressed);

        if (skill2Frame != null && skill2NormalFrame != null)
            skill2Frame.sprite = skill2NormalFrame;
        if (skill2Icon != null && skill2NormalIcon != null)
            skill2Icon.sprite = skill2NormalIcon;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Keypad4))
            Skill1();

        if (Input.GetKeyDown(KeyCode.Keypad6))
            Skill2();
        // ------------------------------
        // Skill 2 UI state update
        // ------------------------------
        if (playerInGrab)
        {
            if (skill2Frame != null) skill2Frame.sprite = skill2ActiveFrame;
            if (skill2Icon != null) skill2Icon.sprite = skill2ActiveIcon;
        }
        else
        {
            if (skill2Frame != null) skill2Frame.sprite = skill2NormalFrame;
            if (skill2Icon != null) skill2Icon.sprite = skill2NormalIcon;
        }

        if (isCoolDown1) ApplyCooldown1();
        if (isCoolDown3) ApplyCooldown3();

        // ------------------------------
        // Spin + Bob + Glow
        // ------------------------------
        if (activeSpinObj != null && girlTransform != null)
        {
            float heightOffset = 3.5f;
            float bob = Mathf.Sin(Time.time * 3f) * 0.25f;

            activeSpinObj.transform.position =
                girlTransform.position +
                new Vector3(0, heightOffset + bob, 0);

            activeSpinObj.transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);

            Renderer r = activeSpinObj.GetComponentInChildren<Renderer>();
            if (r != null && r.material.HasProperty("_EmissionColor"))
            {
                float glow = (Mathf.Sin(Time.time * 5f) + 1f) * 0.5f;
                Color baseColor = Color.white * 2f;
                r.material.SetColor("_EmissionColor", baseColor * glow);
            }
        }
    }

    public void SetPlayerInGrab(bool inside)
    {
        playerInGrab = inside;
    }

    // -----------------------------
    // Skill 1
    // -----------------------------
    public void Skill1()
    {
        if (!isCoolDown1 && !isReversed)
        {
            UseCooldown1();
            StartCoroutine(ReverseControls());
        }
    }

    private IEnumerator ReverseControls()
    {
        isReversed = true;
        playerMove.reverseControls = true;

        // Spawn dizzy VFX
        if (spinPrefab != null && activeSpinObj == null)
        {
            activeSpinObj = Instantiate(spinPrefab);
            StartCoroutine(FadeInObject(activeSpinObj));
        }

        // 🔥 EXTREME SCREEN SHAKE FOR 1 SEC ONLY
        if (camShake != null)
            camShake.TriggerShake(shakeDuration, shakeMagnitude);

        yield return new WaitForSeconds(reverseDuration);

        playerMove.reverseControls = false;
        isReversed = false;

        // Fade out and remove
        if (activeSpinObj != null)
        {
            yield return StartCoroutine(FadeOutObject(activeSpinObj));
            Destroy(activeSpinObj);
        }
    }

    // -----------------------------
    // Fade In
    // -----------------------------
    private IEnumerator FadeInObject(GameObject obj)
    {
        Renderer r = obj.GetComponentInChildren<Renderer>();
        if (r == null) yield break;

        float t = 0;
        while (t < fadeDuration)
        {
            float a = t / fadeDuration;
            r.material.color = new Color(1, 1, 1, a);
            t += Time.deltaTime;
            yield return null;
        }
    }

    // -----------------------------
    // Fade Out
    // -----------------------------
    private IEnumerator FadeOutObject(GameObject obj)
    {
        Renderer r = obj.GetComponentInChildren<Renderer>();
        if (r == null) yield break;

        float t = 0;
        while (t < fadeDuration)
        {
            float a = 1f - (t / fadeDuration);
            r.material.color = new Color(1, 1, 1, a);
            t += Time.deltaTime;
            yield return null;
        }
    }

    // -----------------------------
    // Skill 2
    // -----------------------------
    public void Skill2()
    {
        if (playerInGrab)
        {
            if (girlTransform != null && giantTransform != null)
                data_game.SavePositions(girlTransform, giantTransform);

            SceneManager.LoadScene("grab_scene");
        }
    }

    // -----------------------------
    // Skill 3
    // -----------------------------
    public void OnSkill3Pressed()
    {
        if (!isCoolDown3)
            buttonPressed3 = true;
    }

    public void UseCooldown3()
    {
        isCoolDown3 = true;
        textCooldown3.gameObject.SetActive(true);
        cooldownTimer3 = cooldownTime3;
        textCooldown3.text = cooldownTimer3.ToString("0.0");
        imageCooldown3.fillAmount = 1.0f;
    }

    void ApplyCooldown3()
    {
        cooldownTimer3 -= Time.deltaTime;

        if (cooldownTimer3 <= 0.0f)
        {
            isCoolDown3 = false;
            textCooldown3.gameObject.SetActive(false);
            imageCooldown3.fillAmount = 0.0f;
        }
        else
        {
            textCooldown3.text = cooldownTimer3.ToString("0.0");
            imageCooldown3.fillAmount = cooldownTimer3 / cooldownTime3;
        }
    }

    // -----------------------------
    // Skill 1 Cooldown
    // -----------------------------
    void ApplyCooldown1()
    {
        cooldownTimer1 -= Time.deltaTime;

        if (cooldownTimer1 <= 0.0f)
        {
            isCoolDown1 = false;
            textCooldown1.gameObject.SetActive(false);
            imageCooldown1.fillAmount = 0.0f;
        }
        else
        {
            textCooldown1.text = cooldownTimer1.ToString("0.0");
            imageCooldown1.fillAmount = cooldownTimer1 / cooldownTime1;
        }
    }

    void UseCooldown1()
    {
        isCoolDown1 = true;
        textCooldown1.gameObject.SetActive(true);
        cooldownTimer1 = cooldownTime1;
        textCooldown1.text = cooldownTimer1.ToString("0.0");
        imageCooldown1.fillAmount = 1.0f;
    }
}
