using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word {

    public string word;
    public int frequency;
    public ArrayList nextWord; //arraylist of Word

    public static Word CreateFromJSON(string jsonString) {
        return JsonUtility.FromJson<Word>(jsonString);
    }
}