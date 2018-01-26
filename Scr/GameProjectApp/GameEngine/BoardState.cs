using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class BoardState
    {
        public enum BoardOptions { tutorial, center, random };
        public string[] HexGridImgPath { get; set; }
        public int[] ResourceNumber { get; set; }
        public Player[] Road { get; set; }
        public Player[] Settlement { get; set; }
        public Player[] City { get; set; }
        public int Thief { get; set; }

        public BoardState(BoardOptions template)
        {
            HexGridImgPath = GenerateHexPaths(template);
            ResourceNumber = GenerateResourceNumbers(template);
            Road = new Player[72];
            Settlement = new Player[53];
            City = new Player[53];
            Thief = FindDesert();
        }

        private int[] GenerateResourceNumbers(BoardOptions template)
        {
            //new resource numbers
            switch (template)
            {
                case BoardOptions.tutorial:
                    return new int[] { 11, 12, 9, 4, 6, 5, 10, 7, 3, 11, 4, 8, 8, 10, 9, 3, 5, 2, 6 };
            }

            int[] result = new int[] { 2, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12, 7 };
            result = (int[])Randomize(result);
            for (int i = 0; i < HexGridImgPath.Length; i++)
            {
                if (HexGridImgPath[i].Contains("Desert"))
                {
                    Random random = new Random();
                    int swap = 19;
                    while (swap != i)
                    {
                        swap = random.Next(0, 18);
                    }
                    result[swap] = result[i];
                    result[i] = 7;
                }
            }
            return result;
        }

        private IEnumerable<T> Randomize<T>(IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }

        private int FindDesert()
        {
            //Looks for the position of the Desert Hex
            return 0;
        }

        private string[] GenerateHexPaths(BoardOptions choosenOption)
        {
            // MODIFY
            int maxDesert = 1;
            int maxBrick = 3;
            int maxFeild = 4;
            int maxForest = 4;
            int maxHill = 4;
            int maxMountain = 3;

            // MODIFY
            string desert = @"/Images/hex/Desert.png";
            string hill = @"/Images/hex/Hill.png";
            string feild = @"/Images/hex/Field.png";
            string forest = @"/Images/hex/Forest.png";
            string pasture = @"/Images/hex/Pasture.png";
            string mountain = @"/Images/hex/Mountain.png";
            string[] pathToHexpicture = new string[19];
            switch (choosenOption)
            {
                case BoardOptions.tutorial:
                    pathToHexpicture[0] = hill;
                    pathToHexpicture[1] = feild;
                    pathToHexpicture[2] = forest;
                    pathToHexpicture[3] = pasture;
                    pathToHexpicture[4] = mountain;
                    pathToHexpicture[5] = hill;
                    pathToHexpicture[6] = feild;
                    pathToHexpicture[7] = forest;
                    pathToHexpicture[8] = pasture;
                    pathToHexpicture[9] = desert;
                    pathToHexpicture[10] = mountain;
                    pathToHexpicture[11] = hill;
                    pathToHexpicture[12] = feild;
                    pathToHexpicture[13] = forest;
                    pathToHexpicture[14] = pasture;
                    pathToHexpicture[15] = forest;
                    pathToHexpicture[16] = mountain;
                    pathToHexpicture[17] = feild;
                    pathToHexpicture[18] = pasture;

                    break;
                case BoardOptions.random:
                    pathToHexpicture = PlacePicturePath(hill, maxBrick, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(feild, maxFeild, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(forest, maxForest, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(pasture, maxHill, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(mountain, maxMountain, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(desert, maxDesert, pathToHexpicture);

                    break;

                case BoardOptions.center:
                    //Always desert on CenterTile.
                    pathToHexpicture[9] = desert;
                    pathToHexpicture = PlacePicturePath(hill, maxBrick, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(feild, maxFeild, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(forest, maxForest, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(pasture, maxHill, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(mountain, maxMountain, pathToHexpicture);
                    break;

            }
            return pathToHexpicture;
        }

        private int RandomPositionForHexPicturePath()
        {
            //Guid.NewGuid().GetHashCode() Somewhat ok seed for randomness.
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int value = random.Next(0, 19);
            return value;
        }

        private string[] PlacePicturePath(string pathToHexpicture, int maxAmount, string[] board)
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
    }
}
