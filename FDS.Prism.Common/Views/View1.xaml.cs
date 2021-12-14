using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace FDS.Prism.Common.Views
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class View1 : Control
    {
        public View1()
        {
            this.DefaultStyleKey = typeof(View1);
        }
    }
}
