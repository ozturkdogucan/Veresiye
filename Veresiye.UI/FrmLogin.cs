﻿using System;
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
    
    public partial class FrmLogin : Form
    {
        private readonly IUserService userService;
        public FrmMain MasterForm { get; set; }
        
        public FrmLogin(IUserService userService)
        {
            this.userService = userService;
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var login = userService.Login(txtUserName.Text, txtPassword.Text);
            if (login != null)
            {
                MasterForm.ShowFrmCompanies();
                this.Close();
            }
            else
            {
                MessageBox.Show("Geçersiz kullanıcı adı veya parola! Lütfen tekrar deneyiniz. ");
            }
        }
        
    }
}
