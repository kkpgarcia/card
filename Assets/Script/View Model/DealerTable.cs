using UnityEngine;

public class DealerTable : MonoBehaviour {
    public CardObject[] CardReference;

    private Vector3[] CardPosition = new Vector3[5];

    public void Awake() {
        CardPosition[0] = CardReference[0].transform.position;
        CardPosition[1] = CardReference[1].transform.position;
        CardPosition[2] = CardReference[2].transform.position;
        CardPosition[3] = CardReference[3].transform.position;
        CardPosition[4] = CardReference[4].transform.position;
    
        Clear(false);
    }

    public void DistributeCard(int index, Card card) {
        CardReference[index].Show(card, CardPosition[index]);
    }

    public Card[] Clear(bool animated) {
        Card[] onHand = new Card[5];
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