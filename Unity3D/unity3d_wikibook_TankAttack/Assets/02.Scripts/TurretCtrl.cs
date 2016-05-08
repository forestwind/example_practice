using UnityEngine;
using System.Collections;

public class TurretCtrl : MonoBehaviour {

    private Transform tr;
    private RaycastHit hit;

    public float rotSpeed = 5.0f;

    private PhotonView pv = null;
    private Quaternion currRot = Quaternion.identity;

	
	void Awake () {
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();

        pv.ObservedComponents[0] = this;
        pv.synchronization = ViewSynchronization.UnreliableOnChange;

        currRot = tr.localRotation;
	}
	
	// Update is called once per frame
	void Update () {

        if (pv.isMine)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
            {
                Vector3 relative = tr.InverseTransformPoint(hit.point);

                //역탄젠트 함수로 두점간의 각도 계산
                // 라디언 단위에 Rad2Deg를 곱해 각도로 변경
                float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
                tr.Rotate(0, angle * Time.deltaTime * rotSpeed, 0);
            }
        }
        else
        {
            tr.localRotation = Quaternion.Slerp(tr.localRotation, currRot, Time.deltaTime * 3.0f);
        }

	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(tr.localRotation);
        }
        else
        {
            currRot = (Quaternion)stream.ReceiveNext();
        }

    }


}
