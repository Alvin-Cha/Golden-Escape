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
    public Image skill2Frame;          // frame image
    public Image skill2Icon;           // icon image
    public Sprite skill2NormalFrame;   // normal frame
    public Sprite skill2ActiveFrame;   // active frame
    public Sprite skill2NormalIcon;    // normal icon
    public Sprite skill2ActiveIcon;    // active icon

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

    void Start()
    {
        // --- Initialize Cooldowns ---
        if (textCooldown1 != null) textCooldown1.gameObject.SetActive(false);
        if (imageCooldown1 != null) imageCooldown1.fillAmount = 0.0f;

        if (textCooldown3 != null) textCooldown3.gameObject.SetActive(false);
        if (imageCooldown3 != null) imageCooldown3.fillAmount = 0.0f;

        // --- Button Listeners ---
        if (skill1Button != null)
            skill1Button.onClick.AddListener(Skill1);
        if (skill2Button != null)
            skill2Button.onClick.AddListener(Skill2);
        if (skill3Button != null)
            skill3Button.onClick.AddListener(OnSkill3Pressed);

        // --- Default Skill 2 visuals ---
        if (skill2Frame != null && skill2NormalFrame != null)
            skill2Frame.sprite = skill2NormalFrame;
        if (skill2Icon != null && skill2NormalIcon != null)
            skill2Icon.sprite = skill2NormalIcon;
    }

    void Update()
    {
        // update both frame and icon when player in grab range
        if (playerInGrab)
        {
            if (skill2Frame != null && skill2ActiveFrame != null)
                skill2Frame.sprite = skill2ActiveFrame;
            if (skill2Icon != null && skill2ActiveIcon != null)
                skill2Icon.sprite = skill2ActiveIcon;
        }
        else
        {
            if (skill2Frame != null && skill2NormalFrame != null)
                skill2Frame.sprite = skill2NormalFrame;
            if (skill2Icon != null && skill2NormalIcon != null)
                skill2Icon.sprite = skill2NormalIcon;
        }

        if (isCoolDown1) ApplyCooldown1();
        if (isCoolDown3) ApplyCooldown3();
    }

    public void SetPlayerInGrab(bool inside)
    {
        playerInGrab = inside;
    }

    // -----------------------------
    // Skill 1 (Reverse Controls)
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
        yield return new WaitForSeconds(reverseDuration);
        playerMove.reverseControls = false;
        isReversed = false;
    }

    // -----------------------------
    // Skill 2 (Grab)
    // -----------------------------
    public void Skill2()
    {
        if (playerInGrab)
        {
            if (girlTransform != null && giantTransform != null)
                game_data.SavePositions(girlTransform, giantTransform);

            SceneManager.LoadScene("grab_scene");
        }
    }

    // -----------------------------
    // Skill 3 (Spike Summon)
    // -----------------------------
    public void OnSkill3Pressed()
    {
        if (!isCoolDown3)
        {
            buttonPressed3 = true;
        }
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
                new Vector3(0, 0, 360.0f * (cooldownTimer3 / cooldownTime3));
        }
    }

    // -----------------------------
    // Skill 1 Cooldown System
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
                new Vector3(0, 0, 360.0f * (cooldownTimer1 / cooldownTime1));
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
