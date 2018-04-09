using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using PMDEvers.CQRS.Interfaces;
using PMDEvers.CQRS.Sample.Domain;
using PMDEvers.CQRS.Sample.Domain.Commands;
using PMDEvers.Servicebus;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMDEvers.CQRS.Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceBus _serviceBus;
        private readonly IRepository<SampleAggregate> _repository;

        public HomeController(IServiceBus serviceBus, IRepository<SampleAggregate> repository)
        {
            _serviceBus = serviceBus;
            _repository = repository;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var aggregate = await _repository.GetCurrentStateAsync(id, Request.HttpContext.RequestAborted);
            return View("Index2", aggregate);
        }

        public async Task<IActionResult> Create()
        {
            var command = new CreateSample();

            await _serviceBus.SendAsync(command, Response.HttpContext.RequestAborted);

            return RedirectToAction("Index", new { id = command.AggregateId });
        }
    }
}
