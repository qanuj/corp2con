using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using log4net;

namespace e10.Shared.Data
{
    public class BasicCommandInterceptor : IDbCommandInterceptor
    {
        private static readonly ILog Logger = LogManager.GetLogger("Database");

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogIfNonAsync(command, interceptionContext);
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogIfNonAsync(command, interceptionContext);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogIfNonAsync(command, interceptionContext);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }

        private void LogIfNonAsync<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            var msgs = new StringBuilder();
            try
            {
                for (var i = 0; i < command.Parameters.Count; i++)
                {
                    var cmd = (SqlParameter)command.Parameters[i];
                    var val = cmd.SqlValue;
                    if (cmd.SqlValue!=null)
                    {
                        if (cmd.SqlDbType == SqlDbType.NVarChar) val = string.Format("'{0}'", cmd.SqlValue);
                    }
                    if (string.IsNullOrWhiteSpace(val.ToString())) val = null;
                    msgs.AppendFormat("\r\ndeclare @{0} as {1}{3}; set @{0}={2};", 
                        cmd.ParameterName, cmd.SqlDbType.ToString().ToLower(), val ,
                        cmd.SqlDbType == SqlDbType.NVarChar ? string.Format("({0})", cmd.Size) : "");
                }
                var z = 0;
                var stacks = new StackTrace().GetFrames();
                if (stacks != null)
                {
                    var rows = from x in stacks
                        let mt = x.GetMethod()
                        let ty = mt?.DeclaringType
                        let nm = ty == null ? "" : ty.Namespace
                        where nm.StartsWith("Assetry")
                        select x;
                    foreach (var stack in rows)
                    {
                        z++;
                        if (z > 2)
                        {
                            msgs.AppendFormat("\r\n\tat {0}.{1} in {2}:{3}", stack.GetMethod().ReflectedType,
                                stack.GetMethod().Name, stack.GetFileName(), stack.GetFileLineNumber());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msgs.AppendFormat("OOH! {0} \r\n{1}", ex.Message, ex.StackTrace);
            }
            Logger.Info(string.Format("{0}\r\n{1}", msgs, command.CommandText));
        }

        private void LogIfError<TResult>(
            DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            if (interceptionContext.Exception != null)
            {
                Logger.Error(string.Format("Command {0} failed with exception {1}", command.CommandText, interceptionContext.Exception));
            }
        }
    }
}