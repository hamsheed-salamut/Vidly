using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Vidly.Hubs;

namespace Vidly.Models
{
    public static class NotificationService
    {
        static readonly string conStr = ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;

        internal static SqlCommand command = null;
        internal static SqlDependency dependency = null;

        public static string GetNotification()
        {
            try
            {
                var movers = new List<Mover>();

                using (var connection = new SqlConnection(conStr))
                {
                    connection.Open();

                    using (command = new SqlCommand(@"SELECT [Id], [Name] FROM [dbo].[Movers]", connection))
                    {
                        command.Notification = null;

                        if (dependency == null)
                        {
                            dependency = new SqlDependency(command);
                            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                        }

                        if (connection.State == System.Data.ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            movers.Add(item: new Mover
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"] != DBNull.Value ? (string)reader["Name"] : ""
                            });
                        }
                    }
                }

                var jsonSerializer = new JavaScriptSerializer();

                var json = jsonSerializer.Serialize(movers);

                return json;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (dependency != null)
            {
                dependency.OnChange -= dependency_OnChange;
                dependency = null;

            }

            if (e.Type == SqlNotificationType.Change)
            {
                NotificationHub.Send();
            }
        }
    }
}