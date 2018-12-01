using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Hitbox : EventTrigger {

	// Use this for initialization
	void Start () {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        SendMessageUpwards("buttonTriggerOn", this.name);
    }

    public override void OnPointerExit(PointerEventData eventData) {
    }


    // Update is called once per frame
    void Update () {
		
	}
}
