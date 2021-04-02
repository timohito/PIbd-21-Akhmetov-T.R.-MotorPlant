using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.BusinessLogics;
using MotorPlantBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
namespace MotorPlantView
{
	public partial class FormEngine : Form
	{
		[Dependency]
		public new IUnityContainer Container { get; set; }
		public int Id { set { id = value; } }

		private readonly EngineLogic logic;

		private int? id;

		private Dictionary<int, (string, int)> EngineComponents;

		public FormEngine(EngineLogic service)
		{
			InitializeComponent();
			logic = service;
		}

		private void FormEngine_Load(object sender, EventArgs e)
		{
			if (id.HasValue)
			{
				try
				{
				EngineViewModel view = logic.Read(new EngineBindingModel { Id = id.Value })?[0];
					if (view != null)
					{
						textBoxName.Text = view.EngineName;
						textBoxPrice.Text = view.Price.ToString();
						EngineComponents = view.EngineComponents;
						LoadData();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				EngineComponents = new Dictionary<int, (string, int)>();
			}
		}

        private void LoadData()
		{
			try
			{
				if (EngineComponents != null)
				{
					dataGridView.Rows.Clear();
					foreach (var pc in EngineComponents)
					{
						dataGridView.Rows.Add(new object[] { pc.Value.Item1, pc.Value.Item2 });
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ButtonAdd_Click(object sender, EventArgs e)
		{
			var form = Container.Resolve<FormEngineComponent>();
			if (form.ShowDialog() == DialogResult.OK)
			{
				if (EngineComponents.ContainsKey(form.Id))
				{
					EngineComponents[form.Id] = (form.ComponentName, form.Count);
				}
				else
				{
					EngineComponents.Add(form.Id, (form.ComponentName, form.Count));
				}
				LoadData();
			}
		}
		private void ButtonUpd_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				var form = Container.Resolve<FormEngineComponent>();
				int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
				form.Id = id;
				form.Count = EngineComponents[id].Item2;
				if (form.ShowDialog() == DialogResult.OK)
				{
					EngineComponents[form.Id] = (form.ComponentName, form.Count);
					LoadData();
				}
			}
		}
		private void ButtonDel_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					try
					{
						EngineComponents.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					LoadData();
				}
			}
		}
		private void ButtonRef_Click(object sender, EventArgs e)
		{
			LoadData();
		}
		private void ButtonSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxName.Text))
			{
				MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (string.IsNullOrEmpty(textBoxPrice.Text))
			{
				MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (EngineComponents == null || EngineComponents.Count == 0)
			{
				MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			try
			{
				logic.CreateOrUpdate(new EngineBindingModel
				{
					Id = id,
				EngineName = textBoxName.Text,
					Price = Convert.ToDecimal(textBoxPrice.Text),
					EngineComponents = EngineComponents
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