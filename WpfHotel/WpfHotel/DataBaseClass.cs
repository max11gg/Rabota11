using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfHotel
{
    /// <summary>
    /// Класс для взаимодействия с базой данных.
    /// </summary>
    public class DataBaseClass
    {
        // Переменные для хранения значений и настроек подключения.
        public static string Users_ID = "null", Password = "null", App_Name = "Администратор";
        public static string ConnectionStrig = "Data Source = DESKTOP-EDQFE2B\\SQL; Initial Catalog = Hotel; Persist Security Info = true; User ID = sa; Password = 123;";

        // Объекты для работы с базой данных.
        public SqlConnection connection = new SqlConnection(ConnectionStrig);
        private SqlCommand command = new SqlCommand();
        public DataTable resultTable = new DataTable();
        public SqlDependency dependency = new SqlDependency();

        // Перечисление для указания типа операции в методе sqlExecute.
        public enum act { select, manipulation };

        /// <summary>
        /// Метод для выполнения SQL-запроса.
        /// </summary>
        /// <param name="query">SQL-запрос.</param>
        /// <param name="action">Тип операции (select/manipulation).</param>
        public void sqlExecute(string query, act action)
        {
            command.Connection = connection;
            command.CommandText = query;
            command.Notification = null;

            // В зависимости от типа операции выполняем соответствующие действия.
            switch (action)
            {
                case act.select:
                    dependency.AddCommandDependency(command);
                    SqlDependency.Start(connection.ConnectionString);
                    connection.Open();
                    resultTable.Load(command.ExecuteReader());
                    connection.Close();
                    break;
                case act.manipulation:
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    break;
            }
        }
    }
}
