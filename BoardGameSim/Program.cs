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
    
    public Player(string name)
    {
        Name = name;
        Position = 0;
        Score = 0;
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

public class Game
{
    private Board board;
    private List<Player> players;
    private List<IPlayerType> playerTypes;
    private int currentPlayerIndex;

    public event Action<Player, int> OnRewardCollected;

    public Game(int boardSize, List<Player> playersList, List<IPlayerType> playerTypesList)
    {
        board = new Board(boardSize);
        players = playersList;
        playerTypes = playerTypesList;
        currentPlayerIndex = 0;

        OnRewardCollected += (player, reward) =>
        {
            Console.WriteLine($"{player.Name} zebrał nagrodę: {reward} pkt!");
        };
    }

    public void Start()
    {
        Console.WriteLine("Gra rozpoczyna się!");
        while (!IsGameOver())
        {
            TakeTurn();
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }
        DisplayResults();
    }

    private void TakeTurn()
    {
        Player currentPlayer = players[currentPlayerIndex];
        IPlayerType currentType = playerTypes[currentPlayerIndex];
        Console.WriteLine($"\nTura gracza: {currentPlayer.Name}");
        
        Random random = new Random();
        int steps = random.Next(1, 7);
        currentPlayer.Movement(steps);
        Console.WriteLine($"{currentPlayer.Name} przesuwa się o {steps} pola, pozycja: {currentPlayer.Position}.");

        int reward = board.CheckReward(currentPlayer.Position);
        if (reward > 0)
        {
            currentPlayer.Update(reward);
            OnRewardCollected?.Invoke(currentPlayer, reward);
        }

        currentType.SpecialAbility(currentPlayer, board);
    }

    private bool IsGameOver()
    {
        return players.Exists(p => p.Position >= board.BoardSize);
    }

    private void DisplayResults()
    {
        Console.WriteLine("\nGra zakończona! Wyniki:");
        foreach (var player in players)
        {
            Console.WriteLine($"{player.Name} - Punkty: {player.Score}");
        }
    }
}
public interface IPlayerType
{
    void SpecialAbility(Player player, Board board);
}

public class Warrior : IPlayerType
{
    public void SpecialAbility(Player player, Board board)
    {
        player.Update(5);
        Console.WriteLine($"{player.Name} zdobył 5 dodatkowych punktów za walkę!");
    }
}
public class Mage : IPlayerType
{
    public void SpecialAbility(Player player, Board board)
    {
        Console.WriteLine($"{player.Name} rzuca zaklęcie na planszę!");
        board.ModifyRewards(3);
    }
}

public class Healer : IPlayerType
{
    public void SpecialAbility(Player player, Board board)
    {
        player.Update(3);
        Console.WriteLine($"{player.Name} zdobył 3 punkty za leczenie!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Player> players = new List<Player>
        {
            new Player("Gracz 1"),
            new Player("Gracz 2"),
            new Player("Gracz 3")
        };

        List<IPlayerType> playerTypes = new List<IPlayerType>
        {
            new Warrior(),
            new Mage(),
            new Healer()
        };

        Game game = new Game(20, players, playerTypes);
        game.Start();
    }
}



