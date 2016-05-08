using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {

	
	void Awake () {

        CreateTank();
        PhotonNetwork.isMessageQueueRunning = true;
	}

    void CreateTank()
    {
        float pos = Random.Range(-100.0f, 100.0f);
        PhotonNetwork.Instantiate("Tank", new Vector3(pos, 20.0f, pos), Quaternion.identity, 0);
    }
}
