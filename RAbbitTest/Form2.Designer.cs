namespace InfraTest
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listView1 = new ListView();
            Detail = new ColumnHeader();
            groupBox3 = new GroupBox();
            button2 = new Button();
            button9 = new Button();
            button10 = new Button();
            button11 = new Button();
            button4 = new Button();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { Detail });
            listView1.GridLines = true;
            listView1.Location = new Point(12, 36);
            listView1.Name = "listView1";
            listView1.Size = new Size(1052, 280);
            listView1.TabIndex = 6;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(button2);
            groupBox3.Controls.Add(button9);
            groupBox3.Controls.Add(button10);
            groupBox3.Controls.Add(button11);
            groupBox3.Location = new Point(1120, 36);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(144, 352);
            groupBox3.TabIndex = 13;
            groupBox3.TabStop = false;
            groupBox3.Text = "Queued Event";
            // 
            // button2
            // 
            button2.Location = new Point(20, 285);
            button2.Name = "button2";
            button2.Size = new Size(100, 44);
            button2.TabIndex = 11;
            button2.Text = "Generate Events";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button9
            // 
            button9.Location = new Point(20, 72);
            button9.Name = "button9";
            button9.Size = new Size(100, 44);
            button9.TabIndex = 9;
            button9.Text = "Subscribe Events";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button10
            // 
            button10.Enabled = false;
            button10.Location = new Point(20, 122);
            button10.Name = "button10";
            button10.Size = new Size(100, 44);
            button10.TabIndex = 8;
            button10.Text = "Close Publisher";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // button11
            // 
            button11.Location = new Point(20, 22);
            button11.Name = "button11";
            button11.Size = new Size(100, 44);
            button11.TabIndex = 7;
            button11.Text = "Start Publisher";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // button4
            // 
            button4.Location = new Point(842, 344);
            button4.Name = "button4";
            button4.Size = new Size(100, 44);
            button4.TabIndex = 13;
            button4.Text = "Clear Events";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1353, 450);
            Controls.Add(button4);
            Controls.Add(groupBox3);
            Controls.Add(listView1);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
        private ColumnHeader Detail;
        private GroupBox groupBox3;
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button2;
        private Button button4;
    }
}