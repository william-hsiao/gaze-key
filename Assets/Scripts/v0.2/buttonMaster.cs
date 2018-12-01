using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonMaster : MonoBehaviour {
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
	
    public void SetBtnLabels (string[] newLabels) {
        //print("-- SetBtnLabels --");
        int i;
        for (i=0; i<btns.Length; i++) {
            btns[i].GetComponent<buttonValue>().SetLabel(newLabels[i]);
        }
        //print("-- Finished --");
    }

    public void SetBtnActive (bool status) {
        btnMaster.SetActive(status);
    }

    // Update is called once per frame
    void Update () {

    }
}
