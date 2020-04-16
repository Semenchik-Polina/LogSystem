using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using LogSystem.BLL.Interfaces;
using LogSystem.BLL.Services;
using LogSystem.BLL.DTO.UserActionDTO;

namespace LogSystem.BLL.Utils
{
    public class UserActionReportHelper
    {
        private IUserActionService UserActionService { get; set; }
        private AMapper AMapper { get; set; }

        private readonly string dateReportNameFormat = "yyyyMMdd";

        public UserActionReportHelper()
        {
            UserActionService = new UserActionService();
            AMapper = new AMapper();
        }

        public string GetReportPath()
        {
            DateTime date = DateTime.Now.AddDays(-1);
            string dateStr = date.ToString(dateReportNameFormat);
            string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            string reportPath = $"{path}\\report_{dateStr}.csv";
            return reportPath;
        }

        public async Task GenerateReportFile()
        {
            // get date and report file path
            string reportPath = GetReportPath();

            // get and map data to report
            var rawData = await UserActionService.GetUserActionsByDate(DateTime.Now.AddDays(-1));
            var reportData = AMapper.Mapper.Map<IEnumerable<UserActionGetDetailDTO>, IEnumerable<UserActionReportDTO>>(rawData);

            using (var writer = new StreamWriter(reportPath))
            using (var csvWriter = new CsvWriter(writer, 
                System.Globalization.CultureInfo.CreateSpecificCulture("en-US")))
            {
                csvWriter.Configuration.HasHeaderRecord = true;
                csvWriter.Configuration.AutoMap<UserActionReportDTO>();

                csvWriter.WriteHeader<UserActionReportDTO>();
                csvWriter.NextRecord();
                csvWriter.WriteRecords(reportData);

                writer.Flush();
            }
        }

    }
}
