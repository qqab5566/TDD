

using ConsoleApp1;

class Program
{
    static void Main()
    {
        Console.Write("Enter Player Left Name: ");
        var player1Name = Console.ReadLine();
        Console.Write("Enter Player Right Name: ");
        var player2Name = Console.ReadLine();

        var scoreBox = new TennisBox(player1Name, player2Name);

        Console.WriteLine("'L' for Player Left to score, Press 'R' for Player Right to score, 'Q' to quit.");
        Console.WriteLine("love all");
        
        var win = false;
        while (!win)
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.L)
            {
                win = scoreBox.Player1Goal();
            }
            else if (key == ConsoleKey.R)
            {
                win = scoreBox.Player2Goal();
            }
            else if (key == ConsoleKey.Q)
            {
                break;
            }
        }
    }
}