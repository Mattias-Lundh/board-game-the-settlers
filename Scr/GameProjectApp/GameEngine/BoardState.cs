using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class BoardState
    {
        public string[] HexGridImgPath { get; set; }
        public int[] ResourceNumber { get; set; }
        public Player[] Road { get; set; }
        public Player[] Village { get; set; }
        public Player[] City { get; set; }
        public int Thief { get; set; }

        public BoardState(string template)
        {            
            HexGridImgPath = GenerateHexPaths(template);
            ResourceNumber = GenerateResourceNumbers();
            Road = new Player[72];
            Village = new Player[53];
            City = new Player[53];
            Thief = FindDesert();
        }

        public enum BoardOptions { tutorial, center, random };

        private static int RandomPositionForHexPicturePath()
        {
            //Guid.NewGuid().GetHashCode() Somewhat ok seed for randomness.
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int value = random.Next(0, 19);
            return value;
        }
        private static string[] PlacePicturePath(string pathToHexpicture, int maxAmount, string[] board)
        {
            for (int i = 0; i < maxAmount; i++)
            {
                bool notPlaced = true;
                while (notPlaced)
                {
                    int possiblePositionToPlacePath = RandomPositionForHexPicturePath();
                    if (board[possiblePositionToPlacePath] == null)
                    {
                        board[possiblePositionToPlacePath] = pathToHexpicture;
                        notPlaced = false;
                    }
                }
            }
            return board;
        }
             public enum BoardOptions { tutorial, center, random };

        private static int RandomPositionForHexPicturePath()
        {
            //Guid.NewGuid().GetHashCode() Somewhat ok seed for randomness.
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int value = random.Next(0, 19);
            return value;
        }
        private static string[] PlacePicturePath(string pathToHexpicture, int maxAmount, string[] board)
        {
            for (int i = 0; i < maxAmount; i++)
            {
                bool notPlaced = true;
                while (notPlaced)
                {
                    int possiblePositionToPlacePath = RandomPositionForHexPicturePath();
                    if (board[possiblePositionToPlacePath] == null)
                    {
                        board[possiblePositionToPlacePath] = pathToHexpicture;
                        notPlaced = false;
                    }
                }
            }
            return board;
        }
        
        private int[] GenerateResourceNumbers()
        {
            //new resource number function            
            return new int[19];
        }

        private int FindDesert()
        {
            //Looks for the position of the Desert Hex
            return 0;
        }
    }
}
