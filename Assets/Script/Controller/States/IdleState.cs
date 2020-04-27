using UnityEngine;
public class IdleState : GameState {

    // public override void Enter() {
    //     this.m_Owner.ChangeState<DealState>();
    // }
    
    protected override void AddListeners() {
        this.AddObserver(OnPlayGame, "PlayGame");
    }

    protected override void RemoveListeners() {
        this.RemoveObserver(OnPlayGame, "PlayGame");
    }

    private void OnPlayGame(object sender, object args) {
        this.m_Owner.ChangeState<DealState>();
    }
}