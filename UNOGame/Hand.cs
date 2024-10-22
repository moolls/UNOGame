
using System;
using System.Collections;
using System.Collections.Generic;

public class Hand
{
    private List<IUnoCard> hand;
    int indexCurrentCard;
   public int indexCurrentCardComputer;
    int drawCounter = 0;

    public Hand()
    {
        this.hand = new List<IUnoCard>();
        indexCurrentCard = 0;
    }

    public void GenerateHand(Deck deck)
    {
        for (int i = 0; i < 7; i++)
            hand.Add(deck.DrawCard());
    }

    public void AddCardToHand(IUnoCard unoCard)
    {
        hand.Add(unoCard);
    }



    public void ShowHand()
    {
        foreach (var card in hand)
        {
            card.DisplayCard();
        }
    }

    public void ShowAnonym()
    {
        foreach(IUnoCard card in hand)
        {
            Console.Write("[] ");
        }
    }
    public IEnumerable<IUnoCard> InfiniteIterator()
    {
        while (true)  // Infinite loop
        {
            yield return hand[indexCurrentCard];
            indexCurrentCard = (indexCurrentCard + 1) % hand.Count; // Circular navigation
        }
    }

    public IEnumerable<IUnoCard> InfiniteReverseIterator()
    {
        indexCurrentCard--;
        while (true)
        {
            yield return hand[indexCurrentCard];
            indexCurrentCard = (indexCurrentCard - 1 + hand.Count) % hand.Count; // Circular navigation backwards
        }
    }

    public IUnoCard IterateHand(Deck deck, out bool maxCardsDrawn)
    {
        IEnumerator<IUnoCard> forwardIterator = InfiniteIterator().GetEnumerator();
        IEnumerator<IUnoCard> backwardIterator = InfiniteReverseIterator().GetEnumerator();

        bool usingForward = true;
        IUnoCard currentItem = null;
        ConsoleKey key;
        
        maxCardsDrawn = false; // Flagga för att hålla reda på om tre kort dragits

        do
        {
            ShowHand(); // Visa nuvarande hand

            // Hämta aktuellt kort baserat på riktningen
            if (usingForward)
            {
                if (!forwardIterator.MoveNext())
                {
                    forwardIterator = InfiniteIterator().GetEnumerator(); // Återställ om vi når slutet
                    forwardIterator.MoveNext(); // Flytta till första kortet
                }
                currentItem = forwardIterator.Current;
            }
            else
            {
                if (!backwardIterator.MoveNext())
                {
                    backwardIterator = InfiniteReverseIterator().GetEnumerator(); // Återställ om vi når början
                    backwardIterator.MoveNext(); // Flytta till sista kortet
                }
                currentItem = backwardIterator.Current;
            }

            // Visa aktuellt kort och alternativen till spelaren
            Console.WriteLine($"Current item: {currentItem.GetColor()} {currentItem.GetValue()}");
            Console.WriteLine("Use Left/Right arrows to navigate, Enter to select item, or press D to draw a new card.");

            key = Console.ReadKey().Key;

            // Hantera navigering och kortdragning
            if (key == ConsoleKey.RightArrow)
            {
                usingForward = true; // Bläddra framåt
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                usingForward = false; // Bläddra bakåt
            }
            else if (key == ConsoleKey.D)
            {
                if (drawCounter < 3)
                {
                    DrawCardToHand(deck); // Dra ett kort och lägg till i handen
                    drawCounter++;
                    Console.WriteLine($"You drew a new card. Cards drawn: {drawCounter}/3");
                    //break;
                }
                else{
                    
                    maxCardsDrawn = true; // Flagga sätts till true när max kort har dragits
                }
            }

        } while (key != ConsoleKey.Enter && !maxCardsDrawn); // Avsluta när Enter trycks eller tre kort dragits
        
        return currentItem;
    }


    public void RemoveCard(IUnoCard card)
    {
        hand = hand.Where(x => x != card).ToList();
        ShowHand();

    }


    public void DrawCardToHand(Deck deck)
    {
        IUnoCard card = deck.DrawCard();
        AddCardToHand(card);
    }


    public bool HasMaxDrawnCards(int drawCounter)
    {
        return drawCounter >= 3;
    }

    public void ResetDraw()
    {
        drawCounter = 0;
    }





    public IUnoCard IterateComputerHand(Deck deck, out bool maxCardsDrawn)
    {
        
        for(i = indexCurrentCardComputer; i <= hand.Count; i++) 
        {
            return hand[i];
            indexCurrentCardComputer++;
            maxCardsDrawn = false;
        }
        
         if (drawCounter < 3)
                {
                    ShowAnonym();
                    DrawCardToHand(deck); // Dra ett kort och lägg till i handen
                    drawCounter++;
                    Console.WriteLine($"Robotics drew a new card. Cards drawn: {drawCounter}/3");
                    return hand[0];
                }
                else{
                    
                    maxCardsDrawn = true; // Flagga sätts till true när max kort har dragits
                }
                Thread.Sleep(5000);
                
            }
       

        
    }


