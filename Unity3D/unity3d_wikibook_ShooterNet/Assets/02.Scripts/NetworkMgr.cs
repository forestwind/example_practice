using UnityEngine;
using System.Collections;

public class NetworkMgr : MonoBehaviour {

    private const string ip = "127.0.0.1";
    private const int port = 30000;
    private bool _useNat = false;

    public GameObject player;
	
	void OnGUI ()
    {
	    if(Network.peerType == NetworkPeerType.Disconnected)
        {
            if(GUI.Button(new Rect(20,20,200,25), "Start Server"))
            {
                Network.InitializeServer(20, port, _useNat);
            }

            if (GUI.Button(new Rect(20, 50, 200, 25), "Connect to Server"))
            {
                Network.Connect(ip, port);
            }

        }
        else
        {
            if (Network.peerType == NetworkPeerType.Server)
            {
                GUI.Label(new Rect(20, 20, 200, 25), "Initialization Server...");
                GUI.Label(new Rect(20, 50, 200, 25), "Client Count = " + Network.connections.Length.ToString());
            }

            if (Network.peerType == NetworkPeerType.Client)
            {
                GUI.Label(new Rect(20, 20, 200, 25), "Connected to Server");
            }
        }

	}

    void OnServerInitialized()
    {
       CreatePlayer();
    }
	
    void OnConnectedToServer()
    {
        CreatePlayer();
    }

    void CreatePlayer()
    {
        Vector3 pos = new Vector3(Random.Range(-20.0f, 20.0f), 0, Random.Range(-20.0f, 20.0f));

        Network.Instantiate(player, pos, Quaternion.identity, 0 );
    }

    void OnPlayerDisconnected(NetworkPlayer netplayer)
    {
        Network.RemoveRPCs(netplayer);
        Network.DestroyPlayerObjects(netplayer);
    }
}
