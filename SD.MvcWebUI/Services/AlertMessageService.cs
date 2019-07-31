using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SD.MvcWebUI.Models;

namespace SD.MvcWebUI.Services
{
    public class AlertMessageService
    {
        private readonly ITempDataDictionary _tempDataDictionary;

        public AlertMessageService(ITempDataDictionaryFactory tempDataDictionaryFactory, IHttpContextAccessor httpContextAccessor)
        {
            _tempDataDictionary = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);
        }

        public void AlertSuccess(string title, string message, bool dismissible = true)
        {
            AddAlert(title, AlertType.Success, message, dismissible);
        }

        public void AlertError(string title, string message, bool dismissible = true)
        {
            AddAlert(title, AlertType.Error, message, dismissible);
        }

        public void AlertWarning(string title, string message, bool dismissible = true)
        {
            AddAlert(title, AlertType.Warning, message, dismissible);
        }

        public void AlertInformation(string title, string message, bool dismissible = true)
        {
            AddAlert(title, AlertType.Information, message, dismissible);
        }

        private void AddAlert(string title, string alertType, string message, bool dismissible)
        {
            var alerts = _tempDataDictionary.ContainsKey(AlertMessage.TempDataKey)
                ? (List<AlertMessage>)_tempDataDictionary[AlertMessage.TempDataKey]
                : new List<AlertMessage>();

            alerts.Add(new AlertMessage
            {
                AlertType = alertType,
                Title = title,
                Message = message,
                Dismissible = dismissible
            });

            _tempDataDictionary[AlertMessage.TempDataKey] = alerts;
        }
    }
}
