using UnityEngine;
using System.Collections;

public class FireCannon : MonoBehaviour {

    private GameObject cannon = null;

    private AudioClip fireSfx = null;

    private AudioSource sfx = null;

    public Transform firePos;

	void Awake ()
    {
        cannon = (GameObject)Resources.Load("Cannon");

        fireSfx = Resources.Load<AudioClip>("CannonFire");

        sfx = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
	}
    
    void Fire()
    {
        sfx.PlayOneShot(fireSfx, 1.0f);
        Instantiate(cannon, firePos.position, firePos.rotation);
    }
}
