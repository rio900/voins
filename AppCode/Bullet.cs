using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voins.AppCode
{
    public class Bullet
    {
        public string Name { get; set; }

        private int _stunDuration = 0;
        /// <summary>
        /// Длительность стана
        /// </summary>
        public int StunDuration
        {
            get { return _stunDuration; }
            set { _stunDuration = value; }
        }

        private int _range = 1;
        /// <summary>
        /// Сколько осталось литеть пуле
        /// </summary>
        public int Range
        {
            get { return _range; }
            set { _range = value; }
        }

        private IUnit _unitUsed;

        public IUnit UnitUsed
        {
            get { return _unitUsed; }
            set { _unitUsed = value; }
        }

        private Game_Object_In_Call _gameObject;

        public Game_Object_In_Call GameObject
        {
            get { return _gameObject; }
            set { _gameObject = value; }
        }
        private EAngel _angel;
        /// <summary>
        /// Угол поворота снаряда
        /// </summary>
        public EAngel Angel
        {
            get { return _angel; }
            set { _angel = value; }
        }
        private int _orijDemage;
        public int OrijDemage
        {
            get { return _orijDemage; }
            set { _orijDemage = value; }
        }
        private int _demagePhys;
        public int DemagePhys
        {
            get { return _demagePhys; }
            set { _demagePhys = value; }
        }
        private int _demageMagic;
        public int DemageMagic
        {
            get { return _demageMagic; }
            set { _demageMagic = value; }
        }
        private int _demagePure;
        public int DemagePure
        {
            get { return _demagePure; }
            set { _demagePure = value; }
        }

        private int _minusArmor;
        public int MinusArmor
        {
            get { return _minusArmor; }
            set { _minusArmor = value; }
        }

        private int _orijSpeed;
        public int OrijSpeed
        {
            get { return _orijSpeed; }
            set { _orijSpeed = value; }
        }
        private bool _exept;
        /// <summary>
        /// Попадение пули
        /// </summary>
        public bool Exept
        {
            get { return _exept; }
            set { _exept = value; }
        }
        
        private double _speed = 1;
        /// <summary>
        /// Множетель на одну сикунду
        /// стандартный 1
        /// </summary>
        public double Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        private double _speedSlow;
        /// <summary>
        /// Замедление
        /// </summary>
        public double SpeedSlow
        {
            get { return _speedSlow; }
            set { _speedSlow = value; }
        }

        private double _attackSpeedSlow;
        /// <summary>
        /// Замедление атаки
        /// </summary>
        public double AttackSpeedSlow
        {
            get { return _attackSpeedSlow; }
            set { _attackSpeedSlow = value; }
        }

        private double _duration = 1;
        /// <summary>
        /// Длительность воздействия
        /// </summary>
        public double Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        private int _positionX;

        public int PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }

        private int _positionY;

        public int PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }
        public Map CurrentMap { get; set; }
        private List<ISpell> _spells = new List<ISpell>();
        /// <summary>
        /// Список способностей юнита
        /// </summary>
        public List<ISpell> Spells { get { return _spells; } set { _spells = value; } }

        private Buff _buff;
        public Buff Buff
        {
            get { return _buff; }
            set { _buff = value; }
        }

        private IUnit _aim;
        /// <summary>
        /// Тут хранится юнит на которого наведен каст
        /// </summary>
        public IUnit Aim
        {
            get { return _aim; }
            set { _aim = value; }
        }

        /// <summary>
        /// Использование способности
        /// </summary>
        /// <param name="name">Название способности</param>
        /// <param name="obj">Уникальные параметры, могут отсуцтвовать</param>
        public void UseSpall(string name)
        {
            var spall = Spells.Single(p => p.Name == name);
            spall.UseSpall(CurrentMap, null, null, this);
        }

        #region Pause
        bool _paused;
        public bool Paused { get { return _paused; } set { _paused = value; } }

        public void Pause()
        {
            Paused = true;
            foreach (var item in Spells)
                item.Pause();
        }
        public void StopPause()
        {
            Paused = false;
            foreach (var item in Spells)
                item.StopPause();
        }
        #endregion


        public int BonusDemage { get; set; }

        public bool BuffDemage { get; set; }

        public double Splash { get; set; }

        public bool IsRoket { get; set; }

        public int SproutHealth { get; set; }
        public int GrassDamage { get; internal set; }
        public IUnit RemoveUnit { get; internal set; }
        public int TrentDamage { get; internal set; }
        public int TrentArmor { get; internal set; }
        public int LifeTime { get; internal set; }
    }
}
