using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [Header("Guns")]
    [SerializeField]
    private Image gunImage;
    [Header("Health")]
    [SerializeField]
    private List<Image> hearts;
    [SerializeField]
    private List<Sprite> heartSprites;
    [Header("Score")]
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [Header("AMMO")]
    [SerializeField]
    private TextMeshProUGUI magazineText;
    [SerializeField]
    private Slider ammoSlider;
    [Header("Stamina")]
    [SerializeField]
    private Slider staminaSlider;
    [Header("Pause Game")]
    [SerializeField]
    private GameObject pauseMenu;
    [Header("Game Over")]
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private TextMeshProUGUI finalScoreText;

    private CustomAnimator animator;

    private int score;

    public static GUIController instance;

    private void Awake()
    {
        instance = this;

        animator = GetComponentInChildren<CustomAnimator>();

        var player = GameObject.Find("Player");

        ammoSlider.maxValue = player.GetComponent<WeaponController>().Weapons[0].MagazineBullets;
        staminaSlider.maxValue = player.GetComponent<PlayerMovement>().MaxStamina;

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0;
        }
    }

    public void UpdateLives(int lives)
    {
        for (int i = hearts.Count - 1; i >= 0; i--)
        {
            if (lives >= ((i + 1) * 4))
                hearts[i].sprite = heartSprites[^1];
            else
            {
                hearts[i].sprite = heartSprites[lives % 4];
                if (i < hearts.Count - 1)
                    for (int j = i + 1; j < hearts.Count; j++)
                        hearts[j].sprite = heartSprites[0];
            }
        }
    }

    public void SetupNewWeapon(Weapon weapon)
    {
        gunImage.sprite = weapon.GunImage;
        animator.SetupAnimator(weapon.Frames);
        ammoSlider.maxValue = weapon.MagazineBullets;
    }

    public void UpdateScore(int score)
    {
        this.score += score;

        scoreText.text = "Score: \n" + this.score.ToString("0000");
    }

    public void UpdateAmmo(int ammo, int magazines)
    {
        magazineText.text = "Ammo: \n" + magazines.ToString("0000");
        ammoSlider.value = ammo;
    }

    public void UpdateStamina(float currentStamina)
    {
        staminaSlider.value = currentStamina;
    }

    public void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;

        finalScoreText.text = "Your score: " + score.ToString("000000");

        gameOverMenu.SetActive(true);

        List<int> highScores = new();

        for (int i = 0; i < 5; i++)
            highScores.Add(PlayerPrefs.GetInt("HighScore" + i));

        highScores.Add(score);
        highScores.Sort();
        highScores.Reverse();

        for (int i = 0; i < 5; i++)
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
    }

    public void OnResumeSelected()
    {
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;
    }

    public void OnMainMenuSelected()
    {
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public CustomAnimator Animator { get => animator; set => animator = value; }
}
