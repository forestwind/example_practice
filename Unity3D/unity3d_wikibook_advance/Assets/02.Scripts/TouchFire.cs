using UnityEngine;
using System.Collections;

public class TouchFire : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);

        RaycastHit hit;

#if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
           if(Physics.Raycast(ray,out hit,100.0f))
            {
                if(hit.collider.tag == "EXP_BOX")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
#endif

#if UNITY_IPHONE

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            if(Physics.Raycast(ray,out hit,100.0f))
            {
                if(hit.collider.tag == "EXP_BOX")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }

#endif

    }
}
