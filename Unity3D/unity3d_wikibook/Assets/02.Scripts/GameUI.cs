using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {

    public Text txtScore;

    private int totScore = 0;

	// Use this for initialization
	void Start () {
        totScore = PlayerPrefs.GetInt("ToT_SCORE", 0);
        DispScore(0);
	}
	
	public void DispScore(int score)
    {
        totScore += score;
        txtScore.text = "score <color=#ff0000>" + totScore.ToString() + "</color>";

        PlayerPrefs.SetInt("ToT_SCORE", totScore);
    }
}
