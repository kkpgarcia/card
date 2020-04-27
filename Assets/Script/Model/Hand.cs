using System.Linq;

public class Hand {
    public Size Size;
    public Card[] Cards;
    public int[] Indices;

    public Hand(Size size, Card[] cards, int[] indices) {
        this.Size = size;
        this.Cards = cards;
        this.Indices = indices != null ? indices.OrderBy(x => x).ToArray() : null;
    }

    public int GetLastIndex() {
        return Indices[Indices.Length - 1];
    }

    public Card GetHighestCard() {
        return Cards[GetLastIndex()];
    }
}