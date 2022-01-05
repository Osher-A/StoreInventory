using Microsoft.EntityFrameworkCore;
using StoreManager.Interfaces;
using StoreManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManager.DAL
{
    public class OrderProductRepository : IOrderProductRepository
    {
        public void UpdateOrderProducts(List<IOrderProduct> orderProducts)
        {
            using var db = new StoreContext();
            foreach (var op in orderProducts)
            {
                var orderProduct = new Model.OrderProduct()
                {
                    OrderId = op.OrderId,
                    ProductId = op.ProductId,
                    Quantity = op.Quantity
                };
                db.OrdersProducts.Add(orderProduct);
            }
            db.SaveChanges();
        }
    }
}


































