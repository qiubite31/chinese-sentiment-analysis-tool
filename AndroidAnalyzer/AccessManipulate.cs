using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace AndroidAnalyzer
{
    public class AccessManipulate
    {
        private String AccessPath;
        private OleDbConnection conn;
        private List<String> TableList;

        public AccessManipulate(String Path)
        {
            SetAccessPath(Path);
        }
        private void SetAccessPath(String Path)
        {
            AccessPath = Path;
        }
        public void ConnAccess()
        {
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AccessPath);
            conn.Open();

            SetTableList();
        }
        public void CreateTable(String queryString)
        {
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = queryString;
            
            cmd.ExecuteNonQuery();
        }
        public void DropTable(String TableName)
        {
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DROP TABLE " + TableName;
            cmd.ExecuteNonQuery();
        }
        public DataSet SelectQuery(String queryString, String TableName)
        {
            //將資料存進DataSet-----------------------------
            OleDbDataAdapter DataAdapter = new OleDbDataAdapter(queryString, conn);
            DataSet AllData = new DataSet();
            DataAdapter.Fill(AllData, TableName);
            
            return AllData;
        }
        public void UpdateQuery(String queryString)
        {
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = queryString;
            cmd.ExecuteNonQuery();
        }
        private void SetTableList()
        {
            DataTable schemaInformation = conn.GetSchema("Tables");
            TableList = new List<string>();

            foreach (DataRow row in schemaInformation.Rows)
            {
                TableList.Add(row.ItemArray[2].ToString());
            }
        }
        public Boolean CheckTableExist(String tableName)
        {
            if (TableList.Contains(tableName))
                return true;
            else
                return false;
        }
        public Boolean CheckColumnExist(String TableName, String ColumnName)
        {
            String[] restrictions = new string[4] { null, null, TableName, null };
            DataTable schemaInformation = conn.GetSchema("Columns", restrictions);

            foreach (DataRow row in schemaInformation.Rows)
            {
                if (row.ItemArray[3].ToString() == ColumnName)
                    return true;
            }
            return false;
        }
    }
}
