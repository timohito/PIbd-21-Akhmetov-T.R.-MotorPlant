namespace MotorPlantView
{
    partial class FormReportOrders
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
			this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.ButtonMake = new System.Windows.Forms.Button();
			this.ButtonToPdf = new System.Windows.Forms.Button();
			this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
			this.SuspendLayout();
			// 
			// dateTimePickerFrom
			// 
			this.dateTimePickerFrom.Location = new System.Drawing.Point(37, 12);
			this.dateTimePickerFrom.Name = "dateTimePickerFrom";
			this.dateTimePickerFrom.Size = new System.Drawing.Size(200, 20);
			this.dateTimePickerFrom.TabIndex = 0;
			// 
			// dateTimePickerTo
			// 
			this.dateTimePickerTo.Location = new System.Drawing.Point(295, 12);
			this.dateTimePickerTo.Name = "dateTimePickerTo";
			this.dateTimePickerTo.Size = new System.Drawing.Size(200, 20);
			this.dateTimePickerTo.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(258, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(19, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "по";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(13, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "с";
			// 
			// ButtonMake
			// 
			this.ButtonMake.Location = new System.Drawing.Point(537, 9);
			this.ButtonMake.Name = "ButtonMake";
			this.ButtonMake.Size = new System.Drawing.Size(92, 23);
			this.ButtonMake.TabIndex = 4;
			this.ButtonMake.Text = "Сформировать";
			this.ButtonMake.UseVisualStyleBackColor = true;
			this.ButtonMake.Click += new System.EventHandler(this.ButtonMake_Click);
			// 
			// ButtonToPdf
			// 
			this.ButtonToPdf.Location = new System.Drawing.Point(696, 9);
			this.ButtonToPdf.Name = "ButtonToPdf";
			this.ButtonToPdf.Size = new System.Drawing.Size(92, 23);
			this.ButtonToPdf.TabIndex = 5;
			this.ButtonToPdf.Text = "в PDF";
			this.ButtonToPdf.UseVisualStyleBackColor = true;
			this.ButtonToPdf.Click += new System.EventHandler(this.ButtonToPdf_Click);
			// 
			// reportViewer
			// 
			this.reportViewer.LocalReport.ReportEmbeddedResource = "MotorPlantView.Report.rdlc";
			this.reportViewer.Location = new System.Drawing.Point(0, 38);
			this.reportViewer.Name = "reportViewer";
			this.reportViewer.ServerReport.BearerToken = null;
			this.reportViewer.Size = new System.Drawing.Size(800, 414);
			this.reportViewer.TabIndex = 6;
			// 
			// FormReportOrders
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.reportViewer);
			this.Controls.Add(this.ButtonToPdf);
			this.Controls.Add(this.ButtonMake);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateTimePickerTo);
			this.Controls.Add(this.dateTimePickerFrom);
			this.Name = "FormReportOrders";
			this.Text = "Заказы клиентов";
			this.Load += new System.EventHandler(this.FormReportOrders_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonMake;
        private System.Windows.Forms.Button ButtonToPdf;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
}