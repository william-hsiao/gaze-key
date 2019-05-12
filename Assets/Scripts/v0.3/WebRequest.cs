using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour {
    //public Button submit;
    public Master master;
    public ButtonContainer buttonContainer;

    //void Start() {
    //    submit = this.gameObject.GetComponent<Button>();
    //    submit.onClick.AddListener(SendRequest);
    //}

    public void SendNext(string lastWord) {
        StartCoroutine(SendHttpNext(lastWord));
    }
    public void SendRequest(WordCandidate[] inputs) {
        StartCoroutine(SendHttp(inputs));
    }

    IEnumerator SendHttpNext(string lastWord) {
        string url = "localhost:3000/next";
        inputData data = new inputData(new WordCandidate[1] {new WordCandidate(lastWord, 0)});
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
            inputResult tmp = JsonUtility.FromJson<inputResult>(response);
            buttonContainer.newSet(tmp.suggestions);
            master.wordCandidates.clear();
        }
    }

    IEnumerator SendHttp(WordCandidate[] inputs) {
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
            inputResult tmp = JsonUtility.FromJson<inputResult>(response);
            buttonContainer.newSet(tmp.suggestions);
            master.wordCandidates.updateList(tmp.inputs);
        }

    }

    public class inputData {
        public WordCandidate[] inputs;
        public inputData(WordCandidate[] inputs) {
            this.inputs = inputs;
        }
    }

    [System.Serializable]
    public class inputResult {
        public WordCandidate[] inputs;
        public string[] suggestions;


    }

    public class nextData {
        public string lastWord;
        public nextData(string lastWord) {
            this.lastWord = lastWord;
        }
    }
}

        // response = response.Substring(1, response.Length - 1);
        // string[] responseArray;
        // responseArray = response.Split(',');
        // Debug.Log(responseArray[0]);
        // for (int i = 0; i < responseArray.Length; i++) {
        //     if (i == responseArray.Length - 1) responseArray[i] = responseArray[i].Substring(1, responseArray[i].Length - 3);
        //     else responseArray[i] = responseArray[i].Substring(1, responseArray[i].Length-2);
        // }
        // //Debug.Log(responseArray[0]);

        
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
