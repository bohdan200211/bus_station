using BusStation.DataAccess;
using BusStation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusStation.Forms
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            string _username = username.Text;
            string _password = password.Text;
            if (username.Text.Trim() != "" && password.Text.Trim() != "")
            {

                UserAccess db = new UserAccess();
                List<User> users = db.GetManyBySelector(user => user.Username == _username && user.Password == _password);
                if (users.Count != 0)
                {
                    this.Hide();
                    AdminFrom form = new AdminFrom(users[0]);
                    form.Closed += (s, args) => this.Close();
                    form.Show();
                }
                else
                    MessageLabel.Text = "Incorrect username or password. Please try typing again";
            }else
                MessageLabel.Text = "Please enter username and password";
        }
        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string _username = username.Text.Trim();
            string _password = password.Text.Trim();
            if (_username != "" && _password != "")
            {

                UserAccess db = new UserAccess();
                List<User> users = db.GetManyBySelector(user => user.Username.Trim() == _username);
                if (users.Count == 0)
                {
                    try
                    {
                        User user = new User(-1, _username, _password, DateTime.Now);
                        db.Add(user);
                        MessageLabel.Text = "Successfull sign up";
                    }
                    catch (Exception ex)
                    {
                        MessageLabel.Text = ex.Message;
                    }
                }
                else
                    MessageLabel.Text = "This username exists. Enter a different username or use the username to sign in.";
            }
            else
                MessageLabel.Text = "Please enter username and password";
        }

        private void PasswordButton_MouseDown(object sender, MouseEventArgs e)
        {
            password.PasswordChar = '\0';
        }

        private void PasswordButton_MouseUp(object sender, MouseEventArgs e)
        {
            password.PasswordChar = '*';
        }

        private void username_TextChanged(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
        }

        private void password_TextChanged(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
        }
    }
}
