namespace APICatalogo.Repositories;

//"Repositório de repositórios"
public interface IUnitOfWork
{
    //Obtendo instâncias dos repositórios 
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    void Commit(); //Confirma as alterações 
    
    
}