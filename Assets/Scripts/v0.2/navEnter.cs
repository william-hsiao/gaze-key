using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class navEnter : EventTrigger {

    void Start() {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        this.GetComponentInChildren<Text>().text = "Enter";
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        SendMessageUpwards("EnterTrigger");
    }

    public override void OnPointerExit(PointerEventData eventData) {

    }
}
