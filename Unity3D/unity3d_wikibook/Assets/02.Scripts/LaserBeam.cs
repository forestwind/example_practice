using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {

    private Transform tr;
    private LineRenderer line;
    private RaycastHit hit;

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
        line = GetComponent<LineRenderer>();

        line.useWorldSpace = false;
        line.enabled = false;
        line.SetWidth(0.3f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(tr.position + (Vector3.up * 0.02f), tr.forward);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);

        if(Input.GetMouseButtonDown(0))
        {
            line.SetPosition(0, tr.InverseTransformPoint(ray.origin));

            if(Physics.Raycast(ray,out hit,100.0f))
            {
                line.SetPosition(1, tr.InverseTransformPoint(hit.point));
            }
            else
            {
                line.SetPosition(1, tr.InverseTransformPoint(ray.GetPoint(100.0f)));
            }

            StartCoroutine(this.ShowLaserBeam());
        }
	}


    IEnumerator ShowLaserBeam()
    {
        line.enabled = true;
        yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
        line.enabled = false;
    }
}
