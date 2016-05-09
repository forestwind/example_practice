using UnityEngine;
using System.Collections;
using SimpleJSON;

public class DataMgr : MonoBehaviour {

    public static DataMgr instance = null;

    private const string seqNo = "11201605101";

    private string urlSave = "http://www.unity3dstudy.com/Tankwar/save_score.php";

    private string urlScoreList = "http://www.unity3dstudy.com/Tankwar/get_score_list";

    void Awake () {
        instance = this;
	}
	
	public IEnumerator SaveScore( string user_name, int killCount)
    {
        WWWForm form = new WWWForm();

        form.AddField("user_name", user_name);
        form.AddField("kill_count", killCount);
        form.AddField("seq_no", seqNo);

        var www = new WWW(urlSave, form);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);
        }
        else
        {
            Debug.Log("Error : "+www.error);
        }

        StartCoroutine(this.GetScoreList());
    }


    IEnumerator GetScoreList()
    {
        WWWForm form = new WWWForm();
        form.AddField("seq_no", seqNo);

        var www = new WWW(urlScoreList, form);

        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);

            DispScoreList(www.text);
        }
        else
        {
            Debug.Log("Error : " + www.error);
        }
    }


    void DispScoreList(string strJsonData)
    {
        var N = JSON.Parse(strJsonData);

        for (int i=0; i< N.Count; ++i)
        {
            int ranking = N[i]["ranking"].AsInt;
            string userName = N[i]["user_name"].ToString();
            int killCount = N[i]["kill_count"].AsInt;

            Debug.Log(ranking.ToString() + userName + killCount.ToString());
        }
    }
}
