using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veresiye.Service;

namespace Veresiye.UI
{
    public partial class FrmMain : Form
    {
        private readonly IUserService userService;
        private readonly FrmRegister frmRegister;
        private readonly FrmCompanies frmCompanies;
        private readonly FrmLogin frmLogin;
        private readonly FrmCompanyAdd frmCompanyAdd;
        private readonly FrmCompanyEdit frmCompanyEdit;

        public FrmMain(IUserService userService, FrmRegister frmRegister, FrmCompanies frmCompanies, FrmLogin frmLogin, FrmCompanyAdd frmCompanyAdd, FrmCompanyEdit frmCompanyEdit)
        {
            this.userService= userService;
            this.frmCompanies = frmCompanies;
            this.frmRegister = frmRegister;
            this.frmLogin = frmLogin;
            this.frmCompanyAdd = frmCompanyAdd;
            this.frmCompanyEdit = frmCompanyEdit;
            InitializeComponent();
            this.frmRegister.MdiParent = this;
            this.frmRegister.FormClosed += FrmRegister_FormClosed;
            this.frmCompanies.MdiParent = this;
            this.frmLogin.MdiParent = this;
            this.frmLogin.MasterForm = this;
            
        }

        private void FrmRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin.Show();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            var userCount = userService.GetAll().Count();
            
            if (userCount == 0)
            {
                frmRegister.Show();
                
            }
            else
            {
                frmLogin.Show();
            }
        }
        public void ShowFrmCompanies()
        {
            frmCompanies.Show();
        }
    }
}
