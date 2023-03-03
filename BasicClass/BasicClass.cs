using BasicClass;
using BasicClassLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace BasicClassLibrary
{
    public class BasicClass
    {
        static String serverAddress = Settings.Default["serverAddress"].ToString();
        static String username = Settings.Default["username"].ToString();
        static String password = Settings.Default["password"].ToString();
        static String databse = Settings.Default["database"].ToString();

        
        public static String font_name = "Arial Narrow";

        public static DBConnection GetDBConnectionParameters()
        {
            return new DBConnection() { serverAddress = serverAddress, database = databse, password = password, username = username };
        }

        public static void saveSettings(string serverAddress, string location, string username, string password, string database)
        {
            Settings.Default["serverAddress"] = serverAddress;
            Settings.Default["location"] = location;
            Settings.Default["username"] = username;
            Settings.Default["password"] = password;
            Settings.Default["database"] = database;
            Settings.Default.Save();
        }

        //public static String[] getSettings()
        //{
        //    MessageBox.Show(" get setting is Called ");

        //    String[] settings = { serverAddress, location, username, password, databse };
        //    return settings;
        //}
        //public static void generateExcel(System.Data.DataTable dtt, string sheetName = "Sheet1")
        //{
        //    //await Task.Run(() =>
        //    //{
        //    try
        //    {
        //        int size = dtt.Rows.Count;
        //        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
        //        Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
        //        Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;// ActiveSheet;
        //        if (sheetName.Length > 31)
        //        {
        //            sheetName = sheetName.Substring(0, 31);
        //        }
        //        ws.Name = sheetName;

        //        int i = 1, j = 0;//k = 0, p = 0;
        //        foreach (DataColumn cm in dtt.Columns)
        //        {
        //            j++;
        //            ws.Cells[i, j] = cm.ColumnName;
        //        }
        //        foreach (DataRow dr in dtt.Rows)
        //        {
        //            i++;
        //            j = 0;

        //            foreach (var cell in dr.ItemArray)
        //            {
        //                j++;
        //                ws.Cells[i, j] = "'" + cell.ToString();

        //            }
        //            //k = ((i * 1000 / size));
        //            //if (k <= 1000)
        //            //    p = k;
        //            //progress.Report(p);
        //        }
        //        app.Visible = true;
        //    }
        //    catch (Exception p)
        //    {
        //        MessageBox.Show(p.Message);
        //    }
        //    //});

        //}
        public static System.Data.DataTable prepareDataTable(SqlCommand cmd)
        {
            try
            {
                SqlDataAdapter adapter3 = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter3.Fill(dt);
                return dt;
            }
            catch (Exception p)
            {

                MessageBox.Show("Data Preparation Error. " + p.Message);
                return null;
            }
        }
        public static DataSet prepareDataSet(SqlCommand cmd)
        {
            try
            {
                SqlDataAdapter adapter3 = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                adapter3.Fill(dt);
                return dt;
            }
            catch (Exception p)
            {

                //MessageBox.Show("Data Prepartion Error. " + p.Message);
                return null;
            }
        }
        public static bool userAccessContains(string acc, System.Data.DataTable table)
        {
            bool contains = false;
            foreach (DataRow item in table.Rows)
            {
                if (item["role_name"].ToString().Trim() == acc)
                {
                    contains = true;
                }
            }
            return contains;
        }

        public static SqlCommand executeProcedureWithParameter(string procedure, string[] keys, params object[] param)
        {
            if (keys.Length != param.Length)
            {
                MessageBox.Show("Fields and values don't match.");
                MessageBox.Show("keys " + keys.Length + " values " + param.Length);
                Environment.Exit(0);
            }

            SqlParameter[] sqlParameter = new SqlParameter[param.Length];

            for (int i = 0; i < param.Length; i++)
            {
                sqlParameter[i] = new SqlParameter("@" + keys[i], param[i]);
            }

            return executeProcedure(procedure, sqlParameter);
        }
        public static int executeProcedureWithParameterWithStatus(string procedure, string[] keys, params object[] param)
        {
            if (keys.Length != param.Length)
            {
                MessageBox.Show("Fields and values don't match.");
                Environment.Exit(0);
            }

            SqlParameter[] sqlParameter = new SqlParameter[param.Length];

            for (int i = 0; i < param.Length; i++)
            {
                sqlParameter[i] = new SqlParameter("@" + keys[i], param[i]);
            }

            return executeProcedureWithStatus(procedure, sqlParameter);
        }
        public static String makeLABID(String client_id, DateTime date_time, String location)
        {

            int date = date_time.Day;
            int month = date_time.Month;
            int iclientId = Convert.ToInt16(client_id);
            String syear = date_time.Year.ToString();
            int year = Convert.ToInt16(syear);
            year = year - 2000;
            syear = year.ToString();
            String sdate = date.ToString();
            String smonth = month.ToString();
            String location_code = "";
            if (date < 10)
                sdate = "0" + date.ToString();
            if (month < 10)
                smonth = "0" + month.ToString();
            if (iclientId < 10)
                client_id = "00" + iclientId.ToString();
            if (iclientId > 9 && iclientId <= 99)
                client_id = "0" + iclientId.ToString();

            if (location.Equals("Ambassador"))
            {
                location_code = "A";
            }
            if (location.Equals("Stadium"))
            {
                location_code = "BHS";
            }
            if (location.Equals("Central"))
            {
                location_code = "C";
            }
            if (location.Equals("Bole"))
            {
                location_code = "B";
            }
            if (location.Equals("IM"))
            {
                location_code = "IM";
            }
            if (location.Equals("INPATIENT"))
            {
                location_code = "BHI";
            }
            client_id = location_code + "-" + sdate + smonth + syear + client_id;


            return client_id;
        }
        //public static SqlCommand ExecuteProcedure(string procedure, SqlParameter[] parameter)
        //{
        //    try
        //    {
        //        SqlConnection connection = Connection();
        //        connection.Open();
        //        SqlCommand query = new SqlCommand(procedure, connection);
        //        query.CommandType = CommandType.StoredProcedure;
        //        query.Parameters.AddRange(parameter);
        //        SqlDataReader data_reader = query.ExecuteReader();
        //        data_reader.Close();
        //        connection.Close();
        //        return query;
        //    }
        //    catch (SqlException exp)
        //    {
        //        MessageBox.Show("Error Excuting Procedure " + exp.Message);
        //        return null;
        //    }

        //}

        public static SqlCommand executeProcedure(string procedure, SqlParameter[] parameter)
        {
            try
            {
                SqlConnection connection = Connection();
                connection.Open();
                SqlCommand query = new SqlCommand(procedure, connection);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.AddRange(parameter);
                SqlDataReader data_reader = query.ExecuteReader();
                data_reader.Close();
                connection.Close();
                return query;
            }
            catch (SqlException exp)
            {
                MessageBox.Show("Error Excuting Procedure " + exp.Message);
                return null;
            }

        }
        public static DataTable GetMinistoreAccess(DataTable userAcess)
        {
            int user_id = int.Parse(userAcess.Rows[0]["user_identifier"].ToString().Trim());
            return BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("user_management_get_accessible_ministore_list", new string[] { "id" }, new object[] { user_id }));
        }
        public static int executeProcedureWithStatus(string procedure, SqlParameter[] parameter)
        {
            try
            {
                SqlConnection connection = Connection();
                connection.Open();
                SqlCommand query = new SqlCommand(procedure, connection);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.AddRange(parameter);
                //SqlDataReader data_reader = query.ExecuteReader();
                //data_reader.Close();
                //connection.Close();
                int x = query.ExecuteNonQuery();
                //return query;
                return x;
            }
            catch (SqlException exp)
            {
                MessageBox.Show("Error Excuting Procedure " + exp.Message);
                return 0;
            }

        }


        public static DataSet ExecuteQuerySet(string query)
        {

            SqlConnection connection = Connection();
            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.Connection = connection;

            SqlDataAdapter dp = new SqlDataAdapter(command);
            DataSet ds = new DataSet("data");
            dp.Fill(ds, "table");
            connection.Close();
            return ds;
        }
        public static int lab_id_extractor(String lab_code)
        {
            return Int32.Parse(lab_code.Substring(lab_code.Length - 3, 3));
        }

        public static void ExecuteQuery(string query)
        {
            SqlConnection connection = Connection();
            connection.Open();
            SqlCommand command = new SqlCommand();

            command.CommandText = query;
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static string getFullNameByUserName(string username)
        {

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@username", username);

            SqlCommand command = ExecuteProcedure("sp_get_full_name_by_username", parameter);
            SqlDataAdapter dp = new SqlDataAdapter(command);
            DataSet ds = new DataSet("exp");
            dp.Fill(ds);
            System.Data.DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["full_name"].ToString().Trim();
            }

            return "";

        }

        public static SqlCommand ExecuteProcedureWithValue(string procedure, SqlParameter[] parameter, System.Data.DataTable dt)
        {
            try
            {
                SqlConnection connection = Connection();
                connection.Open();
                SqlCommand query = new SqlCommand(procedure, connection);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.AddRange(parameter);
                SqlParameter tvparam = query.Parameters.AddWithValue("@test_ids", dt);
                tvparam.SqlDbType = SqlDbType.Structured;
                SqlDataReader data_reader = query.ExecuteReader();
                data_reader.Close();
                return query;
            }
            catch (SqlException exp)
            {
                //MessageBox.Show("Error Excuting Procedure " + exp.Message);
                return null;
            }

        }
        public static SqlCommand ExecuteProcedureWithConnection(string procedure, SqlConnection connection)
        {
            try
            {
                //SqlConnection connection = Connection();
                connection.Open();
                SqlCommand query = new SqlCommand(procedure, connection);
                query.CommandType = CommandType.StoredProcedure;
                //    query.Parameters.AddRange(parameter);
                SqlDataReader data_reader = query.ExecuteReader();
                data_reader.Close();
                connection.Close();
                return query;
            }
            catch (SqlException exp)
            {
                MessageBox.Show("Error Excuting Procedure " + exp.Message);
                return null;
            }

        }

        public static SqlCommand ExecuteProcedure(string procedure)
        {
            try
            {
                SqlConnection connection = Connection();
                connection.Open();
                SqlCommand query = new SqlCommand(procedure, connection);
                query.CommandType = CommandType.StoredProcedure;
                //    query.Parameters.AddRange(parameter);
                SqlDataReader data_reader = query.ExecuteReader();
                data_reader.Close();
                connection.Close();
                return query;
            }
            catch (SqlException exp)
            {
                MessageBox.Show("Error Excuting Procedure " + exp.Message);
                return null;
            }

        }

        public static SqlDataReader ExecuteProcedureReader(string procedure)
        {
            try
            {
                SqlConnection connection = Connection();
                connection.Open();
                SqlCommand query = new SqlCommand(procedure, connection);
                query.CommandType = CommandType.StoredProcedure;
                //    query.Parameters.AddRange(parameter);
                SqlDataReader data_reader = query.ExecuteReader();

                return data_reader;
            }
            catch (SqlException exp)
            {
                MessageBox.Show("Error Excuting Procedure " + exp.Message);
                return null;
            }

        }



        public static string age_for_display(DateTime dob)
        {
            int[] age = BasicClass.caclculate_age(dob);

            string result = "";

            if (age.Length > 0)
            {
                if (age[0] > 0)
                    result = age[0] + " Years";
                else if (age[1] > 0)
                    result = age[1] + " Months";
                else if (age[2] > 0)
                    result = age[2] + " Weeks";
                else if (age[3] > 0)
                    result = age[3] + " days";
            }
            else
                return result;

            return result;
        }

        public int[] age2rray(int days)
        {
            int[] ageArray = new int[4];
            ageArray[0] = days / 365;
            ageArray[1] = (days % 365) / 30;
            ageArray[2] = (ageArray[1] % 30);

            return ageArray;
        }

        public int array2age(int[] ageArray)
        {
            return 0;
        }

        public int age_in_numbers(int age_in_days)
        {
            int[] age_divided = new int[4];
            int temp_age = 0;
            if (age_in_days > 365)
            {
                age_divided[0] = age_in_days / 365;
                temp_age = age_in_days % 365;
            }
            if (temp_age > 30 && temp_age < 7)
            {
                age_divided[1] = temp_age / 30;
                temp_age = temp_age % 30;
            }


            return 0;
        }


        public static DateTime get_dob(int[] age)
        {
            DateTime dob = DateTime.Now;

            dob = dob.Subtract(TimeSpan.FromDays(age[0] * 365));
            dob = dob.Subtract(TimeSpan.FromDays(age[1] * 30));
            dob = dob.Subtract(TimeSpan.FromDays(age[2] * 7));
            dob = dob.Subtract(TimeSpan.FromDays(age[3]));

            return dob;
        }

        public static bool to_bool(String S)
        {
            if (S.Equals(""))
                return false;
            return Boolean.Parse(S);
        }

        internal static string getPriceGroup(string p)
        {
            //throw new NotImplementedException();
            return "CV";
        }

        public static DataRow getMeasureOfUncertainity()
        {

            SqlParameter[] parameter = new SqlParameter[0];

            SqlCommand command = BasicClass.ExecuteProcedure("sp_get_measure_of_uncertainity", parameter);
            if (command != null)
            {
                SqlDataAdapter dp = new SqlDataAdapter(command);
                DataSet ds = new DataSet("exp");

                dp.Fill(ds);

                DataRow rd = ds.Tables[0].Rows[0];

                return rd;

            }
            return null;

        }

        public static SqlConnection ConnectionQ()
        {
            //connects to queue machine database
            //String connectionString = "Server=" + serverAddressQ + "\\MSSQLSERVER01;DataBase=" + databseQ + ";UID=" + usernameQ + ";password=" + passwordQ + ";";
            string connectionString = @"Data Source=192.168.1.77;Initial Catalog=UniQUEUE;User ID=sa;Password=abc@123;";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public static SqlConnection Connection()
        {
            String connectionString = "Server=" + serverAddress + "\\SQLEXPRESS;DataBase=" + databse + ";UID=" + username + ";password=" + password + ";";
            // String connectionString = "Server=192.168.2.101;DataBase=Sefedms_dummy;UID=" + username + ";password=" + password + ";";
            //  MessageBox.Show(connectionString);
            //String connectionString = "Server=.\\SQLEXPRESS;DataBase=SEFEDMS;UID=sa;password=abc@123;";
            //String connectionString = "Server=.\\SQLEXPRESS;DataBase=Pharmacy;UID=sa;password=abc@123;";
            //String connectionString = "Server=" + serverAddress + "\\SQLexpress;DataBase=SEFEDMS_2;UID=sa;password=abc@123;";
            //String connectionString = "Server=" + serverAddress + "\\SQLexpress;DataBase=SEFEDMS_3;UID=sa;password=abc@123;";

            // String connectionString = "Server=.\\SQLexpress;DataBase=SEFEDMS_3;UID=sa;password=abc@123;";

            // yabi 
            // String connectionString = "Server=USER-PC;DataBase=SEFEDMS_3;UID=sa;password=abc@123;";

            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public static SqlCommand ExecuteProcedure(string procedure, SqlParameter[] parameter)
        {
            try
            {
                SqlConnection connection = Connection();
                connection.Open();
                SqlCommand query = new SqlCommand(procedure, connection);
                query.CommandType = CommandType.StoredProcedure;
                query.CommandTimeout = 3600;
                // MessageBox.Show(query.CommandTimeout.ToString());
                query.Parameters.AddRange(parameter);
                SqlDataReader data_reader = query.ExecuteReader();
                data_reader.Close();
                connection.Close();
                return query;
            }
            catch (SqlException exp)
            {
                //MessageBox.Show("Error Executing Procedure " + exp.Message);
                return null;
            }

        }

        public static SqlCommand ExecuteProcedureWithConnection(string procedure, SqlParameter[] parameter, SqlConnection connection)
        {
            try
            {
                //SqlConnection connection = Connection();
                connection.Open();
                SqlCommand query = new SqlCommand(procedure, connection);
                query.CommandType = CommandType.StoredProcedure;
                query.CommandTimeout = 3600;
                // MessageBox.Show(query.CommandTimeout.ToString());
                query.Parameters.AddRange(parameter);
                SqlDataReader data_reader = query.ExecuteReader();
                data_reader.Close();
                connection.Close();
                return query;
            }
            catch (SqlException exp)
            {
                //MessageBox.Show("Error Executing Procedure " + exp.Message);
                return null;
            }

        }
        public static SqlCommand ExecuteProcedureOnly(string procedure)
        {
            try
            {
                SqlConnection connection = Connection();
                connection.Open();
                SqlCommand query = new SqlCommand(procedure, connection);
                query.CommandType = CommandType.StoredProcedure;
                //    query.Parameters.AddRange(parameter);
                //SqlDataReader data_reader = query.ExecuteReader();
                //data_reader.Close();
                //connection.Close();
                query.ExecuteNonQuery();
                return query;
            }
            catch (SqlException exp)
            {
                MessageBox.Show("Error Excuting Procedure " + exp.Message);
                return null;
            }

        }

        //public static SqlCommand ExecuteProcedureWithValue(string procedure, SqlParameter[] parameter, System.Data.DataTable dt)
        //{
        //    try
        //    {
        //        SqlConnection connection = Connection();
        //        connection.Open();
        //        SqlCommand query = new SqlCommand(procedure, connection);
        //        query.CommandType = CommandType.StoredProcedure;
        //        query.Parameters.AddRange(parameter);
        //        SqlParameter tvparam = query.Parameters.AddWithValue("@test_ids", dt);
        //        tvparam.SqlDbType = SqlDbType.Structured;
        //        SqlDataReader data_reader = query.ExecuteReader();
        //        data_reader.Close();
        //        return query;
        //    }
        //    catch (SqlException exp)
        //    {
        //        MessageBox.Show("Error Excuting Procedure " + exp.Message);
        //        return null;
        //    }

        //}

        //public static SqlCommand ExecuteProcedure(string procedure)
        //{
        //    try
        //    {
        //        SqlConnection connection = Connection();
        //        connection.Open();
        //        SqlCommand query = new SqlCommand(procedure, connection);
        //        query.CommandType = CommandType.StoredProcedure;
        //        //    query.Parameters.AddRange(parameter);
        //        SqlDataReader data_reader = query.ExecuteReader();
        //        data_reader.Close();
        //        connection.Close();
        //        return query;
        //    }
        //    catch (SqlException exp)
        //    {
        //        MessageBox.Show("Error Excuting Procedure " + exp.Message);
        //        return null;
        //    }

        //}

        public static DateTime getServerTime()
        {

            SqlCommand command = BasicClass.ExecuteProcedure("sp_get_server_time", new SqlParameter[0]);

            SqlDataAdapter dp = new SqlDataAdapter(command);
            DataSet ds = new DataSet("exp");
            dp.Fill(ds);

            return DateTime.Parse(ds.Tables[0].Rows[0]["T"].ToString());

        }
        public static DateTime getServerTimeWithConnection(SqlConnection connection)
        {

            SqlCommand command = BasicClass.ExecuteProcedureWithConnection("sp_get_server_time", new SqlParameter[0], connection);

            SqlDataAdapter dp = new SqlDataAdapter(command);
            DataSet ds = new DataSet("exp");
            dp.Fill(ds);

            return DateTime.Parse(ds.Tables[0].Rows[0]["T"].ToString());

        }

        //public static string age_for_display(DateTime dob)
        //{
        //    int[] age = Basic.caclculate_age(dob);

        //    string result = "";

        //    if (age.Length > 0)
        //    {
        //        if (age[0] > 0)
        //            result = age[0] + " Years";
        //        else if (age[1] > 0)
        //            result = age[1] + " Months";
        //        else if (age[2] > 0)
        //            result = age[2] + " Weeks";
        //        else if (age[3] > 0)
        //            result = age[3] + " days";
        //    }
        //    else
        //        return result;

        //    return result;

        //}

        //public static DateTime get_dob(int[] age)
        //{
        //    DateTime dob = DateTime.Now;

        //    dob = dob.Subtract(TimeSpan.FromDays(age[0] * 365));
        //    dob = dob.Subtract(TimeSpan.FromDays(age[1] * 30));
        //    dob = dob.Subtract(TimeSpan.FromDays(age[2] * 7));
        //    dob = dob.Subtract(TimeSpan.FromDays(age[3]));

        //    return dob;
        //}

        public static int[] caclculate_age(DateTime dob)
        {
            int[] Calculated_age = new int[4];


            DateTime now = BasicClass.getServerTime();
            TimeSpan age = now.Subtract(dob);
            //  Calculated_age = age.Days.ToString();
            int days = age.Days;

            if (days > 365)
            {
                Calculated_age[0] = (days / 365);//.ToString() + " years";
                days = days - Calculated_age[0] * 365;
            }
            if (days < 365 && days > 30)
            {
                Calculated_age[1] = (days / 30);//.ToString() + " months";
                days = days - Calculated_age[1] * 30;
            }
            if (days <= 30 && days > 7)
            {
                Calculated_age[2] = (days / 7);//.ToString() + " weeks";
                days = days - Calculated_age[2] * 7;
            }
            if (days < 7)
            {
                Calculated_age[3] = days;// +" days";
            }

            //MessageBox.Show(Calculated_age + "");
            return Calculated_age;
        }

        //public static bool to_bool(String S)
        //{
        //    if (S.Equals(""))
        //        return false;
        //    return Boolean.Parse(S);

        //}

        //internal static string getPriceGroup(string p)
        //{
        //    //throw new NotImplementedException();
        //    return "CV";
        //}

        //public static DataRow getMeasureOfUncertainity()
        //{

        //    SqlParameter[] parameter = new SqlParameter[0];

        //    SqlCommand command = Basic.ExecuteProcedure("sp_get_measure_of_uncertainity", parameter);
        //    if (command != null)
        //    {
        //        SqlDataAdapter dp = new SqlDataAdapter(command);
        //        DataSet ds = new DataSet("exp");

        //        dp.Fill(ds);

        //        DataRow rd = ds.Tables[0].Rows[0];

        //        return rd;

        //    }
        //    return null;

        //}



        //#region Yoni Magna

        //public static SqlCommand executeProcedure(string procedure, SqlParameter[] parameter, bool execute)
        //{
        //    try
        //    {
        //        SqlConnection connection = Connection();
        //        connection.Open();
        //        SqlCommand query = new SqlCommand(procedure, connection);
        //        query.CommandType = CommandType.StoredProcedure;
        //        query.Parameters.AddRange(parameter);

        //        if (execute)
        //        {
        //            SqlDataReader data_reader = query.ExecuteReader();
        //            data_reader.Close();
        //        }
        //        connection.Close();
        //        return query;
        //    }
        //    catch (SqlException exp)
        //    {
        //        MessageBox.Show("Error Excuting Procedure " + exp.Message);
        //        return null;
        //    }

        //}

        //// Executes plain query
        //public static SqlCommand executeQuery(string query)
        //{
        //    SqlConnection connection = Connection();
        //    SqlCommand command = new SqlCommand();
        //    command.CommandText = query;
        //    command.Connection = connection;

        //    return command;
        //}

        //// Accepts SqlCommand to System.Data.DataTable
        //public static System.Data.DataTable extractSystem.Data.DataTable(SqlCommand command)
        //{
        //    try
        //    {
        //        SqlDataAdapter dp = new SqlDataAdapter(command);
        //        DataSet ds = new DataSet("exp");
        //        dp.Fill(ds);

        //        return ds.Tables[0];
        //    }
        //    catch(Exception exp)
        //    {
        //        MessageBox.Show(exp.Message);
        //        return new System.Data.DataTable();
        //    }
        //}

        //// Accepts multiple attribute to execute a procedure
        //public static SqlCommand executeProcedureWithParameter(string procedure, bool execute, string [] keys, params object[] param)
        //{

        //    if (keys.Length != param.Length) {
        //        MessageBox.Show("Fields and values don't match. Stop waisting my time.");
        //        Environment.Exit(0);
        //    }

        //    SqlParameter [] sqlParameter = new SqlParameter[param.Length];

        //    for(int i=0; i<param.Length; i++){

        //        sqlParameter[i] = new SqlParameter("@" + keys[i], param[i]);

        //        if(param[i] == null)
        //            sqlParameter[i].Value = DBNull.Value;
        //    }

        //    return executeProcedure(procedure, sqlParameter, execute);
        //}

        //// Accepts a table name and an ID then retrives the item with that ID
        //public static System.Data.DataTable getDataByID(string tableName, string key, int id){
        //    SqlCommand command = executeProcedureWithParameter("sp_get_" + tableName + "_by_ID", false, new string[]{ key }, id);
        //    return extractSystem.Data.DataTable(command);
        //}

        //// Accepts a table name then retrives all the items
        //public static System.Data.DataTable getAllData(string tableName)
        //{
        //    SqlCommand command = executeProcedureWithParameter("sp_get_all_" + tableName + "", false, new string[] {});
        //    return extractSystem.Data.DataTable(command);
        //}

        //// Accepts a table name, a foreign ID and field name then retrives items with that foreign ID
        //public static System.Data.DataTable getDataByForeignID(string tableName, string field , int id)
        //{
        //    SqlCommand command = executeProcedureWithParameter("sp_get_" + tableName + "_on_" + field , false, new string[]{ field } , id);
        //    return extractSystem.Data.DataTable(command);
        //}

        //public static System.Data.DataTable insertData(string tableName, string[] fields, params object[] param) {
        //    return extractSystem.Data.DataTable(executeProcedureWithParameter("sp_insert_" + tableName, false, fields, param));
        //}

        //public static void updateData(string tableName, string[] fields, params object[] param)
        //{
        //    executeProcedureWithParameter("sp_update_" + tableName, true, fields, param);
        //}

        //public static void deleteData(string tableName, int id)
        //{
        //    executeProcedureWithParameter("sp_delete_" + tableName,true , new string[]{"Id"}, id);
        //}

        //// Create user object




        //// Important functions

        //public static void fillComboBox(System.Data.DataTable table, string initial, ref RadDropDownList combo, params object[] fields)
        //{
        //    // combo.Items.Clear();

        //    if (initial != "")
        //    {
        //        RadListDataItem item = new RadListDataItem();
        //        item.Value = "";
        //        item.Text = initial;

        //        combo.Items.Add(item);
        //    }

        //    for (int i = 0; i < table.Rows.Count; i++)
        //    {
        //        RadListDataItem item = new RadListDataItem();
        //        item.Text = "";

        //        for (int j = 0; j < fields.Length; j++)
        //        {
        //            if (j == 0 && (fields[j].ToString() == "Id" || fields[j].ToString() == "Batch"))
        //                item.Value = table.Rows[i][fields[j].ToString()].ToString().TrimEnd();
        //            else
        //                item.Text += table.Rows[i][fields[j].ToString()].ToString().TrimEnd() + " - ";
        //        }

        //        if (item.Text.Length > 2)
        //            item.Text = item.Text.Substring(0, item.Text.Length - 2);

        //        combo.Items.Add(item);
        //    }

        //    if (table.Rows.Count > 0)
        //    {
        //        combo.SelectedIndex = 0;
        //        combo.Text = (combo.Items.Count > 0 ? combo.Items[0].Text : "");
        //    }

        //}

        //public static void fillComboBox(System.Data.DataTable table, string initial, ref RadDropDownList combo, params object[] fields)
        //{
        //    // combo.Items.Clear();

        //    if (initial != "")
        //    {
        //        RadListDataItem item = new RadListDataItem();
        //        item.Value = "";
        //        item.Text = initial;

        //        combo.Items.Add(item);
        //    }

        //    for (int i = 0; i < table.Rows.Count; i++)
        //    {
        //        RadListDataItem item = new RadListDataItem();
        //        item.Text = "";

        //        for (int j = 0; j < fields.Length; j++)
        //        {
        //            if (j == 0 && (fields[j].ToString() == "Id" || fields[j].ToString() == "Batch"))
        //                item.Value = table.Rows[i][fields[j].ToString()].ToString().TrimEnd();
        //            else
        //                item.Text += table.Rows[i][fields[j].ToString()].ToString().TrimEnd() + " - ";
        //        }

        //        if (item.Text.Length > 2)
        //            item.Text = item.Text.Substring(0, item.Text.Length - 2);

        //        combo.Items.Add(item);
        //    }

        //    if (table.Rows.Count > 0)
        //    {
        //        combo.SelectedIndex = 0;
        //        combo.Text = (combo.Items.Count > 0 ? combo.Items[0].Text : "");
        //    }

        //}

        //public static void resetComboBox(ref RadDropDownButton combo)
        //{
        //    combo.Text = (combo.Items.Count > 0 ? combo.Items[0].Text : "");
        //}

        public static RadGridView reduceColumns(ref RadGridView table, params string[] columns)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                int found = -1;

                for (int j = 0; j < columns.Length; j++)
                {
                    if (columns[j] == table.Columns[i].HeaderText.ToString().TrimEnd())
                    {
                        found = j;
                        break;
                    }
                }

                if (found == -1)
                {
                    table.Columns[i].IsVisible = false;
                }
            }

            table.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            return table;
        }

        //public static System.Data.DataTable convertSelectedRowsToSystem.Data.DataTable(ref RadGridView rgv)
        //{
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    foreach (GridViewDataColumn column in rgv.Columns)
        //        if( column.IsVisible)
        //            dt.Columns.Add(column.Name);
        //    // MessageBox.Show(rgv.SelectedRows.Count.ToString());
        //    for (int i = 0; i < rgv.SelectedRows.Count; i++)
        //    {
        //        dt.Rows.Add();
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            dt.Rows[i][j] = rgv.SelectedRows[i].Cells[ dt.Columns[j].ColumnName ].Value;
        //        }
        //    }
        //    // MessageBox.Show(rgv.Columns.Count.ToString());
        //    return dt;
        //}

        //public static DialogResult Show(string title, string promptText, ref string value)
        //{
        //    Form form = new Form();
        //    Label label = new Label();
        //    TextBox textBox = new TextBox();
        //    Button buttonOk = new Button();
        //    Button buttonCancel = new Button();

        //    form.Text = title;
        //    label.Text = promptText;
        //    textBox.Text = value;

        //    buttonOk.Text = "OK";
        //    buttonCancel.Text = "Cancel";
        //    buttonOk.DialogResult = DialogResult.OK;
        //    buttonCancel.DialogResult = DialogResult.Cancel;

        //    label.SetBounds(9, 20, 372, 13);
        //    textBox.SetBounds(12, 36, 372, 20);
        //    buttonOk.SetBounds(228, 72, 75, 23);
        //    buttonCancel.SetBounds(309, 72, 75, 23);

        //    label.AutoSize = true;
        //    textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
        //    buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        //    buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        //    form.ClientSize = new Size(396, 107);
        //    form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
        //    form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
        //    form.FormBorderStyle = FormBorderStyle.FixedDialog;
        //    form.StartPosition = FormStartPosition.CenterScreen;
        //    form.MinimizeBox = false;
        //    form.MaximizeBox = false;
        //    form.AcceptButton = buttonOk;
        //    form.CancelButton = buttonCancel;

        //    DialogResult dialogResult = form.ShowDialog();
        //    value = textBox.Text;
        //    return dialogResult;
        //}


        //#endregion 



        #region Yoni Magna

        public static SqlCommand executeProcedureWithConnection(SqlConnection connection, string procedure, SqlParameter[] parameter, bool execute)
        {
            try
            {
                //SqlConnection connection = Connection();
                connection.Open();
                SqlCommand query = new SqlCommand(procedure, connection);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.AddRange(parameter);

                //if (execute)
                //{
                //    SqlDataReader data_reader = query.ExecuteReader();
                //    data_reader.Close();
                //}
                query.ExecuteNonQuery();
                connection.Close();
                return query;
            }
            catch (SqlException exp)
            {
                MessageBox.Show("Error Excuting Procedure " + exp.Message);
                return null;
            }

        }

        public static SqlCommand executeProcedure(string procedure, SqlParameter[] parameter, bool execute)
        {
            try
            {
                SqlConnection connection = Connection();
                connection.Open();
                SqlCommand query = new SqlCommand(procedure, connection);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.AddRange(parameter);

                if (execute)
                {
                    SqlDataReader data_reader = query.ExecuteReader();
                    data_reader.Close();
                }
                connection.Close();
                return query;
            }
            catch (SqlException exp)
            {
                MessageBox.Show("Error Excuting Procedure " + exp.Message);
                return null;
            }

        }

        public static SqlDataReader executeProcedureReaderWithParameter(string procedure, SqlParameter[] parameter)
        {
            try
            {
                SqlConnection connection = Connection();
                connection.Open();
                SqlCommand query = new SqlCommand(procedure, connection);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.AddRange(parameter);


                SqlDataReader data_reader = query.ExecuteReader();
                return data_reader;
            }
            catch (SqlException exp)
            {
                MessageBox.Show("Error Excuting Procedure " + exp.Message);
                return null;
            }

        }

        // Executes plain query
        public static SqlCommand executeQuery(string query)
        {
            SqlConnection connection = Connection();
            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.Connection = connection;

            return command;
        }

        // Accepts SqlCommand to System.Data.DataTable
        public static System.Data.DataTable extractDataTable(SqlCommand command)
        {
            try
            {
                SqlDataAdapter dp = new SqlDataAdapter(command);
                DataSet ds = new DataSet("exp");
                dp.Fill(ds);

                return ds.Tables[0];
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return new System.Data.DataTable();
            }
        }

        // Accepts multiple attribute to execute a procedure
        public static SqlCommand executeProcedureWithParameter(string procedure, bool execute, string[] keys, params object[] param)
        {

            if (keys.Length != param.Length)
            {
                MessageBox.Show("Fields and values don't match.");
                Environment.Exit(0);
            }

            SqlParameter[] sqlParameter = new SqlParameter[param.Length];

            for (int i = 0; i < param.Length; i++)
            {

                sqlParameter[i] = new SqlParameter("@" + keys[i], param[i]);

                if (param[i] == null)
                    sqlParameter[i].Value = DBNull.Value;
            }

            return executeProcedure(procedure, sqlParameter, execute);
        }

        public static SqlCommand executeProcedureWithParameterWithConnection(SqlConnection connection, string procedure, bool execute, string[] keys, params object[] param)
        {

            if (keys.Length != param.Length)
            {
                MessageBox.Show("Fields and values don't match.");
                Environment.Exit(0);
            }

            SqlParameter[] sqlParameter = new SqlParameter[param.Length];

            for (int i = 0; i < param.Length; i++)
            {

                sqlParameter[i] = new SqlParameter("@" + keys[i], param[i]);

                if (param[i] == null)
                    sqlParameter[i].Value = DBNull.Value;
            }

            return executeProcedureWithConnection(connection, procedure, sqlParameter, execute);
        }
        public static SqlDataReader ExecuteProcedureReaderWithParameter(string procedure, string[] keys, params object[] param)
        {

            if (keys.Length != param.Length)
            {
                MessageBox.Show("Fields and values don't match.");
                Environment.Exit(0);
            }

            SqlParameter[] sqlParameter = new SqlParameter[param.Length];

            for (int i = 0; i < param.Length; i++)
            {

                sqlParameter[i] = new SqlParameter("@" + keys[i], param[i]);

                if (param[i] == null)
                    sqlParameter[i].Value = DBNull.Value;
            }

            return executeProcedureReaderWithParameter(procedure, sqlParameter);
        }

        // Accepts a table name and an ID then retrives the item with that ID
        public static System.Data.DataTable getDataByID(string tableName, string key, int id)
        {
            SqlCommand command = executeProcedureWithParameter("sp_get_" + tableName + "_by_ID", false, new string[] { key }, id);
            return extractDataTable(command);
        }

        // Accepts a table name then retrives all the items
        public static System.Data.DataTable getAllData(string tableName)
        {
            SqlCommand command = executeProcedureWithParameter("sp_get_all_" + tableName + "", false, new string[] { });
            return extractDataTable(command);
        }

        // Accepts a table name, a foreign ID and field name then retrives items with that foreign ID
        public static System.Data.DataTable getDataByForeignID(string tableName, string field, int id)
        {
            SqlCommand command = executeProcedureWithParameter("sp_get_" + tableName + "_on_" + field, false, new string[] { field }, id);
            return extractDataTable(command);
        }

        public static System.Data.DataTable insertData(string tableName, string[] fields, params object[] param)
        {
            return extractDataTable(executeProcedureWithParameter("sp_insert_" + tableName, false, fields, param));
        }

        public static void updateData(string tableName, string[] fields, params object[] param)
        {
            executeProcedureWithParameter("sp_update_" + tableName, true, fields, param);
        }

        public static SqlCommand updateDataWithStatus(string tableName, string[] fields, params object[] param)
        {
            return executeProcedureWithParameter("sp_update_" + tableName, true, fields, param);
        }

        public static void deleteData(string tableName, int id)
        {
            executeProcedureWithParameter("sp_delete_" + tableName, true, new string[] { "Id" }, id);
        }

        // Create user object




        // Important functions

        //public static void fillComboBox(System.Data.DataTable table, string initial, ref RadDropDownList combo, params object[] fields)
        //{
        //    // combo.Items.Clear();

        //    if (initial != "")
        //    {
        //        RadListDataItem item = new RadListDataItem();
        //        item.Value = "";
        //        item.Text = initial;

        //        combo.Items.Add(item);
        //    }

        //    for (int i = 0; i < table.Rows.Count; i++)
        //    {
        //        RadListDataItem item = new RadListDataItem();
        //        item.Text = "";

        //        for (int j = 0; j < fields.Length; j++)
        //        {
        //            if (j == 0 && (fields[j].ToString() == "Id" || fields[j].ToString() == "Batch"))
        //                item.Value = table.Rows[i][fields[j].ToString()].ToString().TrimEnd();
        //            else
        //                item.Text += table.Rows[i][fields[j].ToString()].ToString().TrimEnd() + " - ";
        //        }

        //        if (item.Text.Length > 2)
        //            item.Text = item.Text.Substring(0, item.Text.Length - 2);

        //        combo.Items.Add(item);
        //    }

        //    if (table.Rows.Count > 0)
        //    {
        //        combo.SelectedIndex = 0;
        //        combo.Text = (combo.Items.Count > 0 ? combo.Items[0].Text : "");
        //    }

        //}

        //public static void resetComboBox(ref RadDropDownButton combo)
        //{
        //    combo.Text = (combo.Items.Count > 0 ? combo.Items[0].Text : "");
        //}

        //public static RadGridView reduceColumns(ref RadGridView table, params string[] columns)
        //{
        //    for (int i = 0; i < table.Columns.Count; i++)
        //    {
        //        int found = -1;

        //        for (int j = 0; j < columns.Length; j++)
        //        {
        //            if (columns[j] == table.Columns[i].HeaderText.ToString().TrimEnd())
        //            {
        //                found = j;
        //                break;
        //            }
        //        }

        //        if (found == -1)
        //        {
        //            table.Columns[i].IsVisible = false;
        //        }
        //    }

        //    table.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

        //    return table;
        //}

        //public static System.Data.DataTable convertSelectedRowsToDataTable(ref RadGridView rgv)
        //{
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    foreach (GridViewDataColumn column in rgv.Columns)
        //        if (column.IsVisible)
        //            dt.Columns.Add(column.Name);
        //    // MessageBox.Show(rgv.SelectedRows.Count.ToString());
        //    for (int i = 0; i < rgv.SelectedRows.Count; i++)
        //    {
        //        dt.Rows.Add();
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            dt.Rows[i][j] = rgv.SelectedRows[i].Cells[dt.Columns[j].ColumnName].Value;
        //        }
        //    }
        //    // MessageBox.Show(rgv.Columns.Count.ToString());
        //    return dt;
        //}

        //public static DialogResult Show(string title, string promptText, ref string value)
        //{
        //    Form form = new Form();
        //    System.Windows.Forms.Label label = new System.Windows.Forms.Label();
        //    System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
        //    System.Windows.Forms.Button buttonOk = new System.Windows.Forms.Button();
        //    System.Windows.Forms.Button buttonCancel = new System.Windows.Forms.Button();

        //    form.Text = title;
        //    label.Text = promptText;
        //    textBox.Text = value;

        //    buttonOk.Text = "OK";
        //    buttonCancel.Text = "Cancel";
        //    buttonOk.DialogResult = DialogResult.OK;
        //    buttonCancel.DialogResult = DialogResult.Cancel;

        //    label.SetBounds(9, 20, 372, 13);
        //    textBox.SetBounds(12, 36, 372, 20);
        //    buttonOk.SetBounds(228, 72, 75, 23);
        //    buttonCancel.SetBounds(309, 72, 75, 23);

        //    label.AutoSize = true;
        //    textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
        //    buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        //    buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        //    form.ClientSize = new Size(396, 107);
        //    form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
        //    form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
        //    form.FormBorderStyle = FormBorderStyle.FixedDialog;
        //    form.StartPosition = FormStartPosition.CenterScreen;
        //    form.MinimizeBox = false;
        //    form.MaximizeBox = false;
        //    form.AcceptButton = buttonOk;
        //    form.CancelButton = buttonCancel;

        //    DialogResult dialogResult = form.ShowDialog();
        //    value = textBox.Text;
        //    return dialogResult;
        //}

        //public static void showReport(ref RadGridView grid, string title, string rightHeader, string leftFooter, string middleFooter, string rightFooter, bool hasNumber)
        //{

        //    if (hasNumber)
        //    {
        //        GridViewTextBoxColumn col = new GridViewTextBoxColumn();
        //        col.FieldName = "No";
        //        col.Width = 50;
        //        grid.Columns.Insert(0, col);

        //        int j = 1;
        //        for (int i = 0; i < grid.Rows.Count; i++)
        //        {
        //            if (grid.Rows[i].IsVisible)
        //            {
        //                grid.Rows[i].Cells["No"].Value = j;
        //                j++;
        //            }
        //        }
        //    }

        //    RadPrintDocument rpd = new RadPrintDocument();
        //    Image logo = (Image)Properties.Resources.Logo;

        //    rpd.Logo = logo;
        //    rpd.LeftHeader = "\n\n    [Logo]    \n\n";
        //    rpd.MiddleHeader = "Bethzatha Hospital\n" + title;
        //    rpd.RightHeader = rightHeader;
        //    rpd.HeaderHeight = 100;
        //    rpd.HeaderFont = new System.Drawing.Font(BasicClass.font_name, 20, FontStyle.Bold);
        //    rpd.LeftFooter = leftFooter;
        //    rpd.MiddleFooter = middleFooter;
        //    rpd.RightFooter = rightFooter;
        //    rpd.FooterHeight = 150;
        //    rpd.Landscape = true;
        //    grid.PrintPreview(rpd);

        //    if (hasNumber)
        //    {
        //        grid.Columns.RemoveAt(0);
        //    }
        //}

        public static void insertPharmacyHistory(string id, string batch, string received, string cost, string reason, string user, int GIV, int GRV, string miniStoreName = "")
        {
            System.Data.DataTable dt = BasicClass.extractDataTable(BasicClass.executeProcedureWithParameter("sp_get_quantity_by_item_id_and_batch",
                           new string[] { "item", "batch" },
                           id, batch));

            System.Data.DataTable dtBatch = BasicClass.extractDataTable(BasicClass.executeProcedureWithParameter("sp_get_PharmacyBatch_by_Batch", false,
                           new string[] { "batch" },
                           batch));

            string expiryDate = dtBatch.Rows[0]["ExpiryDate"].ToString();


            System.Data.DataTable item = BasicClass.getDataByID("PharmacyItem", "Id", int.Parse(id));
            DateTime date = BasicClass.getServerTime();
            string quantity = dt.Rows[0]["quantity"].ToString();
            string value = dt.Rows[0]["value"].ToString();

            BasicClass.executeProcedureWithParameter("sp_insert_pharmacy_history", true, new string[] { "code", "batch", "description", "date", "received", "cost", "quantity", "value", "ExpiryDate", "Reason", "user", "MinistoreName", "GIV", "GRV" },
                item.Rows[0]["Code"].ToString(), batch, item.Rows[0]["Name"].ToString(), date, received, cost, "", value, expiryDate, reason, user, miniStoreName, GIV, GRV);


            dt = BasicClass.extractDataTable(BasicClass.executeProcedureWithParameter("sp_get_quantity_by_item_id",
                           new string[] { "item" },
                           id));
            //if (reason.Equals("Batch Ballance Adjustment"))
            //    quantity = received;
            //else
            quantity = dt.Rows[0]["quantity"].ToString();
            value = dt.Rows[0]["value"].ToString();

            BasicClass.executeProcedureWithParameter("sp_insert_pharmacy_history", true, new string[] { "code", "batch", "description", "date", "received", "cost", "quantity", "value", "ExpiryDate", "Reason", "user", "MinistoreName", "GIV", "GRV" },
                item.Rows[0]["Code"].ToString(), "", item.Rows[0]["Name"].ToString(), date, "", "", quantity, value, expiryDate, "", user, "", GIV, GRV);
        }


        #endregion

    }
}
