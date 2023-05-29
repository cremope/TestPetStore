namespace TestPetStore.Utils
{
    public class Base
    {
        private bool REPORT;
        private Report REPORTPORTAL = new();

        public Base()
        {
            REPORT = bool.Parse(Startup.APPSETTINGS.GetSection("Report:ReportPortal").Value);
        }

        public void Mensagem(string msg)
        {
            if (REPORT)
            {
                REPORTPORTAL.Mensagem(msg);
            }
            else
            {
                Console.WriteLine(msg);
            }
        }
    }
}
