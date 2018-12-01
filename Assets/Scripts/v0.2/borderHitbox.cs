using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class borderHitbox : EventTrigger {

    void Start() {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        this.gameObject.SetActive(false);
    }

    public void SetBtnActive(bool status) {
        this.gameObject.SetActive(status);
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        SendMessageUpwards("borderTrigger");
    }

    public override void OnPointerExit(PointerEventData eventData) {

    }
}
