using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Voins.AppCode
{
   public class Game_Object_In_Call
    {
       private FrameworkElement _view;

       public FrameworkElement View
        {
            get { return _view; }
            set { _view = value; }
        }

        private EnumCallType _enumCallType;

        public EnumCallType EnumCallType
        {
            get { return _enumCallType; }
            set { _enumCallType = value; }
        }


        
    }
}
