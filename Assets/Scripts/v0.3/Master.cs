using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour {
    public GameObject buttonContainer;
    Vector3 mousePosition;
    ButtonContainer buttonScript;
    MeshGenerator mesh;
    Text input;
    WebRequest API;
    string textInput, lastWord, currentWord;
    public double capitalizeDelay = 1.5;

    public Texture2D cursorTexture;

    void Start () {
        API = this.GetComponent<WebRequest>();
        API.SendReset();
        mesh = this.GetComponent<MeshGenerator>();
        input = this.GetComponent<Text>();
        mesh.SetSize((int)(Screen.height * 0.3));
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(mesh.size - 30, 100);
        buttonScript = buttonContainer.GetComponent<ButtonContainer>();
        mesh.SetAngles(0, 360);

        CursorMode cursorMode = CursorMode.Auto;
        //Vector2 hotSpot = new Vector2(cursorTexture.width/2, cursorTexture.height/2);
        Vector2 hotSpot = new Vector2(4, 4);
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
	
	void Update () {

        //Tracking Mouse Position
        mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        if ((mousePosition - new Vector3(Screen.width/2,Screen.height/2,0)).magnitude > buttonContainer.GetComponent<ButtonContainer>().size) {
            buttonContainer.SetActive(false);
            buttonScript.tempSelect = "";
        }
    }
    
    private void OnMouseEnter() {
        buttonContainer.SetActive(true);
        if (buttonScript.tempSelect != "") {
            switch (buttonScript.tempSelect) {
                case "/toggle":
                    buttonScript.Toggle();
                    break;
                case "/space":
                    // On space, add current word+" " to input.text and reset currentWord/update last word
                    textInput = textInput + lastWord;
                    API.SendNext(currentWord);
                    lastWord = currentWord + " ";
                    currentWord = "";
                    break;
                case "/backspace":
                    if (currentWord.Length > 0) {
                        currentWord = currentWord.Remove(currentWord.Length - 1);
                    }
                    else if (lastWord.Length > 0) {
                        currentWord = lastWord;
                        lastWord = textInput.Split(' ')[textInput.Split(' ').Length - 1];
                        currentWord = currentWord.Remove(currentWord.Length - 1);
                    }
                    else if (textInput.Length > 0) {
                        textInput = textInput.Remove(input.text.Length - 1);
                    }
                    break;
                default:
                    // input.text = base + currentWord
                    if (buttonScript.tempSelect.Length > 1) {
                        textInput = textInput + lastWord;
                        API.SendNext(buttonScript.tempSelect);
                        lastWord = buttonScript.tempSelect + " ";
                        currentWord = "";
                    }
                    else {
                        if (buttonScript.delay > capitalizeDelay) buttonScript.tempSelect = buttonScript.tempSelect.ToUpper();
                        currentWord += buttonScript.tempSelect;
                        API.SendRequest(buttonScript.tempSelection);
                    }
                    break;
            }
            input.text = textInput + lastWord + currentWord;
            buttonScript.tempSelect = "";
        }
    }
}
