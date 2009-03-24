using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using System.Xml;

/// <summary>
/// Summary description for App_Lib
/// </summary>
namespace EPaySoap
{
    public class App_Lib
    {
        private OracleConnection oraCnn;
        private OracleCommand oraCmd;
        private DataSet Dst;
        private DataTable Tbl;
        private DataView Dv;

        private string strSql;

        public App_Lib()
        {
            oraCnn = new OracleConnection();
            oraCmd = new OracleCommand();
            Dst = new DataSet();
            Tbl = new DataTable();
            Dv = new DataView();

            strSql = string.Empty;                
        }

        #region public string GetConnectionString()

        public string GetConnectionString()
        {
            return (System.Configuration.ConfigurationManager.AppSettings["OraConnectString"].ToString());
        }

        #endregion

        #region private void OpenCnn()
        //Connect 2 Database
        //ChungNN - 8/2006

        private void OpenCnn()
        {
            this.oraCnn = new OracleConnection(GetConnectionString());

            if (this.oraCnn.State != ConnectionState.Open)
                try
                {
                    this.oraCnn.Open();
                }
                catch (Exception ex)
                {
                    throw (ex);
                    //OccurError(ex, "Lỗi kết nối CSDL!");                             
                }

        }

        #endregion

        #region private void CloseCnn()
        //Close connection
        //ChungNN - 8/2006

        private void CloseCnn()
        {
            if (oraCnn.State == ConnectionState.Open)
                try
                {
                    this.oraCnn.Close();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
        }

        #endregion

        #region public OracleCommand CreateDbCommand ( ref OracleConnection oraCnn, string strSql, IDbDataParameter[] paramArr, CommandType strCmdType )
        //Create sql parameter list
        //ChungNN - 8/2006        
        public OracleCommand CreateDbCommand(ref OracleConnection oraCnn, string strSql, IDbDataParameter[] paramArr, CommandType strCmdType)
        {
            try
            {

                oraCmd = new OracleCommand(strSql, oraCnn);
                oraCmd.CommandType = strCmdType;

                if (paramArr != null)
                {
                    if (paramArr.Length > 0)
                    {
                        oraCmd.Parameters.AddRange(paramArr);
                        //foreach (SqlParameter param in paramArr)
                        //{
                        //    oraCmd.Parameters.Add(param);
                        //}
                    }
                }


                return this.oraCmd;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region void ExecuteQuery ( string strSql, IDbDataParameter[] paramArr, CommandType strCmdType )

        //------ Thuc hien 1 cau lenh ( store ) sql ----------
        //Exp:  ExecuteQuery("store1", arrPar, CommandType.StoredProcedure);
        //      ExecuteQuery("Select * from table1 ", null, CommandType.Text);

        public void ExecuteQuery(string strSql, IDbDataParameter[] paramArr, CommandType strCmdType)
        {
            try
            {
                //Tạo đối tượng dbCommand với câu lệnh SQL truyền vào và thiết lập tham số
                OpenCnn();

                oraCmd = new OracleCommand();
                oraCmd.Connection = oraCnn;
                oraCmd = CreateDbCommand(ref oraCnn, strSql, paramArr, strCmdType);
                oraCmd.CommandTimeout = 1500;
                oraCmd.ExecuteNonQuery();
                oraCmd.Dispose();

                CloseCnn();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region public DataSet ExecuteDataset ( string strSql, IDbDataParameter[] paramArr, CommandType strCmdType )

        public DataSet ExecuteDataset(string strSql, IDbDataParameter[] paramArr, CommandType strCmdType)
        {
            try
            {
                OpenCnn();
                Dst = new DataSet();

                OracleCommand oraCmd = CreateDbCommand(ref oraCnn, strSql, paramArr, strCmdType);
                oraCmd.Connection = oraCnn;
                oraCmd.CommandTimeout = 1500;

                OracleDataAdapter Adr = new OracleDataAdapter();
                Adr.SelectCommand = oraCmd;
                Adr.Fill(Dst);

                oraCmd.Dispose();
                CloseCnn();

                return Dst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region public DataView ExecuteDataview ( string strSql, IDbDataParameter[] paramArr, CommandType strCmdType, string SortExpression )

        public DataView ExecuteDataview(string strSql, IDbDataParameter[] paramArr, CommandType strCmdType, string SortExpression)
        {
            try
            {
                OpenCnn();
                Dst = new DataSet();

                //Tạo đối tượng dbCommand với câu lệnh SQL truyền vào và thiết lập tham số              
                OracleCommand oraCmd = CreateDbCommand(ref oraCnn, strSql, paramArr, strCmdType);
                oraCmd.Connection = oraCnn;
                oraCmd.CommandTimeout = 1500;
                OracleDataAdapter Adr = new OracleDataAdapter();
                Adr.SelectCommand = oraCmd;
                Adr.Fill(Dst);
                Dv = new DataView(Dst.Tables[0]);
                Dv.Sort = SortExpression;

                oraCmd.Dispose();
                CloseCnn();

                return Dv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region public DataView ExecuteDataview ( string strSql, IDbDataParameter[] paramArr, CommandType strCmdType )

        public DataView ExecuteDataview(string strSql, IDbDataParameter[] paramArr, CommandType strCmdType)
        {
            try
            {
                OpenCnn();
                Dst = new DataSet();

                //Tạo đối tượng dbCommand với câu lệnh SQL truyền vào và thiết lập tham số              
                OracleCommand oraCmd = CreateDbCommand(ref oraCnn, strSql, paramArr, strCmdType);
                oraCmd.Connection = oraCnn;
                oraCmd.CommandTimeout = 1500;
                OracleDataAdapter Adr = new OracleDataAdapter();
                Adr.SelectCommand = oraCmd;
                Adr.Fill(Dst);
                Dv = new DataView(Dst.Tables[0]);

                oraCmd.Dispose();
                CloseCnn();

                return Dv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DataSet ExecuteDataset ( strSql, strCmdType )

        public DataSet ExecuteDataset(string strSql, CommandType strCmdType)
        {
            return ExecuteDataset(strSql, null, strCmdType);
        }

        #endregion

        #region DataView ExecuteDataview(string strSql, CommandType strCmdType, string SortExpression)

        public DataView ExecuteDataview(string strSql, CommandType strCmdType, string SortExpression)
        {
            return ExecuteDataview(strSql, null, strCmdType, SortExpression);
        }

        #endregion

        #region public void Xml_DeleteNode(string FilePath, string RootName, string NodeName)
        //Delete a node 
        public void Xml_DeleteNode(string FilePath, string RootName, string NodeName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(FilePath);
            XmlNode root = doc.SelectSingleNode(RootName);
            XmlNode nod = root.SelectSingleNode(NodeName);
            root.RemoveAll();
            doc.Save(FilePath);
        }
        #endregion 

        #region public void AddNodeToXMLFile(string XmlFilePath, string NodeNameToAddTo, string NodeName, string[] arrElement, string[] arrInnerText)
        public void AddNodeToXMLFile(string XmlFilePath, string NodeNameToAddTo, string NodeName, string[] arrElement, string[] arrInnerText)
        {
            //create new instance of XmlDocument
            XmlDocument doc = new XmlDocument();
            //load from file
            doc.Load(XmlFilePath);
            //create main node
            XmlNode node = doc.CreateNode(XmlNodeType.Element, NodeName, null);

            for (int i = 0; i < arrElement.Length; i++)
            {
                //create the nodes first child
                XmlNode nodeTem = doc.CreateElement(arrElement[i]);
                nodeTem.InnerText = arrInnerText[i];
                node.AppendChild(nodeTem);
            }

            // find the node we want to add the new node to
            XmlNodeList xmlNodeList = doc.GetElementsByTagName(NodeNameToAddTo);
            // append the new node
            xmlNodeList[0].AppendChild(node);
            // save the file
            doc.Save(XmlFilePath);
        }
        #endregion

        #region public void RemoveNodeInXMLFile(string XmlFilePath, string NodeNameToAddTo, string NodeName, string ChildName)
        public void RemoveNodeInXMLFile(string XmlFilePath, string NodeNameToRemove, string NodeName, string ChildName)
        {
            //create new instance of XmlDocument
            XmlDocument doc = new XmlDocument();
            //load from file
            doc.Load(XmlFilePath);           
         
            foreach (XmlNode cn in doc.ChildNodes)
            {               
                //remove the nodes   
                if (cn.Name == NodeName)
                {                    
                    //cn.RemoveAll();
                    //break;
                }                                 
            }          
           
            // save the file
            doc.Save(XmlFilePath);
        }
        #endregion

        #region public string SearchInXMLFile(string XmlFilePath, string RootNode, string NodeName, string NodeValue)
        public string SearchInXMLFile(string XmlFilePath, string RootNode, string NodeName, string NodeValue)
        {
            string strRetValue = string.Empty;
            //create new instance of XmlDocument
            XmlDocument doc = new XmlDocument();
            //load from file
            doc.Load(XmlFilePath);
         
            // find the node we want to add the new node to
            XmlNodeList xmlNodeList = doc.GetElementsByTagName(NodeName);
            for(int i=0; i<xmlNodeList.Count; i++)
            {                
                XmlNode node1;
                node1 = xmlNodeList.Item(i);
                for (int j = 0; j < node1.ChildNodes.Count; j++)
                {
                    XmlNode nodechild1;
                    nodechild1 = node1.ChildNodes[j];
                    if (nodechild1.Name == NodeName && nodechild1.Value == NodeValue)
                    {
                        strRetValue = node1.ChildNodes[j + 1].Value.ToString();
                        break;
                    }
                }              
            }

            return strRetValue;
        }
        #endregion



    }
}
