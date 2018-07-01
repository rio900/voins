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
    public class SPB_Alchemist_AcidSpray : ISpell
    {
        /// <summary>
        /// Событие для тестирования спелов
        /// </summary>
        public event EventHandler E_TestEvent;
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo();
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        public string Name { get; set; }
        Bullet _bullet;
        IUnit _unit;
        Map _map;
        int _xNew = 0;
        int _yNew = 0;
        /// <summary>
        /// Если попадение то true
        /// </summary>
        bool _exept = false;
        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }
        double _duration = 0;
        public double Duration { get { return _duration; } set { _duration = value; } }
        int _levelCast = 1;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 1;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }
        bool _isUlt = false;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }
        Storyboard _storyboard;
        DoubleAnimation _animation;
        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }
        Map_Cell _oldCall;
        public Storyboard _firstTimer;
        public Storyboard _secondTimer;

        public SPB_Alchemist_AcidSpray()
        {
            _imageTile = new UC_View_ImageTileControl("SPB_FireArrow", this);
        }

        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            _bullet = property as Bullet;
            _map = map;
            _unit = _bullet.UnitUsed;
            Duration = _bullet.Duration;
            _bullet.BuffDemage = true;
            ///Ячейка где действует тучка
            _oldCall = map.Calls.Single(p => p.IndexLeft == _bullet.PositionX &&
                p.IndexTop == _bullet.PositionY);
            _oldCall.Bullet.Add(_bullet);

            ///Таймер время жизни тучки
            _firstTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(1) };
            _firstTimer.Completed += _firstTimer_Completed; 
            _firstTimer.Begin();

            if (Paused)
                Pause();
        }

        void _firstTimer_Completed(object sender, object e)
        {

            if (Duration-- >= 0)
            {
                UnitGenerator.AddBufDemageOne(_oldCall, _bullet, _bullet.DemageMagic);

                for (int i = 0; i < _oldCall.IUnits.Count; i++)
                {
                    var item = _oldCall.IUnits[i];
                    if (item != null && item.GroupType != _unit.GroupType)
                    {
                        //item.GatDamage(_bullet.DemagePhys, _bullet.DemageMagic, _bullet.DemagePure, _unit);

                        Buff buff = item.Buffs.FirstOrDefault(p => p.Name == "AcidSpray");
                        if (buff != null)
                        {
                            buff.Duration = 2;
                        }
                        else
                        {
                            Buff alchBuff = new Buff()
                            {
                                MinusArmor = _bullet.MinusArmor,
                                Duration = 2,
                                Name = "AcidSpray"
                            };
                            item.Buffs.Add(alchBuff);
                        }
                    }
                }
                _firstTimer.Begin();
            }
            else
            {
                _firstTimer.Completed -= _firstTimer_Completed;
                _firstTimer = null;
                ///Удаление тучки
                (_bullet.GameObject.View as IGameControl).Remove(_map.MapCanvas);
                ///Удаляем из масива всех объектов
                _map.GameObjectInCall.Remove(_bullet.GameObject);
                _oldCall.Bullet.Remove(_bullet);
            }

        }


        /// <summary>
        /// Изучить способность хоть один раз
        /// </summary>
        public void UpSpell(Player player)
        {
            LevelCast += 1;
            ImageTile.UpSpell();
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
            if (_storyboard != null)
                _storyboard.Pause();

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
            if (_storyboard != null)
                _storyboard.Resume();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        #endregion

        public int ManaCost
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public int HelthCost
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public double Culdaun
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
