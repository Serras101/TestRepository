using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextBoxTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.btnOK.Enabled = false;

            //Tag values for testing if the data is valid
            this.txtAddress.Tag = false;
            this.txtAge.Tag = false;
            this.txtName.Tag = false;

            //Subscriptions to events
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtBoxEmpty_Validating);
            this.txtAddress.Validating += new System.ComponentModel.CancelEventHandler(this.txtBoxEmpty_Validating);
            this.txtAge.Validating += new System.ComponentModel.CancelEventHandler(this.txtBoxEmpty_Validating);
            this.txtAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAge_KeyPress);
            this.txtName.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtAddress.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtAge.TextChanged += new System.EventHandler(this.txtBox_TextChanged);

        }

        private void txtBox_TextChanged(object sender, System.EventArgs e)
        {
            TextBox tb;
            try
            {
                tb = (TextBox)sender;
            }
            catch (InvalidCastException)
            {
                throw new InvalidOperationException("A non Textbox object has called a Textbox specific event handler.");
            }

            if (tb.Text.Length == 0)
            {
                tb.Tag = false;
                tb.BackColor = Color.Red;
            }
            else
            {
                tb.Tag = true;
                tb.BackColor = SystemColors.Window;
            }

            //Call ValidateAll to set the OK btn.
            ValidateAll();
        }

        private void ValidateAll()
        {
            this.btnOK.Enabled = ((bool)(this.txtAddress.Tag) &&
                                    (bool)(this.txtAge.Tag) &&
                                    (bool)(this.txtName.Tag) &&
                                    (bool)(this.txtName.Tag));
        }

        private void txtAge_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57 || (e.KeyChar == 48 && ((TextBox)sender).SelectionStart == 0)) && e.KeyChar != 8)
                e.Handled = true; //Remove the character    
        }

        private void txtBoxEmpty_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox tb;
            try
            {
                tb = (TextBox)sender;
            }
            catch (InvalidCastException)
            {
                throw new InvalidOperationException("A non Textbox object has called a Textbox specific event handler.");
            }

            if (tb.Text.Length == 0)
            {
                tb.BackColor = Color.Red;
                tb.Tag = false;

                //e.Cancel = true; //could cancel further processing with this call.
            }
            else
            {
                tb.BackColor = System.Drawing.SystemColors.Window;
                tb.Tag = true;
            }

            //Set the state of the OK btn.
            ValidateAll();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            StringBuilder output = new StringBuilder();

            output.Append("Name: " + this.txtName.Text + Environment.NewLine);
            output.Append("Address: " + this.txtAddress.Text + Environment.NewLine);
            output.Append("Occupation: " + (string)(this.chkProgrammer.Checked ? "Programmer" : "Not a programmer") + Environment.NewLine);
            output.Append("Sex: " + (string)(this.rdoFemale.Checked ? "Female" : "Male") + Environment.NewLine);
            output.Append("Age: " + this.txtAge.Text);

            this.txtOutput.Text = output.ToString();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            StringBuilder output = new StringBuilder();

            output.Append("Name = Your name" + Environment.NewLine);
            output.Append("Address = Your address" + Environment.NewLine);
            output.Append("Programmer = Check 'Programmer' if you are a programmer" + Environment.NewLine);
            output.Append("Sex = Choose your sex" + Environment.NewLine);
            output.Append("Age = Your age");

            this.txtOutput.Text = output.ToString();
        }
    }
}
