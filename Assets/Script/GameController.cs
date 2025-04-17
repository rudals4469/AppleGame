using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelMainMenu;
    [SerializeField]
    private GameObject panelInGame;
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField]
    private Text textInGameScore;
    [SerializeField]
    private Text textOutGameScore;
    [SerializeField]
    private Image timeGauge;
    [SerializeField]
    private float maxTime = 120f;

    private int curruntScore;

    private AudioSource audioSource;

    public bool IsGameStart { private set; get; } = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void GameStart()
    {
        IsGameStart = true;

        panelMainMenu.SetActive(false);
        panelInGame.SetActive(true);

        audioSource.Play();

        StartCoroutine(nameof(TimeCounter));

    }

    public void IncreaseScore(int addScore)
    {
        curruntScore += addScore;
        textInGameScore.text = curruntScore.ToString();
    }

    public void ButtonRestart()
    {
        SceneManager.LoadScene(0);

    }
    public void GameOver()
    {
        textOutGameScore.text = $"SCROE\n{curruntScore}";

        panelInGame.SetActive(false );
        panelGameOver.SetActive(true);
        audioSource.Stop();
    }
    private IEnumerator TimeCounter()
    {
        float curruenTime = maxTime;
        while (curruenTime > 0)
        {
            curruenTime -= Time.deltaTime;
            timeGauge.fillAmount = curruenTime / maxTime;

            yield return null;
        }

        GameOver();
       
    }
}
