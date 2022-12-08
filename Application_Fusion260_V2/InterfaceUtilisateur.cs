using Application_Fusion260;
using hole_namespace;
using namespace_Feature_Explorer;
using SldWorks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wrapper;

namespace Application_Fusion260_V2
{
    public partial class InterfaceUtilisateur : Form
    {
        //Global variable to be modified according to user input
      
        public InterfaceUtilisateur()
        {
            InitializeComponent();
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Analyse app = new Analyse();
            if(Dmin_TextBox.Text == "" || Pmin_TextBox.Text == "" || Pmax_TextBox.Text == "" || Dmax_TextBox.Text == "")
            {
                app.run((float)0.1, 1000, (float)0.1, 100);
            }
            else if(Dmin_TextBox.Text == "Diamètre Min" || Pmin_TextBox.Text == "Profondeur Min" || Pmax_TextBox.Text == "Profondeur Max" || Dmax_TextBox.Text == "Diamètre Max") 
            {
                app.run((float)0.1, 1000, (float)0.1, 100);
            }
            else {
                app.run(float.Parse(Pmin_TextBox.Text), float.Parse(Pmax_TextBox.Text), float.Parse(Dmin_TextBox.Text), float.Parse(Dmax_TextBox.Text));
            }
           
            
            //Analyse();
        }

        private void Pmin_TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Pmin_TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            Pmin_TextBox.Text =  "";
        }

        private void Pmax_TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            Pmax_TextBox.Text = "";
        }

        private void Dmin_TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Dmin_TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            Dmin_TextBox.Text = "";
        }

        private void Dmax_TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            Dmax_TextBox.Text = "";
        }

        private void InterfaceUtilisateur_Load(object sender, EventArgs e)
        {

        }
    }
}