using System.Net.Mail;


namespace KFC.UseCases.Utility;

public class EmailHelper
{
    public string To { get; set; }
    public string Cc { get; set; }
    public string Subject { get; set; }
    public string TemplateEmail { get; set; }
    public Dictionary<string, string>? Vars { get; set; }

    private string serverEmail { get; set; }
    private string serverHost { get; set; }
    private string serverPassword { get; set; }
    private int serverPort { get; set; }
    private bool serverSSL { get; set; }

    
    
    private string baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateEmail");

    public EmailHelper(string to, string subject, string templateMail, Dictionary<string, string>? vars)
    { // TO DO cambiar config
        To = to;
        Subject = subject;
        TemplateEmail = templateMail;
        Vars = vars;
        serverEmail = "soporte.ti@consultKFCa.pe";
        serverPassword = "9JmQ8nYPPZn4";
        serverHost = "smtp.zoho.com";
        serverPort = 587;
        serverSSL = true;
    }

    private string LoadHtml()
    {
        using (StreamReader sr = new StreamReader(Path.Combine(baseDirectory, this.TemplateEmail)))
            return sr.ReadToEnd();
    }

    private string BuildHtml() {

        string html = LoadHtml();

        if (Vars is not null) {
            foreach (string key in Vars.Keys)
            {
                html = html.Replace(string.Format("{{{{{0}}}}}", key), Vars[key]);
            }
        }

        return html;
    }

    public Task Send()
    {

        MailMessage msg = new MailMessage();
        msg.To.Add(this.To);
        msg.From = new MailAddress(serverEmail);
        msg.Subject = Subject;
        msg.SubjectEncoding = System.Text.Encoding.UTF8;
        msg.Body = BuildHtml(); 
        msg.IsBodyHtml = true;

        SmtpClient client = new SmtpClient();
        client.Credentials = new System.Net.NetworkCredential(serverEmail, serverPassword);

        client.Host = serverHost;
        client.Port = serverPort;
        client.EnableSsl = serverSSL;
        client.Send(msg);
        client.Dispose();

        return Task.CompletedTask;

    }

}
