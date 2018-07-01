using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voins.AppCode
{
    public interface IAnimationControl
    {
        EAngel Angel { get; set; }

        void ChengAngel(EAngel angel);

        void StartMuveAnimation(double duration);

        void Pause();
        void StopPause();
    }
}
