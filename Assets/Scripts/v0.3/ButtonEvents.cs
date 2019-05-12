using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : MonoBehaviour {
    MeshRenderer meshRenderer;
    MeshGenerator mesh;
    public string btnValue;

    private void Start() {
        mesh = this.gameObject.GetComponent<MeshGenerator>();
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
    }
    void OnMouseEnter() {
        meshRenderer.material.color = mesh.highlight;
        SendMessageUpwards("Selection", btnValue);
    }

    void OnMouseExit() {
        meshRenderer.material.color = mesh.baseColour;
    }
}
