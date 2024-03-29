using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment3_Darius
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            string feedback = "";
            string path = @"C:\Users\Taken\Desktop\App Dev C#.Net\users.txt";

            try
            {
                var userLines = File.ReadAllLines(path);

                // Check if the entered username and password match any user in the list using LINQ
                bool authenticated = userLines.Any(line =>
                {
                    string[] lineArray = line.Split(':');
                    return lineArray.Length == 2 && lineArray[0] == username && lineArray[1] == password;
                });

                if (authenticated)
                {
                    // Authentication successful, open the next form and dispose the login form
                    feedback = "Access granted";
                    StudentsView form = new StudentsView();
                    MessageBox.Show(feedback, "Login Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    form.ShowDialog();
                    this.Dispose();
                }
                else
                {
                    // Authentication unsuccessful, check if the username was found
                    bool usernameExists = userLines.Any(line =>       //we are checking here using LINQ ANY method
                    {
                        string[] parts = line.Split(':');
                        return parts.Length >= 1 && parts[0] == username;
                    });

                    if (usernameExists) // if the username exists
                    {
                        feedback = "Incorrect password";
                        MessageBox.Show(feedback, "Login Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else  //if username not exist, display that the user doesn't have any rights
                    {
                        feedback = "Sorry,You have no rights, please contact the support team";
                        MessageBox.Show(feedback, "Login Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {
                feedback = "An error occurred while processing your request";
                MessageBox.Show(feedback, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
