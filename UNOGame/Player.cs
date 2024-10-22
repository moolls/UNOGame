// See https://aka.ms/new-console-template for more information
public abstract class Player
{
    public string name {get; private set;}
    
    public Hand hand = new Hand();
    
    public Player(string name, Deck deck)
    {
      this.name = name;
      hand.GenerateHand(deck);
    }

 
    public void PlaceCard()
    {
        
    }

    

}