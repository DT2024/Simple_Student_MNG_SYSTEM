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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Assignment3_Darius
{
    public partial class StudentsView : Form
    {
       
        // Initializing a list to hold Student objects.
        List<Students> StudentsList = new List<Students>();
        public StudentsView()
        {
            InitializeComponent();

        }



        DataTable dataTable = new DataTable();
        private void StudentsView_Load(object sender, EventArgs e)
        {
            LoadDataFromTextFile();
            StudentsList = StudentDB.GetStudents(); // Load data into StudentsList

        }
        private void LoadDataFromTextFile()
        {
            try
            {
                string[] lines = File.ReadAllLines(@"C:\Users\Taken\Desktop\App Dev C#.Net\students.txt");
                dataGridView1.Rows.Clear(); // Clear existing data
                foreach (string line in lines)
                {
                    string[] values = line.Split('/');
                    dataGridView1.Rows.Add(values);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data into DataGridView: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void studentInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            int row = e.RowIndex;
            // Set textbox and combobox values
            textBox1.Text = Convert.ToString(dataGridView1[0, row].Value);
            textBox2.Text = Convert.ToString(dataGridView1[3, row].Value);
            textBox3.Text = Convert.ToString(dataGridView1[1, row].Value);
            textBox5.Text = Convert.ToString(dataGridView1[2, row].Value);
            textBox6.Text = Convert.ToString(dataGridView1[5, row].Value);
            textBox7.Text = Convert.ToString(dataGridView1[6, row].Value);
            comboBox1.Text = Convert.ToString(dataGridView1[4, row].Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "";
            textBox4.Text = "";

        }
        private void RefreshDataGridView()
        {
            dataGridView1.Rows.Clear();
            foreach (var student in StudentsList)
            {
                dataGridView1.Rows.Add(student.ID, student.FirstName, student.LastName, student.Age, student.Gender, student.ClassName, student.Grade);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.PerformOperation();
        }
        private void PerformOperation()
        {
            if (radioButton1.Checked)
            {

                // Input validation for adding a new student
                // ID
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("ID cannot be empty. Please enter an ID!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!int.TryParse(textBox1.Text, out int id))
                {
                    MessageBox.Show("Please enter a valid ID! ID should be an integer value", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (StudentsList.Any(item => item.ID == id))
                {
                    MessageBox.Show("This ID already exists! Please enter a different ID.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //First Name
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("First Name cannot be empty. Please enter the name!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (textBox3.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("First Name should not contain numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Last Name
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Last Name cannot be empty. Please enter the name!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (textBox3.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Last Name should not contain numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                //Gender
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please select a gender.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Age
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Age cannot be empty. Please enter the Age", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!int.TryParse(textBox2.Text, out int age))
                {
                    MessageBox.Show("Please enter a valid Age!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Class Name
                if (string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    MessageBox.Show("Class Name cannot be empty. Please enter the name!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (textBox6.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Class Name should not contain numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Grade
                if (string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    MessageBox.Show("Grade cannot be empty. Please enter the Age", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!double.TryParse(textBox7.Text, out double grade))
                {
                    MessageBox.Show("Please enter a valid Grade!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dataGridView1.Rows.Add(textBox1.Text, textBox3.Text, textBox5.Text, textBox2.Text, comboBox1.Text, textBox6.Text, textBox7.Text);
                MessageBox.Show("Successfully Added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Add the new student to the list
                Students newStudent = new Students
                {
                    ID = int.Parse(textBox1.Text),
                    FirstName = textBox3.Text,
                    LastName = textBox5.Text,
                    Age = int.Parse(textBox2.Text),
                    Gender = comboBox1.Text,
                    ClassName = textBox6.Text,
                    Grade = double.Parse(textBox7.Text)
                };
                StudentsList.Add(newStudent);


            }
            else if (radioButton3.Checked)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Remove the selected student from the list
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int studentID = Convert.ToInt32(row.Cells[0].Value);
                        Students studentToRemove = StudentsList.FirstOrDefault(student => student.ID == studentID);
                        if (studentToRemove != null)
                        {
                            StudentsList.Remove(studentToRemove);
                        }
                    }

                    // Clear the DataGridView
                    dataGridView1.Rows.Clear();

                    // Update the DataGridView with the modified list
                    foreach (var student in StudentsList)
                    {
                        dataGridView1.Rows.Add(student.ID, student.FirstName, student.LastName, student.Age, student.Gender, student.ClassName, student.Grade);
                    }


                    MessageBox.Show("Successfully Deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please select a student to delete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (radioButton2.Checked)
            {

                if (dataGridView1.SelectedRows.Count == 1)
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    // Input validation for updating 
                    // ID
                    if (string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        MessageBox.Show("ID cannot be empty. Please enter an ID!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (!int.TryParse(textBox1.Text, out int id2))
                    {
                        MessageBox.Show("Please enter a valid ID! ID should be an integer value", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //First Name
                    if (string.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        MessageBox.Show("First Name cannot be empty. Please enter the name!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (textBox3.Text.Any(char.IsDigit))
                    {
                        MessageBox.Show("First Name should not contain numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //Last Name
                    if (string.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        MessageBox.Show("Last Name cannot be empty. Please enter the name!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (textBox3.Text.Any(char.IsDigit))
                    {
                        MessageBox.Show("Last Name should not contain numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }



                    //Gender
                    if (comboBox1.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a gender.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //Age
                    if (string.IsNullOrWhiteSpace(textBox2.Text))
                    {
                        MessageBox.Show("Age cannot be empty. Please enter the Age", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (!int.TryParse(textBox2.Text, out int age))
                    {
                        MessageBox.Show("Please enter a valid Age!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //Class Name
                    if (string.IsNullOrWhiteSpace(textBox6.Text))
                    {
                        MessageBox.Show("Class Name cannot be empty. Please enter the name!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (textBox6.Text.Any(char.IsDigit))
                    {
                        MessageBox.Show("Class Name should not contain numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //Grade
                    if (string.IsNullOrWhiteSpace(textBox7.Text))
                    {
                        MessageBox.Show("Grade cannot be empty. Please enter the Age", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (!double.TryParse(textBox7.Text, out double grade))
                    {
                        MessageBox.Show("Please enter a valid Grade!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Students updatedStudent = new Students
                    {
                        ID = id2,
                        FirstName = textBox3.Text,
                        LastName = textBox5.Text,
                        Age = int.Parse(textBox2.Text),
                        Gender = comboBox1.Text,
                        ClassName = textBox6.Text,
                        Grade = double.Parse(textBox7.Text)
                    };

                    // Update the student in StudentsList
                    StudentsList[rowIndex] = updatedStudent;

                    // Update student in the database
                    StudentDB.UpdateStudent(updatedStudent);



                    // Update DataGridView
                    dataGridView1.Rows[rowIndex].SetValues(
                        updatedStudent.ID,
                        updatedStudent.FirstName,
                        updatedStudent.LastName,
                        updatedStudent.Age,
                        updatedStudent.Gender,
                        updatedStudent.ClassName,
                        updatedStudent.Grade);


                    MessageBox.Show("Successfully Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please select a single row to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (radioButton4.Checked)
            {
                string search = textBox4.Text.ToLower();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    var filter = StudentsList.Where(student => student.ID.ToString().Contains(search)|| 
                    student.FirstName.ToLower().Contains(search)|| student.LastName.ToLower().Contains(search)).ToList();

                    //clear the datagridview to see the search and found information
                    dataGridView1.Rows.Clear();

                    // Populate DataGridView with filtered student records
                    foreach (var student in filter)
                    {
                        dataGridView1.Rows.Add(student.ID, student.FirstName, student.LastName, student.Age, student.Gender, student.ClassName, student.Grade);
                    }
                }
                else
                {
                    MessageBox.Show("There is no data based on the student info provided!","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DialogResult message;
                message = MessageBox.Show("Do you want to Save and Exit or Cancel Exit?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (message == DialogResult.Yes)
                {
                    // Save the changes to the text file
                    StudentDB.SaveItems(StudentsList);
                    this.Close();
                }
                else if (message == DialogResult.No)
                {
                    this.Close();
                }

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.radioButton5.Checked = true;
            this.PerformOperation();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.radioButton1.Checked = true;
            this.PerformOperation();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.radioButton2.Checked = true;
            this.PerformOperation();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.radioButton3.Checked = true;
            this.PerformOperation();
        }

        private void fIndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.radioButton4.Checked = true;
            this.PerformOperation();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.radioButton5.Checked = true;
            this.PerformOperation();
        }
    }
}

