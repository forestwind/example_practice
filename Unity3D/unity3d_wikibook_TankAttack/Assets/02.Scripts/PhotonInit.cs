using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhotonInit : MonoBehaviour {

    public string version = "V1.0";

    public InputField userId;
	
	void Awake ()
    {
        PhotonNetwork.ConnectUsingSettings(version);
	}

    void OnJoinedLobby()
    {
        Debug.Log("Entered Lobby");
        
            userId.text = GetUserId();

        //PhotonNetwork.JoinRandomRoom();
    }

    string GetUserId()
    {
        string userId = PlayerPrefs.GetString("USER_ID");

        if(string.IsNullOrEmpty(userId))
        {
            userId = "USER_" + Random.Range(0, 999).ToString("000");
        }

        return userId;
    }


    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No Rooms !");

        RoomOptions option = new RoomOptions();
        option.isOpen = true;
        option.isVisible = true;
        option.maxPlayers = 20;

        PhotonNetwork.CreateRoom("MyRoom", option, TypedLobby.Default);

    }

    void OnJoinedRoom()
    {
        Debug.Log("Enter Room");
        //CreateTank();
        StartCoroutine(this.LoadBattleField());
    }

    IEnumerator LoadBattleField()
    {
        PhotonNetwork.isMessageQueueRunning = false;

        AsyncOperation ao = Application.LoadLevelAsync("scBattleField");
        yield return ao;
    }

    public void OnClickJoinRandomRoom()
    {
        PhotonNetwork.player.name = userId.text;

        PlayerPrefs.SetString("USER_ID", userId.text);

        PhotonNetwork.JoinRandomRoom();
    }


    //void CreateTank()
    //{
    //    float pos = Random.Range(-100.0f, 100.0f);
    //    PhotonNetwork.Instantiate("Tank", new Vector3(pos, 20.0f, pos), Quaternion.identity, 0);
    //}

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}
