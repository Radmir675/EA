using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;
using System.Threading;


namespace EAwinforms
{
    public partial class Form1 : Form
    {
        public static Form1 Main;
        public Form1()
        {
            InitializeComponent();
            Main = this;

            
            
        }

       
        
        

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openfile = new OpenFileDialog())
            {
              
                openfile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); 
                openfile.Filter = "Office Files|*.doc;*.xls;*.ppt;*.xlsx";
                if (openfile.ShowDialog() == DialogResult.OK) 
                {
                    textBox1.Text = openfile.FileName;
                    fieldmist.Visible = false;
                }
                    
            }
          
        }



        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog save_file = new FolderBrowserDialog()) 
            {
                if (save_file.ShowDialog()==DialogResult.OK)
                {
                    textBox2.Text = save_file.SelectedPath;
                }
                fieldmist.Visible = false;
            }

            //using (SaveFileDialog savefile = new SaveFileDialog())
            //{
            //    savefile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //    savefile.Filter = "Text files(*.txt)|*.txt";
            //    if (savefile.ShowDialog() == DialogResult.OK)
            //    {
            //        textBox2.Text = savefile.FileName; ;
                   
            //    }


            //}

        }

        
        private async void button3_Click(object sender, EventArgs e)
        {
         
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
            {
                fieldmist.Visible = true;

            }
            else
            {
                MyProgram myProgram = new MyProgram(this);

                await Task.Run(() => myProgram.Maining());
               

                fieldmist.Visible = false;
              
            }



        }

        private void fieldmist_VisibleChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && textBox2.Text.Length != 0)
                fieldmist.Visible = false;
        }
    }
}
