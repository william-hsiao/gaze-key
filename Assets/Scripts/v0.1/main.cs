using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class main : MonoBehaviour {

    GameObject mainButton;
    Button_Hitbox[] buttons;
    //Button_Hitbox centre;
    Text[] labels;
    Text message;
    //bool active = false, inputActive = false;
    string[] letterPalette = { "a", "b", "c", "d", "e", "f", "g", "h" };
    string lastActive;
    int i;

	// Use this for initialization
	void Start () {
        buttons = new Button_Hitbox[this.gameObject.transform.childCount-1];
        mainButton = this.gameObject.transform.GetChild(this.gameObject.transform.childCount - 2).gameObject;
        message = this.gameObject.transform.GetChild(this.gameObject.transform.childCount - 1).GetComponentInChildren<Text>();

        for (i=0; i<=this.gameObject.transform.childCount-2; i++) {
            buttons[i] = this.gameObject.transform.GetChild(i+1).GetComponentInChildren<Button_Hitbox>();
        }
        //centre = mainButton.GetComponentInChildren<Button_Hitbox>();

        labels = new Text[mainButton.transform.childCount];
        for (i = 0; i < mainButton.transform.childCount; i++) {
            labels[i] = mainButton.transform.GetChild(i).GetComponentInChildren<Text>();
        }
        updateLabels();

    }

    void buttonTriggerOn (string button) {
        if (button == "Main" && lastActive != "Main" && lastActive != "") {
            int buttonIndex = (int)char.GetNumericValue(lastActive[lastActive.Length - 1]);
            //Debug.Log(letterPalette[buttonIndex]);
            message.text += letterPalette[buttonIndex];
        }
        lastActive = button;
    }

    void updateLabels () {
        for (i = 0; i < mainButton.transform.childCount; i++) {
            labels[i].text = letterPalette[i];
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
