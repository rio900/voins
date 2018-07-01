using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Voins.AppCode
{
    public interface ISpell
    {
        /// <summary>
        /// Способность начала использоватся
        /// </summary>
        event EventHandler StartUseSpell;
        event EventHandler CompletedUseSpell;
        event EventHandler PausedEvent;
        string Name { get; set; }
        int ManaCost { get; set; }
        int HelthCost { get; set; }
        bool Paused { get; set; }
        int LevelCast { get; set; }
        int MaxLevelCast { get; set; }
        
        double Culdaun { get; set; }
        bool CuldaunBool { get; set; }

        bool IsUlt { get; set; }
        /// <summary>
        /// Время действия заклинания
        /// </summary>
        double Duration { get; set; }

        UC_View_ImageTileControl ImageTile { get; set; }
        SpellDescriptionInfo SpellDescriptionInfo { get; set; }
        void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property);

        /// <summary>
        /// Изучить способность хоть один раз
        /// </summary>
        void UpSpell(Player player);

        void Pause();
        void StopPause();
    }
}
