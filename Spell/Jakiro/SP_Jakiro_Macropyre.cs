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
    public class SP_Jakiro_Macropyre : ISpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Alchemist Acid Spray, Culdaun 8 sec. Duration 6 sec., Demage type: physical",
            LevelDescription =
            "Level1: Demage per second - 15, Mana cost - 15" + Environment.NewLine
        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        int _size = 3;
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

        int _manaCost = 50;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 20;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;

        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 5;

        public double Duration { get { return _duration; } set { _duration = value; } }

        int _levelCast = 0;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 1;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }

        bool _isUlt = true;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }

        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;
        IAnimationControl _animationControl;

        public SP_Jakiro_Macropyre()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Jakiro_Macropyre", this);
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

                    ///Тут кординаты ячеек в которых действует тучка
                    List<Point> callsPoint = new List<Point>();
                    ///Получаем ячейки которые находятся перед героем
                    int xNew = unit.PositionX;
                    int yNew = unit.PositionY;
                    if (unit.Angel == EAngel.Left)
                    {
                        for (int i = 0; i <= _size; i++)
                        {
                            xNew = xNew - 1;
                            callsPoint.Add(new Point(xNew, yNew));
                            callsPoint.Add(new Point(xNew, yNew + 1));
                            callsPoint.Add(new Point(xNew, yNew - 1));
                        }
                    }
                    else if (unit.Angel == EAngel.Right)
                    {
                        for (int i = 0; i <= _size; i++)
                        {
                            xNew = xNew + 1;
                            callsPoint.Add(new Point(xNew, yNew));
                            callsPoint.Add(new Point(xNew, yNew + 1));
                            callsPoint.Add(new Point(xNew, yNew - 1));
                        }
                    }
                    else if (unit.Angel == EAngel.Top)
                    {
                        for (int i = 0; i <= _size; i++)
                        {
                            yNew = yNew - 1;
                            callsPoint.Add(new Point(xNew, yNew));
                            callsPoint.Add(new Point(xNew + 1, yNew));
                            callsPoint.Add(new Point(xNew - 1, yNew));
                        }
                    }
                    else if (unit.Angel == EAngel.Bottom)
                    {
                        for (int i = 0; i <= _size; i++)
                        {
                            yNew = yNew + 1;
                            callsPoint.Add(new Point(xNew, yNew));
                            callsPoint.Add(new Point(xNew + 1, yNew));
                            callsPoint.Add(new Point(xNew - 1, yNew));
                        }
                    }

                    ///Кординаты ячеек есть
                    ///теперь спауним тучку
                    foreach (var item in callsPoint)
                    {
                        Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == item.X && p.IndexTop == item.Y);
                        if (call != null &&
                            !call.Block)
                        {
                            ///Создаем визуальный объект тучка
                            UC_Jakiro_Macropyre acidSpray = new UC_Jakiro_Macropyre();
                            acidSpray.ChengAngel(unit.Angel);

                            Bullet bullAcidSpray = new Bullet() { Name = "SP_Jakiro_Macropyre" };
                            bullAcidSpray.GameObject = new Game_Object_In_Call()
                            {
                                EnumCallType = EnumCallType.Bullet,
                                View = acidSpray
                            };
                            bullAcidSpray.UnitUsed = unit;
                            bullAcidSpray.PositionX = (int)item.X;
                            bullAcidSpray.PositionY = (int)item.Y;
                            bullAcidSpray.Speed = Speed;

                            bullAcidSpray.DemageMagic = 30;
                            bullAcidSpray.Duration = Duration;

                            bullAcidSpray.CurrentMap = map;
                            bullAcidSpray.Angel = unit.Angel;
                            bullAcidSpray.Range = _unit.Range;

                            /// Если есть аганим
                            if (UnitGenerator.HasAghanim(unit))
                            {
                                bullAcidSpray.DemageMagic += 20; // 50
                                bullAcidSpray.Duration += 2;
                            }

                            bullAcidSpray.Spells.Add(new SPB_Alchemist_AcidSpray() { Name = "AcidSpray" });

                            ///И его же добавим в масив всех объектов
                            map.GameObjectInCall.Add(bullAcidSpray.GameObject);

                            Canvas.SetLeft(bullAcidSpray.GameObject.View, bullAcidSpray.PositionX * 50);
                            Canvas.SetTop(bullAcidSpray.GameObject.View, bullAcidSpray.PositionY * 50);
                            ///Отображение
                            map.MapCanvas.Children.Add(bullAcidSpray.GameObject.View);
                            _animationControl = (bullAcidSpray.GameObject.View as IAnimationControl);
                            _animationControl.StartMuveAnimation(0);

                            bullAcidSpray.UseSpall("AcidSpray");
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

            if (unit.UnitFrozen == false ///Проверка не в стане ли юнит
                && !Paused)
            {
                Map_Cell unitCall = map.Calls.FirstOrDefault(p => p.IndexLeft == unit.PositionX && p.IndexTop == unit.PositionY);
                ///Может тучка была кинута, тогда можно атаковать
                if (unitCall.Bullet.Any(p => p.Name == "SP_Alchemist_AcidSpray" && p.UnitUsed == unit) &&
                    !CuldaunBoolAttack)
                {
                    Point attackCallPoint = UnitGenerator.AngelCallPoint(unit.Angel, unit.PositionX, unit.PositionY);
                    Map_Cell attackCallCall = map.Calls.FirstOrDefault(p => p.IndexLeft == attackCallPoint.X && p.IndexTop == attackCallPoint.Y);
                    ///Если на против юнита есть ячейка куда атаковать
                    if (attackCallCall != null && !attackCallCall.Block && attackCallCall.IUnits.Any())
                    {
                        ///Флаг кулдауна
                        CuldaunBoolAttack = true;


                        Bullet bullet = new Bullet()
                        {
                            UnitUsed = unit,
                            DemagePhys = unit.Demage
                        };
                        UnitGenerator.MKB_Bush(bullet, unit);

                        UnitGenerator.AddDamage(attackCallCall, bullet);
                        (unit.GameObject.View as IGameControl).ShowAttack(unit.Angel, unit.AttackSpeed);

                        _secondTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(unit.AttackSpeed) };
                        _secondTimer.Completed += _secondTimer_Completed;
                        _secondTimer.Begin();

                        if (Paused)
                            Pause();
                    }
                }
            }
        }

        void _secondTimer_Completed(object sender, object e)
        {
            CuldaunBoolAttack = false;
            _secondTimer.Completed -= _secondTimer_Completed;
            _secondTimer = null;
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
