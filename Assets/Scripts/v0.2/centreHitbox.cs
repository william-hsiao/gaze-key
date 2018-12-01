using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class centreHitbox : EventTrigger {

    void Start() {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        SendMessageUpwards("enterCentre");
    }

    public override void OnPointerExit(PointerEventData eventData) {
    }
}
