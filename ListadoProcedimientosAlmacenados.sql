-- Listado De Procedimientos almacenados usados en la base de datos del Web Service
CREATE PROCEDURE [dbo].[GetPayer] 
	@IdNotification int
AS
BEGIN
	SELECT P.Document,P.DocumentType,P.Name,P.Surname,P.Email,P.Mobile
	FROM Request as R inner join Payer as P on R.IdPayer = P.Document
	WHERE R.IdNotification = @IdNotification
END
-----------------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetAmount] 
	@IdPayment nvarchar(450)
AS
BEGIN
	SELECT Total,Currency
	FROM Amount
	WHERE IdPayment = @IdPayment
END
-----------------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetAmountById] 
	@IdAmount int
AS
BEGIN
	SELECT Total,Currency
	FROM Amount
	WHERE Id = @IdAmount
END
-------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[GetAmountPayment] 
	@IdAmount int
AS
BEGIN
	SELECT Factor,IdTo,IdFrom
	FROM AmountPayment
	WHERE Id = @IdAmount
END
---------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetFields] 
	@IdRequest int
AS
BEGIN
	SELECT Keyword,Value,DisplayOn
	FROM Field
	WHERE IdRequest = @IdRequest
END
----------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetFieldsPayment] 
	@IdPaymentItem int
AS
BEGIN
	SELECT Keyword,Value,DisplayOn
	FROM FieldsPayment
	WHERE IdPaymentItem = @IdPaymentItem
END
-----------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetNotification] 
	@IdNotification int
AS
BEGIN
	SELECT RequestId,Subscription
	FROM Notification
	WHERE RequestId = @IdNotification
END
----------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetPayment] 
	@IdRequest int
AS
BEGIN
	SELECT Reference,Description,AllowPartial,Subscribe
	FROM Payment
	WHERE IdRequest = @IdRequest
END
-----------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetPaymentItem] 
	@IdNotification int
AS
BEGIN
	SELECT Receipt,Refunded,Franchise,Reference,IssuerName,[Authorization],PaymentMethod,InternalReference,PaymentMethodName,Id,IdAmount
	FROM PaymentItem
	WHERE IdNotification = @IdNotification
END
------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetRequest] 
	@IdNotification int
AS
BEGIN
	SELECT Locale,IpAddress,UserAgent,Expiration,Id
	FROM Request
	WHERE IdNotification = @IdNotification
END
-------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetStatus] 
	@IdNotification int
AS
BEGIN
	SELECT status,Reason,Message,Date
	FROM Status
	WHERE IdNotification = @IdNotification
END
-------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[GetStatusPayment] 
	@IdPaymentItem int
AS
BEGIN
	SELECT status,Reason,Message,Date
	FROM StatusPayment
	WHERE IdPaymentItem = @IdPaymentItem
END
-------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[InsertAmount]
	@Currency nvarchar(MAX),
	@Total float,
	@IdPayment nvarchar(450),
	@IdAmount_OUTPUT int OUTPUT
	
AS
BEGIN

	insert into Amount(Currency,Total,IdPayment)
	values(@Currency,@Total,@IdPayment)

	SET @IdAmount_OUTPUT = SCOPE_IDENTITY()
END
------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[InsertAmountPayment]
	@Factor int,
	@Currency_To nvarchar(MAX),
	@Total_To float,
	@Currency_From nvarchar(MAX),
	@Total_From float,
	@IdPayment_Parameter nvarchar(450),
	@IdAmountPayment_OUTPUT int OUTPUT

AS
BEGIN
	
	DECLARE @IdFrom int
	DECLARE @IdTo int

	EXEC InsertAmount @Currency = @Currency_To, @Total = @Total_To, @IdPayment = @IdPayment_Parameter, @IdAmount_OUTPUT= @IdTo OUTPUT
	EXEC InsertAmount @Currency = @Currency_From, @Total = @Total_From, @IdPayment = @IdPayment_Parameter, @IdAmount_OUTPUT= @IdFrom OUTPUT

	insert into AmountPayment(Factor,IdTo,IdFrom)
	values(@Factor,@IdTo,@IdFrom)

	set @IdAmountPayment_OUTPUT= SCOPE_IDENTITY()
END
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[InsertFields]
	@KeyWord nvarchar(MAX),
	@Value nvarchar(MAX),
	@DisplayOn nvarchar(MAX),
	@IdRequest int
AS
BEGIN
	
	insert into Field(Keyword,Value,DisplayOn,IdRequest)
	values(@KeyWord,@Value,@DisplayOn,@IdRequest)

END
-----------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[InsertFieldsPayment]
	@KeyWord nvarchar(MAX),
	@Value nvarchar(MAX),
	@DisplayOn nvarchar(MAX),
	@IdPaymentItem int
AS
BEGIN
	
	insert into FieldsPayment(Keyword,Value,DisplayOn,IdPaymentItem)
	values(@KeyWord,@Value,@DisplayOn,@IdPaymentItem)

END
--------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[InsertNotification]
	@RequestId int,
	@Subscription nvarchar(MAX) null
AS
BEGIN

	insert into Notification(RequestId,Subscription)
	values(@RequestId,@Subscription)
END
--------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[InsertPayer]
	@Document nvarchar(450),
	@DocumentType nvarchar(MAX),
	@Name varchar(MAX),
	@SurName varchar(MAX),
	@Email varchar(MAX),
	@Mobile varchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	insert into Payer(Document,DocumentType,Name,Surname,Email,Mobile)
	values(@Document,@DocumentType,@Name,@SurName,@Email,@Mobile)
END
--------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[InsertPayment]
	@Reference nvarchar(450),
	@Description nvarchar(MAX),
	@AllowPartial bit,
	@Subscribe bit,
	@IdRequest int
AS
BEGIN

	insert into Payment(Reference,Description,AllowPartial,Subscribe,IdRequest)
	values(@Reference,@Description,@AllowPartial,@Subscribe,@IdRequest)
END
------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[InsertPaymentItem]
	@Receipt nvarchar(MAX),
	@Refunded bit,
	@Franchise nvarchar(MAX),
	@Reference nvarchar(MAX),
	@IssuerName nvarchar(MAX),
	@Authorization nvarchar(MAX),
	@PaymentMethod nvarchar(MAX),
	@InternalReference int,
	@PaymentMethodName nvarchar(MAX),
	@IdNotification int,
	@IdAmount int,
	@IdPaymentItem_OUTPUT int OUTPUT
AS
BEGIN
	insert into PaymentItem(Receipt,Refunded,Franchise,Reference,IssuerName,[Authorization],PaymentMethod,InternalReference,PaymentMethodName,IdNotification,IdAmount)
	values(@Receipt,@Refunded,@Franchise,@Reference,@IssuerName,@Authorization,@PaymentMethod,@InternalReference,@PaymentMethodName,@IdNotification,@IdAmount)

	set @IdPaymentItem_OUTPUT = SCOPE_IDENTITY()
END
-------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[InsertRequest]
	@Locale nvarchar(MAX),
	@IpAddress nvarchar(MAX),
	@UserAgent varchar(MAX),
	@Expiration datetime2(7),
	@IdNotification int,
	@IdPayer varchar(450),
	@IdRequest_OUTPUT int OUTPUT
AS
BEGIN

	insert into Request(Locale,IpAddress,UserAgent,Expiration,IdNotification,IdPayer)
	values(@Locale,@IpAddress,@UserAgent,@Expiration,@IdNotification,@IdPayer)
	
	SET @IdRequest_OUTPUT = SCOPE_IDENTITY()
END
-------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[InsertStatus]
	@status nvarchar(MAX),
	@Reason nvarchar(MAX),
	@Message nvarchar(MAX),
	@Date datetime2(7),
	@IdNotification int
AS
BEGIN
	
	insert into Status(status,Reason,Message,Date,IdNotification)
	values(@status,@Reason,@Message,@Date,@IdNotification)
END
-----------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[InsertStatusPayment]
	@status nvarchar(MAX),
	@Reason nvarchar(MAX),
	@Message nvarchar(MAX),
	@Date datetime2(7),
	@IdNotification int,
	@IdPaymentItem int
AS
BEGIN
	
	insert into StatusPayment(status,Reason,Message,Date,IdNotification,IdPaymentItem)
	values(@status,@Reason,@Message,@Date,@IdNotification,@IdPaymentItem)
END


