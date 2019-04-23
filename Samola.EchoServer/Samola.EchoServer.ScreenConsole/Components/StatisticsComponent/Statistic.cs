using System;
using System.Collections.Generic;
using System.Text;

namespace EchoServer.ScreenConsole.Components.StatisticsComponent
{
    /// <summary>
    /// Represents HTTP operation statistics directed at a specific item (identified by its id)
    /// PURPOSE:
    ///     Encapsulates the HTTP operation statistics of a specific item
    /// </summary>
    public class Statistic
    {
        public Statistic(int id)
        {
            this.Id = id;
            this.GetCount = 0;
            this.PutCount = 0;
            this.Deleted = false;
        }

        public int Id { get; }
        public int GetCount { get; set; }
        public int PutCount { get; set; }
        public bool Deleted { get; set; }
    }
}
