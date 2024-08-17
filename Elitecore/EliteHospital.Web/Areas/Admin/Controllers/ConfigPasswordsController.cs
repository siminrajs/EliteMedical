using EliteHospital.Core;
using EliteHospital.Data.Repository;
using EliteHospital.Web.Areas.Admin.ViewModel;
using EliteHospital.Web.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace EliteHospital.Web.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class ConfigPasswordsController : Controller
    {
        // GET: Admin/ConfigPasswords
        CMSConfigPasswordsRepository repository = new CMSConfigPasswordsRepository();
        public ActionResult Index()
        {
            List<ConfigPasswords > configpass = repository.GetAll();
            ViewData["ConfigtList"] = configpass;
            return View();
        }
        public ActionResult EditConfigpassvalues(int Id = 0)
        {
            ConfigPasswordsViewModel  viewModel = new ConfigPasswordsViewModel();
            if (Id > 0)
            {
                ConfigPasswords  configpass = repository.GetById(Id);
                if (configpass != null)
                {
                    viewModel = new ConfigPasswordsViewModel ()
                    {
                        ID = configpass.ID,
                        ConfigPasswordName  = configpass.ConfigPasswordName,
                        ConfigPassValues = configpass.ConfigPassValues
                    };
                }
            }
            return View(viewModel);
        }
        public void LogExceptionToFile(Exception ex)
        {
            string logFilePath = @"C:\inetpub\wwwroot\EliteHospital\errorlog.txt"; // Path to your log file

            // Create the log directory if it does not exist
            string logDirectory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // Append the exception details to the log file
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine("Date: " + DateTime.Now.ToString());
                writer.WriteLine("Exception Message: " + ex.Message);
                writer.WriteLine("Stack Trace: " + ex.StackTrace);
                writer.WriteLine("-------------------------------------------------");
            }
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfigpassvalues(ConfigPasswordsViewModel  model)
        {
            if (ModelState.IsValid)
            {
                ConfigPasswords configpass = new ConfigPasswords()
                {
                    ID = model.ID,
                    ConfigPasswordName = model.ConfigPasswordName,
                    ConfigPassValues = model.ConfigPassValues
                };
                if (model.ID > 0)
                {
                    repository.Update(configpass);
                }
                else
                {
                    repository.Save(configpass);
                }
                try
                {
                    string configFilePath = Path.Combine(Server.MapPath("~"), "Web.config");
                    ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                    configFileMap.ExeConfigFilename = configFilePath; // Replace with your Web.config path
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                    // Update the appSettings section
                    config.AppSettings.Settings[model.ConfigPasswordName].Value = model.ConfigPassValues;

                    // Save the changes
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
                catch (Exception ex)
                {
                    LogExceptionToFile(ex);
                }
                try
                {
                   
                    string appSettingsPath = @"C:\inetpub\wwwroot\EliteHospitalAPI\appsettings.json";
                    string json = System.IO.File.ReadAllText(appSettingsPath);
                    JObject jsonObj = JsonConvert.DeserializeObject<JObject>(json);
                    // Update or add the value
                    jsonObj[model.ConfigPasswordName] = model.ConfigPassValues;

                    // Write updated JSON back to file
                    System.IO.File.WriteAllText(appSettingsPath, JsonConvert.SerializeObject(jsonObj, Formatting.Indented));
                }
                catch (Exception ex)
                {
                    LogExceptionToFile(ex);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}