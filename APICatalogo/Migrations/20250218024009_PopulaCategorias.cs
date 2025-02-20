using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES ('Bebidas', 'bebidas.png')");
            mb.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES ('Lanhces', 'lanhces.png')");
            mb.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES ('Sobremesas', 'sobremesas.png')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Categories");

        }
    }
}
