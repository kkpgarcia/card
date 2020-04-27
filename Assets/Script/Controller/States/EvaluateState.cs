using UnityEngine;
using System.Linq;

public class EvaluateState : GameState {
    public override void Enter() {
        Hand[] result = new Hand[m_Owner.Players.Length];
        for(int i = 0; i < m_Owner.Players.Length; i++) {
            Card[] hand = m_Owner.OnHand[i].Concat(m_Owner.OnTable).ToArray();
            if((result[i] = Evaluator.isRoyalFlush(hand)) != null)
                continue;
            else if ((result[i] = Evaluator.isStraightFlush(hand)) != null)
                continue;
            else if((result[i] = Evaluator.isFours(hand)) != null)
                continue;
            else if ((result[i] = Evaluator.isFlush(hand)) != null)
                continue;
            else if ((result[i] = Evaluator.isFullHouse(hand)) != null)
                continue;
            else if ((result[i] = Evaluator.isStraight(hand)) != null)
                continue;
            else if ((result[i] = Evaluator.isThrees(hand)) != null)
                continue;
            else if ((result[i] = Evaluator.isTwoPairs(hand)) != null)
                continue;
            else if ((result[i] = Evaluator.isPairs(hand)) != null)
                continue;
            else
                result[i] = new Hand(Size.HIGH_CARD, hand, null);
        }

        if(result[0].Size == result[1].Size) {
            Card highCardOne = m_Owner.OnHand[0].OrderBy(x => x.Rank).Last();;
            Card highCardTwo = m_Owner.OnHand[1].OrderBy(x => x.Rank).Last();;

            switch(result[0].Size) {
                case Size.ROYAL_FLUSH:
                    m_Owner.UIController.SetResultText("Draw!");
                    break;
                case Size.STRAIGHT_FLUSH:
                case Size.FOUR_OF_A_KIND:
                case Size.FLUSH:
                case Size.STRAIGHT:
                case Size.THREE_OF_A_KIND:
                case Size.TWO_PAIR:
                case Size.ONE_PAIR:
                    highCardOne = result[0].GetHighestCard();
                    highCardTwo = result[1].GetHighestCard();
                    break;
                case Size.FULL_HOUSE:
                    Hand highResultOne = Evaluator.isThrees(result[0].Cards);
                    Hand highReusltTwo = Evaluator.isThrees(result[1].Cards);
                    highCardOne = highResultOne.GetHighestCard();
                    highCardTwo = highReusltTwo.GetHighestCard();
                    break;
                default:
                    highCardOne = m_Owner.OnHand[0].OrderBy(x => x.Rank).Last();
                    highCardTwo = m_Owner.OnHand[1].OrderBy(x => x.Rank).Last();
                    break;
            }

            if(highCardOne.Rank > highCardTwo.Rank) {
                m_Owner.UIController.SetResultText("Player One Wins!");
            } else if(highCardOne.Rank < highCardTwo.Rank) {
                m_Owner.UIController.SetResultText("Player Two Wins!");
            } else {
                m_Owner.UIController.SetResultText("Draw!");
            }

        } else if(result[0].Size < result[1].Size) {
            m_Owner.UIController.SetResultText("Player One Wins!!");
        } else {
            m_Owner.UIController.SetResultText("Player Two Wins!!");
        }

        m_Owner.Players[0].SetHand(result[0].Size.ToString());
        m_Owner.Players[1].SetHand(result[1].Size.ToString());

        Clean();
    }

    private void Clean() {
        m_Owner.OnHand.Clear();
        m_Owner.OnTable.Clear();

        m_Owner.ChangeState<IdleState>();
    }
}