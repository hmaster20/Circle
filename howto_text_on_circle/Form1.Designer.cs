namespace howto_text_on_circle
{
    partial class Form1
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
            this.picText = new System.Windows.Forms.PictureBox();
            this.buttonOne = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picText)).BeginInit();
            this.SuspendLayout();
            // 
            // picText
            // 
            this.picText.BackColor = System.Drawing.Color.White;
            this.picText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picText.Location = new System.Drawing.Point(12, 12);
            this.picText.Name = "picText";
            this.picText.Size = new System.Drawing.Size(302, 301);
            this.picText.TabIndex = 1;
            this.picText.TabStop = false;
            // 
            // buttonOne
            // 
            this.buttonOne.Location = new System.Drawing.Point(392, 264);
            this.buttonOne.Name = "buttonOne";
            this.buttonOne.Size = new System.Drawing.Size(75, 23);
            this.buttonOne.TabIndex = 2;
            this.buttonOne.Text = "Point One";
            this.buttonOne.UseVisualStyleBackColor = true;
            this.buttonOne.Click += new System.EventHandler(this.buttonOne_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 324);
            this.Controls.Add(this.buttonOne);
            this.Controls.Add(this.picText);
            this.Name = "Form1";
            this.Text = "howto_text_on_circle";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picText)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picText;
        private System.Windows.Forms.Button buttonOne;
    }
}

