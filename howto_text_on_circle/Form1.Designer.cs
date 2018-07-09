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
            this.buttonFor = new System.Windows.Forms.Button();
            this.buttonMaps = new System.Windows.Forms.Button();
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
            this.buttonOne.Location = new System.Drawing.Point(393, 197);
            this.buttonOne.Name = "buttonOne";
            this.buttonOne.Size = new System.Drawing.Size(75, 23);
            this.buttonOne.TabIndex = 2;
            this.buttonOne.Text = "Point One";
            this.buttonOne.UseVisualStyleBackColor = true;
            this.buttonOne.Click += new System.EventHandler(this.buttonOne_Click);
            // 
            // buttonFor
            // 
            this.buttonFor.Location = new System.Drawing.Point(393, 226);
            this.buttonFor.Name = "buttonFor";
            this.buttonFor.Size = new System.Drawing.Size(75, 23);
            this.buttonFor.TabIndex = 2;
            this.buttonFor.Text = "Point For";
            this.buttonFor.UseVisualStyleBackColor = true;
            this.buttonFor.Click += new System.EventHandler(this.buttonFor_Click);
            // 
            // buttonMaps
            // 
            this.buttonMaps.Location = new System.Drawing.Point(393, 255);
            this.buttonMaps.Name = "buttonMaps";
            this.buttonMaps.Size = new System.Drawing.Size(75, 23);
            this.buttonMaps.TabIndex = 2;
            this.buttonMaps.Text = "Maps";
            this.buttonMaps.UseVisualStyleBackColor = true;
            this.buttonMaps.Click += new System.EventHandler(this.buttonMaps_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 324);
            this.Controls.Add(this.buttonMaps);
            this.Controls.Add(this.buttonFor);
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
        private System.Windows.Forms.Button buttonFor;
        private System.Windows.Forms.Button buttonMaps;
    }
}

