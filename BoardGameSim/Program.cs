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

public class Board
{
    private int BoardSize;

    private void RandomRewards()
    {
        Random GenerateRewards = new Random();
        for (int i = 0; i < BoardSize / 4; i++)
        {
            int position = GenerateRewards.Next(1, BoardSize);
            int reward = GenerateRewards.Next(4, 100);
        }
        
    }


}