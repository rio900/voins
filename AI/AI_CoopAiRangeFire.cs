using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AppCode;
using Voins.Spell;
using Windows.UI.Xaml;

namespace Voins.AI
{
    /// <summary>
    /// Аи моба который будет гнатся за игроком если увидит
    /// </summary>
    public class AI_CoopAiRangeFire : IAI
    {
        public bool Farm { get; set; }

        public AI_CoopAiRangeFire()
        {
        }
        Map_Cell _call;
        DispatcherTimer _firstTimer;
        Map _currentMap;
        public Map CurrentMap
        {
            get
            {
                return _currentMap;
            }
            set
            {
                _currentMap = value;
            }
        }
        IUnit _currentUnit;
        public IUnit CurrentUnit
        {
            get
            {
                return _currentUnit;
            }
            set
            {
                _currentUnit = value;
            }
        }

        bool _stoped;
        public bool Stoped
        {
            get
            {
                return _stoped;
            }
            set
            {
                _stoped = value;
            }
        }

        bool _rotation = false;
        public bool Rotation { get { return _rotation; } set { _rotation = value; } }

        public void StartAI()
        {
            if (CurrentMap != null && CurrentUnit != null && !CurrentUnit.Dead)
            {
                Stoped = false;
                StartMuve();
            }
        }

        private void StartMuve()
        {
            if (CurrentUnit != null)
            {
                if (CurrentUnit.Dead)
                    StopAI();

                if (!Stoped)
                {
                    if (!Rotation)
                        ///Выбираем рандомный угол, проверяем или он пустой и перемещпемся туда
                        _call = UnitGenerator.RandonCell((int)DateTime.Now.Ticks,
                            CurrentUnit.PositionX, CurrentUnit.PositionY, CurrentMap, CurrentUnit.GroupType, CurrentUnit.Way, false);
                    else
                        Rotation = false;

                    if (_call != null && !_call.Using && !CurrentUnit.UnitFrozen)
                    {
                        if (_call.Used && _call.IUnits.Any(p =>
                             p.GroupType != CurrentUnit.GroupType))
                        {///Колонка занята врагом нужно атаковать его
                            SP_AttackEasyMob attackEasyMob = CurrentUnit.Spells.FirstOrDefault(p => p.GetType() == typeof(SP_AttackEasyMob)) as SP_AttackEasyMob;
                            if (attackEasyMob != null)
                            {
                                attackEasyMob.CompletedUseSpell += _attackEasyMob_CompletedUseSpell;
                                attackEasyMob.UseSpall(CurrentMap, null, CurrentUnit, _call);
                            }
                            else
                                StopAI();
                        }
                        else
                        {
                            ///Если колонка не занята перейти туда
                            SP_Move muveSpell = CurrentUnit.Spells.FirstOrDefault(p => p.GetType() == typeof(SP_Move)) as SP_Move;
                            if (muveSpell != null)
                            {
                                muveSpell.CompletedUseSpell += _muveSpell_CompletedUseSpell;

                                //Запускаем по игроку шарик
                                SP_EasyMobArrowFifeBall arrow = CurrentUnit.Spells.FirstOrDefault(p => p.GetType() == typeof(SP_EasyMobArrowFifeBall)) as SP_EasyMobArrowFifeBall;
                                arrow.UseSpall(CurrentMap, null, CurrentUnit,null);

                                muveSpell.UseSpall(CurrentMap, null, CurrentUnit, _call.Angel);
                            }
                            else
                                StopAI();
                        }
                    }
                    else
                    {   ///Нету куда ходить 
                        ///Подождем может освободится место
                        _firstTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
                        _firstTimer.Tick += _firstTimer_Tick;
                        _firstTimer.Start();
                    }
                }
            }
        }

        void _attackEasyMob_CompletedUseSpell(object sender, EventArgs e)
        {
            ((SP_AttackEasyMob)sender).CompletedUseSpell -= _attackEasyMob_CompletedUseSpell;
            Rotation = true;
            StartMuve();
        }

        void _firstTimer_Tick(object sender, object e)
        {
            if (_firstTimer != null)
            {
                _firstTimer.Stop();
                _firstTimer.Tick -= _firstTimer_Tick;
                StartMuve();
            }
        }

        void _muveSpell_CompletedUseSpell(object sender, EventArgs e)
        {
            ((SP_Move)sender).CompletedUseSpell -= _muveSpell_CompletedUseSpell;
            Rotation = ((SP_Move)sender).Rotation;



            StartMuve();
        }

        public void StopAI()
        {
            Stoped = true;
            if (_call != null)
            _call.Used = false;
            _call = null;
            CurrentUnit = null;
            CurrentMap = null;
            if (_firstTimer != null)
            {
                _firstTimer.Stop();
                _firstTimer = null;
            }
        }

        #region Pause
        public event EventHandler PausedEvent;
        bool _paused;
        public bool Paused { get { return _paused; } set { _paused = value; } }

        public void Pause()
        {
            Paused = true;

            if (_firstTimer != null)
                _firstTimer.Stop();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        public void StopPause()
        {
            Paused = false;

            if (_firstTimer != null)
                _firstTimer.Start();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        #endregion
    }
}
