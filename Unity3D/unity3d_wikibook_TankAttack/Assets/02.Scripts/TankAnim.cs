using UnityEngine;
using System.Collections;

public class TankAnim : MonoBehaviour {

    private float scrollSpeed = 1.0f;
    private Renderer _renderer;
	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        var offset = Time.time * scrollSpeed * Input.GetAxisRaw("Vertical");

        _renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        _renderer.material.SetTextureOffset("_BumpMap", new Vector2(0, offset));

    }
}
