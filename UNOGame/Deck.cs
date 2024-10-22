public class Deck
{
 public List<IUnoCard> deck;
 private readonly CardColor[] colors = {CardColor.Red, CardColor.Blue, CardColor.Green, CardColor.Yellow};
private readonly CardValue[] specialities = {CardValue.Block, CardValue.plusTwo, CardValue.plusFour};

 public Deck ()
 {
    this.deck = new List<IUnoCard>();
    InitializeDeck();
    Shuffle();
 }

public void AddCard(IUnoCard card)
{
    deck.Add(card);
}

 public void InitializeDeck()
    {
        foreach(var color in colors)
        {
            AddCard(new RegularCard(color, CardValue.Zero));

            for (CardValue number = CardValue.One; number <= CardValue.Nine; number++)
            {
                AddCard(new RegularCard(color, number));
                AddCard(new RegularCard(color, number));
            }

            foreach(var speciality in specialities)
            {
                AddCard(new SpecialCard(color, speciality));
                AddCard(new SpecialCard(color, speciality));
            }
    }

        Console.WriteLine("Deck initialized with cards.");
     }

 public IUnoCard DrawCard()
 {
    if (deck.Count == 0) // ändra sen
        {
            throw new InvalidOperationException("Deck is empty!");
        }

        var card = deck[0]; // Dra det översta kortet
        deck.RemoveAt(0);   // Ta bort kortet från leken
        return card;
 }

// DrawFirstCard kontrollerar så att första kortet är ett regular card

 public void Shuffle()
 {
    Random random = new Random();
    deck = deck.OrderBy(x => random.Next()).ToList();

 }

public void DisplayDeck()
{
    foreach(var card in deck)
    {
        card.DisplayCard();
    }

}

 
}