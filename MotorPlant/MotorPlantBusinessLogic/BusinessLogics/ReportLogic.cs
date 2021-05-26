using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.BuisnessLogics;
using MotorPlantBusinessLogic.HelperModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MotorPlantBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IEngineStorage _engineStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly IStoreStorage _storeStorage;
        public ReportLogic(IEngineStorage engineStorage, IComponentStorage componentStorage, IOrderStorage orderStorage, IStoreStorage storeStorage)
        {
            _engineStorage = engineStorage;
            _orderStorage = orderStorage;
            _storeStorage = storeStorage;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportEngineComponentViewModel> GetEngineComponent()
        {
            var engines = _engineStorage.GetFullList();
            var list = new List<ReportEngineComponentViewModel>();
            foreach (var engine in engines)
            {
                var record = new ReportEngineComponentViewModel
                {
                    EngineName = engine.EngineName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in engine.EngineComponents)
                {
                    record.Components.Add(new Tuple<string, int>(component.Value.Item1, component.Value.Item2));
                    record.TotalCount += component.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                EngineName = x.EngineName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
            .ToList();
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                Engines = _engineStorage.GetFullList()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveEngineComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                EngineComponents = GetEngineComponent()
            });
        }

        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        [Obsolete]
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }

        public List<ReportStoreComponentViewModel> GetStoreComponent()
        {
            var stores = _storeStorage.GetFullList();
            var list = new List<ReportStoreComponentViewModel>();
            foreach (var store in stores)
            {
                var record = new ReportStoreComponentViewModel
                {
                    StoreName = store.StoreName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in store.StoreComponents)
                {
                    record.Components.Add(new Tuple<string, int>(component.Value.Item1, component.Value.Item2));
                    record.TotalCount += component.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        public List<OrderReportByDateViewModel> GetOrderReportByDate()
        {
            return _orderStorage.GetFullList()
                .GroupBy(order => order.DateCreate.ToShortDateString())
                .Select(rec => new OrderReportByDateViewModel
                {
                    Date = Convert.ToDateTime(rec.Key),
                    Count = rec.Count(),
                    Sum = rec.Sum(order => order.Sum)
                })
                .ToList();
        }

        public void SaveStoresToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateStoresDoc(new StoreWordInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                Stores = _storeStorage.GetFullList()
            });
        }

        public void SaveStoreComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateStoresDoc(new StoresExcelInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                StoreComponents = GetStoreComponent()
            });
        }

        public void SaveOrderReportByDateToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDocOrderReportByDate(new PdfInfoOrderReportByDate
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrderReportByDate()
            });
        }
    }
}