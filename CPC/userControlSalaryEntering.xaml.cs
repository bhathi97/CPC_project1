using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using Microsoft.Data.Sql;
using System.Text.RegularExpressions;
using System.Data;

namespace CPC
{
    /// <summary>
    /// Interaction logic for userControlSalaryEntering.xaml
    /// </summary>
    public partial class userControlSalaryEntering : UserControl
    {
        public string[] months { get; set; }
        public int[] years { get; set; }
        public userControlSalaryEntering()
        {
            InitializeComponent();
            InternID.Focus();
            months = new string[] { "january", "february", "march", "april", "may", "june", "july", "august", "september", "octomber", "november", "december" };
            years = new int[] { 2022, 2023, 2024, 2025, 2026 };
            //fill combo boxes
            AddingYear.ItemsSource = years;
            AddingMonth.ItemsSource = months;
            this.DataContext = this;

        }

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-KHI8921;Initial Catalog=CPC_Interns_Salary_Management_System_Database;Integrated Security=True;TrustServerCertificate=True");
                    


        //------------------------------------------------------working on
        private void sendToTable_Click(object sender, RoutedEventArgs e)
        {
            EmptyComboBoxCheck();
            EmptyTextBoxCheck();

            if (EmptyComboBoxCheck() == 0 && EmptyTextBoxCheck() == 0)
            {

                try
                {
                    connection.Open();
                    //validate - check if the data allready in the SalaryStoreTable or temperarySalaryTable
                    //check salarySotere table
                    SqlDataAdapter sda1 = new SqlDataAdapter("SELECT count(ID) FROM SalaryStoreTable WHERE ID ='" + InternID.Text + "' AND Month ='" + AddingMonth.Text + "' AND Year ='" + AddingYear.Text + "' AND AccountNo ='" + InternBankAccNo.Text + "'", connection);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);

                    //check temperary table
                    SqlDataAdapter sda2 = new SqlDataAdapter("SELECT count(ID) FROM TempararySalaryTable WHERE ID ='" + InternID.Text + "'", connection);
                    DataTable dt2 = new DataTable();
                    sda2.Fill(dt2);

                    //check thet Id in the master table
                    SqlDataAdapter sda3 = new SqlDataAdapter("SELECT count(ID) FROM InternsMasterTable WHERE ID ='" + InternID.Text + "'", connection);
                    DataTable dt3 = new DataTable();
                    sda3.Fill(dt3);

                    if(dt3.Rows[0][0].ToString() == "0") 
                    {
                        MessageBox.Show("User is Invalid" , " Error ", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    }

                    else if(dt1.Rows[0][0].ToString() == "0" && dt2.Rows[0][0].ToString() == "0")
                    {
                        SqlCommand c1 = new SqlCommand("INSERT INTO TempararySalaryTable (ID, AccountNo, Month , Year, BankName, BankCode, BranchName, BranchCode, SalaryPerDay, FullWorkDays, HalfWorkDays,TotalWorkDays, TotalSalaryAmount, Name) VALUES ('" + InternID.Text + "','" + InternBankAccNo.Text + "','" + AddingMonth.Text + "','" + int.Parse(AddingYear.Text) + "','" + InternBankName.Text + "','" + InternBankCode.Text + "','" + InternBranchName.Text + "','" + InternBranchCode.Text + "','" + float.Parse(InternSalaryPerDay.Text) + "','" + int.Parse(AddingFullWorkDays.Text) + "','" + int.Parse(AddingHalfWorkDays.Text) + "','" + float.Parse(ShowTotalWorkDays.Text) + "','" + float.Parse(ShowTotalSalary.Text) + "','" + InternName.Text + "')", connection);
                        c1.ExecuteNonQuery();
                        DataShowingMethod();
                        MessageBox.Show("Successfully added to the table", "success", MessageBoxButton.OK, MessageBoxImage.Information);


                    }

















                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel);
                }
                finally
                {
                    connection.Close();
                    
                }
                

            }




















        }

        //---------------------------------------------------------

        private void addNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteRecord_Click(object sender, RoutedEventArgs e)
        {

        }

       

        private void InternID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddingYear.Focus();
            }
        }

        private void InternName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                InternBankAccNo.Focus();
            }
        }

        private void InternBankAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                InternBankName.Focus();
            }
        }

        private void InternBankName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                InternBankCode.Focus();
            }
        }

        private void InternBankCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                InternBranchName.Focus();
            }
        }

        private void InternBranchName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                InternBranchCode.Focus();
            }
        }

        private void InternBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                InternSalaryPerDay.Focus();
            }
        }

        private void InternSalaryPerDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddingYear.Focus();
            }
        }

        private void AddingYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddingMonth.Focus();
            }
        }

        private void AddingMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddingFullWorkDays.Focus();
            }
        }

        private void AddingFullWorkDays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddingHalfWorkDays.Focus();
            }
        }

        private void AddingHalfWorkDays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                sendToTable.Focus();
            }
        }

        private void InternID_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand commandAutofillFields1 = new SqlCommand("SELECT * FROM InternsMasterTable WHERE ID = '" + InternID.Text + "'", connection );
                SqlDataReader dataReader1 = commandAutofillFields1.ExecuteReader();
                while (dataReader1.Read())
                {
                    InternName.Text = dataReader1["Name"].ToString();
                    InternBankAccNo.Text = dataReader1["BankAccountNo"].ToString();
                    InternBankName.Text = dataReader1["BankName"].ToString();
                    InternBankCode.Text = dataReader1["BranchCode"].ToString();
                    InternBranchName.Text = dataReader1["BranchName"].ToString();
                    InternBranchCode.Text = dataReader1["BranchCode"].ToString();
                    InternSalaryPerDay.Text = dataReader1["SalaryPerDay"].ToString();
                }

            }
            catch (Exception ee)
            {
               MessageBox.Show(ee.Message, "loading error1", MessageBoxButton.OK);
            }
            finally
            {
                connection.Close();
            }
        }

        //validate full and half days as numbers
        private void AddingFullWorkDays_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            //only between 1-31
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);

        }
        private void AddingHalfWorkDays_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            //only between 1-31
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }


        public static bool IsValid(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 0 && i <= 31;
        }

        //fill total days of work 
     
        public void Add(object sender, TextChangedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(AddingFullWorkDays.Text))
            {
                float halfDays = float.Parse(AddingHalfWorkDays.Text);
                float total = (float)(halfDays * 0.5);
                ShowTotalWorkDays.Text = total.ToString();
                TotalSalary();

            }
            else if(string.IsNullOrEmpty(AddingHalfWorkDays.Text))
            {
                ShowTotalWorkDays.Text = AddingFullWorkDays.Text;
                TotalSalary();
            }
            else
            {
                float fullDays = float.Parse(AddingFullWorkDays.Text);
                float halfDays = float.Parse(AddingHalfWorkDays.Text);
                float total = (float)(fullDays + (halfDays * 0.5));
                ShowTotalWorkDays.Text = total.ToString();
                TotalSalary();
            }
            

        }

        //totalsalary
        public void TotalSalary()
        {
            if (!(string.IsNullOrEmpty(InternSalaryPerDay.Text)) && !(string.IsNullOrEmpty(ShowTotalWorkDays.Text)))
            {
                float perDay = float.Parse(InternSalaryPerDay.Text);
                float days = float.Parse(ShowTotalWorkDays.Text);
                ShowTotalSalary.Text = (perDay*days).ToString();
            }
            else
            {
                ShowTotalSalary.Text = "0";
            }
        }

        private void InternSalaryPerDay_TextChanged(object sender, TextChangedEventArgs e)
        {
            TotalSalary();
        }


        //check empty textblocks
        public int EmptyTextBoxCheck()
        {
            int flag = 0;
            TextBox[] textBox = { InternID, InternName, InternBankAccNo, InternBankName, InternBankCode, InternBranchName, InternBranchCode, InternSalaryPerDay, AddingFullWorkDays, AddingHalfWorkDays };
            foreach (var textbox in textBox)
            {
                if (string.IsNullOrEmpty(textbox.Text))
                {
                    MessageBox.Show(textbox.Name + " is empty", "empty field", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    textbox.Focus();
                    flag = 1;
                    return flag;
                }

            }
            return flag;

        }

        //check empty combobox
        public int EmptyComboBoxCheck()
        {
            int flag2 = 0;
            ComboBox[] comboBoxes = {AddingYear,AddingMonth};
            foreach (var comboBox in comboBoxes) 
            {
                if (string.IsNullOrEmpty(comboBox.Text))
                {
                    MessageBox.Show(comboBox.Name + "  is empty", "empty field", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    flag2 = 1;
                    return flag2;

                }    
            }
            return flag2;
        }

        //data show in the dataShowng table
        public void DataShowingMethod()
        {
            SqlCommand sqlcommand = new SqlCommand("SELECT * FROM TempararySalaryTable", connection);
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(sqlcommand);
            DataTable showdatatable = new DataTable();
            sqldataadapter.Fill(showdatatable);
            dataShowingTable.ItemsSource = showdatatable.DefaultView;

        }


       




        




    }

       
            

       
    
}
