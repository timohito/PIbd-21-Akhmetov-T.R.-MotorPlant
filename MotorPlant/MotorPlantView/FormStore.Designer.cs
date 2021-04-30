namespace MotorPlantView
{
    partial class FormStore
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
            this.dateTimePickerDateCreation = new System.Windows.Forms.DateTimePicker();
            this.textBoxResponsibleName = new System.Windows.Forms.TextBox();
            this.labelDateCreation = new System.Windows.Forms.Label();
            this.labelResponsibleName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxComponents = new System.Windows.Forms.GroupBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Component = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePickerDateCreation
            // 
            this.dateTimePickerDateCreation.Location = new System.Drawing.Point(133, 65);
            this.dateTimePickerDateCreation.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerDateCreation.Name = "dateTimePickerDateCreation";
            this.dateTimePickerDateCreation.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerDateCreation.TabIndex = 26;
            // 
            // textBoxResponsibleName
            // 
            this.textBoxResponsibleName.Location = new System.Drawing.Point(133, 40);
            this.textBoxResponsibleName.Name = "textBoxResponsibleName";
            this.textBoxResponsibleName.Size = new System.Drawing.Size(218, 20);
            this.textBoxResponsibleName.TabIndex = 25;
            // 
            // labelDateCreation
            // 
            this.labelDateCreation.AutoSize = true;
            this.labelDateCreation.Location = new System.Drawing.Point(12, 65);
            this.labelDateCreation.Name = "labelDateCreation";
            this.labelDateCreation.Size = new System.Drawing.Size(84, 13);
            this.labelDateCreation.TabIndex = 23;
            this.labelDateCreation.Text = "Дата создания";
            // 
            // labelResponsibleName
            // 
            this.labelResponsibleName.AutoSize = true;
            this.labelResponsibleName.Location = new System.Drawing.Point(12, 41);
            this.labelResponsibleName.Name = "labelResponsibleName";
            this.labelResponsibleName.Size = new System.Drawing.Size(117, 13);
            this.labelResponsibleName.TabIndex = 24;
            this.labelResponsibleName.Text = "ФИО ответственного";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(133, 18);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(218, 20);
            this.textBoxName.TabIndex = 22;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 18);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(57, 13);
            this.labelName.TabIndex = 21;
            this.labelName.Text = "Название";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(397, 356);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 21);
            this.buttonCancel.TabIndex = 20;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(317, 356);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 21);
            this.buttonSave.TabIndex = 19;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // groupBoxComponents
            // 
            this.groupBoxComponents.Controls.Add(this.dataGridView);
            this.groupBoxComponents.Location = new System.Drawing.Point(15, 101);
            this.groupBoxComponents.Name = "groupBoxComponents";
            this.groupBoxComponents.Size = new System.Drawing.Size(457, 236);
            this.groupBoxComponents.TabIndex = 18;
            this.groupBoxComponents.TabStop = false;
            this.groupBoxComponents.Text = "Компоненты";
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Component,
            this.Count});
            this.dataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView.Location = new System.Drawing.Point(0, 19);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 62;
            this.dataGridView.Size = new System.Drawing.Size(457, 219);
            this.dataGridView.TabIndex = 5;
            // 
            // Component
            // 
            this.Component.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Component.HeaderText = "Компонент";
            this.Component.MinimumWidth = 8;
            this.Component.Name = "Component";
            // 
            // Count
            // 
            this.Count.HeaderText = "Количество";
            this.Count.MinimumWidth = 8;
            this.Count.Name = "Count";
            this.Count.Width = 150;
            // 
            // FormStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 387);
            this.Controls.Add(this.dateTimePickerDateCreation);
            this.Controls.Add(this.textBoxResponsibleName);
            this.Controls.Add(this.labelDateCreation);
            this.Controls.Add(this.labelResponsibleName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxComponents);
            this.Name = "FormStore";
            this.Text = "Склад";
            this.Load += new System.EventHandler(this.FormStore_Load);
            this.groupBoxComponents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerDateCreation;
        private System.Windows.Forms.TextBox textBoxResponsibleName;
        private System.Windows.Forms.Label labelDateCreation;
        private System.Windows.Forms.Label labelResponsibleName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox groupBoxComponents;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Component;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
    }
}