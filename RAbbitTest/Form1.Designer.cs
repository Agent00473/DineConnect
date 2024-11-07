namespace RAbbitTest
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
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            textBox1 = new TextBox();
            listView1 = new ListView();
            label1 = new Label();
            button5 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(637, 334);
            button1.Name = "button1";
            button1.Size = new Size(100, 44);
            button1.TabIndex = 0;
            button1.Text = "Create Connection";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(637, 211);
            button2.Name = "button2";
            button2.Size = new Size(100, 44);
            button2.TabIndex = 1;
            button2.Text = "Publish Message";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(637, 275);
            button3.Name = "button3";
            button3.Size = new Size(100, 44);
            button3.TabIndex = 2;
            button3.Text = "Create Channel";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(637, 142);
            button4.Name = "button4";
            button4.Size = new Size(100, 44);
            button4.TabIndex = 3;
            button4.Text = "Consume Message";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(83, 117);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(511, 23);
            textBox1.TabIndex = 4;
            // 
            // listView1
            // 
            listView1.Location = new Point(12, 158);
            listView1.Name = "listView1";
            listView1.Size = new Size(582, 280);
            listView1.TabIndex = 5;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 121);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 6;
            label1.Text = "Message";
            // 
            // button5
            // 
            button5.Location = new Point(637, 92);
            button5.Name = "button5";
            button5.Size = new Size(100, 44);
            button5.TabIndex = 7;
            button5.Text = "Stop Consuming";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button5);
            Controls.Add(label1);
            Controls.Add(listView1);
            Controls.Add(textBox1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private TextBox textBox1;
        private ListView listView1;
        private Label label1;
        private Button button5;
    }
}
