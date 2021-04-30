using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MotorPlantBusinessLogic.ViewModels
{
    /// <summary>
    /// Склад для хранения изделий
    /// </summary>
    public class StoreViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название склада")]
        public string StoreName { get; set; }
        [DisplayName("ФИО ответственного")]
        public string ResponsibleName { get; set; }
        [DisplayName("Дата создания склада")]
        public DateTime DateCreation { get; set; }
        public Dictionary<int, (string, int)> StoreComponents { get; set; }
    }
}