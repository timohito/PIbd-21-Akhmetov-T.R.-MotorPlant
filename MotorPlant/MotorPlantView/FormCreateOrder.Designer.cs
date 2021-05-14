namespace MotorPlantView
{
	partial class FormCreateOrder
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
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.textBoxSum = new System.Windows.Forms.TextBox();
			this.labelSum = new System.Windows.Forms.Label();
			this.textBoxCount = new System.Windows.Forms.TextBox();
			this.labelCount = new System.Windows.Forms.Label();
			this.labelEngine = new System.Windows.Forms.Label();
			this.comboBoxEngine = new System.Windows.Forms.ComboBox();
			this.labelClient = new System.Windows.Forms.Label();
			this.comboBoxClients = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(239, 148);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 14;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(158, 148);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 13;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// textBoxSum
			// 
			this.textBoxSum.Enabled = false;
			this.textBoxSum.Location = new System.Drawing.Point(96, 79);
			this.textBoxSum.Name = "textBoxSum";
			this.textBoxSum.Size = new System.Drawing.Size(218, 20);
			this.textBoxSum.TabIndex = 20;
			// 
			// labelSum
			// 
			this.labelSum.AutoSize = true;
			this.labelSum.Location = new System.Drawing.Point(13, 82);
			this.labelSum.Name = "labelSum";
			this.labelSum.Size = new System.Drawing.Size(41, 13);
			this.labelSum.TabIndex = 19;
			this.labelSum.Text = "Сумма";
			// 
			// textBoxCount
			// 
			this.textBoxCount.Location = new System.Drawing.Point(96, 44);
			this.textBoxCount.Name = "textBoxCount";
			this.textBoxCount.Size = new System.Drawing.Size(218, 20);
			this.textBoxCount.TabIndex = 18;
			this.textBoxCount.TextChanged += new System.EventHandler(this.TextBoxCount_TextChanged);
			// 
			// labelCount
			// 
			this.labelCount.AutoSize = true;
			this.labelCount.Location = new System.Drawing.Point(12, 47);
			this.labelCount.Name = "labelCount";
			this.labelCount.Size = new System.Drawing.Size(66, 13);
			this.labelCount.TabIndex = 17;
			this.labelCount.Text = "Количество";
			// 
			// labelEngine
			// 
			this.labelEngine.AutoSize = true;
			this.labelEngine.Location = new System.Drawing.Point(13, 15);
			this.labelEngine.Name = "labelEngine";
			this.labelEngine.Size = new System.Drawing.Size(51, 13);
			this.labelEngine.TabIndex = 22;
			this.labelEngine.Text = "Изделие";
			// 
			// comboBoxEngine
			// 
			this.comboBoxEngine.FormattingEnabled = true;
			this.comboBoxEngine.Location = new System.Drawing.Point(96, 12);
			this.comboBoxEngine.Name = "comboBoxEngine";
			this.comboBoxEngine.Size = new System.Drawing.Size(218, 21);
			this.comboBoxEngine.TabIndex = 21;
			this.comboBoxEngine.SelectedIndexChanged += new System.EventHandler(this.ComboBoxEngine_SelectedIndexChanged);
			// 
			// labelClient
			// 
			this.labelClient.AutoSize = true;
			this.labelClient.Location = new System.Drawing.Point(13, 111);
			this.labelClient.Name = "labelClient";
			this.labelClient.Size = new System.Drawing.Size(43, 13);
			this.labelClient.TabIndex = 23;
			this.labelClient.Text = "Клиент";
			// 
			// comboBoxClients
			// 
			this.comboBoxClients.FormattingEnabled = true;
			this.comboBoxClients.Location = new System.Drawing.Point(96, 108);
			this.comboBoxClients.Name = "comboBoxClients";
			this.comboBoxClients.Size = new System.Drawing.Size(218, 21);
			this.comboBoxClients.TabIndex = 24;
			// 
			// FormCreateOrder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 204);
			this.Controls.Add(this.comboBoxClients);
			this.Controls.Add(this.labelClient);
			this.Controls.Add(this.labelEngine);
			this.Controls.Add(this.comboBoxEngine);
			this.Controls.Add(this.textBoxSum);
			this.Controls.Add(this.labelSum);
			this.Controls.Add(this.textBoxCount);
			this.Controls.Add(this.labelCount);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Name = "FormCreateOrder";
			this.Text = "Заказ";
			this.Load += new System.EventHandler(this.FormCreateOrder_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.TextBox textBoxSum;
		private System.Windows.Forms.Label labelSum;
		private System.Windows.Forms.TextBox textBoxCount;
		private System.Windows.Forms.Label labelCount;
		private System.Windows.Forms.Label labelEngine;
		private System.Windows.Forms.ComboBox comboBoxEngine;
		private System.Windows.Forms.Label labelClient;
		private System.Windows.Forms.ComboBox comboBoxClients;
	}
}