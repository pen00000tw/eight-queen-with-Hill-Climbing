using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 八皇后
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }
        int n = 0;
        private void print(int []arr) //顯示到textbox
        {
            textBox1.Text = "";
            //textBox1.Text = textBox1.Text + "--------------------------------------\r\n";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (arr[i] == j)
                    {
                        textBox1.Text = textBox1.Text + "♞ ";
                    }
                    else
                    {
                        textBox1.Text = textBox1.Text + "◯ ";
                    }
                }
                textBox1.Text = textBox1.Text + "\r\n";
            }
        }

        private void button1_Click(object sender, EventArgs e) //main
        {
            textBox3.Text = "";
            n = int.Parse(textBox2.Text);
            int[] arr = new int[n];
            int maxstep = 50;
            int nowstep = 0;
            int restart = 0;
            while (conflict(arr) > 0)
            {
               
                init(arr);
                //print(arr);
                textBox3.AppendText("restart times:" + restart + "\r\n");
                textBox3.AppendText("conflict amount:" + conflict(arr) + "\r\n");
                do
                {
                    HC(arr);
                    nowstep++;
                    textBox3.AppendText("try: " + nowstep + " conflict amount:" + conflict(arr) + "\r\n");
                    Application.DoEvents();

                } while (conflict(arr) > 0 && nowstep < maxstep); //如果嘗試超過maxstep的話重新開始
                
                restart++;
                nowstep = 0;
            }
            print(arr);
        }
        private void init(int []arr)// 初始化
        {
            for (int i = 0; i < n; i++)
            {
                Random rnd1 = new Random(Guid.NewGuid().GetHashCode());
                arr[i] = rnd1.Next(0, n);
            }
        }
        private int conflict(int[] arr)
        {
            int num = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[i] == arr[j])   //檢查直的
                    {
                        num++;
                    }
                    if (Math.Abs(arr[i] - arr[j]) == (j - i)) //檢查斜的
                    {
                        num++;
                    }
                }
            }
            return num;
        }
        private void HC(int[] arr)
        {
            int[] arr_copy = new int[n];
            int conflictnow = 0;
            int[,] tmp = new int[n, n];
            int x = 0,y = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (arr[i] == j)
                    {
                        continue;
                    }
                    arr_copy = (int[])arr.Clone();
                    arr_copy[i] = j;
                    tmp[i,j] = conflict(arr_copy);
                }
            }
            conflictnow = conflict(arr);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (arr[i] == j)
                    {
                        continue;
                    }
                    else 
                    {
                        if (tmp[i, j] <= conflictnow)
                        {
                            conflictnow = tmp[i, j];
                            x = i;
                            y = j;
                        }
                    }
                }
            }
            arr[x] = y;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
