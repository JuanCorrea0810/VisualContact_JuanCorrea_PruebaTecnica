using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebService_Notification.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subscription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.RequestId);
                });

            migrationBuilder.CreateTable(
                name: "Payer",
                columns: table => new
                {
                    Document = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payer", x => x.Document);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNotification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Status_Notification_IdNotification",
                        column: x => x.IdNotification,
                        principalTable: "Notification",
                        principalColumn: "RequestId");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNotification = table.Column<int>(type: "int", nullable: false),
                    IdPayer = table.Column<string>(type: "nvarchar(450)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_Notification_IdNotification",
                        column: x => x.IdNotification,
                        principalTable: "Notification",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "FK_Request_Payer_IdPayer",
                        column: x => x.IdPayer,
                        principalTable: "Payer",
                        principalColumn: "Document");
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdRequest = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Field_Request_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Request",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Reference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowPartial = table.Column<bool>(type: "bit", nullable: false),
                    Subscribe = table.Column<bool>(type: "bit", nullable: false),
                    IdRequest = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Reference);
                    table.ForeignKey(
                        name: "FK_Payment_Request_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Request",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Amount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: false),
                    IdPayment = table.Column<string>(type: "nvarchar(450)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amount_Payment_IdPayment",
                        column: x => x.IdPayment,
                        principalTable: "Payment",
                        principalColumn: "Reference");
                });

            migrationBuilder.CreateTable(
                name: "AmountPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Factor = table.Column<int>(type: "int", nullable: false),
                    IdTo = table.Column<int>(type: "int", nullable: false),
                    IdFrom = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmountPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmountPayment_Amount_IdFrom",
                        column: x => x.IdFrom,
                        principalTable: "Amount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AmountPayment_Amount_IdTo",
                        column: x => x.IdTo,
                        principalTable: "Amount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Receipt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refunded = table.Column<bool>(type: "bit", nullable: false),
                    Franchise = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Authorization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalReference = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNotification = table.Column<int>(type: "int", nullable: false),
                    IdAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentItem_AmountPayment_IdAmount",
                        column: x => x.IdAmount,
                        principalTable: "AmountPayment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentItem_Notification_IdNotification",
                        column: x => x.IdNotification,
                        principalTable: "Notification",
                        principalColumn: "RequestId");
                });

            migrationBuilder.CreateTable(
                name: "FieldsPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPaymentItem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldsPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldsPayment_PaymentItem_IdPaymentItem",
                        column: x => x.IdPaymentItem,
                        principalTable: "PaymentItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StatusPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNotification = table.Column<int>(type: "int", nullable: false),
                    IdPaymentItem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusPayment_Notification_IdNotification",
                        column: x => x.IdNotification,
                        principalTable: "Notification",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "FK_StatusPayment_PaymentItem_IdPaymentItem",
                        column: x => x.IdPaymentItem,
                        principalTable: "PaymentItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amount_IdPayment",
                table: "Amount",
                column: "IdPayment");

            migrationBuilder.CreateIndex(
                name: "IX_AmountPayment_IdFrom",
                table: "AmountPayment",
                column: "IdFrom");

            migrationBuilder.CreateIndex(
                name: "IX_AmountPayment_IdTo",
                table: "AmountPayment",
                column: "IdTo");

            migrationBuilder.CreateIndex(
                name: "IX_Field_IdRequest",
                table: "Field",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_FieldsPayment_IdPaymentItem",
                table: "FieldsPayment",
                column: "IdPaymentItem");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_IdRequest",
                table: "Payment",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItem_IdAmount",
                table: "PaymentItem",
                column: "IdAmount");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItem_IdNotification",
                table: "PaymentItem",
                column: "IdNotification");

            migrationBuilder.CreateIndex(
                name: "IX_Request_IdNotification",
                table: "Request",
                column: "IdNotification");

            migrationBuilder.CreateIndex(
                name: "IX_Request_IdPayer",
                table: "Request",
                column: "IdPayer");

            migrationBuilder.CreateIndex(
                name: "IX_Status_IdNotification",
                table: "Status",
                column: "IdNotification");

            migrationBuilder.CreateIndex(
                name: "IX_StatusPayment_IdNotification",
                table: "StatusPayment",
                column: "IdNotification");

            migrationBuilder.CreateIndex(
                name: "IX_StatusPayment_IdPaymentItem",
                table: "StatusPayment",
                column: "IdPaymentItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "FieldsPayment");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "StatusPayment");

            migrationBuilder.DropTable(
                name: "PaymentItem");

            migrationBuilder.DropTable(
                name: "AmountPayment");

            migrationBuilder.DropTable(
                name: "Amount");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Payer");
        }
    }
}
