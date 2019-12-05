using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//Need this
using System.Data.SqlClient;

namespace DatabaseDemo
{
    /// <summary>
    /// Interaction logic for DemoMessageBox.xaml
    /// </summary>
    public partial class DemoMessageBox : Page
    {
        public DemoMessageBox()
        {
            InitializeComponent();
        }

        //Connect to the database.
        private SqlConnection connection;

        /// <summary>
        /// Connect to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Connect(object sender, EventArgs args)
        {
            string connectionString;
            connectionString = "Data Source=mssql.cs.ksu.edu; Initial Catalog=Robbieo; User ID=Robbieo; Password=Database!";
            connection = new SqlConnection(connectionString);
            connection.Open();
            MessageBox.Show("Connection Open");
        }
        /// <summary>
        /// Read the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Read(object sender, EventArgs args)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command;
                SqlDataReader dataReader;
                String sql, Output = "";
                sql = "SELECT * FROM Clubs.Club"; 
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Output = Output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2) + "\n";
                }
                MessageBox.Show(Output);

                dataReader.Close();
                command.Dispose();
            }
            else
            {
                MessageBox.Show("Open Connection First");
            }
        }
        /// <summary>
        /// Insert into the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Insert(object sender, EventArgs args)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command;
                SqlDataAdapter adapter = new SqlDataAdapter();
                String sql;
                sql = "INSERT INTO Clubs.Club(Name, Purpose) VALUES('Demo', 'The Demo is Inserting Correctly')";
                command = new SqlCommand(sql, connection);
                adapter.InsertCommand = new SqlCommand(sql, connection);
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show(sql);
                command.Dispose();
            }
            else
            {
                MessageBox.Show("Open Connection First");
            }
        }
        /// <summary>
        /// Insert into the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Update(object sender, EventArgs args)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command;
                SqlDataAdapter adapter = new SqlDataAdapter();
                String sql;
                sql = "UPDATE Clubs.Club SET Purpose = N'The Demo is updating correctly' WHERE Club.ClubId = 3 AND Purpose != N'The Demo is updating correctly'";
                command = new SqlCommand(sql, connection);
                adapter.UpdateCommand = new SqlCommand(sql, connection);
                adapter.UpdateCommand.ExecuteNonQuery();
                MessageBox.Show(sql);
                command.Dispose();
            }
            else
            {
                MessageBox.Show("Open Connection First");
            }
        }
        /// <summary>
        /// Insert into the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Delete(object sender, EventArgs args)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command;
                SqlDataAdapter adapter = new SqlDataAdapter();
                String sql;
                sql = "DELETE Clubs.Club WHERE Club.ClubId = 3";
                command = new SqlCommand(sql, connection);
                adapter.DeleteCommand = new SqlCommand(sql, connection);
                adapter.DeleteCommand.ExecuteNonQuery();
                MessageBox.Show(sql);
                command.Dispose();
            }
            else
            {
                MessageBox.Show("Open Connection First");
            }
        }
        /// <summary>
        /// Read the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Close(object sender, EventArgs args)
        {
            connection.Close();
            MessageBox.Show("Connection Closed");
        }
    }
}
