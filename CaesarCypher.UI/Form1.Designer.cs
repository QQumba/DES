namespace CaesarCypher.UI
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.input = new System.Windows.Forms.TextBox();
            this.output = new System.Windows.Forms.TextBox();
            this.encrypt = new System.Windows.Forms.Button();
            this.key = new System.Windows.Forms.TextBox();
            this.error = new System.Windows.Forms.Label();
            this.decrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // input
            // 
            this.input.Location = new System.Drawing.Point(12, 12);
            this.input.Multiline = true;
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(303, 426);
            this.input.TabIndex = 0;
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(488, 12);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(300, 426);
            this.output.TabIndex = 0;
            // 
            // encrypt
            // 
            this.encrypt.Location = new System.Drawing.Point(321, 170);
            this.encrypt.Name = "encrypt";
            this.encrypt.Size = new System.Drawing.Size(161, 47);
            this.encrypt.TabIndex = 1;
            this.encrypt.Text = "encrypt";
            this.encrypt.UseVisualStyleBackColor = true;
            this.encrypt.Click += new System.EventHandler(this.OnEncryptClicked);
            // 
            // key
            // 
            this.key.Location = new System.Drawing.Point(321, 142);
            this.key.Name = "key";
            this.key.Size = new System.Drawing.Size(161, 22);
            this.key.TabIndex = 2;
            // 
            // error
            // 
            this.error.Location = new System.Drawing.Point(336, 15);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(128, 57);
            this.error.TabIndex = 3;
            // 
            // decrypt
            // 
            this.decrypt.Location = new System.Drawing.Point(321, 223);
            this.decrypt.Name = "decrypt";
            this.decrypt.Size = new System.Drawing.Size(161, 47);
            this.decrypt.TabIndex = 1;
            this.decrypt.Text = "decrypt";
            this.decrypt.UseVisualStyleBackColor = true;
            this.decrypt.Click += new System.EventHandler(this.OnDecryptClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.error);
            this.Controls.Add(this.key);
            this.Controls.Add(this.decrypt);
            this.Controls.Add(this.encrypt);
            this.Controls.Add(this.output);
            this.Controls.Add(this.input);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button decrypt;

        private System.Windows.Forms.Button encrypt;
        private System.Windows.Forms.Label error;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.TextBox key;

        private System.Windows.Forms.TextBox input;

        #endregion
    }
}