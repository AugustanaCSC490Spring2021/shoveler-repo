using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    public GameObject Decision;
    public GameObject JoinLobbyObject;
    public GameObject CreateLobby;

    public TMP_InputField nameJoin;
    public TMP_InputField roomCode;
    public TMP_InputField nameCreate;
    public TMP_InputField roomSeed;
    public TMP_InputField difficulty;
    public TMP_InputField serverIP;
    public TMP_InputField serverPort;

    public void Awake()
    {
        Decision.SetActive(true);
        CreateLobby.SetActive(false);
        JoinLobbyObject.SetActive(false);
    }

    public void ChangeToJoin()
    {
        Decision.SetActive(false);
        JoinLobbyObject.SetActive(true);
    }

    public void ChangeToCreate()
    {
        Decision.SetActive(false);
        CreateLobby.SetActive(true);
    }

    public void JoinLobby()
    {
        PlayerPrefs.SetString("name", nameJoin.text);
        PlayerPrefs.SetString("roomCode", roomCode.text);
        PlayerPrefs.SetString("isHost", "false");
        PlayerPrefs.SetString("serverIP", serverIP.text);
        PlayerPrefs.SetString("serverPort", serverPort.text);
        SceneManager.LoadScene("GameStart");
    }

    public void ReturnMainMenu ()
    {
        SceneManager.LoadScene("Main");
    }

    public void JoinLobbyBack()
    {
        Decision.SetActive(true);
        JoinLobbyObject.SetActive(false);
    }

    public void StartNewLobby()
    {
        PlayerPrefs.SetString("name", nameCreate.text);
        PlayerPrefs.SetString("isHost", "true");
        PlayerPrefs.SetString("seed", roomSeed.text);
        PlayerPrefs.SetString("difficulty", difficulty.text);
        PlayerPrefs.SetString("serverIP", serverIP.text);
        PlayerPrefs.SetString("serverPort", serverPort.text);
        SceneManager.LoadScene("GameStart");
    }

    public void StartNewLobbyBack()
    {
        Decision.SetActive(true);
        CreateLobby.SetActive(false);
    }
}
