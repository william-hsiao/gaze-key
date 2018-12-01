using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class navSpace : EventTrigger {

    void Start() {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        this.GetComponentInChildren<Text>().text = "Space";
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        SendMessageUpwards("ButtonTriggerOn", " ");
    }

    public override void OnPointerExit(PointerEventData eventData) {

    }
}
