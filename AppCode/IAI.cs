using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voins.AppCode
{
    public interface IAI
    {
        Map CurrentMap { get; set; }
        IUnit CurrentUnit { get; set; }
        bool Stoped { get; set; }
        bool Farm { get; set; }
        bool Hunt { get; set; }

        void StartAI();
        void StopAI();
        void Pause();
        void StopPause();

       event EventHandler PausedEvent;
      bool Paused { get; set; }
    }
}
