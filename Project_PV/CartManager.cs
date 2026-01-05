using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Project_PV
{
    // Cart Item class
    public class CartItem
    {
        public int CartDetailID { get; set; }
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

    // Database-backed Cart Manager
    public static class CartManager
    {
        private static string connectionString = "Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;";
        private static int currentCartID = 0;
        private static int currentUserID = 0;
        private static string sessionID = Guid.NewGuid().ToString(); // For guest users

        // Event to notify when cart changes
        public static event EventHandler CartChanged;

        // Set the current user (call this when user logs in)
        public static void SetCurrentUser(int userID)
        {
            currentUserID = userID;
            LoadOrCreateCart();
            OnCartChanged();
        }

        // Initialize cart for guest or logged-in user
        private static void LoadOrCreateCart()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    if (currentUserID > 0)
                    {
                        // Logged-in user - find or create cart by member_id
                        string query = "SELECT ID FROM Cart WHERE member_id = @memberID";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@memberID", currentUserID);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            currentCartID = Convert.ToInt32(result);
                        }
                        else
                        {
                            // Create new cart for user
                            string insertQuery = "INSERT INTO Cart (member_id) VALUES (@memberID); SELECT LAST_INSERT_ID();";
                            MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                            insertCmd.Parameters.AddWithValue("@memberID", currentUserID);
                            currentCartID = Convert.ToInt32(insertCmd.ExecuteScalar());
                        }
                    }
                    else
                    {
                        // Guest user - find or create cart by session_id
                        string query = "SELECT ID FROM Cart WHERE session_id = @sessionID";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@sessionID", sessionID);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            currentCartID = Convert.ToInt32(result);
                        }
                        else
                        {
                            // Create new cart for guest
                            string insertQuery = "INSERT INTO Cart (session_id) VALUES (@sessionID); SELECT LAST_INSERT_ID();";
                            MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                            insertCmd.Parameters.AddWithValue("@sessionID", sessionID);
                            currentCartID = Convert.ToInt32(insertCmd.ExecuteScalar());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading cart: {ex.Message}");
            }
        }

        // Get all cart items from database
        public static List<CartItem> GetCartItems()
        {
            List<CartItem> items = new List<CartItem>();

            if (currentCartID == 0)
            {
                LoadOrCreateCart();
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            cd.ID as CartDetailID,
                            cd.produk_id as ProductID,
                            cd.Qty as Quantity,
                            p.Nama as ProductName,
                            p.Merk as Brand,
                            p.Harga as UnitPrice,
                            p.image_url as ImageUrl,
                            k.Nama as Category
                        FROM Cart_Detail cd
                        INNER JOIN Produk p ON cd.produk_id = p.ID
                        INNER JOIN Kategori k ON p.kategori_id = k.ID
                        WHERE cd.cart_id = @cartID
                        ORDER BY cd.added_at DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@cartID", currentCartID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new CartItem
                            {
                                CartDetailID = reader.GetInt32("CartDetailID"),
                                ProductID = reader.GetInt32("ProductID"),
                                ProductName = reader.GetString("ProductName"),
                                Brand = reader.GetString("Brand"),
                                Category = reader.GetString("Category"),
                                UnitPrice = reader.GetInt32("UnitPrice"),
                                Quantity = reader.GetInt32("Quantity"),
                                ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? "" : reader.GetString("ImageUrl")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting cart items: {ex.Message}");
            }

            return items;
        }

        // Add item to cart
        public static void AddToCart(int productID, string productName, string brand,
                                     string category, int price, string imageUrl, int quantity = 1)
        {
            if (currentCartID == 0)
            {
                LoadOrCreateCart();
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if item already exists
                    string checkQuery = "SELECT ID, Qty FROM Cart_Detail WHERE cart_id = @cartID AND produk_id = @productID";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@cartID", currentCartID);
                    checkCmd.Parameters.AddWithValue("@productID", productID);

                    using (MySqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Item exists, update quantity
                            int existingQty = reader.GetInt32("Qty");
                            int detailID = reader.GetInt32("ID");
                            reader.Close();

                            string updateQuery = "UPDATE Cart_Detail SET Qty = @newQty WHERE ID = @detailID";
                            MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                            updateCmd.Parameters.AddWithValue("@newQty", existingQty + quantity);
                            updateCmd.Parameters.AddWithValue("@detailID", detailID);
                            updateCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            reader.Close();

                            // Item doesn't exist, insert new
                            string insertQuery = @"
                                INSERT INTO Cart_Detail (cart_id, produk_id, Qty) 
                                VALUES (@cartID, @productID, @qty)";

                            MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                            insertCmd.Parameters.AddWithValue("@cartID", currentCartID);
                            insertCmd.Parameters.AddWithValue("@productID", productID);
                            insertCmd.Parameters.AddWithValue("@qty", quantity);
                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    // Update cart timestamp
                    UpdateCartTimestamp(conn);
                }

                OnCartChanged();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding to cart: {ex.Message}");
            }
        }

        // Update item quantity
        public static void UpdateQuantity(int productID, int newQuantity)
        {
            if (newQuantity <= 0)
            {
                RemoveFromCart(productID);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Cart_Detail SET Qty = @qty WHERE cart_id = @cartID AND produk_id = @productID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@qty", newQuantity);
                    cmd.Parameters.AddWithValue("@cartID", currentCartID);
                    cmd.Parameters.AddWithValue("@productID", productID);
                    cmd.ExecuteNonQuery();

                    UpdateCartTimestamp(conn);
                }

                OnCartChanged();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating quantity: {ex.Message}");
            }
        }

        // Remove item from cart
        public static void RemoveFromCart(int productID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "DELETE FROM Cart_Detail WHERE cart_id = @cartID AND produk_id = @productID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@cartID", currentCartID);
                    cmd.Parameters.AddWithValue("@productID", productID);
                    cmd.ExecuteNonQuery();

                    UpdateCartTimestamp(conn);
                }

                OnCartChanged();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing from cart: {ex.Message}");
            }
        }

        // Clear entire cart
        public static void ClearCart()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "DELETE FROM Cart_Detail WHERE cart_id = @cartID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@cartID", currentCartID);
                    cmd.ExecuteNonQuery();
                }

                OnCartChanged();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error clearing cart: {ex.Message}");
            }
        }

        // Transfer guest cart to user after login
        public static void TransferGuestCartToUser(int userID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Get guest cart ID
                        string getGuestCartQuery = "SELECT ID FROM Cart WHERE session_id = @sessionID";
                        MySqlCommand getGuestCmd = new MySqlCommand(getGuestCartQuery, conn, transaction);
                        getGuestCmd.Parameters.AddWithValue("@sessionID", sessionID);
                        object guestCartResult = getGuestCmd.ExecuteScalar();

                        if (guestCartResult != null)
                        {
                            int guestCartID = Convert.ToInt32(guestCartResult);

                            // Check if user already has a cart
                            string getUserCartQuery = "SELECT ID FROM Cart WHERE member_id = @memberID";
                            MySqlCommand getUserCmd = new MySqlCommand(getUserCartQuery, conn, transaction);
                            getUserCmd.Parameters.AddWithValue("@memberID", userID);
                            object userCartResult = getUserCmd.ExecuteScalar();

                            if (userCartResult != null)
                            {
                                // User has cart, merge items
                                int userCartID = Convert.ToInt32(userCartResult);

                                string mergeQuery = @"
                                    INSERT INTO Cart_Detail (cart_id, produk_id, Qty)
                                    SELECT @userCartID, produk_id, Qty FROM Cart_Detail WHERE cart_id = @guestCartID
                                    ON DUPLICATE KEY UPDATE Qty = Qty + VALUES(Qty)";

                                MySqlCommand mergeCmd = new MySqlCommand(mergeQuery, conn, transaction);
                                mergeCmd.Parameters.AddWithValue("@userCartID", userCartID);
                                mergeCmd.Parameters.AddWithValue("@guestCartID", guestCartID);
                                mergeCmd.ExecuteNonQuery();

                                // Delete guest cart
                                string deleteGuestCartQuery = "DELETE FROM Cart WHERE ID = @guestCartID";
                                MySqlCommand deleteCmd = new MySqlCommand(deleteGuestCartQuery, conn, transaction);
                                deleteCmd.Parameters.AddWithValue("@guestCartID", guestCartID);
                                deleteCmd.ExecuteNonQuery();
                            }
                            else
                            {
                                // User doesn't have cart, transfer guest cart
                                string transferQuery = "UPDATE Cart SET member_id = @memberID, session_id = NULL WHERE ID = @guestCartID";
                                MySqlCommand transferCmd = new MySqlCommand(transferQuery, conn, transaction);
                                transferCmd.Parameters.AddWithValue("@memberID", userID);
                                transferCmd.Parameters.AddWithValue("@guestCartID", guestCartID);
                                transferCmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error transferring cart: {ex.Message}");
            }
        }

        public static int GetSubtotal()
        {
            return GetCartItems().Sum(item => item.Subtotal);
        }

        // Get total number of items (quantity sum)
        public static int GetItemCount()
        {
            return GetCartItems().Sum(item => item.Quantity);
        }

        // Get total number of unique items
        public static int GetTotalItems()
        {
            return GetCartItems().Count;
        }

        // Update cart timestamp
        private static void UpdateCartTimestamp(MySqlConnection conn)
        {
            string query = "UPDATE Cart SET updated_at = NOW() WHERE ID = @cartID";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@cartID", currentCartID);
            cmd.ExecuteNonQuery();
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