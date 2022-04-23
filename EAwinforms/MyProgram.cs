using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace EAwinforms
{
   public class MyProgram
    {
       public Form1 form1;
        public MyProgram() { }
        public MyProgram(Form1 owner) 
        {
            form1 = owner;

        }
       public async  void Maining()
        {
            
            string initial_xlsx = form1.textBox1.Text;
            Excel_srt excel = new Excel_srt(initial_xlsx, 1);
            
           
            
            await Task.Run(() => excel.Call());
            await Task.Run(() => excel.Writing_text());
            await Task.Delay(500);
            MessageBox.Show("Программы выполнена");

           
        }


    }

    public class Excel_srt:MyProgram

    {

        delegate void Progress(int value);

        void Start() 
        {
            int up = 100 / (row-1);
            Form1.Main.progressBar1.Invoke(new Progress((s) => Form1.Main.progressBar1.Value = s), Form1.Main.progressBar1.Value+up);
        }

        string path = string.Empty;
        public Workbook wb;
        public Worksheet ws;

        Form1 form;
        public Excel_srt(Form1 owner) 
        {
            form = owner;
        }

        public Excel_srt(string path, int Sheet)
        {
            this.path = path;


            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[Sheet];
            row = excel.Rows.CurrentRegion.EntireRow.Count;
            col = excel.Columns.CurrentRegion.EntireColumn.Count;
        }

        _Application excel = new _Excel.Application();
        int row;
        int col;

        string[,] array;

        public async void Call()
        {
            int row = excel.Rows.CurrentRegion.EntireRow.Count;
            int col = excel.Columns.CurrentRegion.EntireColumn.Count;
            array = new string[row, col];



            for (int i = 2; i <= row; i++)
            {
                for (int j = 2; j <= col; j++)
                {
                    try
                    {
                        string receivу_str = Convert.ToString(ws.Cells[i, j].Value2);
                        array[i - 2, j - 2] = receivу_str;//  запись в массив

                     

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Вот он я");
                        throw new Exception();

                    }

                }

            }
            wb.Close(true);
            excel.Quit();

            Marshal.ReleaseComObject(ws);
            Marshal.ReleaseComObject(wb);
            Marshal.ReleaseComObject(excel);
            excel = null;
            wb = null;
            ws = null;

            GC.Collect();


        }

       
        public async void Writing_text()
        {

            string origin = @"C:\Users\Радмир\source\repos\EAwinforms\EAwinforms\Resources\002_d10.txt";
            

            using (StreamReader bloknot = new StreamReader(origin))
            {
                string obtain = bloknot.ReadToEnd();

                for (int i = 0; i < row-1; i++)
                {

                    var dict = new Dictionary<string, string>()
                    {
                        ["Круг"] = array[i, 0],
                        ["В2-II-НД-10"] = array[i, 1],
                        ["ГОСТ 2590-2006"] = array[i, 2],
                        ["40X"] = array[i, 3],
                        ["steel"] = array[i, 4],
                        ["STEEL"] = array[i, 5],

                    };

                    


                    var result = dict.Aggregate(obtain, (s, kvp) => s.Replace(kvp.Key, kvp.Value));


                    
                    

                    string finish = Path.Combine(Form1.Main.textBox2.Text, array[i, 0]+" "+ array[i, 1] + ".txt");
                        //$(@"Form1.Main.textBox2.Text+ {array[i,1]} + " "+ array[i, 2] + ".txt"" );

                    File.WriteAllText(finish, result);
                    Start();

                }
                
               
            }
        }
    }
}
