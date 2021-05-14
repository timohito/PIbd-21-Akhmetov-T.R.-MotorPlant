using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.BusinessLogics;
using MotorPlantBusinessLogic.ViewModels;
using System;
using System.Windows.Forms;
using Unity;
using System.Collections.Generic;

namespace MotorPlantView
{
	public partial class FormCreateOrder : Form
	{
		[Dependency]
		public new IUnityContainer Container { get; set; }

		private readonly EngineLogic _logicP;

		private readonly OrderLogic _logicO;

		private readonly ClientLogic _logicC;

		public FormCreateOrder(EngineLogic logicP, OrderLogic logicO, ClientLogic logicC)
		{
			InitializeComponent();
			_logicP = logicP;
			_logicO = logicO;
			_logicC = logicC;
		}

		private void FormCreateOrder_Load(object sender, EventArgs e)
		{
			try
			{
				List<EngineViewModel> list = _logicP.Read(null);
				if (list != null)
				{
					var clients = _logicC.Read(null);
					comboBoxEngine.DisplayMember = "EngineName";
					comboBoxEngine.ValueMember = "Id";
					comboBoxEngine.DataSource = list;
					comboBoxEngine.SelectedItem = null;

					comboBoxClients.DataSource = clients;
					comboBoxClients.DisplayMember = "ClientFIO";
					comboBoxClients.ValueMember = "Id";
				}
                else
                {
					throw new Exception("Не удалось загрузить список изделий");
                }
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void CalcSum()
		{
			if (comboBoxEngine.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
			{
				try
				{
					int id = Convert.ToInt32(comboBoxEngine.SelectedValue);
					EngineViewModel Engine = _logicP.Read(new EngineBindingModel { Id = id })?[0];
					int count = Convert.ToInt32(textBoxCount.Text);
					textBoxSum.Text = (count * Engine?.Price ?? 0).ToString();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void TextBoxCount_TextChanged(object sender, EventArgs e)
		{
			CalcSum();
		}
		private void ComboBoxEngine_SelectedIndexChanged(object sender, EventArgs e)
		{
			CalcSum();
		}
		private void ButtonSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxCount.Text))
			{
				MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (comboBoxEngine.SelectedValue == null)
			{
				MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (comboBoxClients.SelectedValue == null)
			{
				MessageBox.Show("Select client", "Error", MessageBoxButtons.OK,
			   MessageBoxIcon.Error);
				return;
			}
			try
			{
				_logicO.CreateOrder(new CreateOrderBindingModel
				{
					EngineId = Convert.ToInt32(comboBoxEngine.SelectedValue),
					Count = Convert.ToInt32(textBoxCount.Text),
					Sum = Convert.ToDecimal(textBoxSum.Text),
					ClientId = Convert.ToInt32(comboBoxClients.SelectedValue)
				});
				MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
				DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
