using AutoMapper;
using Core.Models;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesDDD.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDDD.Controllers
{
    public class HomeController : BaseController
    {

        private readonly IGenericRepository<Product> _repo;
        private readonly IGenericRepository<Order> _repoOrder;
        IMapper _mapper;
        public HomeController(IGenericRepository<Product> repo, IMapper mapper, IGenericRepository<Order> repoOrder)
        {
            _repo = repo;
            _mapper = mapper;
            _repoOrder = repoOrder;
        }

        public IActionResult Index()
        {
            IQueryable<Product> list = _repo.GetAsQueryable("Unit,Brand");
            IEnumerable<ProductViewModel> result = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(list);
            return View(result);
        }
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel()
            { RequestId = "خطا در احراز هویت" });
        }
        [HttpPost(Name = "Card")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Card(Tag tag)
        {
            var listCard = await _repoOrder.GetAllAsync();
            
            if(tag.Classname.Equals("btn btn-danger"))
            {
                Order remove = listCard.Where(woak => woak.ProductId == tag.Id).OrderByDescending(woak => woak.Id).FirstOrDefault();
                if (remove == null) return Json(new { success = false, responseText = "محصولی در سبد خرید شما نیست" });

                await _repoOrder.RemoveAsync(remove.Id);
                return Json(new { success = false, responseText = "محصول جاری از سبد خرید حذف شد" });

            }

            Product product = await _repo.GetByIdASync(tag.Id);
            int customerOrder = listCard.Where(woak => woak.ProductId == tag.Id).Count();

            if(customerOrder > product.Qty)
                return Json(new { success = false, responseText = "محصول موجود نیست" });

            Order order = new Order
            {
                ProductId = tag.Id,
                Confirm = true,
                Title = product.Title + " " + product.ProductCode + " " + product.Size,
                SystemUserId =  UserId
            };

            var result = await _repoOrder.InsertAsync(order);
            return Json(new { success = true, responseText = "محصول در سبد شما ثبت شد" });
        }
    }
}
