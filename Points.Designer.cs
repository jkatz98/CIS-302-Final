namespace Battleship_Game
{
    partial class Points
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.p1Points_label = new System.Windows.Forms.Label();
            this.p2Points_label = new System.Windows.Forms.Label();
            this.clear_button = new System.Windows.Forms.Button();
            this.back_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Player 2:";
            // 
            // p1Points_label
            // 
            this.p1Points_label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.p1Points_label.Location = new System.Drawing.Point(116, 28);
            this.p1Points_label.Name = "p1Points_label";
            this.p1Points_label.Size = new System.Drawing.Size(100, 23);
            this.p1Points_label.TabIndex = 2;
            // 
            // p2Points_label
            // 
            this.p2Points_label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.p2Points_label.Location = new System.Drawing.Point(116, 87);
            this.p2Points_label.Name = "p2Points_label";
            this.p2Points_label.Size = new System.Drawing.Size(100, 23);
            this.p2Points_label.TabIndex = 3;
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(26, 138);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(75, 23);
            this.clear_button.TabIndex = 4;
            this.clear_button.Text = "Clear Points";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // back_button
            // 
            this.back_button.Location = new System.Drawing.Point(187, 138);
            this.back_button.Name = "back_button";
            this.back_button.Size = new System.Drawing.Size(75, 23);
            this.back_button.TabIndex = 5;
            this.back_button.Text = "Back";
            this.back_button.UseVisualStyleBackColor = true;
            this.back_button.Click += new System.EventHandler(this.back_button_Click);
            // 
            // Points
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 173);
            this.Controls.Add(this.back_button);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.p2Points_label);
            this.Controls.Add(this.p1Points_label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Points";
            this.Text = "Points";
            this.Load += new System.EventHandler(this.Points_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label p1Points_label;
        private System.Windows.Forms.Label p2Points_label;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.Button back_button;
    }
}