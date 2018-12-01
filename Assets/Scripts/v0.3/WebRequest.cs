using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour {
    //public Button submit;
    public ButtonContainer buttonContainer;

    //void Start() {
    //    submit = this.gameObject.GetComponent<Button>();
    //    submit.onClick.AddListener(SendRequest);
    //}

    public void SendReset() {
        StartCoroutine(SendHttpReset());
    }
    public void SendNext(string lastWord) {
        StartCoroutine(SendHttpNext(lastWord));
    }
    public void SendRequest(string[] inputs) {
        StartCoroutine(SendHttp(inputs));
    }

    IEnumerator SendHttpReset() {
        string url = "localhost:3000/reset";

        UnityWebRequest www = UnityWebRequest.Post(url, "");
        www.SetRequestHeader("Content-Type", "application/json");
        www.method = "POST";
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
    }

    IEnumerator SendHttpNext(string lastWord) {
        string url = "localhost:3000/next";
        nextData data = new nextData(lastWord);
        string nextJSON = JsonUtility.ToJson(data);

        UnityWebRequest www = UnityWebRequest.Put(url, nextJSON);
        www.SetRequestHeader("Content-Type", "application/json");
        www.method = "POST";
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            string response = www.downloadHandler.text;
            response = response.Substring(1, response.Length - 1);
            string[] responseArray;
            responseArray = response.Split(',');
            Debug.Log(responseArray[0]);
            for (int i = 0; i < responseArray.Length; i++) {
                if (i == responseArray.Length - 1) responseArray[i] = responseArray[i].Substring(1, responseArray[i].Length - 3);
                else responseArray[i] = responseArray[i].Substring(1, responseArray[i].Length - 2);
            }
            //Debug.Log(responseArray[0]);
            buttonContainer.newSet(responseArray);
        }
    }

    IEnumerator SendHttp(string[] inputs) {
        string url = "localhost:3000/input";
        inputData data = new inputData(inputs);
        string inputJSON = JsonUtility.ToJson(data);

        UnityWebRequest www = UnityWebRequest.Put(url, inputJSON);
        www.SetRequestHeader("Content-Type", "application/json");
        www.method = "POST";
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            string response = www.downloadHandler.text;
            response = response.Substring(1, response.Length - 1);
            string[] responseArray;
            responseArray = response.Split(',');
            Debug.Log(responseArray[0]);
            for (int i = 0; i < responseArray.Length; i++) {
                if (i == responseArray.Length - 1) responseArray[i] = responseArray[i].Substring(1, responseArray[i].Length - 3);
                else responseArray[i] = responseArray[i].Substring(1, responseArray[i].Length-2);
            }
            //Debug.Log(responseArray[0]);
            buttonContainer.newSet(responseArray);
        }

    }
    public class inputData {
        public string[] inputs;
        public inputData(string[] inputs) {
            this.inputs = inputs;
        }
    }
    public class nextData {
        public string lastWord;
        public nextData(string lastWord) {
            this.lastWord = lastWord;
        }
    }
}
        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("inputs=foo"));

        //UnityWebRequest www = UnityWebRequest.Post(url, formData);
        ////www.SetRequestHeader("Content-Type", "application/json");
        //yield return www.SendWebRequest();

        //if (www.isNetworkError || www.isHttpError) {
        //    Debug.Log(www.error);
        //}
        //else {
        //    Debug.Log(www.downloadHandler.text);
        //}
