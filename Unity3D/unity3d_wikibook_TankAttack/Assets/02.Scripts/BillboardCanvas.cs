using UnityEngine;
using System.Collections;

public class BillboardCanvas : MonoBehaviour {

    private Transform tr;
    private Transform mainCameraTr;

	// Use this for initialization
	void Start () {

        tr = GetComponent<Transform>();
        mainCameraTr = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        tr.LookAt(mainCameraTr);
	}
}
