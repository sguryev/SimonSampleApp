namespace SimonSampleApp.Web.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models;
    using Services.OneSignal;
    using Services.OneSignal.Models;

    [Authorize(Roles = "Admin, DataOperator")]
    public class OneSignalAppController : Controller
    {
        private readonly IOneSignalService _oneSignalService;
        private readonly IMapper _mapper;
        private readonly ILogger<OneSignalAppController> _logger;

        public OneSignalAppController(IOneSignalService oneSignalService, IMapper mapper, ILogger<OneSignalAppController> logger)
        {
            _oneSignalService = oneSignalService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: OneSignalApp
        public async Task<IActionResult> Index()
        {
            try
            {
                var apps = await _oneSignalService.GetAppsAsync();
                return View(_mapper.Map<OneSignalAppModel[]>(apps));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to get Apps");
                return Problem("Unexpected problem", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        // GET: OneSignalApp/Details/5
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var app = await _oneSignalService.GetAppAsync(id);
                if (app == null)
                    return NotFound();

                return View(_mapper.Map<OneSignalAppModel>(app));
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to get App {id}", id);
                return Problem("Unexpected problem", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: OneSignalApp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OneSignalApp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OneSignalAppPostModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            try
            {
                var app = await _oneSignalService.CreateAppAsync(_mapper.Map<AppPostModel>(model));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to create App {@App}", model);
                ModelState.AddModelError(string.Empty, "Unexpected problem");
                return View(model);
            }
        }

        // GET: OneSignalApp/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var app = await _oneSignalService.GetAppAsync(id);
                if (app == null)
                    return NotFound();
                
                return View(_mapper.Map<OneSignalAppModel>(app));
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to get App {Id}", id);
                return Problem("Unexpected problem", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        // POST: OneSignalApp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, OneSignalAppPutModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            try
            {
                await _oneSignalService.UpdateAppAsync(id, _mapper.Map<AppPutModel>(model));
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to update App {Id} {@App}", id, model);
                ModelState.AddModelError(string.Empty, "Unexpected problem");
                return View(model);
            }
        }
    }
}
