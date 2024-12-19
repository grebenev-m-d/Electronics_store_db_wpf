using System;
using System.Collections.Generic;

#nullable disable

namespace Electronics_store_db_wpf.Data.DatabaseModel
{
    public partial class Order : BaseEntity
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        private Guid? _clientId;
        public Guid? ClientId
        {
            get { return _clientId; }
            set
            {
                _clientId = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _orderDate;
        public DateTime? OrderDate
        {
            get { return _orderDate; }
            set
            {
                _orderDate = value;
                OnPropertyChanged();
            }
        }

        private decimal? _totalAmount;
        public decimal? TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                _totalAmount = value;
                OnPropertyChanged();
            }
        }

        private Client _client;
        public virtual Client Client
        {
            get { return _client; }
            set
            {
                _client = value;
                OnPropertyChanged();
            }
        }

        private ICollection<OrderItem> _orderItems;
        public virtual ICollection<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set
            {
                _orderItems = value;
                OnPropertyChanged();
            }
        }

        public void UpdateProperties(Order updatedOrder)
        {
            if (updatedOrder.ClientId.HasValue)
            {
                ClientId = updatedOrder.ClientId.Value;
            }

            if (updatedOrder.OrderDate.HasValue)
            {
                OrderDate = updatedOrder.OrderDate.Value;
            }

            if (updatedOrder.TotalAmount.HasValue)
            {
                TotalAmount = updatedOrder.TotalAmount.Value;
            }

        }
    }
}
