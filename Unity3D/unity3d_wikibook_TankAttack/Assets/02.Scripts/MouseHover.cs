using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseHover : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler {

    public static MouseHover instance = null;

    public bool isUIHover = false;

	void Awake () {
        instance = this;
	}
	
	public void OnPointerEnter (PointerEventData eventData)
    {
        isUIHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isUIHover = false;
    }
}
