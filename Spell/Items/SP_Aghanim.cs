using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AppCode;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Voins.Spell
{
    public class SP_Aghanim : ISpell
    {
        double _orchidBonus = 0.5;
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo();
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        IUnit _unit;
        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        public string Name { get; set; }

        int _manaCost = 15;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 20;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;

        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 5;

        public double Duration { get { return _duration; } set { _duration = value; } }

        int _levelCast = 1;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 3;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }

        bool _isUlt = false;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }

        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;

        /// <summary>
        /// Юниты на которых было повешено сало
        /// </summary>
        List<IUnit> _silencedUnits;
        /// <summary>
        /// Тут будет сохранен весь магический урон который получили после получения сайленса
        /// </summary>
        List<int> _saveDemage;

        public SP_Aghanim()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Aghanim", this);
        }

        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            if (unit.UnitFrozen == false &&
                LevelCast != 0 &&
                !unit.Hexed)
            {
                Player player = unit as Player;
                if (player != null && player.Name == "Alchemist")
                {
                    player.DropItem(player.Items.FirstOrDefault(p => p.Name == "Aghanims Scepter"));
                }

                if (Paused)
                    Pause();

                if (StartUseSpell != null)
                    StartUseSpell(this, null);
                UnitGenerator.UpdatePlayerView(unit);
            }
        }

        void item_GetDemageEvent(int phisic, int magic, int pure, IUnit demagedUnit)
        {
            _saveDemage[_silencedUnits.IndexOf(demagedUnit)] += magic;
        }

        /// <summary>
        /// Для итема не имеет смысла
        /// </summary>
        public void UpSpell(Player player)
        {
        }

        #region Pause
        public event EventHandler PausedEvent;
        bool _paused;
        public bool Paused { get { return _paused; } set { _paused = value; } }

        public void Pause()
        {
            Paused = true;

            if (_firstTimer != null)
                _firstTimer.Pause();
            if (_secondTimer != null)
                _secondTimer.Pause();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        public void StopPause()
        {
            Paused = false;

            if (_firstTimer != null)
                _firstTimer.Resume();
            if (_secondTimer != null)
                _secondTimer.Resume();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        #endregion
    }
}
