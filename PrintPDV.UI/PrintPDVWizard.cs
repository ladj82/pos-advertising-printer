using System;
using System.Linq;
using System.Windows.Forms;
using PrintPDV.LanguagePack;

namespace PrintPDV.UI
{
    public partial class PrintPDVWizard : Form
    {
        private const string VALIDATION_MESSAGE = "Erro:"; //TODO: put on resource file

        #region Properties

        public WizardPageCollection WizardPages { get; set; }
        
        public bool ShowLastButton
        {
            get { return !WizardPages.Any(x => x.Value.PageValid.Equals(false)); }
        }

        #endregion

        #region Delegates & Events

        public delegate void WizardCompletedEventHandler();
        
        public event WizardCompletedEventHandler WizardCompleted;

        #endregion

        #region Constructor & Window Event Handlers

        public PrintPDVWizard()
        {
            InitializeComponent();
            WizardPages = new WizardPageCollection();
            WizardPages.WizardPageLocationChanged += WizardPages_WizardPageLocationChanged;
        }

        void WizardPages_WizardPageLocationChanged(WizardPageLocationChangedEventArgs e)
        {
            LoadNextPage(e.PageIndex, e.PreviousPageIndex, true);
        }

        #endregion

        #region Private Methods

        private void NotifyWizardCompleted()
        {
            if (WizardCompleted == null) return;

            OnWizardCompleted();
            WizardCompleted();
        }
        
        private void OnWizardCompleted()
        {
            WizardPages.LastPage.Save();
            WizardPages.Reset();
            DialogResult = DialogResult.OK;
        }

        public void UpdateNavigation()
        {
            #region Reset

            btnNext.Enabled = true;
            btnNext.Visible = true;

            btnLast.Text = "Último >>"; //TODO: put on resource file
            
            btnLast.Enabled = ShowLastButton;

            #endregion

            bool canMoveNext = WizardPages.CanMoveNext;
            bool canMovePrevious = WizardPages.CanMovePrevious;

            btnPrevious.Enabled = canMovePrevious;
            btnFirst.Enabled = canMovePrevious;

            if (canMoveNext)
            {
                btnNext.Text = "Próximo >"; //TODO: put on resource file
                btnNext.Enabled = true;

                if (ShowLastButton)
                {
                    btnLast.Enabled = true;
                }
            }
            else
            {
                if (ShowLastButton)
                {
                    btnLast.Text = "Finalizar"; //TODO: put on resource file
                    btnNext.Visible = false;
                }
                else
                {
                    btnNext.Text = "Finalizar"; //TODO: put on resource file
                    btnNext.Visible = true;
                }
            }
        }

        private bool CheckPageIsValid()
        {
            if (WizardPages.CurrentPage.PageValid) return true;
            
            MessageBox.Show(
                string.Concat(VALIDATION_MESSAGE, Environment.NewLine, Environment.NewLine, WizardPages.CurrentPage.ValidationMessage),
                AppStrings.Generic_MessageBox_WarningTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            
            return false;
        }

        #endregion

        #region Public Methods

        public void LoadWizard()
        {
            WizardPages.MovePageFirst();
        }

        public void LoadNextPage(int pageIndex, int previousPageIndex, bool savePreviousPage)
        {
            if (pageIndex == -1) return;

            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(WizardPages[pageIndex].Content);
            
            if (savePreviousPage && previousPageIndex != -1)
                WizardPages[previousPageIndex].Save();

            WizardPages[pageIndex].Load();
            UpdateNavigation();
        }

        #endregion

        #region Event Handlers

        private void btnFirst_Click(object sender, EventArgs e)
        {
            //if (!CheckPageIsValid()) // Only matters if move forward
            //    return;

            WizardPages.MovePageFirst();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            //if (!CheckPageIsValid()) // Only matters if move forward
            //    return;

            WizardPages.MovePagePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!CheckPageIsValid())
                return;

            if (WizardPages.CanMoveNext)
            {
                WizardPages.MovePageNext();
            }
            else
            {
                //This is the finish button and it has been clicked
                NotifyWizardCompleted();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (!CheckPageIsValid())
                return;

            if (WizardPages.CanMoveNext)
            {
                WizardPages.MovePageLast();
            }
            else
            {
                //This is the finish button and it has been clicked
                NotifyWizardCompleted();
            }
        }

        #endregion

        private void PrintPDVWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WizardPages.CurrentPage == null) return;

            if (WizardPages.CurrentPage.PageValid) return;

            var result = MessageBox.Show(@"O setup não foi finalizado. A aplicação será encerrada. Deseja continuar?",
                AppStrings.Generic_MessageBox_ConfirmYesOrNoTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //TODO: put on resource file

            if (result == DialogResult.No)
                e.Cancel = true;
        }

        private void PrintPDVWizard_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (WizardPages.CurrentPage == null) return;

            if (!WizardPages.CurrentPage.PageValid)
                Application.Exit();
        }
    }
}