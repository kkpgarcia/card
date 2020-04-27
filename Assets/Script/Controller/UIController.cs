using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Button PlayButton;
    public Text ResultText;

    private void Awake() {
        PlayButton.onClick.AddListener(() => {
            this.PostNotification("PlayGame");
        });
    }

    public void SetResultText(string message) {
        ResultText.text = message;
    }
}