using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseProject
{
    public partial class SIgnUpForm : Form
    {
        DBAccess dbAccess = new DBAccess();

        string username;
        string email;
        string password;
        string country;

        public SIgnUpForm()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            username = UsernameTextBox.Text;
            email = EmailTextBox.Text;
            password = PasswordTextBox.Text;
            country = CountryComboBox.Text;

            if (CheckEnteredValues())
            {
                    // Pierwsze nazwy tabelek w db, następne nazwy zmiennych
                SqlCommand insertCommand = new SqlCommand("insert into Users(ID, Username, Email, Password, Country) values(@ID, @Username, @Email, @Password, @Country)");

                // Dopiero tutaj do zmienych przypisujemy wartości. @Username to co innego niż Username. Zabezpieczenie przed SQL injaction
                insertCommand.Parameters.AddWithValue("@ID", Guid.NewGuid());
                insertCommand.Parameters.AddWithValue("@Username", username);
                insertCommand.Parameters.AddWithValue("@Email", email);
                insertCommand.Parameters.AddWithValue("@Password", password);
                insertCommand.Parameters.AddWithValue("@Country", country);

                int row = dbAccess.ExecuteQuery(insertCommand);

                if(row == 1)        // wykona się jeśli zapytanie miało wpływ na tabele
                {
                    MessageBox.Show("Account Created Successfully. FINALLY");
                    User currentUser = User.GetUserWithEmailAndPassword(email, password);
                 
                    HomePage homePage = new HomePage(currentUser);
                    homePage.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create account. Sorry");
                }
            }
        }

        bool CheckEnteredValues()
        {
            if (!DatabaseProject.ValidateDate.Username(username))
            {
                MessageBox.Show("Please enter your name");
            }
            else if(!DatabaseProject.ValidateDate.Email(email))
            {
                MessageBox.Show("Your email is incorrect");
            }
            else if (!DatabaseProject.ValidateDate.Password(password))
            {
                MessageBox.Show("Your password is too short");
            }
            else if(country == "")
            {
                MessageBox.Show("Please select a country");
            }
            else
            {
                return true;
            }

            return false;
        }

        private void LoginLabel_Click(object sender, EventArgs e)
        {
            SIgnUpForm signUpForn = new SIgnUpForm();
            signUpForn.Show();
            this.Hide();
        }
    }
}
