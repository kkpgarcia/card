public class GameState : State {
    protected Bootstrap m_Owner;
    public Dealer Dealer { get { return m_Owner.Dealer; } }

    public void Awake() {
        this.m_Owner = this.GetComponent<Bootstrap>();
    }
}