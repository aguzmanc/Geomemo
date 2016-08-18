using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// the ui dialogue controller will only ensure that the current talkable
// is correctly displayed on the dialogue canvas.
public class UiDialogueController : MonoBehaviour {
    private GameObject _uiDialogueContainer;
    private Image _portrait;
    private Text _text;

    public Talkable EnabledTalkable;

    void Start () {
        _uiDialogueContainer = transform.FindChild("Dialogue").gameObject;
        _portrait = _uiDialogueContainer.gameObject.transform.FindChild("Portrait").gameObject
            .GetComponent<Image>();
        _text = _uiDialogueContainer.transform.FindChild("Text").gameObject
            .GetComponent<Text>();
    }

    void Update () {
        _keepEnabledTalkableCoherent();

        if (null == EnabledTalkable) {
            _uiDialogueContainer.SetActive(false);
        } else {
            _uiDialogueContainer.SetActive(true);
            _updateDialogueContainer();
        }
    }

    private void _updateDialogueContainer () {
        Dialogue currentDialogue = EnabledTalkable.GetCurrentDialogue();
        _portrait.sprite = EnabledTalkable.GetCurrentDialogue().EmotionShown.GetFace();
        _text.text = currentDialogue.Text;
    }

    private void _keepEnabledTalkableCoherent () {
        if (null != EnabledTalkable) {
            if (EnabledTalkable.IsDone()) {
                EnabledTalkable = null;
            }
        }
    }
}
