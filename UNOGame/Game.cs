public class Game 
{
private Deck deck;
private HumanPlayer humanPlayer;
private ComputerPlayer computerPlayer;

public List<IUnoCard> playedCards = new List<IUnoCard>();

public Game ()
{
Play();    
}

    public void Play()
    {
        Console.WriteLine("Welcome to UNO!");
        Console.WriteLine("What is your name?");
        string name = Console.ReadLine();
        Console.WriteLine($"Welcome {name}! You will play against a computer.");
        //choose your difficulty
        deck = new Deck();
        humanPlayer = new HumanPlayer(name, deck); // Kontrollera så att name inte är tom eller tilldela default name
        computerPlayer = new ComputerPlayer(deck);
        Console.WriteLine($"Let the game begin!");


        // Console.WriteLine($"Computers hand:");
        // computerPlayer.ShowHand();

        Console.WriteLine($"First card");
        IUnoCard card = deck.DrawCard();
        playedCards.Add(card);


        Console.WriteLine($"{name} starts");
        Console.WriteLine($"{name}s hand:");
        humanPlayer.hand.ShowHand();

        Hand playerHand = humanPlayer.hand;
        Hand computerHand = computerPlayer.hand;
        IUnoCard choosenCard;

   

        while (true)
        {
            // Visa senaste spelade kort
            Console.Write($"Last drawn card: ");
            DisplayFirstCard();

            // Låt spelaren välja ett kort
            bool maxCardsDrawn;
            choosenCard = playerHand.IterateHand(deck, out maxCardsDrawn);
            //Console.WriteLine(maxCardsDrawn);
            // Kontrollera om det valda kortet kan spelas
            if (CanPlayCard(choosenCard) && !maxCardsDrawn)
            {
                // Kortet kan spelas, bryt ut ur loopen och spela kortet
                Console.Clear();
                Console.WriteLine($"You selected: {choosenCard.GetColor()} {choosenCard.GetValue()}");
                Console.WriteLine("");
                Console.WriteLine("Your current hand: ");
                playerHand.RemoveCard(choosenCard); // Ta bort valt kort från handen
                AddCardToFront(choosenCard);  // Lägg till kortet bland spelade kort
                //d
                break;
            }
            else if(maxCardsDrawn)
            {
                Console.WriteLine("You have drawn the maximum number of cards and cannot play. Next player's turn.");
                break; // Byt till nästa spelare
            }


             else{
                // Kortet kan inte spelas, ge feedback till spelaren
                Console.Clear();
                Console.WriteLine($"You cannot play {choosenCard.GetColor()} {choosenCard.GetValue()}. Try again or draw a card.");

             }

        }

        // Hantera spelbart kort (när loopen avbrutits)
        playerHand.ResetDraw();

    while(true)
        {
        Console.Write("Last played card: ");
        DisplayFirstCard(); // Visa det senaste spelade kortet

        bool maxCardsDrawn;
        Console.WriteLine($"{computerPlayer.name}s hand");
        
        choosenCard = computerHand.IterateComputerHand(deck, out maxCardsDrawn);
        if(CanPlayCard(choosenCard) && !maxCardsDrawn)
        {
              Console.WriteLine($"{computerPlayer.name} selected: {choosenCard.GetColor()} {choosenCard.GetValue()}");
                playerHand.RemoveCard(choosenCard); // Ta bort valt kort från handen
                AddCardToFront(choosenCard);  // Lägg till kortet bland spelade kort
                break;
        }
        
        else if(maxCardsDrawn)
        {
                Console.WriteLine("You have drawn the maximum number of cards and cannot play. Next player's turn.");
                break; // Byt till nästa spelare
        }


        else{
                // Kortet kan inte spelas, ge feedback till spelaren
                Console.Clear();
                Console.WriteLine($"{computerPlayer.name}s could not play {choosenCard.GetColor()} {choosenCard.GetValue()}. {computerPlayer.name} tries again.");


             }
        }     
          
    

    
}

public void AddCardToFront(IUnoCard choosenCard)
    {
        playedCards.Insert(0, choosenCard);
    }

public void DisplayFirstCard()
{
    if (playedCards.Count > 0)
    {
        IUnoCard firstCard = playedCards[0]; // Hämtar första kortet
        firstCard.DisplayCard(); // Visar det första kortet
    }
    else
    {
        Console.WriteLine("No cards have been played yet.");
    }
}

public bool CanPlayCard(IUnoCard card)
{
    IUnoCard lastPlayedCard = playedCards.Last();
    CardColor LastCardColor = lastPlayedCard.GetColor();
    CardColor playedCardColor = card.GetColor();

    CardValue LastCardValue = lastPlayedCard.GetValue();
    CardValue playedCardValue = card.GetValue();


    if(LastCardColor == playedCardColor)
    {
        return true;
    }

    if (LastCardValue == playedCardValue)
    {
        return true;
    }

    return false;
}




}