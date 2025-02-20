using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, DateRegister, CategoryId) " +
                "VALUES('Coca-Cola', 'Refrigerante de Cola 350 ml', '5.45', 'cocacola.jpg', '50', now(), '1')");
            
            mb.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, DateRegister, CategoryId) " +
                   "VALUES('X-Bacon', 'Hamburgúer de bacon', '17.80', 'xbacon.jpg', '20', now(), '2')");
            
            mb.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, DateRegister, CategoryId) " +
                   "VALUES('Chocolate', 'Barra de chocolate Sonho de Valsa', '12.5', 'chocolate.jpg', '35', now(), '3')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");

        }
    }
}
