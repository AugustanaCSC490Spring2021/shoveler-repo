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
        PlayerPrefs.SetString("isHost", "true");
        SceneManager.LoadScene("GameStart");
    }

    public void StartNewLobby()
    {
        PlayerPrefs.SetString("name", nameJoin.text);
        PlayerPrefs.SetString("isHost", "false");
        SceneManager.LoadScene("GameStart");
    }
}
