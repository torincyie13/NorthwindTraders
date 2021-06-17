using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace NorthwindTraders
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();
        }

        SqlConnection NWindConnection;
        SqlCommand customersCommand;
        SqlDataAdapter customersAdapter;
        DataTable customersTable;
        CurrencyManager customersManager;

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            string path = Path.GetFullPath("SQLNWindDB.mdf");
            MessageBox.Show(path);
            // connect to NWind database
            NWindConnection = new
                SqlConnection("Data Source=.\\SQLEXPRESS; AttachDBFilename=" + path + "Integrated Security=True; Connect Timeout=30; User Instance=True");
            // open the connection
            NWindConnection.Open();
            // establish command object
            customersCommand = new SqlCommand("SELECT * FROM Customers", NWindConnection);
            // establish data adapter/data table
            customersAdapter = new SqlDataAdapter();
            customersAdapter.SelectCommand = customersCommand;
            customersTable = new DataTable();
            customersAdapter.Fill(customersTable);
            // bind controls to data table
            txtCustomerID.DataBindings.Add("Text", customersTable, "CustomersID");
            txtCompanyName.DataBindings.Add("Text", customersTable, "ContactName");
            txtContactName.DataBindings.Add("Text", customersTable, "ContactName");
            txtContactTitle.DataBindings.Add("Text", customersTable, "customerTitle");
            // establish currecny manager
            customersManager = (CurrencyManager)
                BindingContext[customersTable];
            // close connection
            NWindConnection.Close();
            // dispose of the conneciton object
            NWindConnection.Dispose();
            customersCommand.Dispose();
            customersAdapter.Dispose();
            customersTable.Dispose();
        }
    }
}
