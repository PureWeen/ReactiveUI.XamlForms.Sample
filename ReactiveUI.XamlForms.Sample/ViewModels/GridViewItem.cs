using System;
using System.Collections.Generic;
using System.Text;

namespace ReactiveUI.XamlForms.Sample.ViewModels
{
    public class GridViewItem : ReactiveObject
    {
        public int Index { get; private set;  }


        public GridViewItem(int index)
        {
            Index = index;
        }
    }
}
