﻿
namespace TxtToCSV {
    partial class Form1 {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent() {
            this.btnAddTxt = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnConvertToCsv = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddTxt
            // 
            this.btnAddTxt.Location = new System.Drawing.Point(12, 12);
            this.btnAddTxt.Name = "btnAddTxt";
            this.btnAddTxt.Size = new System.Drawing.Size(81, 42);
            this.btnAddTxt.TabIndex = 0;
            this.btnAddTxt.Text = "加入 Txt";
            this.btnAddTxt.UseVisualStyleBackColor = true;
            this.btnAddTxt.Click += new System.EventHandler(this.btnAddTxt_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(112, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(450, 88);
            this.listBox1.TabIndex = 1;
            // 
            // btnConvertToCsv
            // 
            this.btnConvertToCsv.Location = new System.Drawing.Point(12, 60);
            this.btnConvertToCsv.Name = "btnConvertToCsv";
            this.btnConvertToCsv.Size = new System.Drawing.Size(81, 42);
            this.btnConvertToCsv.TabIndex = 2;
            this.btnConvertToCsv.Text = "轉換為 CSV";
            this.btnConvertToCsv.UseVisualStyleBackColor = true;
            this.btnConvertToCsv.Click += new System.EventHandler(this.btnConvertToCsv_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnConvertToCsv);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnAddTxt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAddTxt;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnConvertToCsv;
    }
}

