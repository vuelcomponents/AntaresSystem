using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Store.StoreModel;

[ApiController]
[Route("api/store-model")]
public class StoreModelController
{
    private readonly DataContext _context;
    public StoreModelController(DataContext context)
    {
        _context = context;
    }
    [HttpGet("get")]
    //obiekty statusowe i filtrowalne
    public List<Models.StoreModel.StoreModel> GetModelsList()
    {
        List<Models.StoreModel.StoreModel> list = _context.StoreModels.ToList();
        return list;
    }
    
}