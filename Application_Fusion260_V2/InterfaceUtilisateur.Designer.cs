namespace Application_Fusion260_V2
{
    partial class InterfaceUtilisateur
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaceUtilisateur));
            this.button1 = new System.Windows.Forms.Button();
            this.Pmin_TextBox = new System.Windows.Forms.TextBox();
            this.Pmax_TextBox = new System.Windows.Forms.TextBox();
            this.Dmin_TextBox = new System.Windows.Forms.TextBox();
            this.Dmax_TextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(57, 500);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(235, 105);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Pmin_TextBox
            // 
            this.Pmin_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Pmin_TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pmin_TextBox.Location = new System.Drawing.Point(468, 67);
            this.Pmin_TextBox.Name = "Pmin_TextBox";
            this.Pmin_TextBox.Size = new System.Drawing.Size(170, 15);
            this.Pmin_TextBox.TabIndex = 1;
            this.Pmin_TextBox.Text = "Profondeur min";
            this.Pmin_TextBox.TextChanged += new System.EventHandler(this.Pmin_TextBox_TextChanged);
            this.Pmin_TextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pmin_TextBox_MouseDown);
            // 
            // Pmax_TextBox
            // 
            this.Pmax_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Pmax_TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pmax_TextBox.Location = new System.Drawing.Point(468, 115);
            this.Pmax_TextBox.Name = "Pmax_TextBox";
            this.Pmax_TextBox.Size = new System.Drawing.Size(170, 15);
            this.Pmax_TextBox.TabIndex = 2;
            this.Pmax_TextBox.Text = "Profondeur Max";
            this.Pmax_TextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pmax_TextBox_MouseDown);
            // 
            // Dmin_TextBox
            // 
            this.Dmin_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Dmin_TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dmin_TextBox.Location = new System.Drawing.Point(720, 67);
            this.Dmin_TextBox.Name = "Dmin_TextBox";
            this.Dmin_TextBox.Size = new System.Drawing.Size(170, 15);
            this.Dmin_TextBox.TabIndex = 3;
            this.Dmin_TextBox.Text = "Diamètre Min";
            this.Dmin_TextBox.TextChanged += new System.EventHandler(this.Dmin_TextBox_TextChanged);
            this.Dmin_TextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Dmin_TextBox_MouseDown);
            // 
            // Dmax_TextBox
            // 
            this.Dmax_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Dmax_TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dmax_TextBox.Location = new System.Drawing.Point(720, 115);
            this.Dmax_TextBox.Name = "Dmax_TextBox";
            this.Dmax_TextBox.Size = new System.Drawing.Size(170, 15);
            this.Dmax_TextBox.TabIndex = 4;
            this.Dmax_TextBox.Text = "Diamètre Max";
            this.Dmax_TextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Dmax_TextBox_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(574, 354);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // InterfaceUtilisateur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1060, 633);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Dmax_TextBox);
            this.Controls.Add(this.Dmin_TextBox);
            this.Controls.Add(this.Pmax_TextBox);
            this.Controls.Add(this.Pmin_TextBox);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Name = "InterfaceUtilisateur";
            this.Text = "InterfaceUtilisateur";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Pmin_TextBox;
        private System.Windows.Forms.TextBox Pmax_TextBox;
        private System.Windows.Forms.TextBox Dmin_TextBox;
        private System.Windows.Forms.TextBox Dmax_TextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}