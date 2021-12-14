using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace FDS.Prism.Common.Views
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class View2 : Control
    {
        public View2()
        {
            this.DefaultStyleKey = typeof(View2);
        }
    }
}
