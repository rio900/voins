using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voins.AppCode
{
    /// <summary>
    /// Нанесен урон юниту
    /// </summary>
    /// <param name="demagedUnit">Кому нанесен урон</param>
    public delegate void GetDemageDelegate(int phisic, int magic, int pure, IUnit demagedUnit);
    public interface IUnit
    {
        event GetDemageDelegate GetDemageEvent;
        event EventHandler PausedEvent;
        event EventHandler DeadEvent;
        
        Game_Object_In_Call GameObject { get; set; }
        List<ISpell> Spells { get; set; }
        /// <summary>
        /// Позитивные и негативные воздействия на героя
        /// </summary>
        List<Buff> Buffs { get; set; }
        
        bool Invisibility { get; set; }
        bool Silenced { get; set; }
        int Health { get; set; }
        int MaxHealth { get; set; }
        int OrijHealth { get; set; }
        int Mana { get; set; }
        int OrijMana { get; set; }
        int MaxMana { get; set; }
        bool Dead { get; set; }
        int GroupType { get; set; }
        EUnitType UnitType { get; set; }

        double OrijHealthRegeneration { get; set; }
        double HealthRegeneration { get; set; }

        int OrijManaRegeneration { get; set; }
        int ManaRegeneration { get; set; }

        double AttackSpeed { get; set; }
        double OrijAttackSpeed { get; set; }

        int Arrmor { get; set; }
        int MagicArrmor { get; set; }

        int OrijDemage { get; set; }
        int Range { get; set; }
        int Demage { get; set; }
        double OrijSpeed { get; set; }
        double Speed { get; set; }
        int PositionX { get; set; }
        int PositionY { get; set; }
        Map CurrentMap { get; set; }
        int DemageStart { get; set; }
        int DemageItem { get; set; }
        bool Paused { get; set; }
        
        /// <summary>
        /// Неуязвимость
        /// </summary>
        bool Invulnerability { get; set; }

        int NGamePoint { get; set; }


        int Kills { get; set; }
        int Death { get; set; }
        int Asists { get; set; }

        /// <summary>
        /// Список ячеек в которых прежде был юнит
        /// </summary>
        List<Map_Cell> Way { get; set; }

        IAI AI { get; set; }

        /// <summary>
        /// Угол поворота юнита
        /// </summary>
        EAngel Angel { get; set; }
        /// <summary>
        /// Юнит использует какой то спел и не может воспринимать команды
        /// на другие спелы
        /// </summary>
        bool UnitFrozen { get; set; }
        /// <summary>
        /// Юнит в стане
        /// </summary>
        bool IsUnitStun { get; set; }

        /// <summary>
        /// Использовать заклинание и имя
        /// </summary>
        void UseSpall(string name, object obj);
        /// <summary>
        /// Получение урона
        /// </summary>
        /// <param name="phisic">физический</param>
        /// <param name="magic">магический</param>
        /// <param name="pure">чистый</param>
        /// <param name="demagedUnit">Юнит который наносит урон</param>
        IUnit GatDamage(int phisic, int magic, int pure, IUnit demagedUnit);

        /// <summary>
        /// Удаление юнита
        /// </summary>
        /// <returns></returns>
        IUnit RemoveUnit(IUnit demagedUnit);
        #region Награды за убийство
        int NGold { get; set; }
        int NExp { get; set; }
        ItemClass NItem { get; set; }
        #endregion

        /// <summary>
        /// Таймеры регенерации хп и маны
        /// </summary>
        void StartRegenerationTimers_NewObject();

        /// <summary>
        /// Сразу использовать баф, без ожидания таймера
        /// </summary>
        /// <param name="buff"></param>
        void UseBuff(Buff buff);


        /// <summary>
        /// Удалить баф по имени
        /// </summary>
        /// <param name="buff">Название бафа</param>
        void RemoveBuff(string buff);

        void Pause();
        void StopPause();
        void DeleteUnit();
    }
}
