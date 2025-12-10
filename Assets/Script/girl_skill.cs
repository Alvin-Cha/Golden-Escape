using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class girl_skill : MonoBehaviour
{
    [Header("UI")]
    public Text energyText;

    public Button skill1Button;
    public Button skill2Button;
    public Button skill3Button;

    [Header("Cooldown UI")]
    public Image skill1CooldownImage;
    public Text skill1CooldownText;

    public Image skill2CooldownImage;
    public Text skill2CooldownText;

    public Image skill3CooldownImage;
    public Text skill3CooldownText;

    [Header("Energy Bar")]
    public Image energyBar;
    public Image topMarker;
    public float smoothSpeed = 5f;

    [Header("Energy Settings")]
    public float energy = 0f;
    public float maxEnergy = 99f;
    public float perBallEnergy = 1f;

    [Header("Skill Costs")]
    public float costSkill1 = 0.25f;
    public float costSkill2 = 0.50f;
    public float costSkill3 = 0.75f;

    [Header("Skill Cooldowns")]
    public float cdSkill1 = 0.5f;
    public float cdSkill2 = 5f;

    private float cd1Timer = 0f;
    private float cd2Timer = 0f;

    [Header("Skill Events")]
    public UnityEvent onSkill1Used;
    public UnityEvent onSkill2Used;
    public UnityEvent onSkill3Used;

    [Header("References")]
    public giant_movement giant;
    public Skill2TimingManager skill2Timing;

    private const string ENERGY_KEY = "GirlEnergy";
    private float targetFill;

    // ------------------------------------------------------------

    void Start()
    {
        if (PlayerPrefs.HasKey(ENERGY_KEY))
            energy = PlayerPrefs.GetFloat(ENERGY_KEY, 0f);

        UpdateUI();

        skill1Button.onClick.AddListener(OnSkill1Pressed);
        skill2Button.onClick.AddListener(OnSkill2Pressed);
        skill3Button.onClick.AddListener(OnSkill3Pressed);

        if (giant == null)
            giant = FindObjectOfType<giant_movement>();

        if (skill2Timing == null)
            skill2Timing = FindObjectOfType<Skill2TimingManager>();

        ResetCooldownUI(skill1CooldownImage, skill1CooldownText);
        ResetCooldownUI(skill2CooldownImage, skill2CooldownText);
        ResetCooldownUI(skill3CooldownImage, skill3CooldownText);
    }

    // ------------------------------------------------------------

    void Update()
    {
        UpdateEnergyBar();
        UpdateCooldowns();

        // -------------------------------
        // KEYBINDS
        // -------------------------------
        if (Input.GetKeyDown(KeyCode.S))
            OnSkill1Pressed();

        if (Input.GetKeyDown(KeyCode.W))
            OnSkill2Pressed();
    }

    // ------------------------------------------------------------
    // COOLDOWN SYSTEM
    // ------------------------------------------------------------

    void UpdateCooldowns()
    {
        // Skill 1 cooldown
        if (cd1Timer > 0)
        {
            cd1Timer -= Time.deltaTime;
            float ratio = cd1Timer / cdSkill1;

            skill1CooldownImage.fillAmount = ratio;
            skill1CooldownText.text = Mathf.Ceil(cd1Timer).ToString();

            if (cd1Timer <= 0)
                ResetCooldownUI(skill1CooldownImage, skill1CooldownText);
        }

        // Skill 2 cooldown
        if (cd2Timer > 0)
        {
            cd2Timer -= Time.deltaTime;
            float ratio = cd2Timer / cdSkill2;

            skill2CooldownImage.fillAmount = ratio;
            skill2CooldownText.text = Mathf.Ceil(cd2Timer).ToString();

            if (cd2Timer <= 0)
                ResetCooldownUI(skill2CooldownImage, skill2CooldownText);
        }

        skill1Button.interactable = (energy >= costSkill1 && cd1Timer <= 0);
        skill2Button.interactable = (energy >= costSkill2 && cd2Timer <= 0);
        skill3Button.interactable = (energy >= costSkill3);
    }

    void ResetCooldownUI(Image img, Text txt)
    {
        if (img != null) img.fillAmount = 0f;
        if (txt != null) txt.text = "";
    }

    // ------------------------------------------------------------
    // ENERGY SYSTEM
    // ------------------------------------------------------------

    void UpdateEnergyBar()
    {
        if (energyBar == null) return;

        float newFill = Mathf.Lerp(energyBar.fillAmount, targetFill, Time.deltaTime * smoothSpeed);
        energyBar.fillAmount = newFill;

        if (topMarker != null)
        {
            RectTransform barRect = energyBar.rectTransform;
            RectTransform markerRect = topMarker.rectTransform;

            float fillHeight = barRect.rect.height * newFill;

            markerRect.anchoredPosition = new Vector2(
                markerRect.anchoredPosition.x,
                barRect.rect.yMin + fillHeight
            );
        }
    }

    void SaveEnergy()
    {
        PlayerPrefs.SetFloat(ENERGY_KEY, energy);
        PlayerPrefs.Save();
    }

    public void ResetEnergy()
    {
        energy = 0f;
        PlayerPrefs.DeleteKey(ENERGY_KEY);
        UpdateUI();
    }

    public void AddEnergyFromBall() => AddEnergy(perBallEnergy);

    public void AddEnergy(float amount)
    {
        energy = Mathf.Clamp(energy + amount, 0f, maxEnergy);
        SaveEnergy();
        UpdateUI();
    }

    public bool TryUseEnergy(float cost)
    {
        if (energy < cost)
            return false;

        energy -= cost;
        SaveEnergy();
        UpdateUI();
        return true;
    }

    void UpdateUI()
    {
        float ratio = energy / maxEnergy;
        targetFill = ratio;

        if (energyText != null)
            energyText.text = $"{energy:0.00}";
    }

    // ------------------------------------------------------------
    // SKILLS
    // ------------------------------------------------------------

    public void OnSkill1Pressed()
    {
        if (TryUseEnergy(costSkill1))
        {
            onSkill1Used?.Invoke();
            giant?.ReduceSpeed(0.25f);

            cd1Timer = cdSkill1;
            skill1CooldownImage.fillAmount = 1f;
        }
    }

    public void OnSkill2Pressed()
    {
        if (TryUseEnergy(costSkill2))
        {
            onSkill2Used?.Invoke();
            skill2Timing.StartTimingEvent();

            cd2Timer = cdSkill2;
            skill2CooldownImage.fillAmount = 1f;
        }
    }

    public void OnSkill3Pressed()
    {
        if (TryUseEnergy(costSkill3))
        {
            onSkill3Used?.Invoke();
        }
    }
}
