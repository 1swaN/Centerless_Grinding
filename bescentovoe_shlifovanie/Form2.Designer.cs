
namespace bescentovoe_shlifovanie
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
            this.output_text = new System.Windows.Forms.TextBox();
            this.exit_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // output_text
            // 
            this.output_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.output_text.Location = new System.Drawing.Point(8, 12);
            this.output_text.Multiline = true;
            this.output_text.Name = "output_text";
            this.output_text.ReadOnly = true;
            this.output_text.Size = new System.Drawing.Size(955, 517);
            this.output_text.TabIndex = 0;
            // 
            // exit_button
            // 
            this.exit_button.Font = new System.Drawing.Font("GOST type B", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.exit_button.Location = new System.Drawing.Point(884, 539);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(79, 38);
            this.exit_button.TabIndex = 3;
            this.exit_button.Text = "Выход";
            this.exit_button.UseVisualStyleBackColor = true;
            this.exit_button.Click += new System.EventHandler(this.exit_button_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 585);
            this.Controls.Add(this.exit_button);
            this.Controls.Add(this.output_text);
            this.Name = "Form2";
            this.Text = "Расчеты";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox output_text;
        private System.Windows.Forms.Button exit_button;
    }
}