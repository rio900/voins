using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Voins.AppCode
{
   public interface IGameControl
    {
        EAngel Angel { get; set; }
        Canvas RootCanvas { get; set; }

        void ChengAngel(EAngel Angel);

        void GetDemage(string value);
        void ShowAttack(EAngel angel, double attackSpeed);
        void Silenced(bool visibilitySilenc);
        void Remove(Canvas rootCanvas);
    }
}
