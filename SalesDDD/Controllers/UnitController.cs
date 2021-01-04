using AutoMapper;
using Core.Models;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using SalesDDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SalesDDD.Controllers
{
    public class UnitController : BaseController
    {
        private readonly IGenericRepository<Unit> _repo;
        private readonly IMapper _mapper;
        public UnitController(IGenericRepository<Unit> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Unit> units = await _repo.GetAllAsync();

            IEnumerable<BaseViewModel> unitViewModels = _mapper.Map<IEnumerable<Unit>, IEnumerable<BaseViewModel>>(units);

            return View(unitViewModels);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new BaseViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(BaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Unit item = _mapper.Map<BaseViewModel, Unit>(model);
                var result = await _repo.InsertAsync(item);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Unit item = await _repo.GetByIdASync(id);
            if (item == null) return NotFound();
            BaseViewModel model = _mapper.Map<Unit, BaseViewModel>(item);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Unit item = _mapper.Map<BaseViewModel, Unit>(model);
                var result = await _repo.UpdateAsync(item);
                if (result == -1)
                    ErrorMessage = Resources.Messages.Error;
                SuccessMessage = Resources.Messages.ChangesSavedSuccessfully;

                return RedirectToAction("Edit", new { id = model.Id });
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.RemoveAsync(id);
            if (result < 1)
            {
                ErrorMessage = "خطا در حذف اطلاعات";
            }
            return RedirectToAction("Index");
        }
    }
}
