using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Data.OleDb;
using System.IO;

namespace SMTPCodeGenerator
{
    public partial class MainForm : Form
    {
        public static string sExt
        {
            get;
            set;
        }
        public MainForm()
        {
            InitializeComponent();
        }

        #region FormLoad Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            TSLStatusLabel.Text = "En Espera";
            TSPBMainProgresBar.Visible = false;
        }

        #endregion

        #region Events

        private void button1_Click(object sender, EventArgs e)
        {
            TSLStatusLabel.Text = "Seleccionando Base de Datos";
            TSPBMainProgresBar.Visible = true;
            TSPBMainProgresBar.PerformStep();

            DialogResult dresult = openFileDialog1.ShowDialog();
            TSPBMainProgresBar.PerformStep();

            string sResoult = openFileDialog1.FileName;
            string sFileName = openFileDialog1.SafeFileName;
            string sExt = openFileDialog1.SafeFileName.Split('.')[1];
            tbDBType.Text = sExt;
            textBox1.Text = sResoult;

            TSPBMainProgresBar.PerformStep();
            int icounter = 0;

            DataTable Schema = new DataTable();
            DataTable dCombo = new DataTable();
            dCombo.Columns.Add("Text");
            dCombo.Columns.Add("Value");

            object[] oItem = new object[2];
            string sConnection = "";

            switch (sExt)
            {
                case "mdb":
                    {
                        sConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sResoult;
                        break;
                    }
                case "accdb":
                    {
                        sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sResoult;
                        break;
                    }
            }

            OleDbConnection mConnection = new OleDbConnection();
            mConnection.ConnectionString = sConnection;
            mConnection.Open();
            Schema = mConnection.GetSchema("Tables");

            foreach (DataRow drow in Schema.Rows)
            {
                TSPBMainProgresBar.PerformStep();
                object[] oItemArray = drow.ItemArray;
                if ((string)oItemArray[3] == "TABLE")
                {
                    oItem[0] = (string)oItemArray[2];
                    oItem[1] = icounter;
                    dCombo.Rows.Add(oItem);
                }
                icounter++;
            }
            mConnection.Close();

            mConnection.Dispose();
            TSPBMainProgresBar.PerformStep();
            comboBox1.DataSource = dCombo;
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
            comboBox1.Update();
            TSPBMainProgresBar.PerformStep();

            TSLStatusLabel.Text = "En Espera";
            TSPBMainProgresBar.Visible = false;
            TSPBMainProgresBar.Value = 0;

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TimeSpan tDelay = new TimeSpan(3000);
            TSLStatusLabel.Text = "Procesando Datos";
            TSPBMainProgresBar.Visible = true;
            TSPBMainProgresBar.PerformStep();

            string sResoult = textBox1.Text;
            string sExt = tbDBType.Text;
            string sConnection = "";
            object oItem = new object();
            int comboindex = comboBox1.SelectedIndex;

            int iRow = Convert.ToInt32(comboBox1.SelectedValue.ToString());
            //string sTable = (string)comboBox1.;
            int icounter = 0;

            DataTable Schema = new DataTable();
            DataTable dtable = new DataTable();
            DataTable dFiels = new DataTable();
            dFiels.Columns.Add("FieldName");
            dFiels.Columns.Add("FieldTipe");
            Object[] oField = new Object[2];



            OleDbConnection mConnection = new OleDbConnection();
            switch (sExt)
            {
                case "mdb":
                    {
                        sConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sResoult;
                        break;
                    }
                case "accdb":
                    {
                        sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sResoult;
                        break;
                    }
            }
            mConnection.ConnectionString = sConnection;
            mConnection.Open();
            Schema = mConnection.GetSchema("TABLES");
            dtable = mConnection.GetSchema("COLUMNS");

            string sTName = (string)Schema.Rows[iRow]["TABLE_NAME"];

            foreach (DataRow orow in dtable.Rows)
            {
                TSLStatusLabel.Text = "Creando Columnas";
                TSPBMainProgresBar.PerformStep();

                if (orow["TABLE_NAME"].ToString() == sTName)
                {
                    oField[0] = orow["COLUMN_NAME"].ToString();
                    oField[1] = orow["DATA_TYPE"].ToString();
                    dFiels.Rows.Add(oField);
                }
                icounter++;
            }


            mConnection.Close();

            mConnection.Dispose();
            string[] sLines = new string[135];
            TSLStatusLabel.Text = "Creando Comando Select";
            TSPBMainProgresBar.PerformStep();
            //Lectura de la tabla
            sLines[0] = "#region " + sTName + " Commands";
            sLines[1] = " ";
            sLines[2] = "public static DataTable " + "Dt" + sTName + "(string sConnection)";
            sLines[3] = "{";
            sLines[4] = "   //Comando para leer Tabla";
            sLines[5] = "   OleDbCommand " + "Sp" + sTName + "Load = new OleDbCommand(" + '"' + sReadCommand(dFiels, sTName) + '"' + ");";
            sLines[6] = " ";
            sLines[7] = "DataTable Dt" + sTName + " = new DataTable();";
            sLines[8] = "Object[] oRow = new object[" + dFiels.Rows.Count.ToString() + "];";
            sLines[10] = sAddTableColumns(dFiels, "Dt" + sTName);
            sLines[11] = " ";
            sLines[12] = "SMTConnection.ConnectionString = sConnection;";
            sLines[13] = "Sp" + sTName + "Load.Connection = SMTConnection;";
            sLines[14] = "SMTConnection.Open();";
            sLines[15] = " ";
            sLines[16] = "OleDbDataReader Dreader = " + "Sp" + sTName + "Load" + ".ExecuteReader();";
            sLines[17] = "while (Dreader.Read())";
            sLines[18] = "{";
            sLines[19] = sGetAddTableColumns(dFiels);
            sLines[20] = "Dt" + sTName + ".Rows.Add(oRow);";
            sLines[21] = "}";
            sLines[22] = " ";
            sLines[23] = "Dreader.Close();";
            sLines[24] = "SMTConnection.Close();";
            sLines[25] = "SMTConnection.Dispose();";
            sLines[26] = " ";
            sLines[27] = "return Dt" + sTName + ";";
            sLines[28] = "} ";
            sLines[29] = " ";
            TSLStatusLabel.Text = "Creando Comando SelectRow";
            TSPBMainProgresBar.PerformStep();
            //lectura de la row
            sLines[30] = "public static DataTable " + "DtRow" + sTName + "(string sConnection, int iPKID_" + sTName + ")";
            sLines[31] = " ";
            sLines[32] = "{";
            sLines[33] = "   //Comando para leer linea de la Tabla";
            sLines[34] = "   OleDbCommand " + "Sp" + sTName + "LoadRow = new OleDbCommand(" + '"' + sReadRowCommand(dFiels, sTName) + '"' + ");";
            sLines[35] = " ";
            sLines[36] = "DataTable DtRow" + sTName + " = new DataTable();";
            sLines[37] = "Object[] oRow = new object[" + Convert.ToString(dFiels.Rows.Count - 1) + "];";
            sLines[38] = sEitTableColumns(dFiels, "DtRow" + sTName);
            sLines[39] = " ";
            sLines[40] = "SMTConnection.ConnectionString = sConnection;";
            sLines[41] = "Sp" + sTName + "LoadRow.Connection = SMTConnection;";
            sLines[42] = "Sp" + sTName + "LoadRow.Parameters.Add(" + '"' + "PKID_" + sTName + '"' + ", OleDbType.Integer);";
            sLines[43] = "Sp" + sTName + "LoadRow.Parameters[0].Value = " + "iPKID_" + sTName + ";";
            sLines[44] = " ";
            sLines[45] = "SMTConnection.Open();";
            sLines[46] = " ";
            sLines[47] = "OleDbDataReader Dreader = " + "Sp" + sTName + "LoadRow" + ".ExecuteReader();";
            sLines[48] = "while (Dreader.Read())";
            sLines[49] = "{";
            sLines[50] = sGetEditTableColumns(dFiels);
            sLines[51] = "DtRow" + sTName + ".Rows.Add(oRow);";
            sLines[52] = "}";
            sLines[53] = " ";
            sLines[54] = "Dreader.Close();";
            sLines[55] = "SMTConnection.Close();";
            sLines[56] = "SMTConnection.Dispose();";
            sLines[57] = " ";
            sLines[58] = "return DtRow" + sTName + ";";
            sLines[59] = "} ";
            sLines[60] = " ";
            TSLStatusLabel.Text = "Creando Comando Insert";
            TSPBMainProgresBar.PerformStep();
            //Insert en tabla
            sLines[61] = " ";
            sLines[62] = "public static int " + "Add" + sTName + "(string sConnection, " + sAddValues(dFiels) + ")";
            sLines[63] = "{";
            sLines[64] = "//Comando para Insertar";
            sLines[65] = "OleDbCommand " + "Sp" + sTName + "Add = new OleDbCommand(" + '"' + sAddCommand(dFiels, sTName) + '"' + ");";
            sLines[66] = " ";
            sLines[67] = "int iRowAfected = 0;";
            sLines[68] = "SMTConnection.ConnectionString = sConnection;";
            sLines[69] = "Sp" + sTName + "Add.Connection = SMTConnection;";
            sLines[70] = " ";
            sLines[71] = sAddParameters(dFiels, sTName);
            sLines[72] = sAddParametersValues(dFiels, sTName);
            sLines[73] = " ";
            sLines[74] = "SMTConnection.Open();";
            sLines[75] = " ";
            sLines[76] = "iRowAfected = " + "Sp" + sTName + "Add.ExecuteNonQuery();";
            sLines[77] = " ";
            sLines[78] = "SMTConnection.Close();";
            sLines[79] = "SMTConnection.Dispose();";
            sLines[80] = " ";
            sLines[81] = "return iRowAfected;";
            sLines[82] = "} ";
            sLines[83] = " ";
            TSLStatusLabel.Text = "Creando Comando Update";
            TSPBMainProgresBar.PerformStep();
            //Editar en Tabla
            sLines[84] = " ";
            sLines[85] = "public static int " + "Edit" + sTName + "(string sConnection, " + sEditValues(dFiels) + ")";
            sLines[86] = "{";
            sLines[87] = "//Comando para Editar";
            sLines[88] = "OleDbCommand " + "Sp" + sTName + "Edit = new OleDbCommand(" + '"' + sEditCommand(dFiels, sTName) + '"' + ");";
            sLines[89] = " ";
            sLines[90] = "int iRowAfected = 0;";
            sLines[91] = "SMTConnection.ConnectionString = sConnection;";
            sLines[92] = "Sp" + sTName + "Edit.Connection = SMTConnection;";
            sLines[93] = " ";
            sLines[94] = sEditParameters(dFiels, sTName);
            sLines[95] = sEditParametersValues(dFiels, sTName);
            sLines[96] = " ";
            sLines[97] = "SMTConnection.Open();";
            sLines[98] = " ";
            sLines[99] = "iRowAfected = " + "Sp" + sTName + "Edit.ExecuteNonQuery();";
            sLines[100] = " ";
            sLines[101] = "SMTConnection.Close();";
            sLines[102] = "SMTConnection.Dispose();";
            sLines[103] = " ";
            sLines[104] = "return iRowAfected;";
            sLines[105] = "} ";
            TSLStatusLabel.Text = "Creando Comando Delete";
            TSPBMainProgresBar.PerformStep();
            //Borrar de Tabla
            sLines[106] = " ";
            sLines[107] = "public static int " + "Delete" + sTName + "(string sConnection, " + "int iPKID_" + sTName + ")";
            sLines[108] = "{";
            sLines[109] = "//Comando para Borrar";
            sLines[110] = "OleDbCommand " + "Sp" + sTName + "Delete = new OleDbCommand(" + '"' + "DELETE FROM " + sTName + " WHERE (PKID_" + sTName + " = ?)" + '"' + ");";
            sLines[111] = " ";
            sLines[112] = "int iRowAfected = 0;";
            sLines[113] = "SMTConnection.ConnectionString = sConnection;";
            sLines[114] = "Sp" + sTName + "Delete.Connection = SMTConnection;";
            sLines[115] = " ";
            sLines[116] = "Sp" + sTName + "Delete.Parameters.Add(" + '"' + "PKID_" + sTName + '"' + ", OleDbType.Integer);";
            sLines[117] = "Sp" + sTName + "Delete.Parameters[0].Value = iPKID_" + sTName + ";";
            sLines[118] = " ";
            sLines[119] = "SMTConnection.Open();";
            sLines[120] = " ";
            sLines[121] = "iRowAfected = " + "Sp" + sTName + "Delete.ExecuteNonQuery();";
            sLines[122] = " ";
            sLines[123] = "SMTConnection.Close();";
            sLines[124] = "SMTConnection.Dispose();";
            sLines[125] = " ";
            sLines[126] = "return iRowAfected;";
            sLines[127] = "} ";
            sLines[128] = " ";
            sLines[129] = "#endregion ";
            sLines[130] = " ";

            WriteFile(sLines);

            TSLStatusLabel.Text = "Mostrando resultado";
            TSPBMainProgresBar.PerformStep();

        }

        #endregion

        #region PrivateVoids

        private bool AreFKID(DataTable dTable)
        {
            bool bResult = false;

            foreach (DataRow orow in dTable.Rows)
            {
                string sColumn = orow["FieldName"].ToString();

                if (sColumn.Contains("FKID"))
                {
                    bResult = true;
                }
            }

            return bResult;
        }

        private string sReadCommand(DataTable dTable, string sTableName)
        {
            string sResoult = "SELECT ";
            int iCounter = 0;
            int iFKID = 0;
            string sEnd = "";

            if (AreFKID(dTable))
            {
                foreach (DataRow row in dTable.Rows)
                {
                    string sColumn = row["FieldName"].ToString();

                    if (iCounter > 0)
                    {
                        if (sColumn.Contains("FKID"))
                        {
                            sResoult += ", " + sGetTableFromField(sColumn) + "." + sGetTableFromField(sColumn);

                            if (iFKID > 0)
                                sEnd += " INNER JOIN " + sGetTableFromField(sColumn) + " ON " + sTableName + "." + sColumn + " = " + sGetTableFromField(sColumn) + "." + "PKID_" + sGetTableFromField(sColumn);
                            else
                                sEnd += "(" + sTableName + " INNER JOIN " + sGetTableFromField(sColumn) + " ON " + sTableName + "." + sColumn + " = " + sGetTableFromField(sColumn) + "." + "PKID_" + sGetTableFromField(sColumn) + ")";
                            iFKID++;
                        }
                        else
                        {
                            sResoult += ", " + sTableName + "." + sColumn;
                        }


                    }

                    else
                    {
                        if (sColumn.Contains("FKID"))
                        {
                            sResoult += sGetTableFromField(sColumn) + "." + sColumn;
                            sResoult += ", " + sGetTableFromField(sColumn) + "." + sColumn;

                            if (iFKID > 0)
                                sEnd += " INNER JOIN " + sGetTableFromField(sColumn) + " ON " + sTableName + "." + sColumn + " = " + sGetTableFromField(sColumn) + "." + "PKID_" + sGetTableFromField(sColumn);
                            else
                                sEnd += "(" + sTableName + " INNER JOIN " + sGetTableFromField(sColumn) + " ON " + sTableName + "." + sColumn + " = " + sGetTableFromField(sColumn) + "." + "PKID_" + sGetTableFromField(sColumn) + ")";
                            iFKID++;
                        }
                        else
                        {
                            sResoult += sTableName + "." + sColumn;
                        }
                    }




                    iCounter++;


                }

                sResoult += " FROM (" + sEnd + ")";
            }
            else
            {
                foreach (DataRow row in dTable.Rows)
                {
                    if (iCounter > 0)
                        sResoult += ", " + row["FieldName"].ToString();
                    else
                        sResoult += row["FieldName"].ToString();

                    iCounter++;
                }

                sResoult += " FROM " + sTableName;

            }

            return sResoult;
        }

        private string sReadRowCommand(DataTable dTable, string sTableName)
        {
            string sResoult = "SELECT ";
            int iCounter = 0;
            int iFKID = 0;
            string sEnd = "";

            if (AreFKID(dTable))
            {
                foreach (DataRow row in dTable.Rows)
                {
                    string sColumn = row["FieldName"].ToString();
                    if (!sColumn.Contains("PKID"))
                    {

                        if (iCounter > 0)
                        {
                            if (sColumn.Contains("FKID"))
                            {
                                sResoult += ", " + sGetTableFromField(sColumn) + "." + sGetTableFromField(sColumn);

                                if (iFKID > 0)
                                    sEnd += " INNER JOIN " + sGetTableFromField(sColumn) + " ON " + sTableName + "." + sColumn + " = " + sGetTableFromField(sColumn) + "." + "PKID_" + sGetTableFromField(sColumn);
                                else
                                    sEnd += "(" + sTableName + " INNER JOIN " + sGetTableFromField(sColumn) + " ON " + sTableName + "." + sColumn + " = " + sGetTableFromField(sColumn) + "." + "PKID_" + sGetTableFromField(sColumn) + ")";
                                iFKID++;
                            }
                            else
                            {
                                sResoult += ", " + sTableName + "." + sColumn;
                            }
                        }
                        else
                        {
                            if (sColumn.Contains("FKID"))
                            {
                                sResoult += sGetTableFromField(sColumn) + "." + sColumn;
                                sResoult += ", " + sGetTableFromField(sColumn) + "." + sColumn;

                                if (iFKID > 0)
                                    sEnd += " INNER JOIN " + sGetTableFromField(sColumn) + " ON " + sTableName + "." + sColumn + " = " + sGetTableFromField(sColumn) + "." + "PKID_" + sGetTableFromField(sColumn);
                                else
                                    sEnd += "(" + sTableName + " INNER JOIN " + sGetTableFromField(sColumn) + " ON " + sTableName + "." + sColumn + " = " + sGetTableFromField(sColumn) + "." + "PKID_" + sGetTableFromField(sColumn) + ")";
                                iFKID++;
                            }
                            else
                            {
                                sResoult += sTableName + "." + sColumn;
                            }
                        }

                        iCounter++;
                    }
                }

                sResoult += " FROM (" + sEnd + ") WHERE (" + sTableName + ".PKID_" + sTableName + " = ?)";
            }
            else
            {

                foreach (DataRow row in dTable.Rows)
                {
                    string sColumn = row["FieldName"].ToString();
                    if (!sColumn.Contains("PKID"))
                    {
                        if (iCounter > 0)
                            sResoult += ", " + sColumn;
                        else
                            sResoult += sColumn;

                        iCounter++;
                    }
                }

                sResoult += " FROM " + sTableName + " WHERE (" + sTableName + ".PKID_" + sTableName + " = ?)";

            }

            return sResoult;
        }

        private string sGetTableFromField(string sFieldName)
        {
            string sResoult = "";
            char[] cSeparator = new char[1];
            cSeparator[0] = '_';

            string[] sss = sFieldName.Split(cSeparator);
            sResoult = sss[1];

            return sResoult;
        }

        private string sAddTableColumns(DataTable dTable, string sTable)
        {
            string sresoult = "";

            foreach (DataRow orow in dTable.Rows)
            {
                string sColumn = orow["FieldName"].ToString();

                if (sColumn.Contains("FKID"))
                {
                    sColumn = sGetTableFromField(sColumn);
                }
                sresoult += sTable + ".Columns.Add(" + '"' + sColumn + '"' + ");";

            }

            return sresoult;
        }

        private string sEitTableColumns(DataTable dTable, string sTable)
        {
            string sresoult = "";

            foreach (DataRow orow in dTable.Rows)
            {
                string sColumn = orow["FieldName"].ToString();
                if (!sColumn.Contains("PKID"))
                {
                    if (sColumn.Contains("FKID"))
                    {
                        sColumn = sGetTableFromField(sColumn);
                    }
                    sresoult += sTable + ".Columns.Add(" + '"' + sColumn + '"' + ");";
                }
            }

            return sresoult;
        }

        private string sGetAddTableColumns(DataTable dTable)
        {
            string sresoult = "";
            int iCounter = 0;

            foreach (DataRow orow in dTable.Rows)
            {
                string sColumn = orow["FieldName"].ToString();
                string sType = sConvertDataType(Convert.ToInt32(orow["FieldTipe"].ToString()));

                if (sColumn.Contains("FKID"))
                {
                    sColumn = sGetTableFromField(sColumn);
                    sType = "ToString";
                }

                sresoult += "oRow[" + iCounter.ToString() + "] = Convert." + sType + "(Dreader[" + '"' + sColumn + '"' + "]);";


                iCounter++;
            }

            return sresoult;
        }

        private string sGetEditTableColumns(DataTable dTable)
        {
            string sresoult = "";
            int iCounter = 0;

            foreach (DataRow orow in dTable.Rows)
            {
                string sColumn = orow["FieldName"].ToString();
                string sType = sConvertDataType(Convert.ToInt32(orow["FieldTipe"].ToString()));
                if (!sColumn.Contains("PKID"))
                {
                    if (sColumn.Contains("FKID"))
                    {
                        sColumn = sGetTableFromField(sColumn);
                        sType = "ToString";
                    }

                    sresoult += "oRow[" + iCounter.ToString() + "] = Convert." + sType + "(Dreader[" + '"' + sColumn + '"' + "]);";


                    iCounter++;
                }
            }

            return sresoult;
        }

        private string sConvertDataType(int iDBCode)
        {
            string sresoult = "";

            switch (iDBCode)
            {
                case 3:
                    {
                        sresoult = "ToInt32";
                        break;
                    }
                case 6:
                    {
                        sresoult = "ToDecimal";
                        break;
                    }
                case 7:
                    {
                        sresoult = "ToString";
                        break;
                    }
                case 130:
                    {
                        sresoult = "ToString";
                        break;
                    }
            }

            return sresoult;
        }

        private string sDBType(int iDBCode)
        {
            string sresoult = "";

            switch (iDBCode)
            {
                case 3:
                    {
                        sresoult = "OleDbType.Integer";
                        break;
                    }
                case 6:
                    {
                        sresoult = "OleDbType.Decimal";
                        break;
                    }
                case 7:
                    {
                        sresoult = "OleDbType.DBDate";
                        break;
                    }
                case 130:
                    {
                        sresoult = "OleDbType.VarChar";
                        break;
                    }
            }

            return sresoult;
        }

        private void WriteFile(string[] sLines)
        {
            if (!File.Exists("../nFile.txt"))
            {
                FileStream sWriter = File.Create("../nFile.txt");
                sWriter.Close();


                File.WriteAllLines("../nFile.txt", sLines);
            }
            else
            {

                File.WriteAllLines("../nFile.txt", sLines);
            }

            RTBDBPart.Lines = sLines;
        }

        private string sAddValues(DataTable dTable)
        {
            string sResoult = "";
            int iCounter = 0;

            foreach (DataRow dRow in dTable.Rows)
            {
                string sColumn = dRow["FieldName"].ToString();
                int iFieldType = Convert.ToInt32(dRow["FieldTipe"].ToString());
                if (!sColumn.Contains("PKID"))
                {
                    if (iCounter > 0)
                    {
                        switch (iFieldType)
                        {
                            case 3:
                                {
                                    sResoult += ", int i" + sColumn;
                                    break;
                                }
                            case 6:
                                {
                                    sResoult += ", decimal d" + sColumn;
                                    break;
                                }
                            case 7:
                                {
                                    sResoult += ", DateTime d" + sColumn;
                                    break;
                                }
                            case 130:
                                {
                                    sResoult += ", string s" + sColumn;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        switch (iFieldType)
                        {
                            case 3:
                                {
                                    sResoult += "int i" + sColumn;
                                    break;
                                }
                            case 6:
                                {
                                    sResoult += "decimal d" + sColumn;
                                    break;
                                }
                            case 7:
                                {
                                    sResoult += "DateTime d" + sColumn;
                                    break;
                                }
                            case 130:
                                {
                                    sResoult += "string s" + sColumn;
                                    break;
                                }
                        }
                    }

                    iCounter++;
                }

            }

            return sResoult;
        }

        private string sAddCommand(DataTable dTable, string sTableName)
        {
            string sResoult = "INSERT INTO " + sTableName + " (";
            string sEnd = "VALUES (";
            int iCounter = 0;

            foreach (DataRow dRow in dTable.Rows)
            {
                string sColumn = dRow["FieldName"].ToString();
                if (!sColumn.Contains("PKID"))
                {
                    if (iCounter > 0)
                    {
                        sResoult += ", " + sColumn;
                        sEnd += ", ?";
                    }
                    else
                    {
                        sResoult += sColumn;
                        sEnd += "?";
                    }


                    iCounter++;
                }

            }
            sEnd += ")";
            sResoult += ")" + sEnd;
            return sResoult;
        }

        private string sAddParameters(DataTable dTable, string sTableName)
        {
            string sResoult = "";
            string sCommand = "Sp" + sTableName + "Add";
            int iCounter = 0;

            foreach (DataRow dRow in dTable.Rows)
            {
                string sColumn = dRow["FieldName"].ToString();
                int iFieldType = Convert.ToInt32(dRow["FieldTipe"].ToString());
                if (!sColumn.Contains("PKID"))
                {
                    sResoult += sCommand + ".Parameters.Add(" + '"' + sColumn + '"' + ", " + sDBType(iFieldType) + ");";

                    iCounter++;
                }

            }

            return sResoult;
        }

        private string sAddParametersValues(DataTable dTable, string sTableName)
        {
            string sResoult = "";
            string sCommand = "Sp" + sTableName + "Add";
            int iCounter = 0;


            foreach (DataRow dRow in dTable.Rows)
            {
                string sColumn = dRow["FieldName"].ToString();
                int iFieldType = Convert.ToInt32(dRow["FieldTipe"].ToString());
                string sValueField = "";
                switch (iFieldType)
                {
                    case 3:
                        {
                            sValueField += "i" + sColumn;
                            break;
                        }
                    case 6:
                        {
                            sValueField = "d" + sColumn;
                            break;
                        }
                    case 7:
                        {
                            sValueField = "d" + sColumn;
                            break;
                        }
                    case 130:
                        {
                            sValueField = "s" + sColumn;
                            break;
                        }
                }
                if (!sColumn.Contains("PKID"))
                {
                    sResoult += sCommand + ".Parameters[" + iCounter.ToString() + "].Value = " + sValueField + ";";

                    iCounter++;
                }

            }

            return sResoult;
        }

        private string sEditValues(DataTable dTable)
        {
            string sResoult = "";
            int iCounter = 0;

            foreach (DataRow dRow in dTable.Rows)
            {
                string sColumn = dRow["FieldName"].ToString();
                int iFieldType = Convert.ToInt32(dRow["FieldTipe"].ToString());

                if (iCounter > 0)
                {
                    switch (iFieldType)
                    {
                        case 3:
                            {
                                sResoult += ", int i" + sColumn;
                                break;
                            }
                        case 6:
                            {
                                sResoult += ", decimal d" + sColumn;
                                break;
                            }
                        case 7:
                            {
                                sResoult += ", DateTime d" + sColumn;
                                break;
                            }
                        case 130:
                            {
                                sResoult += ", string s" + sColumn;
                                break;
                            }
                    }
                }
                else
                {
                    switch (iFieldType)
                    {
                        case 3:
                            {
                                sResoult += "int i" + sColumn;
                                break;
                            }
                        case 6:
                            {
                                sResoult += "decimal d" + sColumn;
                                break;
                            }
                        case 7:
                            {
                                sResoult += "DateTime d" + sColumn;
                                break;
                            }
                        case 130:
                            {
                                sResoult += "string s" + sColumn;
                                break;
                            }
                    }
                }

                iCounter++;
            }

            return sResoult;
        }

        private string sEditCommand(DataTable dTable, string sTableName)
        {
            string sResoult = "UPDATE " + sTableName + " SET ";
            int iCounter = 0;

            foreach (DataRow dRow in dTable.Rows)
            {
                string sColumn = dRow["FieldName"].ToString();
                if (!sColumn.Contains("PKID"))
                {
                    if (iCounter > 0)
                    {
                        sResoult += ", " + sColumn + " = ?";
                    }
                    else
                    {
                        sResoult += sColumn + " = ?";
                    }


                    iCounter++;
                }

            }
            sResoult += " WHERE (PKID_" + sTableName + " = ?)";
            return sResoult;
        }

        private string sEditParameters(DataTable dTable, string sTableName)
        {
            string sResoult = "";
            string sCommand = "Sp" + sTableName + "Edit";
            int iCounter = 0;
            string sLastParameter = sCommand + ".Parameters.Add(" + '"' + "PKID_" + sTableName + '"' + ", " + "OleDbType.Integer" + ");"; ;

            foreach (DataRow dRow in dTable.Rows)
            {
                string sColumn = dRow["FieldName"].ToString();
                int iFieldType = Convert.ToInt32(dRow["FieldTipe"].ToString());
                if (!sColumn.Contains("PKID"))
                {
                    sResoult += sCommand + ".Parameters.Add(" + '"' + sColumn + '"' + ", " + sDBType(iFieldType) + ");";

                    iCounter++;
                }

            }
            sResoult += sLastParameter;
            return sResoult;
        }

        private string sEditParametersValues(DataTable dTable, string sTableName)
        {
            string sResoult = "";
            string sCommand = "Sp" + sTableName + "Edit";
            int iCounter = 0;


            foreach (DataRow dRow in dTable.Rows)
            {
                string sColumn = dRow["FieldName"].ToString();
                int iFieldType = Convert.ToInt32(dRow["FieldTipe"].ToString());
                string sValueField = "";

                switch (iFieldType)
                {
                    case 3:
                        {
                            sValueField += "i" + sColumn;
                            break;
                        }
                    case 6:
                        {
                            sValueField = "d" + sColumn;
                            break;
                        }
                    case 7:
                        {
                            sValueField = "d" + sColumn;
                            break;
                        }
                    case 130:
                        {
                            sValueField = "s" + sColumn;
                            break;
                        }
                }
                if (!sColumn.Contains("PKID"))
                {
                    sResoult += sCommand + ".Parameters[" + iCounter.ToString() + "].Value = " + sValueField + ";";

                    iCounter++;
                }

            }
            string sLastValue = sCommand + ".Parameters[" + iCounter++.ToString() + "].Value = iPKID_" + sTableName + ";";
            sResoult += sLastValue;
            return sResoult;
        }

        #endregion

    }
}
