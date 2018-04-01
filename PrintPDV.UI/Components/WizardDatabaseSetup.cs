using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using PrintPDV.Controllers;
using PrintPDV.Utility;

namespace PrintPDV.UI.Components
{
    public partial class WizardDatabaseSetup : UserControl, IWizardStep
    {
        private Dictionary<string, Func<bool>> Commands;
        private StringBuilder ExecutionLog;

        public WizardDatabaseSetup()
        {
            InitializeComponent();
        }

        public UserControl Content
        {
            get
            {
                Dock = DockStyle.Fill;
                return this;
            }
        }
        
        public new void Load()
        {
            InitializeCommands();

            InitializeBackgroundWorker();
        }

        public void Save()
        {
            
        }

        public void Cancel()
        {
            
        }

        public bool IsBusy { get; private set; }
        
        public bool PageValid { get; private set; }
        
        public string ValidationMessage 
        {
            get { return "O banco de dados deve ser configurado."; } //TODO: put on resource file
        }

        private void InitializeCommands()
        {
            Commands = new Dictionary<string, Func<bool>>
            {
                {"Criando banco de dados", () => AppConfigController.CreateDatabase()},
                {"Inserindo configurações", () => AppConfigController.InsertAppConfig("pt-BR")},
                {"Inserindo impressoras", () => AppConfigController.InserPrintersModels()},
                {"Inserindo cliparts", () => AppConfigController.InsertCliparts()}
            };

            ExecutionLog = new StringBuilder();
        }

        private void InitializeBackgroundWorker()
        {
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            IsBusy = true;
            e.Result = SetupDatabase(worker, e);
        }

        void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtExecutionLog.Text = ExecutionLog.ToString();
            progressBar.Value = e.ProgressPercentage;
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                txtExecutionLog.AppendText(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                txtExecutionLog.AppendText("Cancelado!"); //TODO: put on resource file
            }
            else
            {
                AppConfigController.Instance.SetAppConfig();
                txtExecutionLog.AppendText(Convert.ToBoolean(e.Result) ? "Sucesso!" : string.Empty); //TODO: put on resource file
                IsBusy = false;
                PageValid = true;
            }

            ExecutionLog = new StringBuilder();
        }

        private bool SetupDatabase(BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return false;
                }

                double total = Commands.Count;
                double step = 1;

                foreach (var command in Commands)
                {
                    var result = false;

                    ExecutionLog.Append(command.Key + "... ");

                    try
                    {
                        result = command.Value.Invoke();
                        ExecutionLog.AppendLine(result ? "Ok" : "Erro"); //TODO: put on resource file
                    }
                    catch (Exception ex)
                    {
                        ExecutionLog.AppendLine("NOK");
                        ExecutionLog.AppendLine("Erro na criação do banco de dados. Feche a aplicação e tente novamente."); //TODO: put on resource file

                        LogUtility.Log(LogUtility.LogType.SystemError, "Erro na criação do banco de dados.", ex.Message); //TODO: put on resource file
                    }

                    double percentComplete = step / total * 100;

                    worker.ReportProgress(Convert.ToInt32(percentComplete));

                    if (!result)
                        throw new Exception();

                    step++;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void btnSetupDatabase_Click(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            txtExecutionLog.Text = string.Empty;

            bgWorker.RunWorkerAsync();
        }
    }
}
