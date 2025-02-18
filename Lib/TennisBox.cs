namespace ConsoleApp1;

public class TennisBox
{
    private static readonly Dictionary<int, string> ScoreNameMapping = new()
    {
        { 0, "love" },
        { 1, "fifteen" },
        { 2, "thirty" },
        { 3, "forty" }
    };

    private int _player1Score = 0;
    private int _player2Score = 0;
    private readonly string _player1Name;
    private readonly string _player2Name;

    public TennisBox(string player1Name, string player2Name)
    {
        _player1Name = player1Name;
        _player2Name = player2Name;
    }

    public bool Player1Goal()
    {
        _player1Score++;
        return GetScore();
    }

    public bool Player2Goal()
    {
        _player2Score++;
        return GetScore();
    }

    private bool GetScore()
    {
        if (_player1Score == _player2Score)
        {
            Console.WriteLine(_player1Score <= 3
                ? $"{ScoreNameMapping[_player1Score]} all"
                : "deuce");
            
            return false;
        }

        if (_player1Score <= 3 && _player2Score <= 3)
        {
            Console.WriteLine($"{ScoreNameMapping[_player1Score]} {ScoreNameMapping[_player2Score]}");
            return false;
        }
        
        var leadPlayer = _player1Score > _player2Score ? _player1Name : _player2Name;
        var scoreDifference = Math.Abs(_player1Score - _player2Score);

        if (scoreDifference == 1)
        {
            Console.WriteLine($"{leadPlayer} adv");
            return false;
        }
        Console.WriteLine($"{leadPlayer} win");
        return true;
    }
}