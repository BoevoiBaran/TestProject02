using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private int score;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    [SerializeField] private float roundTime;
    public float RoundTime
    {
        get { return roundTime; }
        set { roundTime = value; }
    }
    public bool isGameOver = false;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text endGameScore;

    [SerializeField] private GameObject endGameMenu;


    private void Start()
    {
        endGameMenu.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) RaycastOnClick();

        if (roundTime <= 0.0f)
        {
            GameOver();
        }
        else
        {
            Timer();
        }
    }

    private void RaycastOnClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {
            hit.collider.GetComponent<Ball>().OnClick();
        }
    }

    private void Timer()
    {
        roundTime -= Time.deltaTime;
        timerText.text = "Time: " + roundTime.ToString("f2");
    }


    private void GameOver()
    {
        endGameMenu.SetActive(true);
        isGameOver = true;
        EndGameScore();
        timerText.text = "Time: 0.00";
    }

    public void ScoreRefresh()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void EndGameScore()
    {
        endGameScore.text = "You score: " + score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
        isGameOver = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
