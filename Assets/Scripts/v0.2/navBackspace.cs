using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class navBackspace : EventTrigger {

    void Start() {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        this.GetComponentInChildren<Text>().text = "Backspace";
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        SendMessageUpwards("BackspaceTrigger");
    }

    public override void OnPointerExit(PointerEventData eventData) {

    }
}
