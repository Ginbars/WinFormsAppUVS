namespace WinFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            label1 = new Label();
            listView1 = new ListView();
            ThreadID = new ColumnHeader();
            Data = new ColumnHeader();
            comboBox1 = new ComboBox();
            button2 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(189, 10);
            button1.Name = "button1";
            button1.Size = new Size(61, 25);
            button1.TabIndex = 0;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 17);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 2;
            label1.Text = "Thread Count:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { ThreadID, Data });
            listView1.Location = new Point(12, 55);
            listView1.Name = "listView1";
            listView1.Size = new Size(238, 365);
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // ThreadID
            // 
            ThreadID.Text = "ThreadID";
            ThreadID.Width = 80;
            // 
            // Data
            // 
            Data.Text = "Data";
            Data.Width = 120;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(100, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(54, 23);
            comboBox1.TabIndex = 4;
            // 
            // button2
            // 
            button2.Location = new Point(91, 426);
            button2.Name = "button2";
            button2.Size = new Size(75, 33);
            button2.TabIndex = 5;
            button2.Text = "Stop";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(262, 460);
            Controls.Add(button2);
            Controls.Add(comboBox1);
            Controls.Add(listView1);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private ListView listView1;
        private ComboBox comboBox1;
        public ColumnHeader ThreadID;
        private ColumnHeader Data;
        private Button button2;
    }
}