using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.Sql;
using System.IO;

namespace CosmusGame
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string connectionString = "Server=DESKTOP-07U8J8T\\SQLEXPRESS;Database=StarGame;Trusted_Connection=True;";
        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox4.PasswordChar = '\0';
                textBox5.PasswordChar = '\0';
            }
            else
            {
                textBox4.PasswordChar = '*';
                textBox5.PasswordChar = '*';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {

            string firstname = textBox1.Text.Trim();
            string lastname = textBox3.Text.Trim();
            string username = textBox2.Text.Trim();
            string password = textBox4.Text.Trim();
            string confirmpassword = textBox5.Text.Trim();

            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("All fields required!");
                return;
            }
            else if (password != confirmpassword)
            {
                MessageBox.Show("Password not confirmed!");
                return;
            }
            else
            {
                string hashedPassword = password;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string insertQuery = @"
                    INSERT INTO users (Firstname, Lastname, Username, Pass_word)
                    VALUES (@Firstname, @Lastname, @Username, @Password);";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Firstname", firstname);
                        command.Parameters.AddWithValue("@Lastname", lastname);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", hashedPassword);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("Your account has been created successfully.");

                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            textBox5.Clear();

                            Form1 form1 = new Form1();  
                            form1.Show();
                            this.Hide();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error creating account: " + ex.Message);
                        }
                    }
                }
                button2_Click(sender, e);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.PasswordChar = '*';
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.PasswordChar = '*';
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}