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
            label1 = new Label();
            btnRabbitStop = new Button();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
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
            listView1.Location = new Point(12, 57);
            listView1.Name = "listView1";
            listView1.Size = new Size(582, 280);
            listView1.TabIndex = 5;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.List;
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
            groupBox1.Location = new Point(626, 16);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(144, 305);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "RabbitMQ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(964, 450);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(listView1);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
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
    }
}
