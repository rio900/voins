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
    public class SP_Item_AssaultCuirass : ISpell, IAura
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            MinCd = true,
            Description = "Assault Cuirass Demage",
            LevelDescription = ""
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

        int _manaCost = 10;
        public int ManaCost { get { return _manaCost + LevelCast * 5; } set { _manaCost = value; } }

        double _culdaun = 1;
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

        bool _stop = false;
        public bool Stop { get { return _stop; } set { _stop = value; } }

        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;
        IAnimationControl _animationControl;
        int _minusArmor = 5;
        int _armor = 4;
        double _attackSpeed = 0.1;
        public SP_Item_AssaultCuirass()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Alchemist_AcidSpray", this);
        }
        Map _map;
        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            _unit = unit;
            _map = map;

            if (!_culdaunBool && !Paused )
            {
                ///Флаг кулдауна
                _culdaunBool = true;

                ///Тут кординаты ячеек в которых действует тучка
                List<Point> callsPoint = new List<Point>();
                ///Получаем ячейки которые находятся перед героем
                ///использывавшим тучку
                ///Сначала добавляем ячеку с героем
                callsPoint.Add(new Point(unit.PositionX, unit.PositionY));
                int xNew = unit.PositionX;
                int yNew = unit.PositionY;

                for (int i = 0; i <= _size; i++)
                {
                    xNew = xNew - 1;
                    callsPoint.Add(new Point(xNew, yNew));
                    callsPoint.Add(new Point(xNew, yNew + 1));
                    callsPoint.Add(new Point(xNew, yNew - 1));
                }
                xNew = unit.PositionX;
                yNew = unit.PositionY;
                for (int i = 0; i <= _size; i++)
                {
                    xNew = xNew + 1;
                    callsPoint.Add(new Point(xNew, yNew));
                    callsPoint.Add(new Point(xNew, yNew + 1));
                    // callsPoint.Add(new Point(xNew, yNew + 2));
                    callsPoint.Add(new Point(xNew, yNew - 1));
                    //  callsPoint.Add(new Point(xNew, yNew - 2));
                }
                xNew = unit.PositionX;
                yNew = unit.PositionY;
                for (int i = 0; i <= _size; i++)
                {
                    yNew = yNew - 1;
                    callsPoint.Add(new Point(xNew, yNew));
                    if (i != 0)
                    {
                        callsPoint.Add(new Point(xNew + 1, yNew));
                        callsPoint.Add(new Point(xNew - 1, yNew));
                    }
                }
                xNew = unit.PositionX;
                yNew = unit.PositionY;
                for (int i = 0; i <= _size; i++)
                {
                    yNew = yNew + 1;
                    callsPoint.Add(new Point(xNew, yNew));
                    if (i != 0)
                    {
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
                       // UC_Alchemist_AcidSpray acidSpray = new UC_Alchemist_AcidSpray();
                      //  acidSpray.ChengAngel(unit.Angel);
                        for (int i = 0; i < call.IUnits.Count; i++)
                        {
                            var itemUnit = call.IUnits[i];
                            if (itemUnit != null && itemUnit.GroupType != _unit.GroupType)
                            {
                                //item.GatDamage(_bullet.DemagePhys, _bullet.DemageMagic, _bullet.DemagePure, _unit);

                                Buff buff = itemUnit.Buffs.FirstOrDefault(p => p.Name == "AssaultCuirass");
                                if (buff != null)
                                {
                                    buff.Duration = 2;
                                }
                                else
                                {
                                    Buff alchBuff = new Buff()
                                    {
                                        MinusArmor = _minusArmor,
                                        Duration = 2,
                                        Name = "AssaultCuirass"
                                    };
                                    itemUnit.UseBuff(alchBuff);
                                }
                            }
                            else if (itemUnit != null && itemUnit.GroupType == _unit.GroupType)
                            {
                                Buff buff = itemUnit.Buffs.FirstOrDefault(p => p.Name == "AssaultCuirassPositive");
                                if (buff != null)
                                {
                                    buff.Duration = 2;
                                }
                                else
                                {
                                    Buff alchBuff = new Buff()
                                    {
                                        Armor = _armor,
                                        AttackSpeed = _attackSpeed,
                                        Duration = 2,
                                        Name = "AssaultCuirassPositive"
                                    };
                                    itemUnit.UseBuff(alchBuff);
                                }
                            }
                        }
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

        public void StartUseAura(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            if (!Stop)
                UseSpall(map, obj, unit, property);
        }

        public void StopUseAura()
        {
            Stop = true;
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
            if (!Stop)
                UseSpall(_map, null, _unit, null);
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
