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
    private Dictionary<int, int> Rewards;
    
    public Board(int size)
    {
        BoardSize = size;
        Rewards = new Dictionary<int, int>();
        GenerateRandomRewards();
    }

    private void GenerateRandomRewards()
    {
        Random GenerateRewards = new Random();
        for (int i = 0; i < BoardSize / 4; i++)
        {
            int position = GenerateRewards.Next(1, BoardSize);
            int reward = GenerateRewards.Next(1, 7);
            if (!Rewards.ContainsKey(position))
            {
                Rewards.Add(position, reward);
                Console.WriteLine($"Nagroda: {reward} pkt zostala dodana na polu {position}.");
            }
        }
    }
    public int CheckReward(int position)
    {
        return Rewards.ContainsKey(position) ? Rewards[position] : 0;
    }

    public void ModifyRewards(int modifier)
    {
        foreach (var key in Rewards.Keys)
        {
            Rewards[key] += modifier;
        }
        Console.WriteLine("Nagrody na planszy zostały zmodyfikowane.");
    }
}

