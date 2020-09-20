using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class Board : RoomEquipment
    {
        public int BoardId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Board()
        {
        }

        public Board(int boardId)
        {
            this.BoardId = boardId;
        }
        public Board(int boardId, int width, int height)
        {
            this.BoardId = boardId;
            this.Width = width;
            this.Height = height;
        }

    }
}
