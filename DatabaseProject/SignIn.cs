using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatabaseProject
{
    public partial class SignIn : Form
    {
        User currentUser;

        string email;
        string password;

        public SignIn()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            email = EmailTextBox.Text;
            password = PaswordTextBox.Text;

            if (CheckEnteredValues())
            {
                currentUser = User.GetUserWithEmailAndPassword(email, password);

                MessageBox.Show("You are logged in. NICE");

                HomePage homePage = new HomePage(currentUser);
                homePage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Email or Password is incorrect");
            }
        }


        bool CheckEnteredValues()
        {
            if(!ValidateDate.Email(email))
            {
                MessageBox.Show("Your email is incorrect");
            }
            else if(!ValidateDate.Password(password))
            {
                MessageBox.Show("Your password is too short");
            }
            else
            {
                return true;
            }

            return false;
        }


        private void SignIn_Load(object sender, EventArgs e)
        {

        }

        private void SingUpLabel_Click(object sender, EventArgs e)
        {
            SIgnUpForm signUpForm = new SIgnUpForm();
            signUpForm.Show();
            this.Hide();
        }
    }
}
