using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Voins.AppCode
{
    public class ItemClass
    {
        private bool _bonus;

        public bool Bonus
        {
            get { return _bonus; }
            set { _bonus = value; }
        }

        private bool _recept;

        public bool Recept
        {
            get { return _recept; }
            set { _recept = value; }
        }

        private bool _itemUsed;

        public bool ItemUsed
        {
            get { return _itemUsed; }
            set { _itemUsed = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private bool _boots;

        public bool Boots
        {
            get { return _boots; }
            set { _boots = value; }
        }

        private bool _sellForFullPrice;
        /// <summary>
        /// Если можно продать предмет за полную цену
        /// </summary>
        public bool SellForFullPrice
        {
            get { return _sellForFullPrice; }
            set { _sellForFullPrice = value; }
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

        private double _manaRegen;

        public double ManaRegen
        {
            get { return _manaRegen; }
            set { _manaRegen = value; }
        }

        private int _healthRegen;

        public int HealthRegen
        {
            get { return _healthRegen; }
            set { _healthRegen = value; }
        }

        private int _healthBonus;

        public int HealthBonus
        {
            get { return _healthBonus; }
            set { _healthBonus = value; }
        }

        private int _manaBonus;
        public int ManaBonus
        {
            get { return _manaBonus; }
            set { _manaBonus = value; }
        }

        private int _strength;
        public int Strength
        {
            get { return _strength; }
            set { _strength = value; }
        }

        private int _agility;
        public int Agility
        {
            get { return _agility; }
            set { _agility = value; }
        }

        private int _intelligence;
        public int Intelligence
        {
            get { return _intelligence; }
            set { _intelligence = value; }
        }

        private int _demage;
        public int Demage
        {
            get { return _demage; }
            set { _demage = value; }
        }
        private int _armor;
        public int Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }
        

        private double _attackSpeed;
        public double AttackSpeed
        {
            get { return _attackSpeed; }
            set { _attackSpeed = value; }
        }
    
        private double _speed;
        public double Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        private UC_View_ItemImage _view;
        public UC_View_ItemImage View
        {
            get { return _view; }
            set { _view = value; }
        }

        private ItemDescriptionClassInfo _info = new ItemDescriptionClassInfo();
        public ItemDescriptionClassInfo Info
        {
            get { return _info; }
            set { _info = value; }
        }

        private List<ItemClass> _parts;
        /// <summary>
        /// Части с которых соберается данный итем
        /// </summary>
        public List<ItemClass> Parts
        {
            get { return _parts; }
            set { _parts = value; }
        }

        private ISpell _spell;
        public ISpell SpellItem
        {
            get { return _spell; }
            set { _spell = value; }
        }

        private Buff _buff;
        public Buff Buff
        {
            get { return _buff; }
            set { _buff = value; }
        }

        private int _price;
        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public void ItemPosition()
        {
            Canvas.SetZIndex(View, -1);
            Canvas.SetLeft(View, PositionX * 50);
            Canvas.SetTop(View, PositionY * 50);
        }

        public void Pause()
        {
            if (SpellItem != null)
                SpellItem.Pause();
        }
        public void StopPause()
        {
            if (SpellItem != null)
                SpellItem.StopPause();
        }

        public double BonusMagicDemage { get; set; }

        public Spell.SP_Item_AssaultCuirass AuraItem { get; set; }
    }
}
