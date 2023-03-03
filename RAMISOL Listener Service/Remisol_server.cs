using RAMISOL_Listener_Service.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BasicClassLibrary;

namespace RAMISOL_Listener_Service
{
    public partial class RemisolServer : Telerik.WinControls.UI.RadForm
    {
       // TcpClient clientSocket = default(TcpClient);
        StreamWriter streamwriter =  null; 
        TcpListener tcpListener = null;
        Socket socketForClient = null;
          
        SqlConnection con3 = null;
        SqlConnection con2 = null;

        private static char END_OF_BLOCK = '\u001c';
        private static char START_OF_BLOCK = '\u000b';
        private static char CARRIAGE_RETURN = (char)13;
   

        Thread t = null;

        public RemisolServer()
        {
            InitializeComponent();
        }
        private void RadForm1_Load(object sender, EventArgs e)
        {

             try
            {

             //   this.con3 = new SqlConnection(con03());
              //  con3.Open();

                this.con2 = new SqlConnection(con02());
                con2.Open();


                if (/**con3.State == ConnectionState.Open &&**/ con2.State == ConnectionState.Open)
                {
                  //  radLabelElement1.Text = "Both Databases Connected";
                    updateSebezLog("Sebez Database Connected.");
                 //   updateSebezLog("Sefed Database Connected.");
                }
                else radLabelElement1.Text = "Not Connected";

            }
            catch (Exception p)
            {

                MessageBox.Show("Error in trying to test connection is 0000000000002" + p.Message);
            }

            loadSettings();
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            t = new Thread(standBy);
            t.IsBackground = true;
            t.Start();

        }

        private void loadSettings()
        {
            ipBox.Text = Settings.Default["IP"].ToString().Trim();
            portB.Text = Settings.Default["PORT"].ToString().Trim();
            server1TB.Text = Settings.Default["SERVER1"].ToString().Trim();
            database1TB.Text = Settings.Default["DATABASE1"].ToString().Trim();
            userName1TB.Text = Settings.Default["USERNAME1"].ToString().Trim();
            password1TB.Text = Settings.Default["PASSWORD1"].ToString().Trim();
            server2TB.Text = Settings.Default["SERVER2"].ToString().Trim();
            database2TB.Text = Settings.Default["DATABASE2"].ToString().Trim();
            userName2TB.Text = Settings.Default["USERNAME2"].ToString().Trim();
            password2TB.Text = Settings.Default["PASSWORD2"].ToString().Trim();
        }

        //private string con03()
        //{
        //    try
        //    {
        //        string server = Settings.Default["SERVER2"].ToString().Trim();
        //        string database = Settings.Default["DATABASE2"].ToString().Trim();
        //        string userName = Settings.Default["USERNAME2"].ToString().Trim();
        //        string password = Settings.Default["PASSWORD2"].ToString().Trim();
        //        return @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + userName + ";Password=" + password;// +";connect timeout=10";
        //    }
        //    catch (Exception r)
        //    {

        //        MessageBox.Show(r.Message);
        //        return null;
        //    }
        //}
        private string con02()
        {
            try
            {
                string server = Settings.Default["SERVER1"].ToString().Trim();
                string database = Settings.Default["DATABASE1"].ToString().Trim();
                string userName = Settings.Default["USERNAME1"].ToString().Trim();
                string password = Settings.Default["PASSWORD1"].ToString().Trim();


                return @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + userName + ";Password=" + password;// +";connect timeout=10";
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message);
                return null;
            }
        }

        private void updateText(string t)
        {
            statustBOX.Text = t;

        }

        //private void updateLog(string t)
        //{
        //    sebezLog.Text = t;

        //}

        private string cleanCR(string text)
        {
            string temp = text.Replace("\r", "\n").Trim();

            return temp;
        }

        private void updateSebezLog(string t)
        {
            sebezLog.Text = t + "\n" + sebezLog.Text ;
        }

        private void updateSefedLog(string t)
        {
            //sefedLog.Text = sefedLog.Text + "\n" + t;
        }

        private void updateColor(Color c)
        {
            statustBOX.BackColor = c;
        }

        private void changeBackColorACK(Color co)
        {
           // ACK_LINE.BackColor = co;
        }

        private void changeBackColor(Color co)
        {
            this.BackColor = co;
            this.progressBar1.Style = ProgressBarStyle.Blocks;
            this.progressBar1.Visible = false;
        }
        private void setVisibility(bool x)
        {
            progressBar1.Visible = x;
        }
        private void changeBarStyle(ProgressBarStyle s)
        {
            progressBar1.Style = s;
        }

        private void processData(string data)
        {
            Thread proccessTread = new Thread(() => prepareData(data));
            proccessTread.IsBackground = true;
            proccessTread.Start();
        }

        public void prepareData(string co)
        {
            updateSebezLog("PREPARING : " +co);
        }

        private void standBy()
        {
            try
            {
                string ipAd = Settings.Default["IP"].ToString().Trim();
                int port = Int32.Parse(Settings.Default["PORT"].ToString().Trim());
               // clientSocket = new TcpClient(ipAd, port);
                tcpListener = new TcpListener(IPAddress.Parse(ipAd), port);
                tcpListener.Start();

                this.Invoke(new Action<string>(updateText), "Host Started! waiting for connection.");
                
                this.Invoke(new Action<Color>(updateColor), Color.Yellow);
                socketForClient = tcpListener.AcceptSocket();
                this.Invoke(new Action<Color>(updateColor), Color.LightGreen);
                this.Invoke(new Action<string>(updateText), "Host is successfully connected.");
                this.Invoke(new Action<string>(updateSebezLog), "Connection with Mindray established.");
                this.Invoke(new Action<Color>(changeBackColor), Color.LightGreen);

                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception r)
            {
                MessageBox.Show("External system connection "+ r.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string dataHolder = "";
            while (true)
            {
                try
                {
                    //NetworkStream networkStream = tcpListener.GetStream();
                    NetworkStream networkStream = new NetworkStream(socketForClient);
                    streamwriter = new StreamWriter(networkStream);
                    byte[] bytesFrom = new byte[65536];
                 
                    if (networkStream.DataAvailable)
                    {
                        this.Invoke(new Action<bool>(setVisibility), true);
                        this.Invoke(new Action<ProgressBarStyle>(changeBarStyle), ProgressBarStyle.Marquee);
                        this.Invoke(new Action<string>(updateText), "Receiving data... Please wait.");
                        networkStream.Read(bytesFrom, 0, (int)socketForClient.ReceiveBufferSize);
                       // networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                        string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                      //  MessageBox.Show(dataFromClient);
 
                        this.Invoke(new Action<bool>(setVisibility), false);
                        this.Invoke(new Action<ProgressBarStyle>(changeBarStyle), ProgressBarStyle.Continuous);
                        dataHolder += dataFromClient;

                        if (dataHolder.Contains("OBX"))
                        {
                            this.Invoke(new Action<string>(updateText), "Receiving data completed.");
                           // this.Invoke(new Action<string>(updateSebezLog), cleanCR(dataHolder));
                            this.Invoke(new Action<string>(updateSebezLog), update_result(cleanCR(dataHolder)));
                            File.AppendAllText("MindrayMessageAmbassador.txt",dataHolder);
                            this.Invoke(new Action<string>(SendAck), dataHolder);
                            dataHolder = "";
                        }
                    } 
                }
                catch (Exception r)
                {
                    MessageBox.Show("Error occured while receiving data. " + r.Message);
                }
            }
        }


        private string printable(string[] a)
        {
            string concat = "";
         for (int i = 0 ; i < a.Length; i++)
         {
             concat = concat + "[" + i + "] = " + a[i].Trim() +"\n"; 
         }
         return concat;
        }

        private string update_result(string result)
        {
                string sample_id = "";
                string test_code = "";

                string[] key0 = result.Split('|');
                string[] key = result.Split(new string[] { "OBX|" }, StringSplitOptions.None);
                string[] key2;
                string status = "";
                string result_value = "";
                try
                {
                    string[] key_sample = result.Split(new string[] { "PID|" }, StringSplitOptions.None);
                    if (key_sample.Length > 1)
                    {
                        sample_id = key_sample[1].Split('|')[1].ToString().Trim();//.Remove(key_sample[1].Split('|')[1].ToString().Trim().Length - 1, 1);
                    }


                    for (int i = 1; i < key.Length; i++)
                    {
                        key2 = key[i].Split(new string[] { "|" }, StringSplitOptions.None);

                        if (key2.Length > 4)
                        {
                            test_code = key2[3].Split('^')[0].ToString();
                            if (key2[4].Length > 5)
                                result_value = key2[4].ToString().Substring(0, 5);
                            else
                                result_value = key2[4].ToString();
                            string query = "update lab_history set result = '" + result_value.Trim() + "', is_completed = 1, result_date = GETDATE(), lab_technician = 'MINDRAY' " + " where lab_code ='" + sample_id + "' and is_approved=0  and test_code = RTRIM('" + get_LIS_code_by_mindray_id(test_code) + "')";
                            //    int sc = BasicClassLibrary.BasicClass.executeProcedureWithParameter("remisol_update_result_by_sample_id", new string[] { "sample_id", "test_code", "result" }, new object[] { sample_id, get_LIS_code_by_remisol_id(test_code), result });  
                            //if (sample_id.Contains("BHS") || sample_id.Contains("BHI"))
                            //{
                            //    SqlCommand sc = new SqlCommand(query, con3);
                            //    int e = sc.ExecuteNonQuery();
                            //    if (e >= 1)
                            //    {
                            //        status = " Updated";
                            //    }
                            //    else
                            //    {
                            //        status = " Not_Updated";

                            //    }
                            //}
                            //else
                            if(sample_id.Contains("C") || sample_id.Contains("B-") || sample_id.Contains("A"))
                            {
                                SqlCommand sc = new SqlCommand(query, con2);
                                int e = sc.ExecuteNonQuery();
                                if (e >= 1)
                                {
                                    status = " Updated";
                                }
                                else
                                {
                                    status = " Not_Updated";

                                }
                            }
                            else
                            {
                                status = "Invalid Sample ID";
                            }
                        }
                        else
                        {
                            MessageBox.Show("key2 size error....");
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("String manuplation error. "+e.Message+" Data "+e.Data);
                }
            return sample_id +status;
        }

        private string get_LIS_code_by_mindray_id(string remisol_code)
        {
            DataTable dt = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.ExecuteProcedure("get_all_mindray_configuration"));
            string lis_code = "";
            for(int i = 0 ; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i]["mindray_code"].ToString().Trim() == remisol_code.Trim())
                    lis_code= dt.Rows[i]["lis_code"].ToString().Trim();
            }
            return lis_code;
        }

        private static StringBuilder generate_ack(string result_header)
        {
            string frame_signature = "";
            string datetime = "";
            string[] ack = new string[2];
            string lf = char.ConvertFromUtf32(10);
            string fs = char.ConvertFromUtf32(28);
            string vt = char.ConvertFromUtf32(11);
            string cr = char.ConvertFromUtf32(13);


            string[] key = result_header.Split('|');

            if (key.Length > 9)
            {
                frame_signature = key[8];
                datetime = key[5];
            }

            var testHl7MessageToTransmit = new StringBuilder();
            
            testHl7MessageToTransmit.Append(START_OF_BLOCK)
                .Append(@"MSH|^~\&|||||" + datetime + "||ACK^R01|"+ frame_signature+"|P|2.3.1||||0||ASCII|||")
                .Append(CARRIAGE_RETURN)
                .Append("MSA|AA|" + frame_signature+"|Message accepted|||0|")
                .Append(CARRIAGE_RETURN)
                .Append(END_OF_BLOCK)
                .Append(CARRIAGE_RETURN);

            return testHl7MessageToTransmit;
        }


        private void SendAck(string dataHolder)
        {
            NetworkStream networkStream = new NetworkStream(socketForClient);
            //NetworkStream networkStream = clien.GetStream();
            StringBuilder ack;

            string[] all_line_messages = dataHolder.Split(new string[] { "MSH|^" }, StringSplitOptions.None);

            if (all_line_messages.Length >= 2)
            {
                ack = generate_ack(all_line_messages[1]);
                var sendMessageByteBuffer = Encoding.UTF8.GetBytes(ack.ToString());
                networkStream.Write(sendMessageByteBuffer, 0, sendMessageByteBuffer.Length);
              //  updateSebezLog(ack.ToString());
            }
         }             
   }
}
