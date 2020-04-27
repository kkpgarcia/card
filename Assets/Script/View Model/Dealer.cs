using UnityEngine;
using System.Collections.Generic;

public class Dealer
{
    private Queue<Card> m_Deck;
    private const int MAX_CARDS = 52;

    public Dealer() {
        m_Deck = new Queue<Card>(MAX_CARDS);
        for (int i = 0; i < MAX_CARDS; i++)
            m_Deck.Enqueue(new Card(i));
    }

    public int Count() {
        return m_Deck.Count;
    }

    public void Shuffle() {
        m_Deck = new Queue<Card>(m_Deck.Shuffle<Card>());
    }

    public void Add(Card card) {
        m_Deck.Enqueue(card);
    }

    public Card Deal() {
        if(m_Deck.Peek() == null)
            return null;

        //Debug.Log(m_Deck.Peek().ToString() + " # " + m_Deck.Peek().Number + " Rank: " + m_Deck.Peek().Rank);

        return m_Deck.Dequeue();
    }
}