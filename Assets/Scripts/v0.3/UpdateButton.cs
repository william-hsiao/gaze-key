using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateButton : MonoBehaviour {
    public Button button;
    public ButtonContainer buttonContainer;
    public InputField input;

    void Start () {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick() {
        string[] temp = input.text.Split(',');
        for (int i=0; i<temp.Length; i++) {
            print(temp[i]);
        }
        buttonContainer.newSet(temp);
    }
}