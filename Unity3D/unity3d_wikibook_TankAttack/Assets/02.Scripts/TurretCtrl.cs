using UnityEngine;
using System.Collections;

public class TurretCtrl : MonoBehaviour {

    private Transform tr;
    private RaycastHit hit;

    public float rotSpeed = 5.0f;

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red);

        if( Physics.Raycast(ray, out hit , Mathf.Infinity, 1<<8))
        {
            Vector3 relative = tr.InverseTransformPoint(hit.point);

            //역탄젠트 함수로 두점간의 각도 계산
            // 라디언 단위에 Rad2Deg를 곱해 각도로 변경
            float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
            tr.Rotate(0, angle * Time.deltaTime * rotSpeed, 0);
        }
	}
}
