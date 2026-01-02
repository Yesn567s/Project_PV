using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_PV
{
    public partial class UserCartControl : UserControl
    {
        private int currentMemberID = 0; // Set this from your login system
        private bool isMember = false;

        public UserCartControl()
        {
            InitializeComponent();

            // Subscribe to cart changes
            CartManager.CartChanged += CartManager_CartChanged;

            // Initial load
            LoadCart();
        }

        // Optional: Set member info from parent form
        public void SetMemberInfo(int memberID, bool member)
        {
            currentMemberID = memberID;
            isMember = member;
            UpdateOrderSummary();
        }

        private void CartManager_CartChanged(object sender, EventArgs e)
        {
            LoadCart();
        }

        private void LoadCart()
        {
            var cartItems = CartManager.GetCartItems();

            if (cartItems.Count == 0)
            {
                // Show empty cart panel
                ShowEmptyCart();
            }
            else
            {
                // Show cart with items
                ShowCartItems();
                DisplayCartItems();
                UpdateOrderSummary();
            }
        }

        private void ShowEmptyCart()
        {
            panel1.Visible = true;
            panel1.Dock = DockStyle.Fill;
            panel1.BringToFront();
            mainPanel.Visible = false;
        }

        private void ShowCartItems()
        {
            panel1.Visible = false;
            mainPanel.Visible = true;
            mainPanel.Dock = DockStyle.None;
        }

        private void DisplayCartItems()
        {
            itemsFlowLayoutPanel.Controls.Clear();

            var cartItems = CartManager.GetCartItems();

            // Update item count label
            int totalQty = cartItems.Sum(i => i.Quantity);
            itemCountLabel.Text = totalQty == 1 ?
                "1 item in your cart" :
                $"{totalQty} items in your cart";

            foreach (var item in cartItems)
            {
                Panel itemPanel = CreateCartItemPanel(item);
                itemsFlowLayoutPanel.Controls.Add(itemPanel);
            }
        }

        private Panel CreateCartItemPanel(CartItem item)
        {
            Panel panel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(750, 200),
                Margin = new Padding(3)
            };

            // Product Image
            PictureBox pictureBox = new PictureBox
            {
                Location = new Point(20, 20),
                Size = new Size(150, 150),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(245, 245, 245)
            };

            try
            {
                if (!string.IsNullOrEmpty(item.ImageUrl))
                {
                    pictureBox.Load(item.ImageUrl);
                }
            }
            catch { }

            panel.Controls.Add(pictureBox);

            // Category Label
            Label categoryLabel = new Label
            {
                Text = item.Category,
                Location = new Point(197, 10),
                AutoSize = true,
                BackColor = Color.Gainsboro,
                Font = new Font("Segoe UI", 7.8F, FontStyle.Bold),
                Padding = new Padding(5)
            };
            panel.Controls.Add(categoryLabel);

            // Product Name
            Label nameLabel = new Label
            {
                Text = item.ProductName,
                Location = new Point(194, 37),
                Size = new Size(400, 31),
                Font = new Font("Segoe UI", 13.8F)
            };
            panel.Controls.Add(nameLabel);

            // Brand
            Label brandLabel = new Label
            {
                Text = $"by {item.Brand}",
                Location = new Point(196, 68),
                AutoSize = true,
                Font = new Font("Segoe UI", 10.2F),
                ForeColor = Color.Gray
            };
            panel.Controls.Add(brandLabel);

            // Quantity NumericUpDown
            NumericUpDown quantityUpDown = new NumericUpDown
            {
                Location = new Point(200, 100),
                Size = new Size(120, 34),
                Font = new Font("Segoe UI", 12F),
                Minimum = 1,
                Maximum = 999,
                Value = item.Quantity,
                Tag = item.ProductID
            };
            quantityUpDown.ValueChanged += QuantityUpDown_ValueChanged;
            panel.Controls.Add(quantityUpDown);

            // Remove Button
            Button removeBtn = new Button
            {
                Text = "✕ Remove",
                Location = new Point(200, 150),
                Size = new Size(120, 35),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10.2F),
                ForeColor = Color.Red,
                Tag = item.ProductID,
                Cursor = Cursors.Hand
            };
            removeBtn.FlatAppearance.BorderSize = 0;
            removeBtn.Click += RemoveBtn_Click;
            panel.Controls.Add(removeBtn);

            // Item Price
            Label itemPriceLabel = new Label
            {
                Text = $"Rp {item.Subtotal:N0}",
                Location = new Point(620, 83),
                Size = new Size(110, 28),
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight
            };
            panel.Controls.Add(itemPriceLabel);

            // Price Each
            Label priceEachLabel = new Label
            {
                Text = $"Rp {item.UnitPrice:N0} each",
                Location = new Point(620, 111),
                Size = new Size(110, 23),
                Font = new Font("Segoe UI", 10.2F),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleRight
            };
            panel.Controls.Add(priceEachLabel);

            return panel;
        }

        private void QuantityUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;
            int productID = (int)numericUpDown.Tag;
            int newQuantity = (int)numericUpDown.Value;

            CartManager.UpdateQuantity(productID, newQuantity);
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int productID = (int)btn.Tag;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to remove this item from cart?",
                "Confirm Remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                CartManager.RemoveFromCart(productID);
            }
        }

        private void UpdateOrderSummary()
        {
            int subtotal = CartManager.GetSubtotal();
            int discount = CartManager.CalculateDiscount(isMember, 5); // 5% discount for members
            int tax = CartManager.CalculateTax(0); // No tax for now
            int total = subtotal - discount + tax;

            // Update labels
            subtotalAmountLabel.Text = $"Rp {subtotal:N0}";

            if (isMember && discount > 0)
            {
                discountPanel.Visible = true;
                membershipDiscountLabel.Visible = true;
                discountAmountLabel.Visible = true;
                discountAmountLabel.Text = $"-Rp {discount:N0}";
                memberLabel.Text = "Member Discount";
                discountAppliedLabel.Text = "5% discount applied";
            }
            else
            {
                discountPanel.Visible = false;
                membershipDiscountLabel.Visible = false;
                discountAmountLabel.Visible = false;
            }

            // Tax (if applicable)
            if (tax > 0)
            {
                taxLabel.Visible = true;
                taxAmountLabel.Visible = true;
                taxAmountLabel.Text = $"Rp {tax:N0}";
            }
            else
            {
                taxLabel.Visible = false;
                taxAmountLabel.Visible = false;
            }

            totalAmountLabel.Text = $"Rp {total:N0}";
        }

        // Event handler for Proceed to Checkout button
        private void proceedButton_Click(object sender, EventArgs e)
        {
            if (CartManager.GetTotalItems() == 0)
            {
                MessageBox.Show("Your cart is empty!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open checkout form
            FormCheckout fc = new FormCheckout(currentMemberID, isMember);
            fc.ShowDialog();

            // Refresh cart after checkout
            LoadCart();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Wire up the proceed button if it exists
            if (proceedButton != null)
            {
                proceedButton.Click -= proceedButton_Click; // Remove any existing handler
                proceedButton.Click += proceedButton_Click; // Add handler
            }
        }
    }
}
