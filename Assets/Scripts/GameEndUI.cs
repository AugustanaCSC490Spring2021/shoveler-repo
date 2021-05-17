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
            enemyDied = "Yes";
        }
        else
        {
            enemyDeath = false;
            enemyDied = "No";
        }
        if (PlayerPrefs.GetString("yourDeath") == "Yes")
        {
            yourDeath = true;
            youDied = "Yes";
        }
        else
        {
            yourDeath = false;
            youDied = "No";
        }

        enemyScore = PlayerPrefs.GetInt("enemyScore");
        enemyTime = PlayerPrefs.GetInt("enemyTime");

        yourScore = PlayerPrefs.GetInt("yourScore");
        yourTime = PlayerPrefs.GetInt("yourTime");

        // Here we determine who won.
        if (enemyDeath && yourDeath) // Check score & time for winner
        {
            DetermineWinner();
        }
        else if(enemyDeath) // You Win
        {
            YouWin();
        }
        else if(yourDeath) // Enemy Wins
        {
            YouLose();
        }
        else // Check score & time for winner
        {
            DetermineWinner();
        }

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

    private void DetermineWinner()
    {
        if (enemyScore > yourScore && enemyTime < yourTime) // Enemy Winner
        {
            Debug.Log("First");
            YouLose();
        }
        else if (yourScore > enemyScore && yourTime < enemyTime) // You win
        {
            Debug.Log("Second");
            YouWin();
        }
        else if (enemyTime < yourTime) // Enemy Wins
        {
            Debug.Log("Third");
            YouLose();
        }
        else if (yourTime < enemyTime) // You Win
        {
            Debug.Log("Fourth");
            YouWin();
        }
        else // Default to win? This should probably be a draw.
        {
            Debug.Log("Fifth");
            YouWin();
        }
    } 
}
