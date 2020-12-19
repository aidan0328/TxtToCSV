﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TxtToCSV {
    public partial class Form1 : Form {
        //非同步作業
        void DoWork(object sender, DoWorkEventArgs e) {

            //呼叫自訂函式
            List<String> files = (List<String>)e.Argument;
            OpenAndConverToCSV(files);
        }

        static void OpenAndConverToCSV(List<String> files) {
            foreach (string path in files) {
                if (File.Exists(path) == true) {
                    // Open the file to read from.
                    using (StreamReader sr = File.OpenText(path)) {
                        if (File.Exists(path + ".csv") == true) {
                            File.Delete(path + ".csv");
                        }
                        using (StreamWriter sw = File.CreateText(path + ".csv")) {

                            string s;
                            while ((s = sr.ReadLine()) != null) {
                                string[] str = s.Split('\t');
                                string newStr = null;
                                foreach (String item in str) {
                                    newStr += $"{item},";
                                }
                                Console.WriteLine(newStr);
                                sw.WriteLine(newStr);
                            }
                        }
                    }
                }
            }
        }
        public Form1() {
            InitializeComponent();
        }

        private void btnAddTxt_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "選擇要加入的 TXT 檔";
            openFileDialog1.Filter = "TXT files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                String filePath = openFileDialog1.FileName;
                //MessageBox.Show(openFileDialog1.FileName);

                if (listBox1.Items.Contains(filePath) == false) {
                    listBox1.Items.Add(openFileDialog1.FileName);
                }
            }
        }

        private void btnConvertToCsv_Click(object sender, EventArgs e) {
            if (listBox1.Items.Count <= 0)
                MessageBox.Show("沒有 TXT檔可以加入");
            else {
                btnAddTxt.Enabled = false;
                btnConvertToCsv.Enabled = false;

                List<String> files = new List<String>();
                foreach (String s in listBox1.Items) {
                    files.Add(s);
                }
                BackgroundWorker backgroundWorker1 = new BackgroundWorker();
                backgroundWorker1.DoWork += DoWork;
                backgroundWorker1.RunWorkerAsync(files);

                btnAddTxt.Enabled = true;
                btnConvertToCsv.Enabled = true;
            }
        }
    }
}
