using UnityEngine;
using System.Collections;

public class MoveCtrl : MonoBehaviour {

    private Transform tr;
    private CharacterController controller;

    private float h = 0.0f;
    private float v = 0.0f;

    public float movSpeed = 5.0f;
    public float rotSpeed = 50.0f;

    private Vector3 movDir = Vector3.zero;

	// Use this for initialization
	void Start () {

        this.enabled = GetComponent<NetworkView>().isMine;

        tr = GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        tr.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime);

        movDir = (tr.forward * v) + (tr.right * h);

        movDir.y -= 20f * Time.deltaTime;

        controller.Move(movDir * movSpeed * Time.deltaTime);
	}
}
