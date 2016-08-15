using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            GetBinaryDataToFile(@"Data Source = 10.235.144.219; Initial Catalog = WIND; User ID = AppProd@2016#UATWeb; Password = Paa9BfHDm5pNB6d8SB2t69XTP8npbq5k", @"C:\TFS\General Electric Company\Latam\WiND\WiNDConsoleApplicationImporter\bin\Debug\ExcelSampleFiles\Recebimento_20160809- 2.xls");
        }

        public static void GetBinaryDataToFile(string connectionString, string path)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Bytes] FROM [File] WHERE FileId = 40852";
                    command.CommandType = CommandType.Text;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            SqlBinary sqlBinary = dataReader.GetSqlBinary(0);
                            File.WriteAllBytes(path, sqlBinary.Value);
                        }

                        dataReader.Close();
                    }
                }

                connection.Close();
            }
        }
    }
}
