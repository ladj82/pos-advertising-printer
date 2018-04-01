using System;
using System.Collections.Generic;
using PrintPDV.Models;
using PrintPDV.Utility;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers.Base
{
    public abstract class BasePrinterController
    {
        public abstract IPrinter PrinterConfig { get; protected set; }

        protected abstract List<Command> Commands { get; set; }

        protected BasePrinterController(IPrinter printer)
        {
            Commands = new List<Command>();
            PrinterConfig = printer;
        }

        public virtual void ExecuteCommands()
        {
            if (Commands != null)
            {
                foreach (var command in Commands)
                {
                    var result = command.Function.Invoke();

                    LogUtility.Log(LogUtility.LogType.Information,
                        string.Format("{0} - Return: {1}", command.Name, result));
                }

                Commands = new List<Command>();
            }
        }
    }
}
