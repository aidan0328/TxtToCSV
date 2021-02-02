using System;
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
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();

        //在這麼事件裡，可以存取表單的資訊
        void _BW_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            //若是已經取消背景作業，就不去更新表單資訊，
            //否則會引發 [引動過程的目標傳回例外狀況]。
            if (((BackgroundWorker)sender).CancellationPending == false) {
                progressBar1.Value = e.ProgressPercentage;
                int percentage = (progressBar1.Value * 100 / progressBar1.Maximum);
                label1.Text = $"{percentage}%";

            }
        }

        //非同步作業已完成
        void _BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) // 非同步作業已被取消...
            {
                btnAddTxt.Enabled = true;
                btnConvertToCsv.Enabled = true;
            } else // 非同步作業順利完成
            {
                btnAddTxt.Enabled = true;
                btnConvertToCsv.Enabled = true;
                progressBar1.Value = progressBar1.Maximum;
                label1.Text = "100%";
                btnCancle.Visible = false;
            }
            btnCancle.Visible = false;
        }

        //非同步作業
        void DoWork(object sender, DoWorkEventArgs e) {
            BackgroundWorker worker = sender as BackgroundWorker;
            //設定 BW 物件可以更新表單資訊報告進度
            worker.WorkerReportsProgress = true;

            //設定是否支援非同步作業取消
            worker.WorkerSupportsCancellation = true;
            // If the operation was canceled by the user,
            // set the DoWorkEventArgs.Cancel property to true.
            if (worker.CancellationPending) {
                e.Cancel = true;
            }

            //繫結 ProgressChanged 事件，當 WorkerReportsProgress 屬性設為 true ，
            //會觸發該事件，同時在這麼函式下，可以更新表單資訊
            worker.ProgressChanged += new ProgressChangedEventHandler(_BW_ProgressChanged);

            //當背景作業已完成時、取消、例外發生時皆會觸發該事件
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BW_RunWorkerCompleted);

            //呼叫自訂函式
            OpenAndConverToCSV(worker, e);
        }

        static void OpenAndConverToCSV(BackgroundWorker worker, DoWorkEventArgs e) {
            List<String> files = (List<String>)e.Argument;
            int percentProgress = 0;

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
                                sw.WriteLine(newStr);

                                //判斷使用者是否已經取消背景作業
                                if (worker.CancellationPending == true) {

                                    //設定取消事件
                                    e.Cancel = true;

                                    // 離開
                                    return;
                                }

                                ++percentProgress;
                                if ((percentProgress % 100) == 0) {
                                    // 呼叫這個方法，會觸發 ProgressChanged 事件    
                                    // 這邊會另外開一條執行緒去觸發事件 (_BW_ProgressChanged)。
                                    worker.ReportProgress(percentProgress);
                                }
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
                btnCancle.Visible = true;
                progressBar1.Value = 0;

                // 將檔案的完整路徑加入到 files
                List<String> files = new List<String>();
                foreach (String s in listBox1.Items) {
                    files.Add(s);
                }

                // 以行為單位，決定 progressBar1 的最大值
                int Maximum = 0;

                foreach (String s in files) {
                    if (File.Exists(s) == true) {
                        Maximum += File.ReadAllLines(s).Length;
                    }
                }
                progressBar1.Maximum = Maximum;

                // 採用非同步的 BackgroundWorker 進行轉換

                backgroundWorker1.DoWork += DoWork;
                backgroundWorker1.RunWorkerAsync(files);
            }
        }

        private void btnCancle_Click(object sender, EventArgs e) {
            // 先判斷非同步作業是否仍在執行中。
            if (backgroundWorker1.IsBusy) {
                backgroundWorker1.CancelAsync();
            }
        }
    }
}
