public class Card {
    public int Rank { get; private set; }
    public Suit Suit { get; private set; }
    public int Number;

    public Card(int number) {
        this.Number = number;
        this.Suit = (Suit)(number / 13);
        this.Rank = (number % 13) + 2;
    }

    public override string ToString() {
        string rank = Rank.ToString();
        if(Rank > 10) {
            if(Rank == 11)
                rank = "Jack";
            else if(Rank == 12)
                rank = "Queen";
            else if(Rank == 13)
                rank = "King";
        }

        if(Rank == 14) {
            rank = "Ace";
        }
        return string.Format("{0} of {1}", rank, Suit);
    }
}