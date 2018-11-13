using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulApiStarterTemplate.DataStore.Services;
using RestfulApiStarterTemplate.Models.Dto;
using RestfulApiStarterTemplate.Models.Entities;

namespace RestfulApiStarterTemplate.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly IDataStoreService<Test> _testDataStoreService;

        public TestController(IDataStoreService<Test> testDataStoreService)
        {
            _testDataStoreService = testDataStoreService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var tests = _testDataStoreService.Get().ToList();
            var testDtos = Mapper.Map<IEnumerable<Test>, IEnumerable<TestDto>>(tests);
            return Ok(testDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id, bool includeChildren = false)
        {
            Test test;
            if (!includeChildren)
            {
                test = await _testDataStoreService.Get(id);
            }
            else
            {
                test = _testDataStoreService
                    .Query()
                    .Include(t => t.TestChildren)
                    .FirstOrDefault(t => t.Id == id);
            }

            if (test == null)
            {
                return NotFound();
            }

            var testDto = Mapper.Map<Test, TestDto>(test);
            return Ok(testDto);
        }
    }
}
