using UnityEngine;
using System.Collections;

public class TouchMove : MonoBehaviour {

    private Vector3 target = Vector3.zero;

    private NavMeshAgent nvAgent;

	// Use this for initialization
	void Start () {
        nvAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red);

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity,1<<LayerMask.NameToLayer("FLOOR")))
            {
                target = hit.point;

                nvAgent.SetDestination(target);
            }
        }
    }
}
