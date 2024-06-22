using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;



namespace _2302b1WpfCrud
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        SqlConnection Conn = new SqlConnection("Data Source=.;Initial Catalog=2302B1WPFCRUD;User ID=sa;Password=aptech;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        private void clearData()
        {
            
            pname.Clear();
            desc.Clear();
            price.Clear();
            qty.Clear();
            cat.Clear();
            pid.Clear();

        }

        private void LoadProducts()
        {
            SqlCommand getProducts = new SqlCommand("SELECT * From products",Conn);
            DataTable productTable = new DataTable();
            Conn.Open();
            SqlDataReader reader = getProducts.ExecuteReader();
            productTable.Load(reader);

            Conn.Close();
            productGrid.ItemsSource = productTable.DefaultView;
        }

        private bool isValid()
        {
            if (pname.Text == string.Empty || desc.Text == string.Empty || price.Text == string.Empty || qty.Text == string.Empty || cat.Text == string.Empty)
            {
                MessageBox.Show("All fields are required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        private void ClearFields(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            if (isValid()==true)
            {
                SqlCommand addprod = new SqlCommand("Insert into products values (@pname,@desc,@price,@qty,@cat)", Conn);

                Conn.Open();
                addprod.CommandType = CommandType.Text;

                addprod.Parameters.AddWithValue("@pname", pname.Text);
                addprod.Parameters.AddWithValue("@desc", desc.Text);
                addprod.Parameters.AddWithValue("@price", price.Text);
                addprod.Parameters.AddWithValue("@cat", cat.Text);
                addprod.Parameters.AddWithValue("@qty", qty.Text);
                addprod.ExecuteNonQuery();

                Conn.Close();
                MessageBox.Show("Product added succesfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                clearData();
                LoadProducts();

            }

        }

        private void RemoveProduct(object sender, RoutedEventArgs e)
        {
            if (pid.Text != string.Empty)
            {
                SqlCommand deleteProduct = new SqlCommand("DELETE From products where Id = @pid", Conn);

                deleteProduct.CommandType = CommandType.Text;
                Conn.Open();
                deleteProduct.Parameters.AddWithValue("@pid", pid.Text);
                deleteProduct.ExecuteNonQuery();
                Conn.Close();
                MessageBox.Show("Product deleted succesfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadProducts();
                clearData();

            }
            else
            {
                MessageBox.Show("We need product id to delete it.", "Can't Delete without Id", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetProductDetails(object sender, RoutedEventArgs e)
        {
            if (pid.Text != string.Empty)
            {
                SqlCommand getProduct = new SqlCommand("SELECT * From products where Id = @pid", Conn);
                getProduct.CommandType = CommandType.Text;
                Conn.Open();
                getProduct.Parameters.AddWithValue("@pid",pid.Text);
                SqlDataReader reader = getProduct.ExecuteReader();
                if (reader.Read())
                {
                    pname.Text = reader["pname"].ToString();
                    desc.Text = reader["description"].ToString();
                    price.Text = reader["price"].ToString();
                    qty.Text = reader["qty"].ToString();
                    cat.Text = reader["category"].ToString();
                    update.Content = "Update";
                    update +=Click

                    


                }
                else
                {
                    MessageBox.Show("Invalid Id.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                Conn.Close();
                

            }
            else
            {
                MessageBox.Show("We need product id to update product.", "Can't Update without Id", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateProduct(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                SqlCommand updProduct = new SqlCommand("UPDATE products SET description=@description, pname=@pname, price=@price, qty=@qty, category=@cat WHERE Id=@pid;", Conn);
                Conn.Open();
                updProduct.CommandType = CommandType.Text;
                updProduct.Parameters.AddWithValue("@pname", pname.Text);
                updProduct.Parameters.AddWithValue("@price", price.Text);
                updProduct.Parameters.AddWithValue("@cat", cat.Text);
                updProduct.Parameters.AddWithValue("@qty", qty.Text);
                updProduct.Parameters.AddWithValue("@pid", pid.Text);
                updProduct.Parameters.AddWithValue("@description", desc.Text);
                updProduct.ExecuteNonQuery();
                Conn.Close();


                MessageBox.Show("Product updated successfully.", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                clearData();
                LoadProducts();
            }

        }
    }
}
