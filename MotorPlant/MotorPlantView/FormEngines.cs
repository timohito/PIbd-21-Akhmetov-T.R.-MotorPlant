using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.BusinessLogics;
using System;
using System.Windows.Forms;
using Unity;

namespace MotorPlantView
{
    public partial class FormEngines : Form
    {
		[Dependency]
		public new IUnityContainer Container { get; set; }

		private readonly EngineLogic logic;

		public FormEngines(EngineLogic logic)
        {
			InitializeComponent();
            this.logic = logic;
        }

		private void FormEngines_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		private void LoadData()
		{
			try
			{
				var list = logic.Read(null);
				if (list != null)
				{
					dataGridView.DataSource = list;
					dataGridView.Columns[0].Visible = false;
					dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
					dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
					dataGridView.Columns[3].Visible = false;

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void ButtonAdd_Click(object sender, EventArgs e)
		{
			var form = Container.Resolve<FormEngine>();
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData();
			}
		}
		private void ButtonUpd_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				var form = Container.Resolve<FormEngine>();
				form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
				if (form.ShowDialog() == DialogResult.OK)
				{
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
					int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
					try
					{
						logic.Delete(new EngineBindingModel { Id = id });
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
	}
}
