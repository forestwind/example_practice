using UnityEngine;
using System.Collections;

public class RagdollCtrl : MonoBehaviour {

    public Rigidbody[] rbody;
    private Animation anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        rbody = GetComponentsInChildren<Rigidbody>();

        SetRagdoll(false);

        StartCoroutine(this.WakeupRagdoll());
	}
	
    void SetRagdoll( bool isEnable )
    {
        foreach (Rigidbody _rbody in rbody)
        {
            _rbody.isKinematic = !isEnable;
        }
    }

    IEnumerator WakeupRagdoll()
    {
        yield return new WaitForSeconds(3.0f);

        anim.Stop();

        SetRagdoll(true);
    }
}
