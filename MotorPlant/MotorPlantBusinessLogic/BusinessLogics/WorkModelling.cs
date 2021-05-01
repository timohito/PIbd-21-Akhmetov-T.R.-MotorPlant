using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MotorPlantBusinessLogic.BusinessLogics
{
	public class WorkModelling
	{
        private readonly IImplementerStorage _employeeStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly OrderLogic _orderLogic;
        private readonly Random rnd;
        public WorkModelling(IImplementerStorage employeeStorage, IOrderStorage orderStorage, OrderLogic orderLogic)
        {
            this._employeeStorage = employeeStorage;
            this._orderStorage = orderStorage;
            this._orderLogic = orderLogic;
            rnd = new Random(1000);
        }
        /// <summary>
        /// Запуск работ
        /// </summary>
        public void DoWork()
        {
            var employees = _employeeStorage.GetFullList();
            var orders = _orderStorage.GetFilteredList(new OrderBindingModel { FreeOrders = true });
            foreach (var employee in employees)
            {
                WorkerWorkAsync(employee, orders);
            }
        }
        /// <summary>
        /// Иммитация работы исполнителя
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="orders"></param>
        private async void WorkerWorkAsync(ImplementerViewModel employee, List<OrderViewModel> orders)
        {
            // ищем заказы, которые уже в работе (вдруг исполнителя прервали)
            var runOrders = await Task.Run(() => _orderStorage.GetFilteredList(new OrderBindingModel { ImplementerId = employee.Id }));
            foreach (var order in runOrders)
            {
                // делаем работу заново
                Thread.Sleep(employee.WorkingTime * rnd.Next(1, 5) * order.Count);
                _orderLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = order.Id });
                // отдыхаем
                Thread.Sleep(employee.PauseTime);
            }
            await Task.Run(() =>
            {
                foreach (var order in orders)
                {
                    // пытаемся назначить заказ на исполнителя
                    try
                    {
                        _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = order.Id, ImplementerId = employee.Id });
                        // делаем работу
                        Thread.Sleep(employee.WorkingTime * rnd.Next(1, 5) * order.Count);
                        _orderLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = order.Id });
                        // отдыхаем
                        Thread.Sleep(employee.PauseTime);
                    }
                    catch (Exception) { }
                }
            });
        }
    }
}
