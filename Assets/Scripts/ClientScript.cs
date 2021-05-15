using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class ClientScript : MonoBehaviour
{
    GameObject PlayerObject;
    PlayerController playerController;

    Profile profile;
    bool finished = false;
    long enemyTime = 0;
    long enemyScore = 0;

    long personalTime = 0;
    long personalScore = 0;

    private void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = PlayerObject.GetComponent<PlayerController>();

        if (bool.Parse(PlayerPrefs.GetString("isHost")))
        {
            String seed = PlayerPrefs.GetString("seed");

            if(seed == "")
            {
                CreateLobby( PlayerPrefs.GetString("name") );
            }else
            {
                CreateLobby( PlayerPrefs.GetString("name"), int.Parse(PlayerPrefs.GetString("seed")) );
            }
        }else
        {
            ConnectToLobby(PlayerPrefs.GetString("name"), PlayerPrefs.GetString("roomCode"));
        }


        /*CreateLobby("ServerMan");
        StartGame("TEMPROOMCODE");
        ConnectToLobby("ClientMan", "TEMPROOMCODE");
        StartGame("TEMPROOMCODE");
        SendScore("ServerMan", true, profile.id, 999, 99999, "TEMPROOMCODE");
        SendScore("ClientMan", false, profile.id, 420, 1337, "TEMPROOMCODE");*/

    }

    public void CreateLobby(String name, int seed = 123)
    {
        NewLobby newLobby = new NewLobby
        {
            name = name,
            seed = seed
        };
        string Message = JsonConvert.SerializeObject(newLobby, Formatting.Indented);
        String serverResponse = SendServerMessage(Message);
        JObject serverJSONResponse = JObject.Parse(serverResponse);

        profile = new Profile
        {
            id = long.Parse( serverJSONResponse.GetValue("id").ToString() ),
            name = name,
            isHost = true,
            seed = seed,
            roomCode = serverJSONResponse.GetValue("roomCode").ToString()
        };

        PlayerPrefs.SetString("roomCode", profile.roomCode);

        playerController.setMove(false);
        StartGame(profile.roomCode);

        //Debug.Log(profile.roomCode);
    }

    public void ConnectToLobby(String name, String roomCode)
    {
        JoinRoom joinRoom = new JoinRoom
        {
            name = name,
            roomCode = roomCode
        };
        string Message = JsonConvert.SerializeObject(joinRoom, Formatting.Indented);
        String serverResponse = SendServerMessage(Message);
        JObject serverJSONResponse = JObject.Parse(serverResponse);

        if(serverJSONResponse.GetValue("response").ToString() == "Missing")
        {
            SceneManager.LoadScene("Lobby");
        }else
        {
            profile = new Profile
            {
                id = long.Parse(serverJSONResponse.GetValue("id").ToString()),
                name = name,
                enemyName = serverJSONResponse.GetValue("enemyName").ToString(),
                isHost = true,
                seed = int.Parse(serverJSONResponse.GetValue("seed").ToString()),
                roomCode = roomCode
            };

            playerController.setMove(false);
            StartGame(profile.roomCode);
        }

        //Debug.Log(profile.seed);
        //Debug.Log(profile.roomCode);
    }

    public void StartGame(String roomCode)
    {
        ShouldStart shouldStart = new ShouldStart
        {
            roomCode = roomCode
        };

        string Message = JsonConvert.SerializeObject(shouldStart, Formatting.Indented);
        String serverResponse = SendServerMessage(Message);
        JObject serverJSONResponse = JObject.Parse(serverResponse);

        if(serverJSONResponse.GetValue("response").ToString() == "No")
        {
            stupidFix2(Message);
        }else
        {
            if (profile.isHost)
            {
                profile.enemyName = serverJSONResponse.GetValue("enemyName").ToString();
            }
            GameObject.Find("DungeonManager").GetComponent<DungeonManager>().beginDungeonGeneration(profile.seed, 3);
            PlayerPrefs.SetString("roomCode", "");
            playerController.setMove(true);
        }
    }

    public void stupidFix2(String Message) { StartCoroutine(CheckIfStart(Message)); }
    IEnumerator CheckIfStart(string Message)
    {
        yield return new WaitForSecondsRealtime(3);

        String serverResponse = SendServerMessage(Message);
        JObject serverJSONResponse = JObject.Parse(serverResponse);

        if (serverJSONResponse.GetValue("response").ToString() == "No")
        {
            stupidFix2(Message);
        }
        else
        {
            // TODO: Load level and start game 
            if (profile.isHost)
            {
                profile.enemyName = serverJSONResponse.GetValue("enemyName").ToString();
            }
            GameObject.Find("DungeonManager").GetComponent<DungeonManager>().beginDungeonGeneration(profile.seed, 3);
            PlayerPrefs.SetString("roomCode", "");
            playerController.setMove(true);
            Debug.Log(int.Parse(PlayerPrefs.GetString("seed")));
        }
    }

    public void SendScore(String name, bool isHost, long id, long time, long score, String roomCode)
    {
        SubmitScore submitScore = new SubmitScore
        {
            name = name,
            isHost = isHost,
            id = id,
            time = time,
            score = score,
            roomCode = roomCode
        };
        string Message = JsonConvert.SerializeObject(submitScore, Formatting.Indented);
        String serverResponse = SendServerMessage(Message);
        JObject serverJSONResponse = JObject.Parse(serverResponse);

        // If other player is not done, wait. Otherwise, print score
        if (serverJSONResponse.GetValue("response").ToString() == "NotDone") 
        {
            stupidFix(Message);
        }
    }

    public void stupidFix(String Message) { StartCoroutine(CheckIfFinished(Message)); }
    IEnumerator CheckIfFinished (string Message)
    {
        yield return new WaitForSecondsRealtime(3);
        //Debug.Log("Waiting for other player to finish...");

        String serverResponse = SendServerMessage(Message);
        JObject serverJSONResponse = JObject.Parse(serverResponse);

        if (serverJSONResponse.GetValue("response").ToString() == "NotDone")
        {
            stupidFix(Message);
        } else
        {
            enemyTime = long.Parse(serverJSONResponse.GetValue("enemyTime").ToString());
            enemyScore = long.Parse(serverJSONResponse.GetValue("enemyScore").ToString());
            //Debug.Log("Enemy Time: " + enemyTime);
            //Debug.Log("Enemy Score: " + enemyScore);
            finished = true;
        }
    }



    public static String SendServerMessage(String toSend)
    {
        // Google VM 34.121.188.103
        IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25566);

        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(serverAddress);

        // Sending
        int toSendLen = System.Text.Encoding.ASCII.GetByteCount(toSend);
        byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(toSend);
        byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
        clientSocket.Send(toSendLenBytes);
        clientSocket.Send(toSendBytes);

        // Receiving
        byte[] rcvLenBytes = new byte[4];
        clientSocket.Receive(rcvLenBytes);
        int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
        byte[] rcvBytes = new byte[rcvLen];
        clientSocket.Receive(rcvBytes);
        String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);

        //Debug.Log("Message: " + rcv);

        clientSocket.Close();

        return rcv;
    }

    public class Profile
    {
        public long id { get; set; }
        public String name { get; set; }
        public String enemyName { get; set; }
        public bool isHost { get; set; }
        public int seed { get; set; }
        public String roomCode { get; set; }
    }

    public class NewLobby
    {
        public String action = "NewLobby";
        public String name { get; set; }
        public int seed { get; set; }
    }

    public class JoinRoom
    {
        public String action = "JoinRoom";
        public String name { get; set; }
        public String roomCode { get; set; }
    }

    public class ShouldStart
    {
        public String action = "ShouldStart";
        public String roomCode { get; set; }
    }

    public class SubmitScore
    {
        public String action = "SubmitScore";
        public String name { get; set; }
        public bool isHost { get; set; }
        public long id { get; set; }
        public long time { get; set; }
        public long score { get; set; }
        public String roomCode { get; set; }
    }
}
