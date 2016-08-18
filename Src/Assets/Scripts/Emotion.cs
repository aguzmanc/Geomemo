using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Emotion : MonoBehaviour {
    public List<Sprite> Faces;
    public int Fps = 5;

    public Sprite GetFace () {
        return Faces[(int) Mathf.Floor((Fps * Time.time) % Faces.Count)];
    } 
}
