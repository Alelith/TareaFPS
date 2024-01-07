using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField]
    private GameObject highScorePanel;
    [SerializeField]
    private GameObject creditPanel;
    [SerializeField]
    private GameObject volumePanel;
    [Header("Maps")]
    [SerializeField]
    private int mapsCount;
    [Header("Cursor")]
    [SerializeField]
    private Texture2D cursor;
    [SerializeField]
    private Texture2D cursorClick;
    [Header("Score")]
    [SerializeField]
    private List<TextMeshProUGUI> scoreTexts;

    private void Awake()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);

        for (int i = 0; i < scoreTexts.Count; i++)
        {
            if (!PlayerPrefs.HasKey("HighScore" + i))
                PlayerPrefs.SetInt("HighScore" + i, 0);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (highScorePanel.activeSelf || creditPanel.activeSelf || volumePanel.activeSelf))
        {
            highScorePanel.SetActive(false);
            creditPanel.SetActive(false);
            volumePanel.SetActive(false);
        }

        if (Input.GetMouseButton(0))
            Cursor.SetCursor(cursorClick, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPlayGameButtonPressed()
    {
        SceneManager.LoadScene(Random.Range(1, mapsCount));
    }

    public void OnHighScoreButtonPressed()
    {
        var i = 0;
        foreach (var text in scoreTexts)
        {
            text.text = PlayerPrefs.GetInt("HighScore" + i).ToString();
            i++;
        }

        highScorePanel.SetActive(true);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }

    public void OnCreditButtonPressed()
    {
        creditPanel.SetActive(true);
    }

    public void OnVolumeButtonPressed()
    {
        volumePanel.SetActive(true);
    }
}
