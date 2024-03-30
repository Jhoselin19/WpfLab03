using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLab03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source=LAB1504-21\\SQLEXPRESS;Initial Catalog=Lab03; User Id=jhoselin; Password=123456";
        List<Student> students = new List<Student>();
        DataTable dataTable = new DataTable();

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dataTable);

                dataGridIngresos.ItemsSource = dataTable.DefaultView;

                connection.Close(); 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridIngresos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int StudentId = reader.GetInt32("StudentId");
                    string FirstName = reader.GetString("FirstName");
                    string LastName = reader.GetString("LastName");

                    students.Add(new Student { StudentId = StudentId, FirstName = FirstName, LastName = LastName });

                }

                connection.Close();
                dataGridIngresos.ItemsSource = students;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            List<Student> filteredStudents = new List<Student>();
            string filterName = txtName.Text.Trim();
            string filterLastName = txtLastName.Text.Trim();

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int studentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                    string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    string lastName = reader.GetString(reader.GetOrdinal("LastName"));

                    if (firstName.Contains(filterName) || lastName.Contains(filterName))
                    {
                        filteredStudents.Add(new Student { StudentId = studentId, FirstName = firstName, LastName = lastName });
                    }
                }

                connection.Close();
                dataGridIngresos.ItemsSource = filteredStudents;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}