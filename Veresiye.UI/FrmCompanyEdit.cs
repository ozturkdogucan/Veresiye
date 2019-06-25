using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veresiye.Model;
using Veresiye.Service;

namespace Veresiye.UI
{
    public partial class FrmCompanyEdit : Form
    {
        private readonly ICompanyService companyService;
        private readonly IActivityService activityService;
        private readonly FrmActivityAdd frmActivityAdd;
        private readonly FrmActivityEdit frmActivityEdit;
        public FrmCompanies MasterForm { get; set; }
        public FrmCompanyEdit(ICompanyService companyService, IActivityService activityService, FrmActivityAdd frmActivityAdd, FrmActivityEdit frmActivityEdit)
        {
            this.companyService = companyService;
            this.activityService = activityService;
            this.frmActivityAdd = frmActivityAdd;
            this.frmActivityEdit = frmActivityEdit;
            InitializeComponent();
            this.frmActivityAdd.MdiParent = this.MdiParent;
            this.frmActivityAdd.MasterForm = this;
            this.frmActivityEdit.MdiParent = this.MdiParent;
            this.frmActivityEdit.MasterForm = this;
        }

        private void FrmCompanyEdit_Load(object sender, EventArgs e)
        {
            LoadActivitiy();
        }
        private int Id;
        public void LoadForm(int id)
        {
            var company = companyService.Get(id);
            this.Id = id;
            txtName.Text = company.Name;
            txtPhone.Text = company.Phone;
            txtCity.Text = company.City;
            txtRegion.Text = company.Region;
            LoadActivities();
            
        }
        public void LoadActivities()
        {
            var activities = activityService.GetAllByCompanyId(this.Id);
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = activities;
        }
        private void LoadActivitiy()
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = activityService.GetAll();
        }

        private void FrmCompanyEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            // validasyonlar
            if (txtName.Text == "")
            {
                MessageBox.Show("Şirket adı gereklidir.");
                return;
            }
            else if (txtPhone.Text == "")
            {
                MessageBox.Show("Telefon alanı gereklidir.");
                return;
            }
            var company = companyService.Get(this.Id);
            company.Name = txtName.Text;
            company.Phone = txtPhone.Text;
            company.City = txtCity.Text;
            company.Region = txtRegion.Text;
            companyService.Update(company);
            MessageBox.Show("Şirket kaydı başarıyla güncellendi. ");
            MasterForm.LoadCompanies();

            this.Hide();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            frmActivityAdd.Show();
            frmActivityAdd.LoadForm(this.Id);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                this.frmActivityEdit.Show();
                this.frmActivityEdit.LoadForm(int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz işlemi seçiniz. ");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Bu işlem kaydını silmek istediğinize emin misiniz?", "Silme İşlemi", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.activityService.Delete(int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                    this.LoadActivities();
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz işlemi seçiniz. ");
            }
        }
    }
}
