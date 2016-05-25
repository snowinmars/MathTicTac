using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.DTO
{
    public class Move
    {
        public string Token { get; set; }
        public string IP { get; set; }
        public int GameId { get; set; }
        public Coord BigCellCoord { get; set; }
        public Coord CellCoord { get; set; }

        public Move(string ip, string token, int gameid, Coord bigCellCoord, Coord CellCoord)
        {
            this.Token = token;
            this.GameId = gameid;
            this.CellCoord = CellCoord;
            this.BigCellCoord = bigCellCoord;
            this.IP = ip;
        }
    }
}
