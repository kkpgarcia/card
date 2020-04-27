using UnityEngine;

public class CardObject : MonoBehaviour {
    public Card Card;
    public SpriteRenderer Renderer;

    public void Show(Card card, Vector3 t) {
        Tweener tweener = this.transform.MoveTo(t);
        tweener.duration = 0.5f;
        tweener.equation = EasingEquations.EaseInOutQuad;
    
        this.Card = card;
        int requestId = card.Rank - 2 + (((int)card.Suit) * 13);
        this.Renderer.sprite = SpriteFactory.Instance.CardRequest(requestId);
    }

    public void Hide(bool animated) {
        Tweener tweener = this.transform.MoveTo(this.transform.position + Vector3.right * 10);
        tweener.duration = animated ? 0.5f : 0.0f;
        tweener.equation = EasingEquations.EaseInOutQuad;
    }
}