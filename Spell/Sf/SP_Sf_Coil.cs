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
    public class SP_Sf_Coil : ISpell
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

        int _manaCost = 10;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 6;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;

        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 0;

        public double Duration { get { return _duration; } set { _duration = value; } }

        int _coilRange = 1;
        public int CoilRange { get { return _coilRange; } set { _coilRange = value; } }

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
        List<Point> _callsPointl;
        List<Bullet> _boom = new List<Bullet>();
        int _damageMagic;
        Bullet _bullDualBreath;

        Map _map;

        public SP_Sf_Coil(int coilRange)
        {
            _coilRange = coilRange;

            if (_coilRange == 1)
                _imageTile = new UC_View_ImageTileControl("SP_Sf_Coil", this);
            else
                _imageTile = new UC_View_ImageTileControl("SP_Sf_Coil2", this);

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
                        for (int i = 0; i <= CoilRange; i++)
                        {
                            xNew = xNew - 1;
                            Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                            if (call == null || call != null && call.Block)
                                break;
                            _callsPointl.Add(new Point(xNew, yNew));
                        }
                    }
                    else if (unit.Angel == EAngel.Right)
                    {
                        for (int i = 0; i <= CoilRange; i++)
                        {
                            xNew = xNew + 1;
                            Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                            if (call == null || call != null && call.Block)
                                break;
                            _callsPointl.Add(new Point(xNew, yNew));
                        }
                    }
                    else if (unit.Angel == EAngel.Top)
                    {
                        for (int i = 0; i <= CoilRange; i++)
                        {
                            yNew = yNew - 1;
                            Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                            if (call == null || call != null && call.Block)
                                break;
                            _callsPointl.Add(new Point(xNew, yNew));
                        }
                    }
                    else if (unit.Angel == EAngel.Bottom)
                    {
                        for (int i = 0; i <= CoilRange; i++)
                        {
                            yNew = yNew + 1;
                            Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                            if (call == null || call != null && call.Block)
                                break;
                            _callsPointl.Add(new Point(xNew, yNew));
                        }
                    }

                    _bullDualBreath = new Bullet();
                    if (LevelCast == 1)
                    {
                        _damageMagic = 16;
                    }
                    else if (LevelCast == 2)
                    {
                        _damageMagic = 23;
                    }
                    else if (LevelCast == 3)
                    {
                        _damageMagic = 30;
                    }
                    _bullDualBreath.Angel = unit.Angel;

                    if (!_callsPointl.Any())
                        (unit.GameObject.View as IGameControl).GetDemage("Miss");
                    else
                    {
                        BoomMethod((int)_callsPointl.Last().X, (int)_callsPointl.Last().Y);
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

        /// <summary>
        /// Этот метод наносит урон
        /// </summary>
        private void BoomMethod(int x, int y)
        {
            ///Если колба попала или должна исчезнуть
            ///Берем все ячейки вокруг колбы
            List<Point> roundPoint = UnitGenerator.RoundNumber(x, y);
            roundPoint.Add(new Point(x, y));
            ///Теперь рисуем взрыв
            for (int i = 0; i < roundPoint.Count; i++)
            {
                Map_Cell callBoom = _map.Calls.FirstOrDefault(p => p.IndexLeft == roundPoint[i].X && p.IndexTop == roundPoint[i].Y);
                if (callBoom != null && !callBoom.Block)
                {
                    UC_Jinx_Zap_Roket arrow = new UC_Jinx_Zap_Roket();
                    arrow.ExplosionStyle = 1;

                    if (i == 0)
                        arrow.ChengAngel(EAngel.Left);
                    else if (i == 1)
                        arrow.ChengAngel(EAngel.Right);
                    else if (i == 2)
                        arrow.ChengAngel(EAngel.Top);
                    else if (i == 3)
                        arrow.ChengAngel(EAngel.Bottom);
                    else if (i == 4)
                        ///Центральная ячейка
                        arrow.ChengAngelCenter();

                    Bullet bullArrow = new Bullet();
                    bullArrow.GameObject = new Game_Object_In_Call()
                    {
                        EnumCallType = EnumCallType.Bullet,
                        View = arrow
                    };
                    bullArrow.UnitUsed = _unit;
                    bullArrow.PositionX = (int)roundPoint[i].X;
                    bullArrow.PositionY = (int)roundPoint[i].Y;
                    bullArrow.Speed = 0;
                    bullArrow.DemageMagic = _damageMagic;
                    bullArrow.CurrentMap = _map;
                    bullArrow.Angel = _unit.Angel;
                    bullArrow.Range = 0;

                    ///И его же добавим в масив всех объектов
                    _map.GameObjectInCall.Add(bullArrow.GameObject);
                    Canvas.SetLeft(bullArrow.GameObject.View, bullArrow.PositionX * 50);
                    Canvas.SetTop(bullArrow.GameObject.View, bullArrow.PositionY * 50);
                    ///Отображение
                    _map.MapCanvas.Children.Add(bullArrow.GameObject.View);

                    UnitGenerator.AddBufDemageOne(callBoom, bullArrow, bullArrow.DemageMagic);
                    //UnitGenerator.AddStune(callBoom, bullArrow, _bullet.StunDuration);

                    _boom.Add(bullArrow);
                }
            }

            _secondTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(0.35) };
            _secondTimer.Completed += _secondTimer_Completed;
            _secondTimer.Begin();
        }

        /// <summary>
        /// Таймер исчезновения взрыва
        /// </summary>
        void _secondTimer_Completed(object sender, object e)
        {
            if (_secondTimer != null)
            {
                _secondTimer.Completed -= _secondTimer_Completed;
                _secondTimer = null;

                foreach (var item in _boom)
                {
                    ///Удаляем взрыв
                    _map.GameObjectInCall.Add(item.GameObject);
                    (item.GameObject.View as IGameControl).Remove(_map.MapCanvas);
                }
                _boom.Clear();
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
