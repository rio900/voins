using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voins.AppCode
{
    public interface IAura
    {
        void StartUseAura(Map map, Game_Object_In_Call obj, IUnit unit, object property);
        void StopUseAura();
    }
}
