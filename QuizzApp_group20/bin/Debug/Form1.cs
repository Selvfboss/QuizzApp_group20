using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizzApp_group20
{
    public partial class Form1 : Form
    {
        // Declared the dictionary here which contains strings..

        private Dictionary<string, string> studentDetails;

        public Form1()
        {

            InitializeComponent();

            /*
             the dictory below contains all the names and passwords of the students
             */
            studentDetails = new Dictionary<string, string>
            {
                {"Nawas", "password123"},
                {"Duke", "pass456"},
                {"Nicholas", "pass789"},
                {"Dennis", "pass101112"},
                {"Aaron", "pass131415"}
            };
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            main main = new main();
            main.Show();
            this.Hide();
        }

       
    }
}
