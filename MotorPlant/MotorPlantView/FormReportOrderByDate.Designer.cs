namespace MotorPlantView
{
    partial class FormReportOrderByDate
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
            this.components = new System.ComponentModel.Container();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.labelTo = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonPDF = new System.Windows.Forms.Button();
            this.labelFrom = new System.Windows.Forms.Label();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel = new System.Windows.Forms.Panel();
            this.ReportOrdersViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ReportOrdersViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(81, 17);
            this.dateTimePickerFrom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(298, 26);
            this.dateTimePickerFrom.TabIndex = 0;
            this.dateTimePickerFrom.Value = new System.DateTime(2021, 3, 14, 0, 0, 0, 0);
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(465, 18);
            this.dateTimePickerTo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(298, 26);
            this.dateTimePickerTo.TabIndex = 1;
            this.dateTimePickerTo.Value = new System.DateTime(2021, 3, 14, 0, 0, 0, 0);
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(406, 18);
            this.labelTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(30, 20);
            this.labelTo.TabIndex = 4;
            this.labelTo.Text = "По";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(796, 20);
            this.buttonCreate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(159, 35);
            this.buttonCreate.TabIndex = 5;
            this.buttonCreate.Text = "Сформировать";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.ButtonCreate_Click);
            // 
            // buttonPDF
            // 
            this.buttonPDF.Location = new System.Drawing.Point(1070, 20);
            this.buttonPDF.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonPDF.Name = "buttonPDF";
            this.buttonPDF.Size = new System.Drawing.Size(112, 35);
            this.buttonPDF.TabIndex = 6;
            this.buttonPDF.Text = "В PDF";
            this.buttonPDF.UseVisualStyleBackColor = true;
            this.buttonPDF.Click += new System.EventHandler(this.ButtonToPdf_Click);
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(18, 18);
            this.labelFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(20, 20);
            this.labelFrom.TabIndex = 3;
            this.labelFrom.Text = "С";
            // 
            // reportViewer
            // 
            this.reportViewer.LocalReport.ReportEmbeddedResource = "MotorPlantView.OrderReportByDate.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(18, 80);
            this.reportViewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(1163, 608);
            this.reportViewer.TabIndex = 7;
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(2, 5);
            this.panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1194, 66);
            this.panel.TabIndex = 8;
            // 
            // ReportOrdersViewModelBindingSource
            // 
            this.ReportOrdersViewModelBindingSource.DataSource = typeof(MotorPlantBusinessLogic.ViewModels.ReportOrdersViewModel);
            // 
            // FormReportOrderByDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.buttonPDF);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.panel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormReportOrderByDate";
            this.Text = "Заказы клиентов";
            ((System.ComponentModel.ISupportInitialize)(this.ReportOrdersViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonPDF;
        private System.Windows.Forms.Label labelFrom;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource ReportOrdersViewModelBindingSource;
        private System.Windows.Forms.Panel panel;
    }
}