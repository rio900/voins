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
    public class SP_Jakiro_Ice_Path : ISpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Dual Breath, Culdaun 8 sec. Duration 6 sec., Demage type: physical",
            LevelDescription =
            "Level1: Demage per second - 2, Minus armor - 4, Mana cost - 15" + Environment.NewLine +
            "Level2: Demage per second - 3, Minus armor - 6, Mana cost - 20" + Environment.NewLine +
            "Level3: Demage per second - 4, Minus armor - 8, Mana cost - 25"
        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        int _size = 2;
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

        int _manaCost = 15;
        public int ManaCost { get { return _manaCost + LevelCast * 5; } set { _manaCost = value; } }

        double _culdaun = 10;
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
        IAnimationControl _animationControl;
        List<Point> _callsPointl;
        Bullet _bullDualBreath;
        Map _map;

        public SP_Jakiro_Ice_Path()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Jakiro_Ice_Path", this);
        }

        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);
            _unit = unit;
            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 && !upSpell && 
                !unit.Silenced &&
                !unit.Hexed &&
                !Paused)
            {
                if (unit.Mana >= ManaCost)
                ///Проверка есть ли мана на каст
                {
                    ///Флаг кулдауна
                    _culdaunBool = true;

                    ///Отнимаем нужное количество
                    unit.Mana -= ManaCost;
                    _map = map;

                    ///Тут кординаты ячеек в которых действует тучка
                    _callsPointl = new List<Point>();
                    ///Получаем ячейки которые находятся перед героем
                    ///использывавшим тучку
                    ///Сначала добавляем ячеку с героем
                    int xNew = unit.PositionX;
                    int yNew = unit.PositionY;
                    if (unit.Angel == EAngel.Left)
                    {
                        for (int i = 0; i <= _size; i++)
                        {
                            xNew = xNew - 1;
                            //Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                            //if (call == null || call != null && call.Block)
                            //    break;
                            _callsPointl.Add(new Point(xNew, yNew));
                        }
                    }
                    else if (unit.Angel == EAngel.Right)
                    {
                        for (int i = 0; i <= _size; i++)
                        {
                            xNew = xNew + 1;
                            //Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                            //if (call == null || call != null && call.Block)
                            //    break;
                            _callsPointl.Add(new Point(xNew, yNew));
                        }
                    }
                    else if (unit.Angel == EAngel.Top)
                    {
                        for (int i = 0; i <= _size; i++)
                        {
                            yNew = yNew - 1;
                            //Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                            //if (call == null || call != null && call.Block)
                            //    break;
                            _callsPointl.Add(new Point(xNew, yNew));
                        }
                    }
                    else if (unit.Angel == EAngel.Bottom)
                    {
                        for (int i = 0; i <= _size; i++)
                        {
                            yNew = yNew + 1;
                            //Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                            //if (call == null || call != null && call.Block)
                            //    break;
                            _callsPointl.Add(new Point(xNew, yNew));
                        }
                    }

                    if (!_callsPointl.Any())
                        (unit.GameObject.View as IGameControl).GetDemage("Miss");

                    ///Кординаты ячеек есть
                    ///теперь спауним атаку
                    foreach (var item in _callsPointl)
                    {
                        Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == item.X && p.IndexTop == item.Y);
                        if (call != null &&
                            !call.Block)
                        {
                            ///Создаем визуальный объект атака
                            UC_Jakiro_IcePath dualBreath = new UC_Jakiro_IcePath();
                            dualBreath.ChengAngel(unit.Angel);

                            _bullDualBreath = new Bullet() { Name = "SP_Jakiro_Ice_Path" };
                            _bullDualBreath.GameObject = new Game_Object_In_Call()
                            {
                                EnumCallType = EnumCallType.Bullet,
                                View = dualBreath
                            };
                            _bullDualBreath.UnitUsed = unit;
                            _bullDualBreath.PositionX = (int)item.X;
                            _bullDualBreath.PositionY = (int)item.Y;
                            _bullDualBreath.Speed = Speed;

                            if (LevelCast > 1)
                                _bullDualBreath.StunDuration = 2;
                            else
                                _bullDualBreath.StunDuration = 1;
                            if (LevelCast > 2)
                                Duration = 3;

                            if (LevelCast == 1)
                                _bullDualBreath.DemageMagic = 2;
                            else if (LevelCast == 2)
                                _bullDualBreath.DemageMagic = 4;
                            else if (LevelCast == 3)
                                _bullDualBreath.DemageMagic = 6;

                           // _bullDualBreath.DemageMagic += unit.Demage;

                            _bullDualBreath.CurrentMap = map;
                            _bullDualBreath.Angel = unit.Angel;
                            _bullDualBreath.Range = _unit.Range;
                            _bullDualBreath.Duration = Duration;

                            ///И его же добавим в масив всех объектов
                            //map.GameObjectInCall.Add(_bullDualBreath.GameObject);
                            _bullDualBreath.Spells.Add(new SPB_Jakiro_Ice_Path() { Name = "Ice Path" });
                            Canvas.SetLeft(_bullDualBreath.GameObject.View, _bullDualBreath.PositionX * 50);
                            Canvas.SetTop(_bullDualBreath.GameObject.View, _bullDualBreath.PositionY * 50);
                            ///Отображение
                            map.MapCanvas.Children.Add(_bullDualBreath.GameObject.View);
                            _animationControl = (_bullDualBreath.GameObject.View as IAnimationControl);
                            _animationControl.StartMuveAnimation(0);
                            _bullDualBreath.UseSpall("Ice Path");

                            UnitGenerator.AddDamage(call, _bullDualBreath);
                        }
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

        void _secondTimer_Completed(object sender, object e)
        {
            _culdaunBool = false;
            if (_secondTimer != null)
            {
                _secondTimer.Completed -= _secondTimer_Completed;
                _secondTimer = null;
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

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        #endregion
    }
}
