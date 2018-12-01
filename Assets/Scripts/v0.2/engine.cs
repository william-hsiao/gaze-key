using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class engine : MonoBehaviour {
    public string[] currentLbls = { "a", "b", "c", "d", "e", "f", "g" };
    public buttonMaster btnMaster;
    public navButtonMaster navBtnMaster;
    public centreMaster cntrMaster;
    public borderHitbox border;
    public Text inputStr;
    private string lastSelected = "";

    // Use this for initialization
    void Awake () {
        btnMaster = this.GetComponentInChildren<buttonMaster>();
    }
    void Start () {
        btnMaster.SetBtnLabels(currentLbls);
        inputStr.text = "";
    }



    void enterCentre () {
        SetButtonsActive(true);
        if (lastSelected != "") {
            inputStr.text += lastSelected;
            lastSelected = "";
        }
    }

    void borderTrigger () {
        SetButtonsActive(false);
        lastSelected = "";
    }

    void SetButtonsActive (bool status) {
        btnMaster.SetBtnActive(status);
        navBtnMaster.SetBtnActive(status);
        border.SetBtnActive(status);
    }




    void ButtonTriggerOn(string inputLbl) {
        print(inputLbl);
        lastSelected = inputLbl;
    }

    void EnterTrigger () {
        print("Enter");
    }

    void BackspaceTrigger () {
        if (inputStr.text.Length > 0) {
            inputStr.text = inputStr.text.Remove(inputStr.text.Length - 1);
        }
    }
}
