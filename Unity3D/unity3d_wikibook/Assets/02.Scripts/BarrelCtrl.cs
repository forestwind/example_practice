using UnityEngine;
using System.Collections;

public class BarrelCtrl : MonoBehaviour {

    public GameObject expEffect;

    public Texture[] textures;

    private Transform tr;

    private int hitCount = 0;
	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();

        int idx = Random.Range(0, textures.Length);
        GetComponentInChildren<MeshRenderer>().material.mainTexture = textures[idx];
	}
	
	void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == "BULLET")
        {
            Destroy(coll.gameObject);

            if(++hitCount >= 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        Instantiate(expEffect, tr.position, Quaternion.identity);

        Collider[] colls = Physics.OverlapSphere(tr.position, 10.0f);


        foreach(Collider coll in colls)
        {
            Rigidbody rbody = coll.GetComponent<Rigidbody>();
            if( rbody != null)
            {
                rbody.mass = 1.0f;
                rbody.AddExplosionForce(1000.0f, tr.position, 10.0f, 300.0f);
            }
        }

        Destroy(this.gameObject, 5.0f);
    }

    void OnDamage(object[] _params)
    {
        Vector3 firePos = (Vector3)_params[0];
        Vector3 hitPos = (Vector3)_params[1];

        Vector3 incomeVector = hitPos - firePos;

        incomeVector = incomeVector.normalized;

        GetComponent<Rigidbody>().AddForceAtPosition(incomeVector * 1000f, hitPos);

        if(++hitCount >= 3)
        {
            ExpBarrel();
        } 
    }
}
