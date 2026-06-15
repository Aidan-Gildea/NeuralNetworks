namespace TicTacToeAI
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
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(39, 110);
            button1.Name = "button1";
            button1.Size = new Size(106, 102);
            button1.TabIndex = 0;
            button1.Tag = "0,0";
            button1.Text = "";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(189, 110);
            button2.Name = "button2";
            button2.Size = new Size(105, 102);
            button2.TabIndex = 1;
            button2.Tag = "0,1";
            button2.Text = "";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(345, 110);
            button3.Name = "button3";
            button3.Size = new Size(98, 102);
            button3.TabIndex = 2;
            button3.Tag = "0,2";
            button3.Text = "";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(39, 250);
            button4.Name = "button4";
            button4.Size = new Size(106, 98);
            button4.TabIndex = 3;
            button4.Tag = "1,0";
            button4.Text = "";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(189, 250);
            button5.Name = "button5";
            button5.Size = new Size(105, 98);
            button5.TabIndex = 4;
            button5.Tag = "1,1";
            button5.Text = "";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(345, 250);
            button6.Name = "button6";
            button6.Size = new Size(98, 98);
            button6.TabIndex = 5;
            button6.Tag = "1,2";
            button6.Text = "";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(39, 385);
            button7.Name = "button7";
            button7.Size = new Size(106, 101);
            button7.TabIndex = 6;
            button7.Tag = "2,0";
            button7.Text = "";
            button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Location = new Point(189, 385);
            button8.Name = "button8";
            button8.Size = new Size(105, 101);
            button8.TabIndex = 7;
            button8.Tag = "2,1";
            button8.Text = "";
            button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new Point(345, 385);
            button9.Name = "button9";
            button9.Size = new Size(98, 101);
            button9.TabIndex = 8;
            button9.Tag = "2,2";
            button9.Text = "";
            button9.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 21);
            label1.Name = "label1";
            label1.Size = new Size(106, 25);
            label1.TabIndex = 9;
            label1.Text = "AI TicTacToe";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(39, 59);
            label2.Name = "label2";
            label2.Size = new Size(51, 25);
            label2.TabIndex = 10;
            label2.Text = "Turn:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(483, 524);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
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
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Label label1;
        private Label label2;
    }
}
