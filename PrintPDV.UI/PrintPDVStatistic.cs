using System;
using System.ComponentModel;
using System.Windows.Forms;
using PrintPDV.Controllers;
using PrintPDV.Models;

namespace PrintPDV.UI
{
    public partial class PrintPDVStatistic : Form
    {
        public PrintPDVStatistic()
        {
            InitializeComponent();
        }

        private void PrintPDVStatistic_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void PrintPDVWizard_Enter(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            dtpStartDate.Value = DateTime.Parse(DateTime.Now.ToString("dd/MM/yy") + " 0:00:00").AddDays(-1); 
            dtpEndDate.Value = DateTime.Parse(DateTime.Now.ToString("dd/MM/yy") + " 23:59:59");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var statisticList = StatisticController.Instance.GetByDateRange(dtpStartDate.Value, dtpEndDate.Value);
            var bindingList = new BindingList<Statistic>(statisticList);

            dgvResultset.DataSource = new BindingSource(bindingList, null);

            dgvResultset.Columns[0].Visible = false; // License
            dgvResultset.Columns[4].Visible = true; // Synced
            dgvResultset.Columns[5].Visible = false; // Id

            lblTotal.Text = statisticList.Count.ToString();
        }
    }
}
