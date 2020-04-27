using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public static class Evaluator {
    public static Hand isRoyalFlush(Card[] hand) {
        Card[] sorted = hand.OrderBy(x => x.Rank).ToArray();
        
        bool straightCheck = sorted[2].Rank == 9 &&
                    sorted[3].Rank == 10 &&
                    sorted[4].Rank == 11 &&
                    sorted[5].Rank == 12 &&
                    sorted[6].Rank == 13;

        bool flushCheck = isFlush(hand) != null;

        Hand result = straightCheck && flushCheck ? new Hand(Size.ROYAL_FLUSH, sorted, new int[] {2, 3, 4, 5, 6}) : null;
        return result;
    }
    public static Hand isFlush(Card[] hand) {
        Card[] sorted = hand.OrderBy(x => x.Number).ToArray();
        
        // x x x x x y z
        if(sorted[0].Suit == sorted[4].Suit)
            return new Hand(Size.FLUSH, sorted, new int[] {0, 1, 2, 3, 4});

        // y x x x x x z
        if(sorted[1].Suit == sorted[5].Suit)
            return new Hand(Size.FLUSH, sorted, new int[] {1, 2, 3, 4, 5});
        

        // y z x x x x x 
        if(sorted[2].Suit == sorted[6].Suit)
            return new Hand(Size.FLUSH, sorted, new int[] {2, 3, 4, 5, 6});


        return null;
    }

    public static Hand isStraight(Card[] hand) {
        Card[] sorted = hand.OrderBy(x => x.Rank).ToArray();
        int[] indices = new int[5];
        if(sorted[6].Rank == 14) {
            //With Ace

            //a b b b b x y
            bool firstCheck = sorted[0].Rank == 2 &&
                            sorted[1].Rank == 3 &&
                            sorted[2].Rank == 4 &&
                            sorted[3].Rank == 5;

            //x y b b b b a
            bool secondCheck = sorted[2].Rank == 10 &&
                            sorted[3].Rank == 11 &&
                            sorted[4].Rank == 12 &&
                            sorted[5].Rank == 13;
            
            if(firstCheck)
                indices = new int[] { 0, 1, 2, 3, 6 };
            if(secondCheck)
                indices = new int[] { 2, 3, 4, 5, 6 };

            Hand result = firstCheck || secondCheck ? new Hand(Size.STRAIGHT, sorted, indices) : null;
            return result;
        } else {
            int checker = sorted[0].Rank;
            List<int> ind = new List<int>();
            for(int i = 0; i < 7; i++) {
                if(sorted[i].Rank != checker)
                    return null;
                
                ind.Add(i);
                checker++;
            }

            return new Hand(Size.STRAIGHT, sorted, ind.ToArray());
        }
    }

    public static Hand isStraightFlush(Card[] hand) {
        Hand flush = isFlush(hand);
        Hand straight = isStraight(hand);
        return flush != null && straight != null ? new Hand(Size.STRAIGHT_FLUSH, straight.Cards, straight.Indices) : null;
    }

    public static Hand isFours(Card[] hand) {
        Card[] sorted = hand.OrderBy(x => x.Rank).ToArray();
        int[] indices = new int [4];
        // x x x x a b c
        bool firstCheck = sorted[0].Rank == sorted[1].Rank &&
                            sorted[1].Rank == sorted[2].Rank &&
                            sorted[2].Rank == sorted[3].Rank;

        // a x x x x b c
        bool secondCheck = sorted[1].Rank == sorted[2].Rank &&
                            sorted[2].Rank == sorted[3].Rank &&
                            sorted[3].Rank == sorted[4].Rank;
        // a b x x x x c
        bool thirdCheck = sorted[2].Rank == sorted[3].Rank &&
                            sorted[3].Rank == sorted[4].Rank &&
                            sorted[4].Rank == sorted[5].Rank;
        // a b c x x x x 
        bool fourthCheck = sorted[3].Rank == sorted[4].Rank &&
                            sorted[4].Rank == sorted[5].Rank &&
                            sorted[5].Rank == sorted[6].Rank;

        if(firstCheck)
            indices = new int[] { 0, 1, 2, 3 };
        if(secondCheck)
            indices = new int[] { 1, 2, 3, 4 };
        if(thirdCheck)
            indices = new int[] { 2, 3, 4, 5 };
        if(fourthCheck)
            indices = new int[] { 3, 4, 5, 6 };

        Hand result = firstCheck || secondCheck || thirdCheck || fourthCheck ? 
                        new Hand(Size.FOUR_OF_A_KIND, sorted, indices) : null;
        return result;
    }

    public static Hand isFullHouse(Card[] hand) {
        Card[] sorted = hand.OrderBy(x => x.Rank).ToArray();
        int[] indices = new int[5];
        // x x y y y a b
        bool firstCheck = sorted[0].Rank == sorted[1].Rank &&
                            sorted[2].Rank == sorted[3].Rank &&
                            sorted[3].Rank == sorted[4].Rank;
        // a x x y y y b
        bool secondCheck = sorted[1].Rank == sorted[2].Rank &&
                            sorted[3].Rank == sorted[4].Rank &&
                            sorted[4].Rank == sorted[5].Rank;
        // a b x x y y y
        bool thirdCheck = sorted[2].Rank == sorted[3].Rank &&
                            sorted[4].Rank == sorted[5].Rank &&
                            sorted[5].Rank == sorted[6].Rank;
        // a x x b y y y
        bool fourthCheck = sorted[1].Rank == sorted[2].Rank &&
                            sorted[4].Rank == sorted[5].Rank &&
                            sorted[5].Rank == sorted[6].Rank;
        // x x a b y y y
        bool fifthCheck = sorted[0].Rank == sorted[1].Rank &&
                            sorted[4].Rank == sorted[5].Rank &&
                            sorted[5].Rank == sorted[6].Rank;
        // y y y x x a b
        bool sixthCheck = sorted[0].Rank == sorted[1].Rank &&
                            sorted[1].Rank == sorted[2].Rank &&
                            sorted[3].Rank == sorted[4].Rank;
        // a y y y x x b
        bool seventhCheck = sorted[1].Rank == sorted[2].Rank &&
                            sorted[2].Rank == sorted[3].Rank &&
                            sorted[4].Rank == sorted[5].Rank;
        // a b y y y x x
        bool eightCheck = sorted[2].Rank == sorted[3].Rank &&
                            sorted[3].Rank == sorted[4].Rank &&
                            sorted[5].Rank == sorted[6].Rank;
        // a y y y b x x
        bool ninethCheck = sorted[1].Rank == sorted[2].Rank &&
                            sorted[2].Rank == sorted[3].Rank &&
                            sorted[5].Rank == sorted[6].Rank;
        // y y y a b x x
        bool tenthCheck = sorted[0].Rank == sorted[1].Rank &&
                            sorted[1].Rank == sorted[2].Rank &&
                            sorted[5].Rank == sorted[6].Rank;
        // y y y a x x b
        bool eleventhCheck = sorted[0].Rank == sorted[1].Rank &&
                            sorted[1].Rank == sorted[2].Rank &&
                            sorted[4].Rank == sorted[5].Rank;

        if(firstCheck)
            indices = new int[] { 0, 1, 2, 3, 4 };
        if(secondCheck || sixthCheck || seventhCheck)
            indices = new int[] { 1, 2, 3, 4, 5 };
        if(thirdCheck)
            indices = new int[] { 2, 3, 4, 5, 6 };
        if(fourthCheck)
            indices = new int[] { 1, 2, 4, 5, 6 };
        if(fifthCheck)
            indices = new int[] { 1, 2, 4, 5, 6 };
        if(eightCheck)
            indices = new int[] { 2, 3, 4, 5, 6 };
        if(ninethCheck)
            indices = new int[] { 1, 2, 3, 5, 6 };
        if(tenthCheck)
            indices = new int[] { 0, 1, 2, 5, 6};
        if(eleventhCheck)
            indices = new int[] { 0, 1, 2, 4, 5 };

        Hand result = firstCheck || secondCheck || thirdCheck || fourthCheck ||
                        fifthCheck || sixthCheck || seventhCheck || eightCheck ||
                        ninethCheck || tenthCheck || eleventhCheck ? 
                        new Hand(Size.FULL_HOUSE, sorted, indices) : null;
        return result;
    }

    public static Hand isThrees(Card[] hand) {
        if(isFours(hand) != null)// || isFullHouse(hand) != null)
            return null;

        Card[] sorted = hand.OrderBy(x => x.Rank).ToArray();
        int[] indices = new int[3];

        //x x x a b c d
        bool firstCheck = sorted[0].Rank == sorted[1].Rank &&
                            sorted[1].Rank == sorted[2].Rank;

        //a x x x b c d
        bool secondCheck = sorted[1].Rank == sorted[2].Rank &&
                            sorted[2].Rank == sorted[3].Rank;

        // a b x x x c d
        bool thirdCheck = sorted[2].Rank == sorted[3].Rank &&
                            sorted[3].Rank == sorted[4].Rank;
        // a b c x x x d
        bool fourthCheck = sorted[3].Rank == sorted[4].Rank &&
                            sorted[4].Rank == sorted[5].Rank;
        // a b c d x x x
        bool fifthCheck = sorted[4].Rank == sorted[5].Rank &&
                            sorted[5].Rank == sorted[6].Rank;

        if(firstCheck)
            indices = new int[] { 0, 1, 2 };
        if(secondCheck)
            indices = new int[] { 1, 2, 3 };
        if(thirdCheck)
            indices = new int[] { 2, 3, 4 };
        if(fourthCheck)
            indices = new int[] { 3, 4, 5 };
        if(fifthCheck)
            indices = new int[] { 4, 5, 6 };


        Hand result = firstCheck || secondCheck || thirdCheck || fourthCheck ||
                        fifthCheck ? 
                        new Hand(Size.THREE_OF_A_KIND, sorted, indices) : null;
        return result;
    }

    public static Hand isTwoPairs(Card[] hand) {
        if(isFours(hand) != null || isFullHouse(hand) != null || isThrees(hand) != null)
            return null;

        Card[] sorted = hand.OrderBy(x => x.Rank).ToArray();
        int[] indices = new int[4];

        // x x y y a b c
        bool firstCheck = sorted[0].Rank == sorted[1].Rank &&
                            sorted[2].Rank == sorted[3].Rank;
        //x x a y y b c
        bool secondCheck = sorted[0].Rank == sorted[1].Rank &&
                            sorted[3].Rank == sorted[4].Rank;
        //x x a b y y c
        bool eightCheck = sorted[0].Rank == sorted[1].Rank &&
                            sorted[4].Rank == sorted[5].Rank;
        //x x a b c y y
        bool ninethCheck = sorted[0].Rank == sorted[1].Rank &&
                            sorted[5].Rank == sorted[6].Rank;
        //a x x b c y y
        bool tenthCheck = sorted[1].Rank == sorted[2].Rank &&
                            sorted[5].Rank == sorted[6].Rank;
        //a x x y y b c
        bool thirdCheck = sorted[1].Rank == sorted[2].Rank &&
                            sorted[3].Rank == sorted[4].Rank;
        //a x x b y y c
        bool fourthCheck = sorted[1].Rank == sorted[2].Rank &&
                            sorted[4].Rank == sorted[5].Rank;
        //a b x x y y c
        bool fifthCheck = sorted[2].Rank == sorted[3].Rank &&
                            sorted[4].Rank == sorted[5].Rank;
        //a b x x c y y
        bool sixthCheck = sorted[2].Rank == sorted[3].Rank &&
                            sorted[5].Rank == sorted[6].Rank;
        //a b c x x y y
        bool seventhCheck = sorted[3].Rank == sorted[4].Rank &&
                            sorted[5].Rank == sorted[6].Rank;

        if(firstCheck)
            indices = new int[] { 0, 1, 2, 3 };
        if(secondCheck)
            indices = new int[] { 0, 1, 3, 4 };
        if(eightCheck)
            indices = new int[] { 0, 1, 4, 5 };
        if(ninethCheck)
            indices = new int[] { 0, 1, 5, 6 };
        if(tenthCheck)
            indices = new int[] { 1, 2, 5, 6 };
        if(thirdCheck)
            indices = new int[] { 1, 2, 3, 4 };
        if(fourthCheck)
            indices = new int[] { 1, 2, 4, 5 };
        if(fifthCheck)
            indices = new int[] { 2, 3, 4, 5 };
        if(sixthCheck)
            indices = new int[] { 2, 3, 5, 6 };
        if(seventhCheck)
            indices = new int[] { 3, 4, 5, 6 };

        Hand result = firstCheck || secondCheck || thirdCheck || fourthCheck ||
                        fifthCheck || sixthCheck || seventhCheck || eightCheck ||
                        ninethCheck || tenthCheck ? 
                        new Hand(Size.TWO_PAIR, sorted, indices) : null;
        return result;
    }

    public static Hand isPairs(Card[] hand) {
        if(isFours(hand) != null || isFullHouse(hand) != null || isThrees(hand) != null || isTwoPairs(hand) != null)
            return null;

        Card[] sorted = hand.OrderBy(x => x.Rank).ToArray();
        int[] indices = new int[2];
        //x x y z a b c
        bool firstCheck = sorted[0].Rank == sorted[1].Rank;
        //y x x z a b c
        bool secondCheck = sorted[1].Rank == sorted[2].Rank;
        //y z x x a b c
        bool thirdCheck = sorted[2].Rank == sorted[3].Rank;
        //y z a x x b c
        bool fourthCheck = sorted[3].Rank == sorted[4].Rank;
        //y z a  b x x c
        bool fifthCheck = sorted[4].Rank == sorted[5].Rank;
        //y z a b c x x
        bool sixthCheck = sorted[5].Rank == sorted[6].Rank;

        if(firstCheck)
            indices = new int[] { 0, 1 };
        if(secondCheck)
            indices = new int[] { 1, 2 };
        if(thirdCheck)
            indices = new int[] { 2, 3 };
        if(fourthCheck)
            indices = new int[] { 3, 4 };
        if(fifthCheck)
            indices = new int[] { 4, 5 };
        if(sixthCheck)
            indices = new int[] { 5, 6 };


        Hand result = firstCheck || secondCheck || thirdCheck || fourthCheck ||
                        fifthCheck || sixthCheck ? 
                        new Hand(Size.ONE_PAIR, sorted, indices) : null;
        return result;
    }
}