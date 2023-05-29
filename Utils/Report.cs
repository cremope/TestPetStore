using ReportPortal.Shared;

namespace TestPetStore.Utils
{
    public class Report
    {
        public Report() 
        { 
        }

        public void Mensagem(string msg)
        {
            Context.Current.Log.Info(msg);
        }
    }
}
