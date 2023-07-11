using Modelo.DTO;
using Modelo.Models;
using Modelo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.IServices
{
    public interface IOrderService
    {
        OrderDTO CreateOrder(OrderViewModel orderVM);
        List<OrderDTO> GetOrders();
        List<OrderDTO> GetOrdersByUserId(int id);
        OrderDTO GetOrderById(int id);
        OrderDTO UpdateOrderStatus(int id, OrderStatusViewModel newStatus);
        string DeleteOrder(int id);
    }
}
