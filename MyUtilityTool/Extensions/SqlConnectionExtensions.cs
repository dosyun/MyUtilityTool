using MyUtilityTool.Extention;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MyUtilityTool.Extention
{
    /// <summary>
    /// <see cref="T:System.Data.SqlClient.SqlConnection"/> の拡張クラスです。
    /// </summary>
    public static class SqlConnectionExtensions
    {
        /// <summary>
        /// データベースへ一括でデータを登録します。この処理を使用するとSQLの診断は行われません。
        /// </summary>
        /// <typeparam name="T">登録するデータの型。</typeparam>
        /// <param name="connection">使用する <see cref="T:System.Data.SqlClient.SqlConnection"/> のインスタンス。</param>
        /// <param name="values">登録するデータ。</param>
        /// <param name="transaction">使用する<see cref="T:System.Data.SqlClient.SqlTransaction"/> のインスタンス。</param>
        public static void BulkInsert<T>(this SqlConnection connection, IEnumerable<T> values, SqlTransaction transaction) where T : class
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            if (values == null)
                throw new ArgumentNullException(nameof(values));

            using (var dataTable = values.ToDataTable())
            using (var bulkCopy = (transaction != null)
                ? new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction)
                : new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = dataTable.TableName;
                foreach (var column in dataTable.Columns)
                    bulkCopy.ColumnMappings.Add(column.ToString(), column.ToString());

                var isClosed = (connection.State == ConnectionState.Closed);
                if (isClosed)
                    connection.Open();

                bulkCopy.WriteToServer(dataTable);

                if (isClosed)
                    connection.Close();
            }
        }

        /// <summary>
        /// データベースへ一括でデータを非同期で登録します。この処理を使用するとSQLの診断は行われません。
        /// </summary>
        /// <typeparam name="T">登録するデータの型。</typeparam>
        /// <param name="connection">使用する <see cref="T:System.Data.SqlClient.SqlConnection"/> のインスタンス。</param>
        /// <param name="values">登録するデータ。</param>
        /// <param name="transaction">使用する<see cref="T:System.Data.SqlClient.SqlTransaction"/> のインスタンス。</param>
        /// <returns>一括登録処理のタスク。</returns>
        public static async Task BulkInsertAsync<T>(this SqlConnection connection, IEnumerable<T> values, SqlTransaction transaction) where T : class
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            if (values == null)
                throw new ArgumentNullException(nameof(values));

            using (var dataTable = values.ToDataTable())
            using (var bulkCopy = (transaction != null)
                ? new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction)
                : new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = dataTable.TableName;
                foreach (var column in dataTable.Columns)
                    bulkCopy.ColumnMappings.Add(column.ToString(), column.ToString());

                var isClosed = (connection.State == ConnectionState.Closed);
                if (isClosed)
                    await connection.OpenAsync();

                await bulkCopy.WriteToServerAsync(dataTable);

                if (isClosed)
                    connection.Close();
            }
        }
    }
}