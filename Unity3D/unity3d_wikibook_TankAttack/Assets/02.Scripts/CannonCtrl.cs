using UnityEngine;
using System.Collections;

public class CannonCtrl : MonoBehaviour {

    private Transform tr;
    public float rotSpeed = 500.0f;

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        float angle = -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * rotSpeed;
        tr.Rotate(angle, 0, 0);
	}
}
