using Microsoft.EntityFrameworkCore.Migrations;

namespace Sport_store.Migrations
{
    public partial class CategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Category (Name) VALUES ('Sporty wodne')");
            migrationBuilder.Sql("INSERT INTO Category (Name) VALUES ('Piłka nożna')");
            migrationBuilder.Sql("INSERT INTO Category (Name) VALUES ('Akcesoria')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
