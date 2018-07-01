using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Voins.AppCode
{
    public class Map_Cell
    {
        #region Using and Used

        private bool _using;
        /// <summary>
        /// Если ячейка будет использоватся
        /// (в нее кто то перемещается)
        /// </summary>
        public bool Using
        {
            get { return _using; }
            set { _using = value; }
        }
        private bool _used;
        /// <summary>
        /// Если в ячейке кто то есть
        /// (уже занята в ней кто то находится)
        /// </summary>
        public bool Used
        {
            get { return _used; }
            set { _used = value; }
        }
        #endregion

        private int _indexTop;

        public int IndexTop
        {
            get { return _indexTop; }
            set { _indexTop = value; }
        }

        private int _indexLeft;

        public int IndexLeft
        {
            get { return _indexLeft; }
            set { _indexLeft = value; }
        }

        private Canvas _callControl;

        public Canvas CallControl
        {
            get { return _callControl; }
            set { _callControl = value; }
        }

        private List<IUnit> _iUnit = new List<IUnit>();

        public List<IUnit> IUnits
        {
            get { return _iUnit; }
            set { _iUnit = value; }
        }

        private List<Bullet> _bullet = new List<Bullet>();

        public List<Bullet> Bullet
        {
            get { return _bullet; }
            set { _bullet = value; }
        }

        
        private List<ItemClass> _item= new List<ItemClass>();
        public List<ItemClass> Item
        {
            get { return _item; }
            set { _item = value; }
        }

        private bool _block;

        public bool Block
        {
            get { return _block; }
            set { _block = value; }
        }


        private EAngel _angel;
        /// <summary>
        /// На какой угол нужно развернуть юнит чтобы переместится сюда
        /// используется для метода RandonCell
        /// </summary>
        public EAngel Angel
        {
            get { return _angel; }
            set { _angel = value; }
        }
    }
}
