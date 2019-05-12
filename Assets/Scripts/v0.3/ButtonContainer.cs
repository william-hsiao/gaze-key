using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonContainer : MonoBehaviour {
    public Master master;
    public GameObject button;
    public int size, n;
    static int maxButtons = 18;
    GameObject[] buttons = new GameObject[maxButtons];
    MeshGenerator[] buttonMesh = new MeshGenerator[maxButtons];
    GameObject[] buttonText = new GameObject[maxButtons];
    ButtonEvents[] buttonEvents = new ButtonEvents[maxButtons];
    Text[] buttonLabel = new Text[maxButtons];

    GameObject[] specButtons = new GameObject[3];
    MeshGenerator[] specMesh = new MeshGenerator[3];
    ButtonEvents[] specEvents = new ButtonEvents[3];
    GameObject[] specText = new GameObject[3];
    Text[] specLabel = new Text[3];
    String[] specNames = { "Toggle", "Space", "Backspace" };
    String[] specValues = { "/toggle", "/space", "/backspace" };

    public string tempSelect;
    public string[] tempSelection;
    string[][] letters = { new string[] { }, new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" }, new string[] { "j", "k", "l", "m", "n", "o", "p", "q", "r" }, new string[] { "s", "t", "u", "v", "w", "x", "y", "z" } };
    //string[][] letters = { new string[] { }, new string[] { "e", "t", "a", "o", "i", "n", "s", "r", "h" }, new string[] { "d", "l", "u", "c", "m", "f", "y", "w", "g" }, new string[] { "p", "b", "v", "k", "x", "q", "j", "z" } };

    public float delay;

    //Initialize =============================================================
    void Start() {
        int i;
        tempSelect = "";
        size = (int)(Screen.height * 0.45);

        initiateButtons(buttons, buttonMesh, buttonEvents, buttonText, buttonLabel);
        initiateButtons(specButtons, specMesh, specEvents, specText, specLabel);

        SetSpecials();
        for (i = 0; i < 3; i++) {
            specButtons[i].name = specNames[i];
            specLabel[i].text = specNames[i];
        }

        for (i = 0; i < maxButtons; i++) {
            buttons[i].name = "button" + i;
        }        
        SetButtons(1);
    }

    void Update() {
        delay += Time.deltaTime;
    }

    //EVENTS ===============================================================
    void Selection (string value) {
        tempSelect = value;

        if (!(value == "/toggle" || value == "/space" || value == "/backspace")) {
            List<string> selection = new List<string>();
            int index = Array.IndexOf(letters[n], value);
            selection.Add(letters[n][index]);
            if (index > 0) selection.Add(letters[n][index - 1]);
            if (index < letters[n].Length - 1) selection.Add(letters[n][index + 1]);

            tempSelection = selection.ToArray();
        }
        delay = 0;
    }

    public void Toggle () {
        n = (n + 1) % letters.Length;
        if (letters[n].Length == 0) n++;
        SetButtons(n);
    }


    //DRAW & SETTERS ========================================================
    public void initiateButtons(GameObject[] btn, MeshGenerator[] btnMesh, ButtonEvents[] btnEvents, GameObject[] btnText, Text[] btnLabel) {
        int i;
        for (i = 0; i < btn.Length; i++) {
            btn[i] = Instantiate(button);
            btn[i].transform.parent = this.gameObject.transform;
            btn[i].transform.localScale = new Vector3(1, 1, 1);
            btn[i].transform.localPosition = new Vector3(0, 0, 150);
            btnEvents[i] = btn[i].AddComponent<ButtonEvents>();
            btnMesh[i] = btn[i].GetComponent<MeshGenerator>();
            btnMesh[i].size = size;

            btnText[i] = new GameObject();
            btnText[i].transform.parent = btn[i].transform;
            btnLabel[i] = btnText[i].AddComponent<Text>();
            btnLabel[i].font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            btnLabel[i].alignment = TextAnchor.MiddleCenter;
            btnLabel[i].horizontalOverflow = HorizontalWrapMode.Overflow;
            btnLabel[i].verticalOverflow = VerticalWrapMode.Overflow;
        }
    }

    public void SetSpecials() {
        double[] startAngle = { 230, 250, 290 };
        double[] sectorAngle = { 20, 40, 20 };
        int i;
        for (i = 0; i < 3; i++) {
            specMesh[i].SetAngles(startAngle[i], sectorAngle[i] - 0.5);
            specText[i].transform.localPosition = new Vector3((float)(size * 0.55 * Math.Cos((startAngle[i]+sectorAngle[i]/2) * Math.PI / 180)), (float)(size * 0.55 * Math.Sin((startAngle[i] + sectorAngle[i] / 2) * Math.PI / 180)), -320);
            specLabel[i].text = specValues[i];
            specEvents[i].btnValue = specValues[i];
            specButtons[i].SetActive(true);
            specMesh[i].Refresh();
        }
    }

    public void SetButtons (int index) {
        int i;
        //double angle = 360 / values.Length;
        string[] values = letters[index];
        double angle = 280 / values.Length;
        n = index;
        int offset = 50;
        for (i=0; i<maxButtons; i++) {
            if (i < values.Length) {
                buttonMesh[i].SetAngles(angle * i - offset, angle - 0.5);
                buttonText[i].transform.localPosition = new Vector3((float)(size * 0.55 * Math.Cos((angle * (i + 0.5) - offset) * Math.PI / 180)), (float)(size * 0.55 * Math.Sin((angle * (i + 0.5) - offset) * Math.PI / 180)), -320);
                buttonLabel[i].text = values[values.Length - i - 1];
                buttonEvents[i].btnValue = values[values.Length - i - 1];
                buttons[i].SetActive(true);
            }
            else {
                buttons[i].SetActive(false);
            }
        }
        Refresh();
    }

    public void newSet (string[] list) {
        letters[0] = list;
        SetButtons(0);
    }

    void Refresh () {
        int i;
        for (i = 0; i<maxButtons; i++) {
            buttonMesh[i].Refresh();
        }
    }
}