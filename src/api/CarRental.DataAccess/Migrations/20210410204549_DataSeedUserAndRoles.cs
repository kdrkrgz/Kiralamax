using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DataAccess.Migrations
{
    public partial class DataSeedUserAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActivationCode", "CustomerId", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("7abaed77-7ee2-49f6-9574-391a92b74e36"), 1, new byte[] { 13, 212, 118, 83, 83, 220, 1, 221, 155, 165, 184, 84, 20, 65, 132, 28, 229, 140, 170, 248, 9, 78, 54, 4, 17, 131, 94, 84, 106, 68, 21, 74, 171, 250, 66, 145, 252, 234, 89, 193, 140, 99, 182, 78, 245, 196, 5, 141, 45, 86, 63, 105, 2, 49, 175, 246, 249, 83, 133, 79, 212, 41, 118, 29 }, new byte[] { 204, 244, 142, 253, 255, 133, 143, 64, 185, 20, 79, 69, 178, 227, 183, 158, 251, 13, 77, 77, 6, 90, 215, 212, 212, 116, 166, 232, 130, 144, 196, 4, 166, 218, 17, 217, 67, 84, 15, 217, 91, 249, 210, 139, 96, 173, 3, 81, 167, 36, 181, 124, 161, 107, 92, 87, 243, 142, 159, 188, 192, 83, 1, 26, 64, 49, 75, 223, 74, 224, 154, 152, 181, 232, 136, 9, 134, 131, 98, 96, 240, 179, 129, 223, 168, 78, 83, 92, 7, 155, 82, 177, 162, 33, 227, 210, 13, 131, 214, 148, 219, 21, 142, 96, 203, 220, 136, 43, 165, 220, 38, 225, 116, 60, 158, 25, 232, 185, 197, 123, 26, 255, 57, 220, 178, 232, 91, 85 } });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "OperationClaimId", "UserId", "id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumns: new[] { "OperationClaimId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumns: new[] { "OperationClaimId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumns: new[] { "OperationClaimId", "UserId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActivationCode", "CustomerId", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("06bbc2aa-372e-4f07-8016-2ce8017d2fd9"), 0, new byte[] { 93, 238, 231, 160, 14, 94, 231, 30, 63, 142, 200, 8, 175, 44, 56, 164, 80, 42, 74, 10, 120, 17, 37, 155, 214, 43, 24, 230, 182, 130, 87, 5, 186, 246, 232, 140, 55, 252, 247, 89, 26, 195, 175, 254, 198, 253, 60, 239, 105, 37, 53, 209, 129, 52, 200, 31, 28, 112, 189, 137, 60, 218, 240, 216 }, new byte[] { 184, 203, 87, 217, 152, 107, 15, 59, 201, 249, 81, 229, 209, 7, 202, 91, 81, 81, 75, 23, 169, 6, 184, 240, 148, 166, 208, 230, 125, 54, 201, 1, 154, 215, 233, 140, 116, 74, 83, 241, 106, 23, 243, 210, 208, 221, 191, 179, 180, 193, 52, 90, 233, 236, 5, 130, 40, 40, 126, 206, 170, 165, 228, 17, 224, 82, 203, 152, 47, 202, 199, 135, 27, 68, 136, 209, 222, 185, 55, 235, 37, 134, 43, 70, 167, 206, 200, 189, 212, 22, 247, 45, 70, 28, 140, 187, 252, 234, 99, 126, 131, 0, 131, 36, 2, 201, 186, 208, 254, 10, 29, 110, 127, 17, 71, 110, 214, 46, 128, 21, 157, 184, 112, 95, 116, 118, 148, 78 } });
        }
    }
}
