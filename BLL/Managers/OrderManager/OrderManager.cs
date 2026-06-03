using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public class OrderManager : IOrderManager
    {
        /*------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        /*------------------------------------------------------------------*/
        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<OrderReadDto>> PlaceOrderAsync(string userId)
        {
            //get cart
            var cartItems = await _unitOfWork.CartItemRepository.GetCartByUserId(userId);
            if (!cartItems.Any())
                return GeneralResult<OrderReadDto>.Fail("Your cart is empty");
            //check stock before placing the order
            foreach (var item in cartItems)
            {
                if (item.Product.Count < item.Quantity)
                    return GeneralResult<OrderReadDto>
                        .Fail(
                        $"Not enough stock for '{item.Product.Name}'. Available: {item.Product.Count}"
                        );
            }
            //build order items with snapshot prices
            var orderItems = cartItems.Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                UnitPrice = ci.Product.Price    //price snapshot at time of purchase
            }).ToList();

            //create the order
            var order = new Order
            {
                UserId = userId,
                Status = OrderStatus.Pending,
                TotalAmount = orderItems.Sum(oi => oi.UnitPrice * oi.Quantity),
                OrderItems = orderItems
            };

            _unitOfWork.OrderRepository.Create(order);
            //decrement stock for each product
            foreach (var item in cartItems)
            {
                item.Product.Count -= item.Quantity;
                _unitOfWork.ProductRepository.Update(item.Product);
            }
            //clear the cart
            foreach (var item in cartItems)
                _unitOfWork.CartItemRepository.Delete(item);

            await _unitOfWork.Save();

            return GeneralResult<OrderReadDto>.Ok(MapToDto(order), "Order placed successfully");
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<IEnumerable<OrderReadDto>>> GetOrderHistoryAsync(string userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserId(userId);
            return GeneralResult<IEnumerable<OrderReadDto>>.Ok(orders.Select(MapToDto));
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<OrderReadDto>> GetOrderByIdAsync(string userId, int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithItems(orderId);

            if (order == null)
                return GeneralResult<OrderReadDto>.Fail("Order not found");

            // make sure the order belongs to this user
            if (order.UserId != userId)
                return GeneralResult<OrderReadDto>.Fail("Order not found");

            return GeneralResult<OrderReadDto>.Ok(MapToDto(order));
        }
        /*------------------------------------------------------------------*/
        private OrderReadDto MapToDto(Order o) => new()
        {
            Id = o.Id,
            TotalAmount = o.TotalAmount,
            Status = o.Status.ToString(),
            CreatedAt = o.CreatedAt,
            OrderItems = o.OrderItems?.Select(oi => new OrderItemReadDto
            {
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList() ?? new()
        };
        /*------------------------------------------------------------------*/
    }
}