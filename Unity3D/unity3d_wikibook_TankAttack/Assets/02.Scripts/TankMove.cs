using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class TankMove : MonoBehaviour {

    public float moveSpeed = 20.0f;
    public float rotSpeed = 50.0f;

    private Rigidbody rbody;
    private Transform tr;
    private float h, v;

    private PhotonView pv = null;

    public Transform camPivot;

    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;

	
	void Awake () {
        rbody = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();

        rbody.centerOfMass = new Vector3(0.0f, -0.5f, 0.0f);

        pv = GetComponent<PhotonView>();
        pv.synchronization = ViewSynchronization.UnreliableOnChange;
        pv.ObservedComponents[0] = this;

        if(pv.isMine)
        {
            Camera.main.GetComponent<SmoothFollow>().target = camPivot;
            rbody.centerOfMass = new Vector3(0.0f, -0.5f, 0.0f);

        }
        else
        {
            rbody.isKinematic = true;
        }

        currPos = tr.position;
        currRot = tr.rotation;
	}


    void OnPhotonSerializeView( PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();

        }

    }

	
	// Update is called once per frame
	void Update () {

        if (pv.isMine)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            tr.Rotate(Vector3.up * rotSpeed * h * Time.deltaTime);
            tr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        }
        else 
        {
            tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 3.0f);
            tr.rotation = Quaternion.Slerp(tr.rotation, currRot, Time.deltaTime * 3.0f);
        }

	}
}
