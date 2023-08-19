using System.Diagnostics.Tracing;
using System.Security;
using System.Xml.Linq;

namespace Snake_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
    }
}