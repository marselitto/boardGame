// See https://aka.ms/new-console-template for more information

public class Player
{
    public string Name { get; set; }
    public int Position { get; set; }
    public int Score { get; set; }

    public void Movement(int steps)
    {
        Position += steps;
    }

    public void Update(int points)
    {
        Score += points;
    }
}