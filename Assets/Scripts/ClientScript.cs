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

    public Profile profile;
    public bool finished = false;
    public long enemyTime = 0;
    public long enemyScore = 0;
    public bool enemyDeath = false;

    public long personalTime = 0;
    public long personalScore = 0;

    private void Awake()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = PlayerObject.GetComponent<PlayerController>();

        if (bool.Parse(PlayerPrefs.GetString("isHost")))
        {
            String seed = PlayerPrefs.GetString("seed");
            String difficulty = PlayerPrefs.GetString("difficulty");

            if (seed == "")
            {
                if (difficulty == "")
                {
                    CreateLobby( PlayerPrefs.GetString("name") );
                }else
                {
                    CreateLobby( PlayerPrefs.GetString("name"), 
                                 difficulty:int.Parse(difficulty));
                }
            }else
            {
                if (difficulty == "")
                {
                    CreateLobby( PlayerPrefs.GetString("name"), 
                                 seed:int.Parse(PlayerPrefs.GetString("seed")));
                }
                else
                {
                    CreateLobby( PlayerPrefs.GetString("name"), 
                                 seed:int.Parse(PlayerPrefs.GetString("seed")), 
                                 difficulty: int.Parse(difficulty));
                }
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

    public void CreateLobby(String name, int seed = 123, int difficulty = 5)
    {
        NewLobby newLobby = new NewLobby
        {
            name = name,
            seed = seed,
            difficulty = difficulty
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
            difficulty = difficulty,
            roomCode = serverJSONResponse.GetValue("roomCode").ToString()
        };

        PlayerPrefs.SetString("roomCode", profile.roomCode);

        playerController.setMove(false);
        StartGame(profile.roomCode);
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
                isHost = false,
                seed = int.Parse(serverJSONResponse.GetValue("seed").ToString()),
                difficulty = int.Parse(serverJSONResponse.GetValue("difficulty").ToString()),
                roomCode = roomCode
            };

            playerController.setMove(false);
            StartGame(profile.roomCode);
        }
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
            GameObject.Find("DungeonManager").GetComponent<DungeonManager>().beginDungeonGeneration(profile.seed, profile.difficulty);
            PlayerPrefs.SetString("roomCode", "");
            playerController.setMove(true);
            //Debug.Log(profile.ToString());
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
            GameObject.Find("DungeonManager").GetComponent<DungeonManager>().beginDungeonGeneration(profile.seed, profile.difficulty);
            PlayerPrefs.SetString("roomCode", "");
            playerController.setMove(true);
            //Debug.Log(profile.ToString());
        }
    }

    //public void SendScore(String name, bool isHost, long id, long time, long score, String roomCode)
    public void SendScore(long time, long score, bool hasDied)
    {
        SubmitScore submitScore = new SubmitScore
        {
            name = profile.name,
            isHost = profile.isHost,
            id = profile.id,
            time = time,
            score = score,
            roomCode = profile.roomCode,
            hasDied = hasDied
        };
        string Message = JsonConvert.SerializeObject(submitScore, Formatting.Indented);
        String serverResponse = SendServerMessage(Message);
        JObject serverJSONResponse = JObject.Parse(serverResponse);

        // If other player is not done, wait. Otherwise, print score
        if (serverJSONResponse.GetValue("response").ToString() == "NotDone") 
        {
            stupidFix(Message);
        } else
        {
            enemyTime = long.Parse(serverJSONResponse.GetValue("enemyTime").ToString());
            enemyScore = long.Parse(serverJSONResponse.GetValue("enemyScore").ToString());
            enemyDeath = Boolean.Parse(serverJSONResponse.GetValue("enemyDeath").ToString());
            PlayerPrefs.SetString("roomCode", "");
            finished = true;
        }
    }

    public void stupidFix(String Message) { StartCoroutine(CheckIfFinished(Message)); }
    IEnumerator CheckIfFinished (string Message)
    {
        yield return new WaitForSecondsRealtime(3);

        String serverResponse = SendServerMessage(Message);
        JObject serverJSONResponse = JObject.Parse(serverResponse);

        if (serverJSONResponse.GetValue("response").ToString() == "NotDone")
        {
            stupidFix(Message);
        } else
        {
            enemyTime = long.Parse(serverJSONResponse.GetValue("enemyTime").ToString());
            enemyScore = long.Parse(serverJSONResponse.GetValue("enemyScore").ToString());
            finished = true;
        }
    }



    public static String SendServerMessage(String toSend)
    {
        // Google VM 35.209.36.147
        // Local host 127.0.0.1
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
        public int difficulty { get; set; }
        public String roomCode { get; set; }

        public String ToString()
        {
            return "ID: "      + this.id + "\n" +
                "Name: "       + this.name + "\n" +
                "Enemy Name: " + this.enemyName + "\n" +
                "Is Host: "    + this.isHost + "\n" +
                "Difficulty: " + this.difficulty + "\n" +
                "Room Code: "  + this.roomCode + "\n";
        }
    }

    public class NewLobby
    {
        public String action = "NewLobby";
        public String name { get; set; }
        public int seed { get; set; }
        public int difficulty { get; set; }
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
        public bool hasDied { get; set; }
        public String roomCode { get; set; }
    }
}
