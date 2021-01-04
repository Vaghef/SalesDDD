using AutoMapper;
using Core.Models;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesDDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDDD.Controllers
{
    public class OrderController : BaseController
    {
        readonly IGenericRepository<Order> _repo;
        readonly IGenericRepository<Product> _repoProduct;
        IMapper _mapper;

        public OrderController(IGenericRepository<Order> repo,
            IGenericRepository<Product> repoProduct, IMapper mapper)
        {
            _repo = repo;
            _repoProduct = repoProduct;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            IQueryable<Order> list = _repo.GetAsQueryable("Product");
            if (User.IsInRole("Customer"))
            {
                list = list.Where(woak => woak.SystemUserId == UserId);
            }

            IEnumerable<OrderViewModel> result = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(list);

            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            FillDropDown();
            return View(new OrderViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            model.SystemUserId = UserId;

            if (ModelState.IsValid)
            {
                Order item = _mapper.Map<OrderViewModel, Order>(model);
                item.Confirm = true;
                await _repo.InsertAsync(item);

                SuccessMessage = Resources.Messages.ChangesSavedSuccessfully;

                return RedirectToAction("Edit", new { id = item.Id });
            }
            ErrorMessage = Resources.Messages.RequiredField;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            FillDropDown();
            var model = await _repo.GetByIdASync(id);

            OrderViewModel result = _mapper.Map<Order, OrderViewModel>(model);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderViewModel model)
        {
            model.SystemUserId = UserId;

            if (ModelState.IsValid)
            {
                Order item = _mapper.Map<OrderViewModel, Order>(model);
                item.Confirm = true;
                var result = await _repo.UpdateAsync(item);
                SuccessMessage = Resources.Messages.ChangesSavedSuccessfully;

                return RedirectToAction("Edit", new { id = item.Id });
            }
            ErrorMessage = Resources.Messages.RequiredField;
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.RemoveAsync(id);
            if (result < 1)
            {
                if (TempData["Error"] != null)
                    TempData.Remove("Error");

                TempData.Add("Error", Resources.Messages.ProblemDeletingItem);
            }
            return RedirectToAction("Index");
        }
        private void FillDropDown()
        {
            ViewBag.Products = _repoProduct.GetAll().Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();
        }
    }
}
