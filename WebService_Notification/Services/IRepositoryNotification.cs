using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebService_Notification.Models.DTOs;

namespace WebService_Notification.Services
{
    public interface IRepositoryNotification
    {
        Task<NotificationDTO> GetNotification(int RequestId);
        Task<bool> IdAlreadyExists(int RequestId);
        Task<int> InserData(NotificationDTO data);
    }

    public class RepositoryNotification : IRepositoryNotification
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepositoryNotification(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        /// <summary>
        /// Insertar datos en la base de datos
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Devuelve 1 si la operación sale exitosa, devuelve 0 si falla</returns>
        public async Task<int> InserData(NotificationDTO data)
        {
            //VAMOS A EMPEZAR UNA TRANSACCIÓN PARA QUE SI EN ALGÚN MOMENTO UNA DE LAS INSERCIONES FALLA
            //SE PUEDA REVERTIR LA OPERACIÓN Y ASÍ NO HAYA DATA INCONSISTENTE O INCOMPLETA
            using var transaction = context.Database.BeginTransaction();
            try
            {
                // TODAS LAS OPERACIONES LAS VAMOS A HACER UTILIZANDO LOS PROCEDIMIENTOS ALMACENADOS CREADOS EN LA BASE DE DATOS
                
                //Insertar Notification
                var notificationParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@RequestId",data.RequestId),
                    new SqlParameter("@Subscription",data.Subscription)
                };
                //Llamamos el procemiento almacenado para insertar Notificación
                await context.Database.ExecuteSqlRawAsync("EXEC InsertNotification @RequestId,@Subscription", notificationParameters);

                //Insertar Status
                var statusParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@status",data.Status.status),
                    new SqlParameter("@Reason",data.Status.Reason),
                    new SqlParameter("@Message",data.Status.Message),
                    new SqlParameter("@Date",data.Status.Date),
                    new SqlParameter("@IdNotification",data.RequestId)
                };
                //Llamamos el procemiento almacenado para insertar Status
                await context.Database.ExecuteSqlRawAsync("EXEC InsertStatus @status,@Reason,@Message,@Date,@IdNotification", statusParameters);

                //Vemos si el payer que nos mandaron existe o no en la base de datos, si no existe entonces lo agregamos
                var payerAlreadyExists = await context.Payer.AnyAsync(x => x.Document == data.Request.Payer.Document);
                if (!payerAlreadyExists)
                {
                    // Insertar Payer
                    var payerParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@Document",data.Request.Payer.Document),
                        new SqlParameter("@DocumentType",data.Request.Payer.DocumentType),
                        new SqlParameter("@Name",data.Request.Payer.Name),
                        new SqlParameter("@SurName",data.Request.Payer.Surname),
                        new SqlParameter("@Email",data.Request.Payer.Email),
                        new SqlParameter("@Mobile",data.Request.Payer.Mobile)
                    };
                    //Llamamos el procemiento almacenado para insertar Payer
                    await context.Database.ExecuteSqlRawAsync("EXEC InsertPayer @Document,@DocumentType,@Name,@SurName,@Email,@Mobile", payerParameters);
                }


                //Insertar Request
                var requestParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Locale",data.Request.Locale),
                    new SqlParameter("@IpAddress",data.Request.IpAddress),
                    new SqlParameter("@UserAgent",data.Request.UserAgent),
                    new SqlParameter("@Expiration",data.Request.Expiration),
                    new SqlParameter("@IdNotification",data.RequestId),
                    new SqlParameter("@IdPayer",data.Request.Payer.Document)
                };

                // Esta variable la vamos a utilizar como variable de salida, ya que es un valor que nos devuelve el procedimiento almacenado
                var IdRequest_OUTPUT = new SqlParameter("@IdRequest_OUTPUT", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                requestParameters.Add(IdRequest_OUTPUT);

                //Llamamos el procemiento almacenado para insertar Request

                await context.Database.ExecuteSqlRawAsync("EXEC InsertRequest @Locale,@IpAddress,@UserAgent,@Expiration,@IdNotification,@IdPayer,@IdRequest_OUTPUT OUT", requestParameters);

                //Guardamos en una variable el Id del registro que se acaba de insertar

                int IdRequest = (int)IdRequest_OUTPUT.Value;


                //Insertar Fields
                foreach (var field in data.Request.Fields)
                {
                    var fieldsParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@KeyWord",field.Keyword),
                        new SqlParameter("@Value",field.Value),
                        new SqlParameter("@DisplayOn",field.DisplayOn),
                        new SqlParameter("@IdRequest",IdRequest)
                    };
                    await context.Database.ExecuteSqlRawAsync("EXEC InsertFields @KeyWord,@Value,@DisplayOn,@IdRequest", fieldsParameters);
                }
                //Comprobamos que el Payment que nos manden no esté ya registrado en la base de datos, de lo contrario significa que hay inconsistencias
                var paymentAlreadyExists = await context.Payment.AnyAsync(x => x.Reference == data.Request.Payment.Reference);
                if (paymentAlreadyExists)
                {
                    throw new Exception("Ya exuste el Payment");
                }
                //Insertar Payment
                var paymentParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Reference",data.Request.Payment.Reference),
                    new SqlParameter("@Description",data.Request.Payment.Description),
                    new SqlParameter("@AllowPartial",data.Request.Payment.AllowPartial),
                    new SqlParameter("@Subscribe",data.Request.Payment.Subscribe),
                    new SqlParameter("@IdRequest",IdRequest)
                };

                //Llamamos el procemiento almacenado para insertar Payment
                await context.Database.ExecuteSqlRawAsync("EXEC InsertPayment @Reference,@Description,@AllowPartial,@Subscribe,@IdRequest", paymentParameters);


                //Insertar Amount
                var amountParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Currency",data.Request.Payment.Amount.Currency),
                    new SqlParameter("@Total",data.Request.Payment.Amount.Total),
                    new SqlParameter("@IdPayment",data.Request.Payment.Reference)
                };
                //Variable de salida que nos devuelve el procedimiento almacenado
                var IdAmount_OUTPUT = new SqlParameter("@IdAmount_OUTPUT", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                amountParameters.Add(IdAmount_OUTPUT);

                //Llamamos el procemiento almacenado para insertar Amount
                await context.Database.ExecuteSqlRawAsync("EXEC InsertAmount @Currency,@Total,@IdPayment,@IdAmount_OUTPUT OUT", amountParameters);

                int IdAmount = (int)IdAmount_OUTPUT.Value;


                foreach (var payment in data.Payment)
                {
                    //Insertar AmountPayment
                    var amountPaymentParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@Factor",payment.Amount.Factor),
                        new SqlParameter("@Currency_To",payment.Amount.To.Currency),
                        new SqlParameter("@Total_To",payment.Amount.To.Total),
                        new SqlParameter("@Currency_From",payment.Amount.From.Currency),
                        new SqlParameter("@Total_From",payment.Amount.From.Total),
                        new SqlParameter("@IdPayment_Parameter",data.Request.Payment.Reference)
                    };

                    var IdAmountPayment_OUTPUT = new SqlParameter("@IdAmountPayment_OUTPUT", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    amountPaymentParameters.Add(IdAmountPayment_OUTPUT);

                    //Llamamos el procemiento almacenado para insertar AmountPayment
                    await context.Database.ExecuteSqlRawAsync("EXEC InsertAmountPayment @Factor,@Currency_To,@Total_To,@Currency_From,@Total_From,@IdPayment_Parameter,@IdAmountPayment_OUTPUT OUT", amountPaymentParameters);

                    int IdAmountPayment = (int)IdAmountPayment_OUTPUT.Value;

                    //Insertar PaymentItem
                    var paymentItemParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@Receipt",payment.Receipt),
                        new SqlParameter("@Refunded",payment.Refunded),
                        new SqlParameter("@Franchise",payment.Franchise),
                        new SqlParameter("@Reference",payment.Reference),
                        new SqlParameter("@IssuerName",payment.IssuerName),
                        new SqlParameter("@Authorization",payment.Authorization),
                        new SqlParameter("@PaymentMethod",payment.PaymentMethod),
                        new SqlParameter("@InternalReference",payment.InternalReference),
                        new SqlParameter("@PaymentMethodName",payment.PaymentMethodName),
                        new SqlParameter("@IdNotification",data.RequestId),
                        new SqlParameter("@IdAmount",IdAmountPayment)
                    };

                    var IdPaymentItem_OUTPUT = new SqlParameter("@IdPaymentItem_OUTPUT", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    paymentItemParameters.Add(IdPaymentItem_OUTPUT);

                    //Llamamos el procemiento almacenado para insertar PaymentITem
                    await context.Database.ExecuteSqlRawAsync("EXEC InsertPaymentItem @Receipt,@Refunded,@Franchise,@Reference,@IssuerName,@Authorization,@PaymentMethod,@InternalReference,@PaymentMethodName,@IdNotification,@IdAmount,@IdPaymentItem_OUTPUT OUT", paymentItemParameters);

                    int IdPaymentItem = (int)IdPaymentItem_OUTPUT.Value;

                    //Insertar StatusPayment
                    var statusPaymentParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@status",payment.Status.status),
                        new SqlParameter("@Reason",payment.Status.Reason),
                        new SqlParameter("@Message",payment.Status.Message),
                        new SqlParameter("@Date",payment.Status.Date),
                        new SqlParameter("@IdNotification",data.RequestId),
                        new SqlParameter("@IdPaymentItem",IdPaymentItem)
                    };


                    //Llamamos el procemiento almacenado para insertar PaymentITem
                    await context.Database.ExecuteSqlRawAsync("EXEC InsertStatusPayment @status,@Reason,@Message,@Date,@IdNotification,@IdPaymentItem", statusPaymentParameters);

                    foreach (var field in payment.ProcessorFields)
                    {
                        //Insertar FieldsPayment
                        var fieldsPaymentParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@KeyWord",field.Keyword),
                        new SqlParameter("@Value",field.Value),
                        new SqlParameter("@DisplayOn",field.DisplayOn),
                        new SqlParameter("@IdPaymentItem",IdPaymentItem)
                    };
                        await context.Database.ExecuteSqlRawAsync("EXEC InsertFieldsPayment @KeyWord,@Value,@DisplayOn,@IdPaymentItem", fieldsPaymentParameters);
                    }

                }
                // Si todo salió bien entonces podemos guardar todos los nuevos datos, terminando la transacción
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                // Si hay algún error dentro de la transacción entonces revertirmos los cambios, y regresamos 0 que indica que algo salió mal
                transaction.Rollback();
                return 0;
            }
            // Si todo salió bien regresamos 1s
            return 1;

        }
        public async Task<NotificationDTO> GetNotification(int RequestId)
        {
            // PARA LA RECUPERACIÓN DE DATOS TAMBIÉN VAMOS A UTILIZAR LOS PROCEDIMIENTOS ALMACENADOS
            var existNotification = await context.Notification.AnyAsync(x => x.RequestId == RequestId);
            if (!existNotification)
            {
                return null;
            }
            var IdNotification = new SqlParameter("@IdNotification", RequestId);

            var Status = context.StatusDTO.FromSqlRaw("EXEC GetStatus @IdNotification", IdNotification).AsEnumerable().FirstOrDefault();

            var Payer = context.PayerDTO.FromSqlRaw("EXEC GetPayer @IdNotification", IdNotification).AsEnumerable().FirstOrDefault();

            var RequestWithId = context.RequestDTO.FromSqlRaw("EXEC GetRequest @IdNotification", IdNotification).AsEnumerable().FirstOrDefault();

            var Request = mapper.Map<RequestDTO>(RequestWithId);

            var IdRequest = new SqlParameter("@IdRequest", RequestWithId.Id);

            var Fields = context.FieldsDTO.FromSqlRaw("EXEC GetFields @IdRequest", IdRequest).AsEnumerable().ToList();

            var Payment = context.PaymentDTO.FromSqlRaw("EXEC GetPayment @IdRequest", IdRequest).AsEnumerable().FirstOrDefault();

            var IdPayment = new SqlParameter("@IdPayment", Payment.Reference);

            var Amount = context.AmountDTO.FromSqlRaw("EXEC GetAmount @IdPayment", IdPayment).AsEnumerable().FirstOrDefault();

            Payment.Amount = Amount;
            Request.Payer = Payer;
            Request.Payment = Payment;
            Request.Fields = Fields;
            var PaymentItemWithId = context.PaymentItemDTO.FromSqlRaw("EXEC GetPaymentItem @IdNotification", IdNotification).AsEnumerable().ToList();

            foreach (var item in PaymentItemWithId)
            {
                var IdPaymentItem = new SqlParameter("@IdPaymentItem", item.Id);
                var IdAmount = new SqlParameter("@IdAmount", item.IdAmount);
                var AmountPaymentWithIds = context.AmountPaymentDTO.FromSqlRaw("EXEC GetAmountPayment @IdAmount", IdAmount).AsEnumerable().FirstOrDefault();
                var IdTo_Parameter = new SqlParameter("@IdAmount", AmountPaymentWithIds.IdTo);
                var IdFrom_Paramter = new SqlParameter("@IdAmount", AmountPaymentWithIds.IdFrom);

                var IdTo = context.AmountDTO.FromSqlRaw("EXEC GetAmountById @IdAmount", IdTo_Parameter).AsEnumerable().FirstOrDefault();
                var IdFrom = context.AmountDTO.FromSqlRaw("EXEC GetAmountById @IdAmount", IdFrom_Paramter).AsEnumerable().FirstOrDefault();

                AmountPaymentWithIds.From = IdFrom;
                AmountPaymentWithIds.To = IdTo;

                var AmountPayment = mapper.Map<AmountPaymentDTO>(AmountPaymentWithIds);


                var StatusPayment = context.StatusDTO.FromSqlRaw("EXEC GetStatusPayment @IdPaymentItem", IdPaymentItem).AsEnumerable().FirstOrDefault();

                var FieldsPayment = context.FieldsDTO.FromSqlRaw("EXEC GetFieldsPayment @IdPaymentItem", IdPaymentItem).AsEnumerable().ToList();

                item.Status = StatusPayment;
                item.Amount = AmountPayment;
                item.ProcessorFields = FieldsPayment;
            }

            var PaymentItem = mapper.Map<List<PaymentItemDTO>>(PaymentItemWithId);

            var Notification = context.NotificationDTO.FromSqlRaw("EXEC GetNotification @IdNotification", IdNotification).AsEnumerable().FirstOrDefault();
            Notification.Status = Status;
            Notification.Request = Request;
            Notification.Payment = PaymentItem;

            return Notification;
        }
        public async Task<bool> IdAlreadyExists(int RequestId)
        {
            var resultado = await context.Notification.AnyAsync(x => x.RequestId == RequestId);
            return resultado;
        }
    }
}
