using System;

#nullable disable

namespace Electronics_store_db_wpf.Data.DatabaseModel
{
    public partial class OrderItem : BaseEntity
    {
        private Guid? _orderId;
        public Guid? OrderId
        {
            get { return _orderId; }
            set
            {
                _orderId = value;
                OnPropertyChanged();
            }
        }

        private Guid? _productId;
        public Guid? ProductId
        {
            get { return _productId; }
            set
            {
                _productId = value;
                OnPropertyChanged();
            }
        }

        private int? _quantity;
        public int? Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }

        private decimal? _amount;
        public decimal? Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        private Order _order;
        public virtual Order Order
        {
            get { return _order; }
            set
            {
                _order = value;
                OnPropertyChanged();
            }
        }

        private Product _product;
        public virtual Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

        public void UpdateProperties(OrderItem orderItem)
        {
            if (orderItem.OrderId.HasValue)
            {
                OrderId = orderItem.OrderId.Value;
            }

            if (orderItem.ProductId.HasValue)
            {
                ProductId = orderItem.ProductId.Value;
            }

            if (orderItem.Quantity.HasValue)
            {
                Quantity = orderItem.Quantity.Value;
            }

            if (orderItem.Amount.HasValue)
            {
                Amount = orderItem.Amount.Value;
            }

        }
    }
}
