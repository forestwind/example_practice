using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class PlayerCtrl : MonoBehaviour {

    public enum AnimState { idle = 0, runForward, runBackward, runRight, runLeft }

    public AnimState animState = AnimState.idle;

    public AnimationClip[] animClips;

    private CharacterController controller;

    private Animation anim;
    

    private Transform tr;
    private NetworkView _networkView;

    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;

    public GameObject bullet;
    public Transform firePos;

    private bool isDie = false;
    private int hp = 100;
    private float respawnTime = 3.0f;

    void Awake()
    {
        tr = GetComponent<Transform>();
        _networkView = GetComponent<NetworkView>();
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animation>();

        if (_networkView.isMine)
        {
            Camera.main.GetComponent<SmoothFollow>().target = tr;
        }
    }

    void Update()
    {
        if (_networkView.isMine) 
        {
            if(Input.GetMouseButtonDown(0))
            {

                if (isDie) return;

                //로컬 함수 발사
                Fire();

                //원격 사용자 RPC호출
                _networkView.RPC("Fire", RPCMode.Others);
            }

            Vector3 localVelocity = tr.InverseTransformDirection(controller.velocity);
            Vector3 forwardDir = new Vector3(0f, 0f, localVelocity.z);
            Vector3 rightDir = new Vector3(localVelocity.x, 0f, 0f);

            if(forwardDir.z >= 0.1f)
            {
                animState = AnimState.runForward;
            }
            else if (forwardDir.z <= -0.1f)
            {
                animState = AnimState.runBackward;
            }
            else if (rightDir.x >= 0.1f)
            {
                animState = AnimState.runRight;
            }
            else if (rightDir.x <= -0.1f)
            {
                animState = AnimState.runLeft;
            }
            else
            {
                animState = AnimState.idle;
            }

            anim.CrossFade(animClips[(int)animState].name, 0.2f);

        }
        else // 원격 플레이어
        {

            if (Vector3.Distance(tr.position, currPos) >= 2.0f)
            {
                tr.position = currPos;
                tr.rotation = currRot;
            }
            else
            {
                tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);
                tr.rotation = Quaternion.Slerp(tr.rotation, currRot, Time.deltaTime * 10.0f);  
            }

            anim.CrossFade(animClips[(int)animState].name, 0.1f);
        }

    }

    [RPC]
    void Fire()
    {
        GameObject.Instantiate(bullet, firePos.position, firePos.rotation);
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        //로컬 플레이어
        if (stream.isWriting) 
        {
            Vector3 pos = tr.position;
            Quaternion rot = tr.rotation;
            int _animState = (int)animState;

            stream.Serialize(ref pos);
            stream.Serialize(ref rot);
            stream.Serialize(ref _animState);
        }
        else // 원격 플레이어
        {
            Vector3 revPos = Vector3.zero;
            Quaternion revRot = Quaternion.identity;
            int _animState = 0;

            stream.Serialize(ref revPos);
            stream.Serialize(ref revRot);
            stream.Serialize(ref _animState);

            currPos = revPos;
            currRot = revRot;
            animState = (AnimState)_animState;
        }
    }


    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "BULLET")
        {
            Destroy(coll.gameObject);

            hp -= 20;

            if( hp <= 0)
            {
                StartCoroutine(this.RespawnPlayer(respawnTime));
            }

        }
    }

    IEnumerator RespawnPlayer(float waitTime)
    {
        isDie = true;

        StartCoroutine(this.PlayerVisible(false, 0.0f));

        yield return new WaitForSeconds(waitTime);

        tr.position = new Vector3(Random.Range(-20.0f, 20.0f), 0.0f, Random.Range(-20.0f, 20.0f));

        hp = 100;

        isDie = false;

        StartCoroutine(this.PlayerVisible(true, 0.5f));

    }

    IEnumerator PlayerVisible(bool visibled, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        GetComponentInChildren<SkinnedMeshRenderer>().enabled = visibled;

        GetComponentInChildren<MeshRenderer>().enabled = visibled;


        if(_networkView.isMine)
        {
            GetComponent<MoveCtrl>().enabled = visibled;
            controller.enabled = visibled;
        }


    }
}
