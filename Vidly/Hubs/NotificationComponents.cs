using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Hubs
{
    public class NotificationComponents
    {
        public void RegisterNotification(DateTime currentTime)
        {
            string conStr = ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;
            string sqlCommand = @"SELECT Name FROM [dbo].[Movers]";

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;

                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += SqlDep_OnChange;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                }
            }
        }

        public void SqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= SqlDep_OnChange;

                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.All.notify("added");

                RegisterNotification(DateTime.Now);
            }
        }

        public List<Mover> GetMovers(DateTime afterDate)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Movers.OrderByDescending(a => a.Id).ToList();
            }
        }
    }
}