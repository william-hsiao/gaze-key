using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonValue : MonoBehaviour {
    public int buttonVal;
    public Text label;

	// Use this for initialization
	void Awake () {
        label = this.GetComponentInChildren<Text>();
	}
	
    public void SetLabel (string lbl) {
        //print(this.name);
        label.text = lbl;
    }

	// Update is called once per frame
	void Update () {

	}
}
