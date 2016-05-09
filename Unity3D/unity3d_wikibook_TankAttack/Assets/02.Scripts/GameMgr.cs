using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMgr : MonoBehaviour {

    public Text txtConnect;

    public Text txtLogMsg;

    private PhotonView pv;

	void Awake () {

        pv = GetComponent<PhotonView>();
        CreateTank();
        PhotonNetwork.isMessageQueueRunning = true;

        GetConnectPlayerCount();
	}

    IEnumerator Start()
    {
        string msg = "\n<color=#00ff00> [" + PhotonNetwork.player.name + "] Connected</color>";
        pv.RPC("LogMsg", PhotonTargets.AllBuffered,msg);

        yield return new WaitForSeconds(2.0f);

        SetConnectPlayerScore();
    }


    void SetConnectPlayerScore()
    {
        PhotonPlayer[] players = PhotonNetwork.playerList;

        foreach ( PhotonPlayer _player in players)
        {
            Debug.Log("[" + _player.ID + "]" + _player.name + " " + _player.GetScore() + "kill");

        }

        GameObject[] tanks = GameObject.FindGameObjectsWithTag("TANK");

        foreach ( GameObject tank in tanks)
        {
            int currKillCount = tank.GetComponent<PhotonView>().owner.GetScore();
            tank.GetComponent<TankDamage>().txtKillCount.text = currKillCount.ToString();
        }

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

    [PunRPC]
    void LogMsg(string msg)
    {
        txtLogMsg.text = txtLogMsg.text + msg;
    }

    public void OnClickExitRoom()
    {

        string msg = "\n<color=#ff0000> [" + PhotonNetwork.player.name + "] disconnected</color>";
        pv.RPC("LogMsg", PhotonTargets.AllBuffered, msg);
        PhotonNetwork.LeaveRoom();
    }

    void OnLeftRoom()
    {
        Application.LoadLevel("scLobby");
    }
}
