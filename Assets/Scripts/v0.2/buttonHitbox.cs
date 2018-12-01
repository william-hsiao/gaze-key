using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonHitbox : EventTrigger {
    private buttonValue btnVal;

    void Start() {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        btnVal = this.GetComponent<buttonValue>();
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        SendMessageUpwards("ButtonTriggerOn", this.btnVal.label.text);
    }

    public override void OnPointerExit(PointerEventData eventData) {

    }
}
