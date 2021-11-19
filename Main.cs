using System;
using SetupSenderFile.Modules;
using System.Windows.Forms;

namespace SetupSenderFile
{
    public partial class Main : Form
    {
        private Config cfg = new Config();
        private IniFile INI = new IniFile("config.ini");

        public Main()
        {
            InitializeComponent();
        }

        public void Main_Load(object sender, EventArgs e)
        {
            this.Text = $"{ cfg.ProgrammName } v.{ cfg.Version }";
            this.LoadSetup();
            txtReceivers.Text = cfg.Receivers;
            txtFilePath.Text = cfg.Path_File;
            txtSMTPhost.Text = cfg.SMTP_HOST;
            txtSMTPport.Text = cfg.SMTP_PORT.ToString();
            txtSMTPuser.Text = cfg.SMTP_USER;
            txtSMTPpassword.Text = cfg.SMTP_PASSWORD;
            chkSSL.Checked = cfg.SMTP_SSL;
            txtTimeout.Text = cfg.SMTP_TIMEOUT.ToString();

            this.btnOk.Enabled = false;

        }

        private void LoadSetup()
        {
            if (INI.KeyExists("emails", "receivers")) cfg.Receivers = INI.ReadINI("receivers", "emails");
            if (INI.KeyExists("path", "receivers")) cfg.Path_File = INI.ReadINI("receivers", "path");
            if (INI.KeyExists("host", "smtp")) cfg.SMTP_HOST = INI.ReadINI("smtp", "host");
            if (INI.KeyExists("port", "smtp")) cfg.SMTP_PORT = int.Parse(INI.ReadINI("smtp", "port"));
            if (INI.KeyExists("user", "smtp")) cfg.SMTP_USER = INI.ReadINI("smtp", "user");
            if (INI.KeyExists("password", "smtp")) cfg.SMTP_PASSWORD = Security.DeCrypt(INI.ReadINI("smtp", "password"), "test8");
            if (INI.KeyExists("ssl", "smtp")) cfg.SMTP_SSL = Boolean.Parse(INI.ReadINI("smtp", "ssl"));
            if (INI.KeyExists("timeout", "smtp")) cfg.SMTP_TIMEOUT = int.Parse(INI.ReadINI("smtp", "timeout"));
        }

        private void SaveSetup()
        {
            INI.Write("receivers", "emails", cfg.Receivers);
            INI.Write("receivers", "path", cfg.Path_File);
            INI.Write("smtp", "host", cfg.SMTP_HOST);
            INI.Write("smtp", "port", cfg.SMTP_PORT.ToString());
            INI.Write("smtp", "user", cfg.SMTP_USER);
            INI.Write("smtp", "password", Security.Crypt(cfg.SMTP_PASSWORD, "test8"));
            INI.Write("smtp", "ssl", cfg.SMTP_SSL.ToString());
            INI.Write("smtp", "timeout", cfg.SMTP_TIMEOUT.ToString());
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            cfg.Receivers = txtReceivers.Text.Trim();
            cfg.Path_File = txtFilePath.Text.Trim();
            cfg.SMTP_HOST = txtSMTPhost.Text.Trim();
            cfg.SMTP_PORT = int.Parse(txtSMTPport.Text.Trim());
            cfg.SMTP_USER = txtSMTPuser.Text.Trim();
            cfg.SMTP_PASSWORD = txtSMTPpassword.Text;
            cfg.SMTP_SSL = chkSSL.Checked;
            cfg.SMTP_TIMEOUT = int.Parse(txtTimeout.Text.Trim());

            this.SaveSetup();
            btnOk.Enabled = false;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = true;
        }

        private void chkSSL_CheckedChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = true;
        }

        private void btnSendTest_Click(object sender, EventArgs e)
        {
            try
            {
                SendMail.sendOne(cfg.SMTP_USER, "robot", cfg.SMTP_USER, "Test message", "Тестовое сообщение :)", cfg.SMTP_HOST, cfg.SMTP_PORT, cfg.SMTP_USER, cfg.SMTP_PASSWORD, "", cfg.SMTP_SSL);
                MessageBox.Show("Сообщение отправлено!", "Отправлено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
