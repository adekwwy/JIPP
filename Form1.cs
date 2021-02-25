using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static string path = "rate.txt";
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        public Form1()
        {
            InitializeComponent();
            StreamReader sr = new StreamReader(fs);
            if (File.Exists(path))
            {
                userControl11.RateCount = int.Parse(sr.ReadLine());
            }
            else
                userControl11.RateCount = 0;
            sr.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowDateFromDateBase();
        }

        private void ShowDateFromDateBase()
        {
            using (Context context = new Context())
            {
                List<Contact> data = context.Contacts.ToList();
                var bindingList = new BindingList<Contact>(data);
                var source = new BindingSource(bindingList, null);

                dataGridView.DataSource = source;
            }
        }

        private void InsertDateToDateBase()
        {
            using (Context context = new Context())
            {
                Contact newContact = new Contact()
                {
                    Name = nameTextBox.Text,
                    Telephone = telephoneTextBox.Text,
                    Email = emailTextBox.Text,
                    Type = typeComboBox.SelectedItem.ToString()
                };
                context.Contacts.Add(newContact);
                context.SaveChanges();
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            InsertDateToDateBase();
            ShowDateFromDateBase();
        }

        private void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridViewRow row = dataGridView.SelectedRows[0];
            nameTextBox.Text = row.Cells["Name"].Value.ToString();
            telephoneTextBox.Text = row.Cells["Telephone"].Value.ToString();
            emailTextBox.Text = row.Cells["Email"].Value.ToString();
            typeComboBox.Text = row.Cells["Type"].Value.ToString();
        }

        private void delateButton_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView.SelectedRows)
            {
                int rowId = Convert.ToInt32(row.Cells[0].Value);

                if(rowId > 0)
                {
                    Delate(rowId);
                    dataGridView.Rows.RemoveAt(row.Index);
                }
            }

            ShowDateFromDateBase();
        }

        public void Delate(int rowId)
        {
            using (Context context = new Context())
            {
                var toBeDelated = context.Contacts.First(c => c.Id == rowId);
                context.Contacts.Remove(toBeDelated);
                context.SaveChanges();
            }
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            Change();
            ShowDateFromDateBase();
        }

        public void Change()
        {
            using (SqlConnection connection = new SqlConnection("data source=localhost;initial catalog=ContactBook;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                string cmd = 
                    "UPDATE dbo.Contacts SET " +
                    "Name= '" + nameTextBox.Text + "', " +
                    "Telephone='" + telephoneTextBox.Text + "', " +
                    "Email='" + emailTextBox.Text + "', " +
                    "Type='" + typeComboBox.Text + "' " +
                    "where id=" + dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                SqlCommand command = new SqlCommand(cmd,connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        private void rateButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(userControl11.RateCount.ToString());
                sw.Close();
            }
            MessageBox.Show("Ocena zapisana");
        }
    }
}

