using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndUI : MonoBehaviour
{
    public GameObject Results;

    public TMP_Text ResultsText;
    public TMP_Text YourResultsText;
    public TMP_Text EnemyResultsText;

    private int enemyScore;
    private int enemyTime;
    private bool enemyDeath;

    private int yourScore;
    private int yourTime;
    private bool yourDeath;

    private void Awake()
    {
        string enemyDied;
        string youDied;
        if (PlayerPrefs.GetString("enemyDeath") == "Yes")
        {
            enemyDeath = true;
            enemyDied = PlayerPrefs.GetString("enemyDeath");
        }
        else
        {
            enemyDeath = false;
            enemyDied = PlayerPrefs.GetString("enemyDeath");
        }
        if (PlayerPrefs.GetString("yourDeath") == "Yes")
        {
            yourDeath = true;
            youDied = PlayerPrefs.GetString("yourDeath");
        }
        else
        {
            yourDeath = false;
            youDied = PlayerPrefs.GetString("yourDeath");
        }

        enemyScore = PlayerPrefs.GetInt("enemyScore");
        enemyTime = PlayerPrefs.GetInt("enemyTime");

        yourScore = PlayerPrefs.GetInt("yourScore");
        yourTime = PlayerPrefs.GetInt("yourTime");

        /*// Here we determine who won.
        if (enemyDeath && yourDeath) // Check score & time for winner
        {
            DetermineWinner();
        }
        else if(enemyDeath) // You Win
        {
            DetermineWinner();
        }
        else if(yourDeath) // Enemy Wins
        {
            DetermineWinner();
        }
        else // Check score & time for winner
        {
            DetermineWinner();
        }*/
        DetermineWinner();

        // Display Results
        YourResultsText.text = "Score: " + yourScore + "\n" +
                               "Time: " + yourTime + "\n" +
                               "Died: " + youDied + "\n";

        EnemyResultsText.text = "Score: " + enemyScore + "\n" +
                                "Time: " + enemyTime + "\n" +
                                "Died: " + enemyDied + "\n";
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Lobby");
    }

    private void YouWin()
    {
        ResultsText.text = "Winner!";
        ResultsText.color = Color.green;
    }

    private void YouLose()
    {
        ResultsText.text = "Better luck next time...";
        ResultsText.color = Color.red;
    }

    private void Draw()
    {
        ResultsText.text = "Draw!!!";
        ResultsText.color = Color.yellow;
    }

    private void DetermineWinner()
    {
        if(enemyScore > yourScore)
        {
            YouLose();
        }else if(yourScore > enemyScore)
        {
            YouWin();
        }else if(enemyTime < yourTime)
        {
            YouLose();
        }else if(yourTime < enemyTime)
        {
            YouWin();
        }else
        {
            Draw();
        }
    } 
}
