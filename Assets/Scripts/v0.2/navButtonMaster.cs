using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class navButtonMaster : MonoBehaviour {
    public Button[] btns;
    private GameObject btnMaster;

    // Use this for initialization
    void Awake () {
        btns = this.GetComponentsInChildren<Button>();
        btnMaster = this.gameObject;
    }
    void Start () {
        SetBtnActive(false);
    }

    public void SetBtnActive (bool status) {
        btnMaster.SetActive(status);
    }

    // Update is called once per frame
    void Update () {

    }
}
