using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhotonInit : MonoBehaviour {

    public string version = "V1.0";

    public InputField userId;

    public InputField roomName;

    public GameObject scrollContents;

    public GameObject roomItem;

    void Awake()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(version);
        }

        roomName.text = "Room_" + Random.Range(0, 999).ToString("000");
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

    public void OnClickCreateRoom()
    {
        string _roomName = roomName.text;
       
        if( string.IsNullOrEmpty(roomName.text))
        {
            _roomName = "Room_" + Random.Range(0, 999).ToString("000");
        }

        PhotonNetwork.player.name = userId.text;

        PlayerPrefs.SetString("USER_ID", userId.text);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.isOpen = true;
        roomOptions.isVisible = true;
        roomOptions.maxPlayers = 20;


        PhotonNetwork.CreateRoom(_roomName, roomOptions, TypedLobby.Default);
    }

    //룸 생성 실패할 때 호출되는 콜백 함수
    void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("Create Room Failed =" + codeAndMsg[1]);
    }

  // 생성된 룸 목록이 변경됬을 때 호출되는 콜백 함수
    void OnReceivedRoomListUpdate()
    {
        foreach( GameObject obj in GameObject.FindGameObjectsWithTag("ROOM_ITEM"))
        {
            Destroy(obj);
        }

        int rowCount = 0;
        //스크롤 영역 초기화
        scrollContents.GetComponent<RectTransform>().sizeDelta = Vector2.zero;


        foreach (RoomInfo _room in PhotonNetwork.GetRoomList())
        {
            Debug.Log(_room.name);

            GameObject room = (GameObject)Instantiate(roomItem);

            room.transform.SetParent(scrollContents.transform, false);

            RoomData roomData = room.GetComponent<RoomData>();
            roomData.roomName = _room.name;
            roomData.connectPlayer = _room.playerCount;
            roomData.maxPlayers = _room.maxPlayers;

            roomData.DispRoomData();

            roomData.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { OnClickRoomItem(roomData.roomName); });

            scrollContents.GetComponent<GridLayoutGroup>().constraintCount = ++rowCount;
            scrollContents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 20);
        }
    }

    void OnClickRoomItem(string roomName)
    {
        PhotonNetwork.player.name = userId.text;
        PlayerPrefs.SetString("USER_ID", userId.text);

        PhotonNetwork.JoinRoom(roomName);
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
