﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class ResetPassword : Form
    {
        private readonly CarRentalEntities _db;
        private User _user;
        public ResetPassword(User user)
        {
            InitializeComponent();
            _db = new CarRentalEntities();
            _user = user;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                var password = tbPassword.Text;
                var confirm_password = tbConfirm.Text;

                var user = _db.Users.FirstOrDefault(q => q.id == _user.id);

                if (password != confirm_password)
                {
                    MessageBox.Show("Password do not match. Please try again");
                }
                else
                {
                    _user.password = Utils.HashPassword(password);
                    _db.SaveChanges();
                    MessageBox.Show("Password was reset successfully");
                }
            } catch (Exception)
            {
                MessageBox.Show("An Error Has Ocurred");
            }
        }
    }
}
