using UnityEngine;
using System.Collections;

public class FireCannon : MonoBehaviour {

    private GameObject cannon = null;

    private AudioClip fireSfx = null;

    private AudioSource sfx = null;

    public Transform firePos;

    private PhotonView pv = null;

	void Awake ()
    {
        cannon = (GameObject)Resources.Load("Cannon");

        fireSfx = Resources.Load<AudioClip>("CannonFire");

        sfx = GetComponent<AudioSource>();

        pv = GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {

        if (MouseHover.instance.isUIHover) return;

        if( pv.isMine && Input.GetMouseButtonDown(0))
        {
            Fire();
            pv.RPC("Fire", PhotonTargets.Others, null);
        }
	}
    
    [PunRPC]
    void Fire()
    {
        sfx.PlayOneShot(fireSfx, 1.0f);
        GameObject _cannon =  (GameObject) Instantiate(cannon, firePos.position, firePos.rotation);
        _cannon.GetComponent<Cannon>().playerId = pv.ownerId;
    }
}
