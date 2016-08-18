using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// each talkable is responsable on knowing where on the dialogue he is.
// when the user presses the space key, the dialogue will advance;
public class Talkable : MonoBehaviour {
    public List<Dialogue> Chat;
    public int IndexOfCurrentDialogue = -1;

    private ProximitySensor _sensor;
    private UiDialogueController _uiDialogue;

    void Start () {
        _sensor = GetComponent<ProximitySensor>();
        _uiDialogue = GameObject.FindGameObjectWithTag(Tag.UiDialogueController)
            .GetComponent<UiDialogueController>();
    }
    
    void Update () {
        if (!IsDone()) {
            _uiDialogue.EnabledTalkable = this;
        } else {
            IndexOfCurrentDialogue = -1;
        }
        if (_sensor.IsActive() && Input.GetKeyDown(KeyCode.Space)) {
            Next();
        }
    }

    public bool IsDone () {
        return !_sensor.IsActive() || IndexOfCurrentDialogue < 0;
    }

    public void Next () {
        IndexOfCurrentDialogue++;
        if (IndexOfCurrentDialogue >= Chat.Count) {
            IndexOfCurrentDialogue = -1;
        }
    }

    public Dialogue GetCurrentDialogue () {
        return Chat[IndexOfCurrentDialogue];
    }
}
