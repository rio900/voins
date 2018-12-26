using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AppCode;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Voins.Spell
{
    public class SP_Nature_Wrath : ISpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo();
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        public string Name { get; set; }

        int _manaCost = 40;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 20;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;
        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 0;

        public double Duration { get { return _duration; } set { _duration = value; } }

        bool _isUlt = true;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }

        int _levelCast = 0;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 1;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }

        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;

        IUnit _unit;
        public SP_Nature_Wrath()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Nature_Wrath", this);
        }

        /// <summary>
        /// Запуск заряженой молнии
        /// </summary>
        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            _unit = unit;
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);

            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 &&
                !upSpell && !unit.Silenced &&
                !unit.Hexed &&
                !Paused)
            {
                if (unit.Mana >= ManaCost)
                ///Проверка есть ли мана на каст
                {
                    Speed = unit.AttackSpeed;

                    ///Флаг кулдауна
                    _culdaunBool = true;

                    ///Отнимаем нужное количество
                    unit.Mana -= ManaCost;

                    ///Создаем визуальный объект молния
                    UC_Nature_Wrath arrow = new UC_Nature_Wrath();
                    arrow.ChengAngel(unit.Angel);

                    Bullet bullArrow = new Bullet();
                    bullArrow.GameObject = new Game_Object_In_Call()
                    {
                        EnumCallType = EnumCallType.Bullet,
                        View = arrow
                    };
                    bullArrow.UnitUsed = unit;
                    bullArrow.PositionX = unit.PositionX;
                    bullArrow.PositionY = unit.PositionY;
                    bullArrow.Speed = 0.2;
                  
                   

                    bullArrow.CurrentMap = map;
                    bullArrow.Angel = unit.Angel;

                    SPB_Item_Maelstrom male = new SPB_Item_Maelstrom(1) { Name = "SP_Nature_Wrath" };
                    bullArrow.Spells.Add(male);

                    bullArrow.TrentDamage = 10;
                    bullArrow.TrentArmor = 2;
                    bullArrow.LifeTime = 15;

                    bullArrow.Range = map.Width * 2;
                    male.HitCount = 10;
                    male.Multiply = 30;
                    bullArrow.DemageMagic = 30;

                    ///И его же добавим в масив всех объектов
                    map.GameObjectInCall.Add(bullArrow.GameObject);

                    Canvas.SetLeft(bullArrow.GameObject.View, bullArrow.PositionX * 50);
                    Canvas.SetTop(bullArrow.GameObject.View, bullArrow.PositionY * 50);
                    ///Отображение
                    map.MapCanvas.Children.Add(bullArrow.GameObject.View);

                    bullArrow.UseSpall("SP_Nature_Wrath");

                    ///Таймер кулдауна заклинания
                    _secondTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(Culdaun) };
                    _secondTimer.Completed += _secondTimer_Completed;
                    _secondTimer.Begin();

                    if (Paused)
                    {
                        Pause();
                    }

                    if (StartUseSpell != null)
                        StartUseSpell(this, null);

                    UnitGenerator.UpdatePlayerView(unit);
                }
                else
                ///Маны нету
                {

                }
            }
        }

        void _secondTimer_Completed(object sender, object e)
        {
            _culdaunBool = false;
            _secondTimer.Completed -= _secondTimer_Completed;
            _secondTimer = null;
        }

        /// <summary>
        /// Изучить способность хоть один раз
        /// </summary>
        public void UpSpell(Player player)
        {
            player.PreviousSkill = Name;
            LevelCast += 1;
            ImageTile.UpSpell();
            player.SpellUpedEventCall();
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
