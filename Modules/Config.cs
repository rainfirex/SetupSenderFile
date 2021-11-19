using System;

namespace SetupSenderFile.Modules
{
    class Config
    {
        public string ProgrammName { get; } = "SendFile setup";

        public string Version { get; } = "1.3";

        public string Programmist { get; } = "Poplavskiy Aleksandr";

        public string Receivers { get; set; } = "";

        public string Path_File { get; set; } = @"C:\";

        public string USER { get; set; } = String.Format(@"{0}\\{1}", Environment.UserDomainName, Environment.UserName);

        public string SMTP_HOST { get; set; } = "172.26.79.7";

        public int SMTP_PORT { get; set; } = 25;

        public bool SMTP_SSL { get; set; } = false;

        public string SMTP_USER { get; set; } = "robot@sakh.dvec.ru";

        public string SMTP_PASSWORD { get; set; } = "~msNg3L5zrWgh@7g";

        public int SMTP_TIMEOUT { get; set; } = 40;
    }
}
