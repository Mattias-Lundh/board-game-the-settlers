using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board
{
    class PathToBoardTiles
    {
        public enum BoardOptions { tutorial, center, random };

        private static int RandomPositionForHexPicturePath()
        {
            //Guid.NewGuid().GetHashCode() Somewhat ok seed for randomness.
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int value = random.Next(0, 18);
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

        public static string[] GenerateBoardPicturePath(BoardOptions choosenOption)
        {
            int maxDesert = 1;
            int maxBrick = 3;
            int maxFeild = 4;
            int maxForest = 4;
            int maxHill = 3;
            int maxMountain = 3;

            string desert = @"/Images/hex/Desert.png";
            string brick = @"/Images/hex/Brick.png";
            string feild = @"/Images/hex/Field.png";
            string forest = @"/Images/hex/Forest.png";
            string hill = @"/Images/hex/Hill.png";
            string mountain = @"/Images/hex/Mountain.png";
            string[] pathToHexpicture = new string[18];
            switch (choosenOption)
            {
                case BoardOptions.tutorial:
                    pathToHexpicture[0] = brick;
                    pathToHexpicture[1] = feild;
                    pathToHexpicture[2] = forest;
                    pathToHexpicture[3] = hill;
                    pathToHexpicture[4] = mountain;
                    pathToHexpicture[5] = brick;
                    pathToHexpicture[6] = feild;
                    pathToHexpicture[7] = forest;
                    pathToHexpicture[8] = hill;
                    pathToHexpicture[9] = desert;
                    pathToHexpicture[10] = mountain;
                    pathToHexpicture[11] = brick;
                    pathToHexpicture[12] = feild;
                    pathToHexpicture[13] = forest;
                    pathToHexpicture[14] = hill;
                    pathToHexpicture[15] = forest;
                    pathToHexpicture[16] = mountain;
                    pathToHexpicture[17] = feild;

                    break;
                case BoardOptions.random:
                    pathToHexpicture = PlacePicturePath(brick, maxBrick, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(feild, maxFeild, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(forest, maxForest, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(hill, maxHill, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(mountain, maxMountain, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(desert, maxDesert, pathToHexpicture);

                    break;

                case BoardOptions.center:
                    //Always desert on CenterTile.
                    pathToHexpicture[9] = desert;
                    pathToHexpicture = PlacePicturePath(brick, maxBrick, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(feild, maxFeild, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(forest, maxForest, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(hill, maxHill, pathToHexpicture);
                    pathToHexpicture = PlacePicturePath(mountain, maxMountain, pathToHexpicture);
                    break;

            }
            return pathToHexpicture;
        }
    }
}
