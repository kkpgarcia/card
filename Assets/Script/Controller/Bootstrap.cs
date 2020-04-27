using System.Collections.Generic;

public class Bootstrap : StateMachine {
    public Dealer Dealer;
    public PlayerTable[] Players;
    public DealerTable DealerTable;
    public List<Card> OnTable = new List<Card>();
    public List<List<Card>> OnHand = new List<List<Card>>();
    public UIController UIController;

    public void Awake() {
        this.ChangeState<InitializeGameState>();
    }
}