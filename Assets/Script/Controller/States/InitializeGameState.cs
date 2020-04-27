using UnityEngine;


public class InitializeGameState : GameState {
    public override void Enter() {
        base.Enter();
        m_Owner.Dealer = new Dealer();
        Dealer.Shuffle();
        this.m_Owner.ChangeState<IdleState>();
    }
}