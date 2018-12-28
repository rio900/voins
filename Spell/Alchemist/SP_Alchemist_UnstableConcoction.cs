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
    public class SP_Alchemist_UnstableConcoction : ISpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Unstable Concoction, Culdaun 10 sec. Time cast for max power 3 sec., Demage type: physical",
            LevelDescription =
            "Level1: Hero demage + 12(max), Mana cost - 25" + Environment.NewLine +
            "Level2: Hero demage + 24(max), Mana cost - 25" + Environment.NewLine +
            "Level3: Hero demage + 36(max), Mana cost - 25"
        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        public string Name { get; set; }

        int _manaCost = 25;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 10;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;
        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 3;

        public double Duration { get { return _duration; } set { _duration = value; } }

        bool _isUlt = false;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }

        int _levelCast = 0;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 3;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }

        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;
        int _power = 1;
        /// <summary>
        /// Или запустили банку
        /// </summary>
        bool _startShut;
        IUnit _unit;
        Player _player;

        Map _map;
        public SP_Alchemist_UnstableConcoction()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Alchemist_UnstableConcoction", this);
        }

        /// <summary>
        /// Запуск каста банки
        /// </summary>
        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            _unit = unit;
            _map = map;
            _player = _unit as Player;

            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);

            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 &&
                !upSpell && 
                !unit.Silenced &&
                !unit.Hexed &&
                !Paused)
            {
                if (unit.Mana >= ManaCost)
                ///Проверка есть ли мана на каст
                {
                    Speed = unit.AttackSpeed;

                    ///Флаг кулдауна
                    _culdaunBool = true;

                    _imageTile.ChengImage("SP_Alchemist_UnstableConcoctionGO");
                    ///Отнимаем нужное количество
                    unit.Mana -= ManaCost;

                    _power = 1;

                    if (_player != null)
                        (_player.GameObject.View as UC_Player).ShowEffect(0,true);

                    ///Запуск накастовки банки
                    _secondTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(1) };
                    _secondTimer.Completed += _secondTimer_Completed; 
                    _secondTimer.Begin();

                    if (Paused)
                    {
                        Pause();
                    }

                    ///Таймер кулдауна заклинания
                    _firstTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(Culdaun) };
                    _firstTimer.Completed += _firstTimer_Completed;
                    _firstTimer.Begin();

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
            else if (!_startShut && CuldaunBool && unit.UnitFrozen == false && !unit.Silenced &&
                !_unit.Hexed &&
                !Paused && !unit.Dead)
            {///Если банка набирает силу, но не была запущена 
                ShutUnstableConcoction();
            }
        }

        void _firstTimer_Completed(object sender, object e)
        {
            _culdaunBool = false;
            _startShut = false;
            _firstTimer.Completed -= _firstTimer_Completed;
            _firstTimer = null;
        }

        void _secondTimer_Completed(object sender, object e)
        {
            if (_player != null)
                (_player.GameObject.View as UC_Player).ShowEffect(0, false);

            ///Если это не максимум силы накача банки, и ее не запустили
            if (_power < Duration && !_startShut)
            {
                ///Увеличиваем силу накача банки
                _power += 1;
                _secondTimer.Begin();
            }
            else
            {
                ///Максимум накачки, запускаем банку
                if (!_startShut && _unit.UnitFrozen == false && !_unit.Silenced &&
                    !_unit.Hexed &&
                    !Paused && !_unit.Dead)
                    ShutUnstableConcoction();
                else
                    _imageTile.ChengImage("SP_Alchemist_UnstableConcoction");

                _secondTimer.Completed -= _secondTimer_Completed;
                _secondTimer = null;
            }
        }

        /// <summary>
        /// Запуск банки
        /// </summary>
        void ShutUnstableConcoction()
        {
            _imageTile.ChengImage("SP_Alchemist_UnstableConcoction");
            _startShut = true;
            ///Создаем визуальный объект колбы
            UC_Alchemist_UnstableConcoction arrow = new UC_Alchemist_UnstableConcoction();


            Bullet bullArrow = new Bullet();
            bullArrow.GameObject = new Game_Object_In_Call()
            {
                EnumCallType = EnumCallType.Bullet,
                View = arrow
            };
            bullArrow.UnitUsed = _unit;
            bullArrow.PositionX = _unit.PositionX;
            bullArrow.PositionY = _unit.PositionY;
            bullArrow.Speed = 0.25;

            ///Магический урон зависит от прокача стрел
            //bullArrow.DemageMagic = 5 * (int)property;

            if (LevelCast == 1)
            {
                bullArrow.DemagePhys = _unit.Demage + 3*_power;
            }
            else if (LevelCast == 2)
            {
                bullArrow.DemagePhys = _unit.Demage + 6 * _power;
            }
            else if (LevelCast == 3)
            {
                bullArrow.DemagePhys = _unit.Demage + 9 * _power;
            }

           if (_power > 3)
                bullArrow.StunDuration = 3;
            else
               bullArrow.StunDuration = _power *1;

            bullArrow.CurrentMap = _map;
            bullArrow.Angel = _unit.Angel;
            bullArrow.Range = _power * 2 - 1;

            bullArrow.Spells.Add(new SPB_Alchemist_UnstableConcoction() { Name = "Fly" });

            ///И его же добавим в масив всех объектов
            _map.GameObjectInCall.Add(bullArrow.GameObject);

            Canvas.SetLeft(bullArrow.GameObject.View, bullArrow.PositionX * 50);
            Canvas.SetTop(bullArrow.GameObject.View, bullArrow.PositionY * 50);
            ///Отображение
            _map.MapCanvas.Children.Add(bullArrow.GameObject.View);

            bullArrow.UseSpall("Fly");

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
