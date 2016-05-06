using UnityEngine;
using System.Collections;

public class BulletFire : MonoBehaviour {

    private float lifeTime = 5.0f;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = transform.forward * 10.0f;

        Destroy(gameObject, lifeTime);
	}

}
