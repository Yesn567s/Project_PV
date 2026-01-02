using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_PV
{ 
    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }

        public int Subtotal
        {
            get { return UnitPrice * Quantity; }
        }
    }

    // Static Cart Manager - Singleton pattern
    public static class CartManager
    {
        private static List<CartItem> cartItems = new List<CartItem>();

        // Event to notify when cart changes
        public static event EventHandler CartChanged;

        // Get all cart items
        public static List<CartItem> GetCartItems()
        {
            return new List<CartItem>(cartItems);
        }

        // Add item to cart
        public static void AddToCart(int productID, string productName, string brand,
                                     string category, int price, string imageUrl, int quantity = 1)
        {
            // Check if item already exists in cart
            var existingItem = cartItems.FirstOrDefault(i => i.ProductID == productID);

            if (existingItem != null)
            {
                // Increase quantity
                existingItem.Quantity += quantity;
            }
            else
            {
                // Add new item
                cartItems.Add(new CartItem
                {
                    ProductID = productID,
                    ProductName = productName,
                    Brand = brand,
                    Category = category,
                    UnitPrice = price,
                    Quantity = quantity,
                    ImageUrl = imageUrl
                });
            }

            OnCartChanged();
        }

        // Update item quantity
        public static void UpdateQuantity(int productID, int newQuantity)
        {
            var item = cartItems.FirstOrDefault(i => i.ProductID == productID);
            if (item != null)
            {
                if (newQuantity <= 0)
                {
                    RemoveFromCart(productID);
                }
                else
                {
                    item.Quantity = newQuantity;
                    OnCartChanged();
                }
            }
        }

        // Remove item from cart
        public static void RemoveFromCart(int productID)
        {
            cartItems.RemoveAll(i => i.ProductID == productID);
            OnCartChanged();
        }

        // Clear entire cart
        public static void ClearCart()
        {
            cartItems.Clear();
            OnCartChanged();
        }

        // Get cart totals
        public static int GetSubtotal()
        {
            return cartItems.Sum(i => i.Subtotal);
        }

        public static int GetItemCount()
        {
            return cartItems.Sum(i => i.Quantity);
        }

        public static int GetTotalItems()
        {
            return cartItems.Count;
        }

        // Calculate discount (if member)
        public static int CalculateDiscount(bool isMember, decimal discountPercentage = 5)
        {
            if (!isMember) return 0;
            return (int)(GetSubtotal() * (discountPercentage / 100));
        }

        // Calculate tax
        public static int CalculateTax(decimal taxRate = 0)
        {
            // Indonesia typically doesn't add VAT at retail for small items
            // But if needed, you can set taxRate (e.g., 11 for 11% PPN)
            int subtotal = GetSubtotal();
            int discount = CalculateDiscount(false); // Get discount without member check
            return (int)((subtotal - discount) * (taxRate / 100));
        }

        // Get final total
        public static int GetTotal(bool isMember = false, decimal taxRate = 0)
        {
            int subtotal = GetSubtotal();
            int discount = CalculateDiscount(isMember);
            int tax = CalculateTax(taxRate);
            return subtotal - discount + tax;
        }

        // Raise cart changed event
        private static void OnCartChanged()
        {
            CartChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}