using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class PlayerTable : MonoBehaviour {
    public CardObject[] CardReference;
    public Text HandText;
    private Vector3[] CardPosition = new Vector3[2];

    public void Awake() {
        CardPosition[0] = CardReference[0].transform.position;
        CardPosition[1] = CardReference[1].transform.position;
    
        Clear(false);
    }

    public void DistributeCard(int index, Card card) {
        CardReference[index].Show(card, CardPosition[index]);
    }

    public void SetHand(string msg) {
        HandText.text = "Hand Rank: " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(msg.Replace("_", " ").ToLower());
    }

    public Card[] Clear(bool animated) {
        Card[] onHand = new Card[2];
        int i = 0;
        foreach(CardObject c in CardReference) {
            if(c == null)
                break;
            onHand[i] = c.Card;
            c.Hide(animated);
            i++;
        }

        return onHand;
    }
}