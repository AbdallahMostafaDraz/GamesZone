using GameZone.Enities.Models;

namespace GameZone.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        public GamesController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{ConstantsFile.ImagesPath}";
        }
        public IActionResult Index()
        {
            var games = _unitOfWork.Game.GetAllWithAllIncludes();
            return View(games);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var game = _unitOfWork.Game.GetOneWithAllIncludes(id);
            
            if (game is null)
                return NotFound();

            return View(game);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            CreateGameFormViewModel viewModel = new();
            await LoadCategoriesAndDevices(viewModel);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel gameVM)
        {
            if (!ModelState.IsValid)
            {
                if (gameVM.CategoryId == 0)
                {
                    ModelState.AddModelError("CategoryId", "Category field is required");
                }
                await LoadCategoriesAndDevices(gameVM);
                return View(gameVM);
            }

            
            Game game = new Game();
            _mapper.Map(gameVM, game);
            game.Cover = await SaveImage(gameVM.Cover);

            game.Devices = gameVM.SelectedDevices.Select(d => new GameDevice { DeviceId = d}).ToList();
            

            await _unitOfWork.Game.Add(game);
            await _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var game = _unitOfWork.Game.GetOneWithAllIncludes(id);

            if (game is null)
                return NotFound();

            var viewModel = new EditGameViewModel();
            _mapper.Map(game, viewModel);
            viewModel.OldCoverName = game.Cover;
            await LoadCategoriesAndDevices(viewModel);
            viewModel.SelectedDevices = game.Devices.Select(e => e.DeviceId).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditGameViewModel gameVM)
        {
            if (!ModelState.IsValid) 
            {
                await LoadCategoriesAndDevices(gameVM);
                return View(gameVM);
            }
            else
            {
                var game = _unitOfWork.Game.GetOneWithAllIncludes(gameVM.Id);
                gameVM.OldCoverName = game?.Cover;
                if (gameVM.Cover is not null)
                {
                    // save a new image
                    game!.Cover = await SaveImage(gameVM.Cover);

                    // delete old image
                    var pathToDelete = Path.Combine(_webHostEnvironment.WebRootPath, "Assets", "Images", "Games", gameVM.OldCoverName!);
                    DeleteImage(pathToDelete);

                }
                _mapper.Map(gameVM, game);
                game!.Devices = gameVM.SelectedDevices.Select(e => new GameDevice { DeviceId = e}).ToList();
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));

            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
           
            var game = await _unitOfWork.Game.GetOne(e => e.Id == id);
            if (game is null)
                return NotFound(game);

            var pathToDelete = Path.Combine(_webHostEnvironment.WebRootPath, "Assets", "Images", "Games", game.Cover);
            _unitOfWork.Game.Delete(game);
            if (await _unitOfWork.Complete() > 0)
            {
                DeleteImage(pathToDelete);
                return Ok();
            }
            
            return BadRequest(); 
        }
         

        private void DeleteImage(string pathToDelete)
        {
            System.IO.File.Delete(pathToDelete);
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var path = Path.Combine(_imagesPath, coverName);
            using var stream = System.IO.File.Create(path);
            await image.CopyToAsync(stream);
            return coverName;
        }
        private async Task LoadCategoriesAndDevices(BaseGameViewModel viewModel)
        {
            viewModel.Categories = await _unitOfWork.Category.GetCategorySelectedList();
            viewModel.Devices = await _unitOfWork.Device.GetDevicesSelectedList();
        }
    }
}
