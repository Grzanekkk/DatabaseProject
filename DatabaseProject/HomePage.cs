using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatabaseProject
{
    public partial class HomePage : Form
    {
        User currentUser;

        DBAccess dbAccess = new DBAccess();

        public HomePage(User _currentUser)
        {
            InitializeComponent();
            currentUser = _currentUser;
            ShowPasswordCheckBox.Checked = false;
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            //WelcomeLabel.Text = $"Welcome To Your Profile {currentUser.username}";
            //WelcomeLabel.Dock = DockStyle.Top;

            UsernameTextBox.Text = currentUser.username;
            EmailTextBox.Text = currentUser.email;
            PasswordTextBox.Text = currentUser.password;
            CountryTextBox.Text = currentUser.country;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPasswordCheckBox.Checked == true)
            {
                PasswordTextBox.UseSystemPasswordChar = false;
            }
            else
            {
                PasswordTextBox.UseSystemPasswordChar = true;
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            string newUsername = UsernameTextBox.Text;
            string newEmail = EmailTextBox.Text;
            string newPassword = PasswordTextBox.Text;

            if(ValidateDate.EmailPasswordUsername(newEmail, newPassword, newUsername))
            {
                string query = $"Update Users SET Username = '{@newUsername}', Email = '{@newEmail}', Password = '{@newPassword}' where ID = '{currentUser.id}'";

                SqlCommand updateCommand = new SqlCommand(query);

                updateCommand.Parameters.AddWithValue("@newUsername", newUsername);
                updateCommand.Parameters.AddWithValue("@newEmail", newEmail);
                updateCommand.Parameters.AddWithValue("@newPassword", newPassword);

                int row = dbAccess.ExecuteQuery(updateCommand);

                if (row == 1)        
                {
                    MessageBox.Show("Account Updated Successfully!");
                }
                else
                {
                    MessageBox.Show("Something Went Wrong. Sorry :(");
                }
            }
        }

        private void LoguotButton_Click(object sender, EventArgs e)
        {
            Loguot();
        }

        private void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure??????", "Delete Account", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialog == DialogResult.Yes)
            {
                string query = $"DELETE from Users where ID ='{currentUser.id}'";

                SqlCommand deleteCommand = new SqlCommand(query);

                int row = dbAccess.ExecuteQuery(deleteCommand);

                if (row == 1)
                {
                    MessageBox.Show("Account Deleted Successfully!");

                    Loguot();
                }
                else
                {
                    MessageBox.Show("Something Went Wrong. Sorry :(");
                }
            }
        }

        private void Loguot()
        {
            currentUser = null;

            SignIn signInForm = new SignIn();
            signInForm.Show();
            this.Close();
        }
    }
}
