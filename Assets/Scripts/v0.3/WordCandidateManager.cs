using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WordCandidateList {
    public List<WordCandidate> candidates = new List<WordCandidate>();
    public int charCount = 0;
    
    public void input(string[] textVal) {
        this.charCount++;
        List<WordCandidate> tempList = new List<WordCandidate>();
        foreach(string text in textVal) {
            foreach(WordCandidate candidate in this.candidates.ToArray()) {
                if (candidate.word.Length < this.charCount - 2) this.candidates.Remove(candidate);

                WordCandidate temp = new WordCandidate(candidate.word, candidate.deviation);
                temp.word += text;
                tempList.Add(temp);
            }
            if (this.candidates.Count == 0 && this.charCount < 2) tempList.Add(new WordCandidate(text, 0));
            if (text != textVal[0]) tempList[tempList.Count - 1].deviation++;
        }
        this.candidates.AddRange(tempList);
    }

    public void backspace() {
        if (this.charCount > 1) this.charCount--;
        foreach(WordCandidate candidate in this.candidates.ToArray()) {
            WordCandidate temp = new WordCandidate(candidate.word, candidate.deviation);
            temp.word.Remove(temp.word.Length - 1);
            this.candidates.Add(temp);
            candidate.deviation++;
        }
    }

    public WordCandidate[] getList() {
        return this.candidates.ToArray();
    }

    public void updateList(WordCandidate[] list) {
        this.candidates.Clear();
        foreach(WordCandidate item in list) {
            this.candidates.Add(item);
        }
    }

    public void clear() {
        this.candidates.Clear();
        this.charCount = 0;
    }
}

[System.Serializable]
public class WordCandidate {

    public string word;
    public int deviation;
    
    public WordCandidate(string word, int deviation) {
        this.word = word;
        this.deviation = deviation;
    }
}