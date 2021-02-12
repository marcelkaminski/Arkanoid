using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int lives;
    public int score;
    public Text scoreText;
    public Text highScoreText;
    public bool gameOver = false;
    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevelIndex = 0;
    public Image[] heartsImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
    }


    public void UpdateLives(int changeInLives)
    {
        lives += changeInLives;
        int heartsLimit = 3;
        if(lives > heartsLimit)
        {
            lives = heartsLimit;
            UpdateScore(100);
        }
        else if(lives <= 0)
        {
            lives = 0;
            GameOver();
        }
        HeartsUpdate(lives);
    }

    private void HeartsUpdate(int lives)
    {
        for (int i = 0; i < 3; i++)
        {
            if(i < lives)
            {
                heartsImages[i].sprite = fullHeart;
            }
            else
            {
                heartsImages[i].sprite = emptyHeart;
            }
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;
        if(numberOfBricks <= 0)
        {
            if(currentLevelIndex >= levels.Length - 1)
            {
                GameOver();
            }
            else
            {
                loadLevelPanel.SetActive(true);
                loadLevelPanel.GetComponentInChildren<Text>().text = "Loading Level " + (currentLevelIndex + 2);
                gameOver = true;
                Invoke("LoadLevel", 3f);

            }
        }
    }

    void LoadLevel()
    {
        SoundManagerScript.PlaySound("loadingSound");        
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        gameOver = false;
        loadLevelPanel.SetActive(false);
    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        int highScore = PlayerPrefs.GetInt ("HIGHSCORE");
        if(score > highScore)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreText.text = "Your Score: " + score + "\nNew High Score!";
        }
        else
        {
            highScoreText.text = "Your Score: " + score + "\nHigh Score: " + highScore;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Scenes/Game");
    }

    public void Quit()
    {
        Debug.Log("Game quit");
        Application.Quit ();
    }
}
