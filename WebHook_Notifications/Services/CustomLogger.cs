using WebHook_Notifications.Models;

namespace WebHook_Notifications.Services
{
    public interface IMyLogger
    {
        Task SaveData(PayerDB payerInfo, NotificationDB notificationInfo, TransactionDB transactionInfo);
    }
    public class CustomLogger : IMyLogger
    {
        private readonly string filePath = "C:\\Users\\Usuario\\source\\repos\\Trabajo\\VisualContact_JuanCorrea_PruebaTecnica\\WebHook_Notifications\\Notifications.log";
        public async Task SaveData(PayerDB payerInfo, NotificationDB notificationInfo, TransactionDB transactionInfo)
        {
            try
            {
                //Si el archivo no existe entonces lo creamos
                if (!File.Exists(filePath))
                {
                    var archivo = File.Create(filePath);
                    archivo.Close();
                }
                // Abre  archivo .log y escribe la información
                using (StreamWriter logWriter = File.AppendText(filePath))
                {
                    await logWriter.WriteLineAsync($"● ( Id_Transacción: {notificationInfo.TransactionId} ) => Hora_Notificación: {notificationInfo.DateTime} " +
                        $"=> Estado: {transactionInfo.Status} => Valor: {transactionInfo.Value} => Mensaje: {transactionInfo.Message} " +
                        $"=> Banco: {transactionInfo.IssuerName} => Método_De_Pago: {transactionInfo.PaymentMethod} => " +
                        $"Hora_Transacción: {transactionInfo.DateTime} => Nombre_Cliente: {payerInfo.Name} => Apellido: {payerInfo.Surname} " +
                        $"=> Documento: {payerInfo.Document} => Email: {payerInfo.Email} => Mobile: {payerInfo.Mobile}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir en el archivo .log: {ex.Message}");
            }

        }
    }
}
