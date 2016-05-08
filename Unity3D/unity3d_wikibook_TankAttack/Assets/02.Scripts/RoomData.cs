using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomData : MonoBehaviour {

    [HideInInspector]
    public string roomName = "";
    [HideInInspector]
    public int connectPlayer = 0;
    [HideInInspector]
    public int maxPlayers = 0;

    public Text textRoomName;
    public Text textConnectInfo;

    //룸 정보를 전달한 후 Text UI 항목에 표시하는 함수
    public void DispRoomData()
    {
        textRoomName.text = roomName;
        textConnectInfo.text = "(" + connectPlayer.ToString() + "/" + maxPlayers.ToString() + ")";
    }
}
