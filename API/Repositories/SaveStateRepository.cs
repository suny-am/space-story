using API.Models;

namespace API.Repositories;

public class SaveStateRepository(GameDbContext context) : BaseRepository<SaveState>(context)
{

}

