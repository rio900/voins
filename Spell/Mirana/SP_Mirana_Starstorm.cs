using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AppCode;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Voins.Spell
{
    public class SP_Mirana_Starstorm : ISpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "",
            LevelDescription =
            "" + Environment.NewLine
        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        int _size = 1;
        double _oldCuldaun;
        IUnit _unit;
        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        bool _culdaunBoolAttack;
        public bool CuldaunBoolAttack { get { return _culdaunBoolAttack; } set { _culdaunBoolAttack = value; } }

        double _abilityAction;
        /// <summary>
        /// Время действия способности
        /// </summary>
        public double AbilityAction { get { return _abilityAction; } set { _abilityAction = value; } }

        public string Name { get; set; }

        int _manaCost = 10;
        public int ManaCost { get { return _manaCost + LevelCast * 5; } set { _manaCost = value; } }

        double _culdaun = 6;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;

        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 1;

        public double Duration { get { return _duration; } set { _duration = value; } }

        int _levelCast = 0;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 3;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }

        bool _isUlt = false;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }

        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;
        public Storyboard _therdTimer;
        IAnimationControl _animationControl;
        List<Point> _callsPointl;
        List<UC_Mirana_Starstorm> _attackView;
        Bullet _bullDualBreath;
        bool _removeFire;
        Map _map;

        public SP_Mirana_Starstorm()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Mirana_Starstorm", this);
        }

        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);
            _unit = unit;
            _map = map;

            if (LevelCast == 1)
                ManaCost = 10;
            else if (LevelCast == 2)
                ManaCost = 13;
            else if (LevelCast == 3)
                ManaCost = 15;

            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 && !upSpell && !unit.Silenced &&
                !unit.Hexed &&
                !Paused)
            {
                if (unit.Mana >= ManaCost)
                ///Проверка есть ли мана на каст
                {
                    if (!_unit.Buffs.Any(p => p.Name == "SP_Mirana_MoonlightShadow"))
                        Culdaun = 5;
                    else
                        Culdaun = 4;

                    ///Флаг кулдауна
                    _culdaunBool = true;

                    ///Отнимаем нужное количество
                    unit.Mana -= ManaCost;
                    _map = map;

                    ///Тут кординаты ячеек в которые упадут звездочки
                    _callsPointl = new List<Point>();
                    Starfall(map, unit);

                    _attackView = new List<UC_Mirana_Starstorm>();
                    _removeFire = false;
                    AttackUnits(map, unit, true);

                    ///Таймер второго дыхания, замедляющего
                    _secondTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(0.5) };
                    _secondTimer.Completed += _secondTimer_Completed;
                    _secondTimer.Begin();

                    /// Если есть аганим
                    if (UnitGenerator.HasAghanim(unit))
                    {
                        ///Таймер второго дыхания, замедляющего
                        _therdTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(1.25) };
                        _therdTimer.Completed += _therdTimer_Completed; ;
                        _therdTimer.Begin();
                    }

                    if (StartUseSpell != null)
                        StartUseSpell(this, null);

                    ///Таймер кулдауна заклинания
                    _firstTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(Culdaun) };
                    _firstTimer.Completed += _firstTimer_Completed;
                    _firstTimer.Begin();


                    if (Paused)
                        Pause();

                    UnitGenerator.UpdatePlayerView(unit);
                }
            }
        }

        private void _therdTimer_Completed(object sender, object e)
        {
            if (!_unit.Dead)
            {
                _callsPointl = new List<Point>();
                Starfall(_map, _unit);

                _attackView = new List<UC_Mirana_Starstorm>();
                AttackUnits(_map, _unit, true);
            }
        }

        private void Starfall(Map map, IUnit unit)
        {
            ///Получаем ячейки которые находятся перед героем
            ///использывавшим тучку
            ///Сначала добавляем ячеку с героем
            int xNew = unit.PositionX;
            int yNew = unit.PositionY;

            for (int i = 0; i <= _size; i++)
            {
                xNew = xNew - 1;
                if (!_unit.Buffs.Any(p => p.Name == "SP_Mirana_MoonlightShadow"))
                {
                    Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (call == null || call != null && call.Block)
                        break;
                }
                _callsPointl.Add(new Point(xNew, yNew));
            }
            xNew = unit.PositionX;
            yNew = unit.PositionY;

            for (int i = 0; i <= _size; i++)
            {
                xNew = xNew + 1;
                if (!_unit.Buffs.Any(p => p.Name == "SP_Mirana_MoonlightShadow"))
                {
                    Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (call == null || call != null && call.Block)
                        break;
                }
                _callsPointl.Add(new Point(xNew, yNew));
            }
            xNew = unit.PositionX;
            yNew = unit.PositionY;
            for (int i = 0; i <= _size; i++)
            {
                yNew = yNew - 1;
                if (!_unit.Buffs.Any(p => p.Name == "SP_Mirana_MoonlightShadow"))
                {
                    Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (call == null || call != null && call.Block)
                        break;
                }
                _callsPointl.Add(new Point(xNew, yNew));
            }
            xNew = unit.PositionX;
            yNew = unit.PositionY;
            for (int i = 0; i <= _size; i++)
            {
                yNew = yNew + 1;
                if (!_unit.Buffs.Any(p => p.Name == "SP_Mirana_MoonlightShadow"))
                {
                    Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (call == null || call != null && call.Block)
                        break;
                }
                _callsPointl.Add(new Point(xNew, yNew));
            }
        }

        private void AttackUnits(Map map, IUnit unit, bool attackCount)
        {
            ///Кординаты ячеек есть
            ///теперь спауним атаку
            foreach (var item in _callsPointl)
            {
                Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == item.X && p.IndexTop == item.Y);
                if (call != null &&
                    !call.Block)
                {
                    ///Создаем визуальный объект атака
                    UC_Mirana_Starstorm dualBreath = new UC_Mirana_Starstorm();
                    dualBreath.ChengAngel(unit.Angel);

                    _bullDualBreath = new Bullet() { Name = "SP_Mirana_Starstorm" };
                    _bullDualBreath.GameObject = new Game_Object_In_Call()
                    {
                        EnumCallType = EnumCallType.Bullet,
                        View = dualBreath
                    };
                    _bullDualBreath.UnitUsed = unit;
                    _bullDualBreath.PositionX = (int)item.X;
                    _bullDualBreath.PositionY = (int)item.Y;
                    _bullDualBreath.Speed = Speed;
                    ///Магический урон зависит от прокача стрел
                    //bullArrow.DemageMagic = 5 * (int)property;

                    if (LevelCast == 1)
                    {
                        _bullDualBreath.DemageMagic = 16;
               
                    }
                    else if (LevelCast == 2)
                    {
                        _bullDualBreath.DemageMagic = 26;
                     
                    }
                    else if (LevelCast == 3)
                    {
                        _bullDualBreath.DemageMagic = 36;
                     
                    }

                    if (!attackCount)
                    {
                        _bullDualBreath.DemageMagic = _bullDualBreath.DemageMagic / 2;
                    }

                    _bullDualBreath.CurrentMap = map;
                    _bullDualBreath.Angel = unit.Angel;
                    _bullDualBreath.Range = _unit.Range;
                    _bullDualBreath.Duration = Duration;
                    //_bullDualBreath.SpeedSlow = 0.5;

                    ///И его же добавим в масив всех объектов
                    //map.GameObjectInCall.Add(_bullDualBreath.GameObject);

                    Canvas.SetLeft(_bullDualBreath.GameObject.View, _bullDualBreath.PositionX * 50);
                    Canvas.SetTop(_bullDualBreath.GameObject.View, _bullDualBreath.PositionY * 50);
                    ///Отображение
                    map.MapCanvas.Children.Add(_bullDualBreath.GameObject.View);
                    _attackView.Add(dualBreath);
                    _animationControl = (_bullDualBreath.GameObject.View as IAnimationControl);
                    _animationControl.StartMuveAnimation(0);

                    UnitGenerator.AddDamage(call, _bullDualBreath);


                }
            }
        }

        void _secondTimer_Completed(object sender, object e)
        {
            if (!_removeFire)
            {
                _removeFire = true;
                ///Тут кординаты ячеек в которых действует тучка
                _callsPointl = new List<Point>();
                ///Получаем ячейки которые находятся перед героем
                ///использывавшим тучку
                ///Сначала добавляем ячеку с героем
                int xNew = _unit.PositionX;
                int yNew = _unit.PositionY;

                for (int i = 0; i < 1; i++)
                {
                    xNew = xNew - 1;
                    Map_Cell call = _map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (call == null || call != null && call.Block)
                        break;
                    _callsPointl.Add(new Point(xNew, yNew));
                }
                xNew = _unit.PositionX;
                yNew = _unit.PositionY;
                for (int i = 0; i < 1; i++)
                {
                    xNew = xNew + 1;
                    Map_Cell call = _map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (call == null || call != null && call.Block)
                        break;
                    _callsPointl.Add(new Point(xNew, yNew));
                }
                xNew = _unit.PositionX;
                yNew = _unit.PositionY;
                for (int i = 0; i < 1; i++)
                {
                    yNew = yNew - 1;
                    Map_Cell call = _map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (call == null || call != null && call.Block)
                        break;
                    _callsPointl.Add(new Point(xNew, yNew));
                }
                xNew = _unit.PositionX;
                yNew = _unit.PositionY;
                for (int i = 0; i < 1; i++)
                {
                    yNew = yNew + 1;
                    Map_Cell call = _map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (call == null || call != null && call.Block)
                        break;
                    _callsPointl.Add(new Point(xNew, yNew));
                }

                _attackView = new List<UC_Mirana_Starstorm>();
                AttackUnits(_map, _unit, false);

                _secondTimer.Begin();
            }
            else
            {
                if (_secondTimer != null)
                {
                    _secondTimer.Completed -= _secondTimer_Completed;
                    _secondTimer = null;
                }

                foreach (var item in _attackView)
                {
                    _map.MapCanvas.Children.Remove(item);
                }
            }
        }

        void _firstTimer_Completed(object sender, object e)
        {
            _culdaunBool = false;
            _firstTimer.Completed -= _firstTimer_Completed;
            _firstTimer = null;
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
            if (_animationControl != null)
                _animationControl.Pause();

            if (_firstTimer != null)
                _firstTimer.Pause();
            if (_secondTimer != null)
                _secondTimer.Pause();
            if (_therdTimer != null)
                _therdTimer.Pause();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        public void StopPause()
        {
            Paused = false;
            if (_animationControl != null)
                _animationControl.StopPause();

            if (_firstTimer != null)
                _firstTimer.Resume();
            if (_secondTimer != null)
                _secondTimer.Resume();
            if (_therdTimer != null)
                _therdTimer.Resume();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        #endregion
    }
}
