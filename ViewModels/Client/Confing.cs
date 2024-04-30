using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Client
{
    public class Confing
    {
        #region Constractor
        public Confing()
        {
            Outputs = new List<Output>();
            Rooms = new List<Room>();
        }
        #endregion /Constructor

        #region System

        #endregion /System

        #region Outputs

        public List<Output> Outputs { get; set; }

        #endregion /Outputs

        #region Rooms
        public List<Room> Rooms { get; set; }

        #endregion /Rooms

    }
}
