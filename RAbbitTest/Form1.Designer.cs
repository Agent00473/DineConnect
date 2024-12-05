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
            btnRabbitConfigure = new Button();
            btnRabbitPublish = new Button();
            btnRabbitConsume = new Button();
            textBox1 = new TextBox();
            listView1 = new ListView();
            Detail = new ColumnHeader();
            label1 = new Label();
            btnRabbitStop = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            button6 = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // btnRabbitConfigure
            // 
            btnRabbitConfigure.Location = new Point(20, 237);
            btnRabbitConfigure.Name = "btnRabbitConfigure";
            btnRabbitConfigure.Size = new Size(100, 44);
            btnRabbitConfigure.TabIndex = 0;
            btnRabbitConfigure.Text = "Configure";
            btnRabbitConfigure.UseVisualStyleBackColor = true;
            btnRabbitConfigure.Click += button1_Click;
            // 
            // btnRabbitPublish
            // 
            btnRabbitPublish.Enabled = false;
            btnRabbitPublish.Location = new Point(20, 176);
            btnRabbitPublish.Name = "btnRabbitPublish";
            btnRabbitPublish.Size = new Size(100, 44);
            btnRabbitPublish.TabIndex = 1;
            btnRabbitPublish.Text = "Publish Message";
            btnRabbitPublish.UseVisualStyleBackColor = true;
            btnRabbitPublish.Click += button2_Click;
            // 
            // btnRabbitConsume
            // 
            btnRabbitConsume.Enabled = false;
            btnRabbitConsume.Location = new Point(20, 110);
            btnRabbitConsume.Name = "btnRabbitConsume";
            btnRabbitConsume.Size = new Size(100, 44);
            btnRabbitConsume.TabIndex = 3;
            btnRabbitConsume.Text = "Consume Message";
            btnRabbitConsume.UseVisualStyleBackColor = true;
            btnRabbitConsume.Click += button4_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(83, 16);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(511, 23);
            textBox1.TabIndex = 4;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { Detail });
            listView1.GridLines = true;
            listView1.Location = new Point(12, 57);
            listView1.Name = "listView1";
            listView1.Size = new Size(721, 280);
            listView1.TabIndex = 5;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 20);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 6;
            label1.Text = "Message";
            // 
            // btnRabbitStop
            // 
            btnRabbitStop.Enabled = false;
            btnRabbitStop.Location = new Point(20, 45);
            btnRabbitStop.Name = "btnRabbitStop";
            btnRabbitStop.Size = new Size(100, 44);
            btnRabbitStop.TabIndex = 7;
            btnRabbitStop.Text = "Stop Consuming";
            btnRabbitStop.UseVisualStyleBackColor = true;
            btnRabbitStop.Click += button5_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnRabbitStop);
            groupBox1.Controls.Add(btnRabbitConfigure);
            groupBox1.Controls.Add(btnRabbitPublish);
            groupBox1.Controls.Add(btnRabbitConsume);
            groupBox1.Location = new Point(739, 20);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(144, 305);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "RabbitMQ";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button5);
            groupBox2.Controls.Add(button4);
            groupBox2.Controls.Add(button3);
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(button1);
            groupBox2.Location = new Point(909, 20);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(144, 317);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Event Context";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // button5
            // 
            button5.Enabled = false;
            button5.Location = new Point(20, 257);
            button5.Name = "button5";
            button5.Size = new Size(100, 44);
            button5.TabIndex = 11;
            button5.Text = "Consume Events";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click_1;
            // 
            // button4
            // 
            button4.Enabled = false;
            button4.Location = new Point(20, 207);
            button4.Name = "button4";
            button4.Size = new Size(100, 44);
            button4.TabIndex = 10;
            button4.Text = "Publish Events";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click_1;
            // 
            // button3
            // 
            button3.Enabled = false;
            button3.Location = new Point(20, 157);
            button3.Name = "button3";
            button3.Size = new Size(100, 44);
            button3.TabIndex = 9;
            button3.Text = "Load Event Log";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click_1;
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(20, 106);
            button2.Name = "button2";
            button2.Size = new Size(100, 44);
            button2.TabIndex = 8;
            button2.Text = "Generate Event Log";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // button1
            // 
            button1.Location = new Point(20, 45);
            button1.Name = "button1";
            button1.Size = new Size(100, 44);
            button1.TabIndex = 7;
            button1.Text = "Load Data Context";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button6
            // 
            button6.Location = new Point(1079, 31);
            button6.Name = "button6";
            button6.Size = new Size(100, 44);
            button6.TabIndex = 12;
            button6.Text = "Queued Publisher";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1281, 450);
            Controls.Add(button6);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(listView1);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRabbitConfigure;
        private Button btnRabbitPublish;
        private Button btnRabbitConsume;
        private TextBox textBox1;
        private ListView listView1;
        private Label label1;
        private Button btnRabbitStop;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private ColumnHeader Detail;
        private Button button6;
    }
}
