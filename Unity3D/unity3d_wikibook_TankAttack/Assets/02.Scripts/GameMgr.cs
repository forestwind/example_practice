using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMgr : MonoBehaviour {

    public Text txtConnect;

	void Awake () {

        CreateTank();
        PhotonNetwork.isMessageQueueRunning = true;

        GetConnectPlayerCount();
	}

    void CreateTank()
    {
        float pos = Random.Range(-100.0f, 100.0f);
        PhotonNetwork.Instantiate("Tank", new Vector3(pos, 20.0f, pos), Quaternion.identity, 0);
    }

    void GetConnectPlayerCount()
    {
        Room currRoom = PhotonNetwork.room;

        txtConnect.text = currRoom.playerCount.ToString() + "/" + currRoom.maxPlayers.ToString();
    }

    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        GetConnectPlayerCount();
    }

    void OnPhotonPlayerDisconnected(PhotonPlayer outPlayer)
    {
        GetConnectPlayerCount();
    }

    public void OnClickExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void OnLeftRoom()
    {
        Application.LoadLevel("scLobby");
    }
}
