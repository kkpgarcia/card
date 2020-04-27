using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DealState : GameState {
    public override void Enter() {
        m_Owner.UIController.SetResultText("");
        Card[] cards = m_Owner.Players[0].Clear(true);
        cards = cards.Concat(m_Owner.Players[1].Clear(true)).ToArray();
        cards = cards.Concat(m_Owner.DealerTable.Clear(true)).ToArray();    
    
        foreach(Card c in cards) {
            if(c == null)
                break;
            Dealer.Add(new Card(c.Number));
        }

        foreach(PlayerTable p in m_Owner.Players) {
            p.SetHand("");
        }
        
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart() {
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.Players[0].DistributeCard(0, new Card(2));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.Players[0].DistributeCard(1, new Card(2));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.OnHand.Add(new List<Card>() {
        //     new Card(2),
        //     new Card(2)
        // });
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.Players[1].DistributeCard(0, new Card(2));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.Players[1].DistributeCard(1, new Card(2));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.OnHand.Add(new List<Card>() {
        //     new Card(2),
        //     new Card(2)
        // });
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.DealerTable.DistributeCard(0, new Card(16));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.DealerTable.DistributeCard(1, new Card(16));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.DealerTable.DistributeCard(2, new Card(16));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.DealerTable.DistributeCard(3, new Card(27));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.DealerTable.DistributeCard(4, new Card(28));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.OnTable.Add(new Card(16));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.OnTable.Add(new Card(16));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.OnTable.Add(new Card(16));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.OnTable.Add(new Card(27));
        // yield return new WaitForSeconds(0.15f);
        // m_Owner.OnTable.Add(new Card(28));

        for(int j = 0; j < m_Owner.Players.Length; j++) {
            List<Card> onHand = new List<Card>();
            for(int i = 0; i < 2; i++) {
                yield return new WaitForSeconds(0.15f);
                PlayerTable p = m_Owner.Players[j];
                Card card = Dealer.Deal();
                onHand.Add(card);
                p.DistributeCard(i, card);
            }
            m_Owner.OnHand.Add(onHand);
        }

        for(int i = 0; i < 5; i++) {
            yield return new WaitForSeconds(0.15f);
            Card card = Dealer.Deal();
            m_Owner.DealerTable.DistributeCard(i, card);
            m_Owner.OnTable.Add(card);
        }

        m_Owner.ChangeState<EvaluateState>();
    }
}