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
    List<string> textInput = new List<string>();
    string wordCache;
    public WordCandidateList wordCandidates = new WordCandidateList();
    public double capitalizeDelay = 1.5;

    public Texture2D cursorTexture;

    void Start () {
        API = this.GetComponent<WebRequest>();
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

        textInput.Add(string.Empty);
    }
	
	void Update () {

        //Tracking Mouse Position
        mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        if ((mousePosition - new Vector3(Screen.width/2,Screen.height/2,0)).magnitude > buttonContainer.GetComponent<ButtonContainer>().size) {
            buttonContainer.SetActive(false);
            buttonScript.tempSelect = "";
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            input.text = string.Empty;
            textInput.Clear();
            textInput.Add(string.Empty);
            wordCandidates.clear();
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
                    wordCache = textInput[textInput.Count - 1].Clone().ToString();
                    API.SendNext(textInput[textInput.Count - 1]);
                    textInput.Add(string.Empty);
                    break;
                case "/backspace":
                    if (textInput[textInput.Count - 1].Length > 0) {
                        if (buttonScript.delay > capitalizeDelay) textInput[textInput.Count - 1] = string.Empty;
                        else textInput[textInput.Count - 1] = textInput[textInput.Count - 1].Remove(textInput[textInput.Count - 1].Length - 1);
                        wordCandidates.backspace();
                        API.SendRequest(wordCandidates.getList());
                    } else {
                        if (textInput.Count > 1) textInput.RemoveAt(textInput.Count - 1);
                        textInput[textInput.Count - 1] = wordCache.Clone().ToString();
                        wordCandidates.input(new string[] { textInput[textInput.Count - 1] });
                        API.SendRequest(wordCandidates.getList());
                    }
                    break;
                default:
                    if (buttonScript.tempSelect.Length > 1) {
                        wordCache = textInput[textInput.Count - 1].Clone().ToString();
                        textInput[textInput.Count - 1] = buttonScript.tempSelect;
                        textInput.Add(string.Empty);
                        API.SendNext(buttonScript.tempSelect);
                    } else {
                        if (buttonScript.delay > capitalizeDelay) buttonScript.tempSelect = buttonScript.tempSelect.ToUpper();
                        textInput[textInput.Count - 1] += buttonScript.tempSelect;
                        wordCandidates.input(buttonScript.tempSelection);
                        API.SendRequest(wordCandidates.getList());
                    }
                    break;
            }
            input.text = string.Join(" ", textInput.ToArray());
            buttonScript.tempSelect = "";
        }
    }
}
