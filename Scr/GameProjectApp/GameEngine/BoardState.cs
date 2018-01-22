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

        private string[] GenerateHexPaths(string template)
        {
            //new board function
            return new string[19];
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
