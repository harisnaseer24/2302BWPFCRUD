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

            }

        }
    }
}
