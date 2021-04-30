using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.BusinessLogics;
using MotorPlantBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace MotorPlantView
{
    public partial class FormFillStore : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly StoreLogic storeLogic;
        private readonly ComponentLogic componentLogic;
        public int StoreId { get { return Convert.ToInt32(comboBoxStore.SelectedValue); } set { comboBoxStore.SelectedValue = value; } }
        public int ComponentId { get { return Convert.ToInt32(comboBoxComponent.SelectedValue); } set { comboBoxComponent.SelectedValue = value; } }
        public int Count { get { return Convert.ToInt32(textBoxCount.Text); } set { textBoxCount.Text = value.ToString(); } }
        public FormFillStore(StoreLogic logicStore, ComponentLogic logicComponent)
        {
            InitializeComponent();
            storeLogic = logicStore;
            List<StoreViewModel> listStore = logicStore.Read(null);
            if (listStore != null)
            {
                comboBoxStore.DisplayMember = "StoreName";
                comboBoxStore.ValueMember = "Id";
                comboBoxStore.DataSource = listStore;
                comboBoxStore.SelectedItem = null;
            }
            List<ComponentViewModel> listComponent = logicComponent.Read(null);
            if (listComponent != null)
            {
                comboBoxComponent.DisplayMember = "ComponentName";
                comboBoxComponent.ValueMember = "Id";
                comboBoxComponent.DataSource = listComponent;
                comboBoxComponent.SelectedItem = null;
            }
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {
            if (textBoxCount.Text.Length == 0)
            {
                MessageBox.Show("Пустое поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxStore.SelectedValue == null)
            {
                MessageBox.Show("Вы не выбрали склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Вы не выбрали компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int count = Convert.ToInt32(textBoxCount.Text);
                if (count < 1)
                {
                    throw new Exception("Надо пополнять, а не уменьшать");
                }
                storeLogic.Fill(new StoreBindingModel
                {
                    Id = Convert.ToInt32(comboBoxStore.SelectedValue)
                }, Convert.ToInt32(comboBoxComponent.SelectedValue), Convert.ToInt32(textBoxCount.Text));
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
