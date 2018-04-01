using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrintPDV.UI
{
    public interface IWizardStep
    {
        UserControl Content { get; }
        
        void Load();
        
        void Save();
        
        void Cancel();
        
        bool IsBusy { get; }

        bool PageValid { get; }
        
        string ValidationMessage { get; }
    }
}
