using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMS.API.Migrations
{
    /// <inheritdoc />
    public partial class initforOthersTableCraeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Library_LibraryId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Books_BookId",
                table: "Loan");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Library_LibraryId",
                table: "Loan");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Member_MemberId",
                table: "Loan");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_Library_LibraryId",
                table: "Member");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loan",
                table: "Loan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Library",
                table: "Library");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.RenameTable(
                name: "Loan",
                newName: "Loans");

            migrationBuilder.RenameTable(
                name: "Library",
                newName: "Libraries");

            migrationBuilder.RenameIndex(
                name: "IX_Member_LibraryId",
                table: "Members",
                newName: "IX_Members_LibraryId");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_MemberId",
                table: "Loans",
                newName: "IX_Loans_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_LibraryId",
                table: "Loans",
                newName: "IX_Loans_LibraryId");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_BookId",
                table: "Loans",
                newName: "IX_Loans_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loans",
                table: "Loans",
                column: "LoanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Libraries",
                table: "Libraries",
                column: "LibraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Libraries_LibraryId",
                table: "Books",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "LibraryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BookId",
                table: "Loans",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Libraries_LibraryId",
                table: "Loans",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "LibraryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Members_MemberId",
                table: "Loans",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Libraries_LibraryId",
                table: "Members",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "LibraryId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Libraries_LibraryId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BookId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Libraries_LibraryId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Members_MemberId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Libraries_LibraryId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loans",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Libraries",
                table: "Libraries");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.RenameTable(
                name: "Loans",
                newName: "Loan");

            migrationBuilder.RenameTable(
                name: "Libraries",
                newName: "Library");

            migrationBuilder.RenameIndex(
                name: "IX_Members_LibraryId",
                table: "Member",
                newName: "IX_Member_LibraryId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_MemberId",
                table: "Loan",
                newName: "IX_Loan_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_LibraryId",
                table: "Loan",
                newName: "IX_Loan_LibraryId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_BookId",
                table: "Loan",
                newName: "IX_Loan_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loan",
                table: "Loan",
                column: "LoanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Library",
                table: "Library",
                column: "LibraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Library_LibraryId",
                table: "Books",
                column: "LibraryId",
                principalTable: "Library",
                principalColumn: "LibraryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Books_BookId",
                table: "Loan",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Library_LibraryId",
                table: "Loan",
                column: "LibraryId",
                principalTable: "Library",
                principalColumn: "LibraryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Member_MemberId",
                table: "Loan",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Library_LibraryId",
                table: "Member",
                column: "LibraryId",
                principalTable: "Library",
                principalColumn: "LibraryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
