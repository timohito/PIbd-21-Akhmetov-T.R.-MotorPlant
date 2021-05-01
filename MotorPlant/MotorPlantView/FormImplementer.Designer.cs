namespace MotorPlantView
{
	partial class FormImplementer
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
            this.textBoxWork = new System.Windows.Forms.TextBox();
            this.labelWork = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelRest = new System.Windows.Forms.Label();
            this.textBoxRest = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxWork
            // 
            this.textBoxWork.Location = new System.Drawing.Point(153, 68);
            this.textBoxWork.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxWork.Name = "textBoxWork";
            this.textBoxWork.Size = new System.Drawing.Size(325, 26);
            this.textBoxWork.TabIndex = 20;
            // 
            // labelWork
            // 
            this.labelWork.AutoSize = true;
            this.labelWork.Location = new System.Drawing.Point(27, 68);
            this.labelWork.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWork.Name = "labelWork";
            this.labelWork.Size = new System.Drawing.Size(118, 20);
            this.labelWork.TabIndex = 19;
            this.labelWork.Text = "Время работы";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(153, 19);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(325, 26);
            this.textBoxName.TabIndex = 18;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(27, 19);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(47, 20);
            this.labelName.TabIndex = 17;
            this.labelName.Text = "ФИО";
            // 
            // labelRest
            // 
            this.labelRest.AutoSize = true;
            this.labelRest.Location = new System.Drawing.Point(27, 122);
            this.labelRest.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRest.Name = "labelRest";
            this.labelRest.Size = new System.Drawing.Size(118, 20);
            this.labelRest.TabIndex = 19;
            this.labelRest.Text = "Время отдыха";
            // 
            // textBoxRest
            // 
            this.textBoxRest.Location = new System.Drawing.Point(153, 122);
            this.textBoxRest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxRest.Name = "textBoxRest";
            this.textBoxRest.Size = new System.Drawing.Size(325, 26);
            this.textBoxRest.TabIndex = 20;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(256, 170);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 35);
            this.buttonCancel.TabIndex = 22;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(135, 170);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(112, 35);
            this.buttonSave.TabIndex = 21;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // FormEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 230);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxRest);
            this.Controls.Add(this.textBoxWork);
            this.Controls.Add(this.labelRest);
            this.Controls.Add(this.labelWork);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Name = "FormEmployee";
            this.Text = "Исполнитель";
            this.Load += new System.EventHandler(this.FormImplementer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxWork;
        private System.Windows.Forms.Label labelWork;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelRest;
        private System.Windows.Forms.TextBox textBoxRest;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
    }
}