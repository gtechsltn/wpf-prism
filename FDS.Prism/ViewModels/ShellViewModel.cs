using FDS.Prism.Common.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDS.Prism.ViewModels
{
    [Export]
    public class ShellViewModel : ViewModelBase
    {
        public ShellViewModel() : base("Shell")
        {

        }
    }
}
