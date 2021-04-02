namespace MotorPlantView
{
	partial class FormEngineComponent
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
			this.comboBoxComponent = new System.Windows.Forms.ComboBox();
			this.textBoxCount = new System.Windows.Forms.TextBox();
			this.labelComponent = new System.Windows.Forms.Label();
			this.labelVAlue = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comboBoxComponent
			// 
			this.comboBoxComponent.FormattingEnabled = true;
			this.comboBoxComponent.Location = new System.Drawing.Point(96, 10);
			this.comboBoxComponent.Name = "comboBoxComponent";
			this.comboBoxComponent.Size = new System.Drawing.Size(187, 21);
			this.comboBoxComponent.TabIndex = 0;
			// 
			// textBoxCount
			// 
			this.textBoxCount.Location = new System.Drawing.Point(96, 40);
			this.textBoxCount.Name = "textBoxCount";
			this.textBoxCount.Size = new System.Drawing.Size(187, 20);
			this.textBoxCount.TabIndex = 1;
			// 
			// labelComponent
			// 
			this.labelComponent.AutoSize = true;
			this.labelComponent.Location = new System.Drawing.Point(13, 13);
			this.labelComponent.Name = "labelComponent";
			this.labelComponent.Size = new System.Drawing.Size(63, 13);
			this.labelComponent.TabIndex = 2;
			this.labelComponent.Text = "Компонент";
			// 
			// labelVAlue
			// 
			this.labelVAlue.AutoSize = true;
			this.labelVAlue.Location = new System.Drawing.Point(13, 43);
			this.labelVAlue.Name = "labelVAlue";
			this.labelVAlue.Size = new System.Drawing.Size(66, 13);
			this.labelVAlue.TabIndex = 2;
			this.labelVAlue.Text = "Количество";
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(208, 66);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(127, 66);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// FormEngineComponent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(290, 103);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.labelVAlue);
			this.Controls.Add(this.labelComponent);
			this.Controls.Add(this.textBoxCount);
			this.Controls.Add(this.comboBoxComponent);
			this.Name = "FormEngineComponent";
			this.Text = "Компонент изделия";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxComponent;
		private System.Windows.Forms.TextBox textBoxCount;
		private System.Windows.Forms.Label labelComponent;
		private System.Windows.Forms.Label labelVAlue;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonSave;
	}
}