using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulApiStarterTemplate.DataStore.Services;
using RestfulApiStarterTemplate.Models.Dto.Input;
using RestfulApiStarterTemplate.Models.Dto.Output;
using RestfulApiStarterTemplate.Models.Entities;

namespace RestfulApiStarterTemplate.Controllers
{
    [ApiController]
    [Route("api/tests")]
    public class TestsController : ControllerBase
    {
        private readonly IDataStoreService<Test> _testDataStoreService;

        public TestsController(IDataStoreService<Test> testDataStoreService)
        {
            _testDataStoreService = testDataStoreService;
        }

        [HttpGet]
        public IActionResult GetTests()
        {
            var tests = _testDataStoreService.Get().ToList();
            var testDtos = Mapper.Map<IEnumerable<Test>, IEnumerable<TestDto>>(tests);
            return Ok(testDtos);
        }

        [HttpGet("{id:int}", Name = nameof(GetTest))]
        public async Task<IActionResult> GetTest(int id, bool includeChildren = false)
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

            var testDto = Mapper.Map<TestDto>(test);
            return Ok(testDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody]TestForCreationDto testForCreationDto)
        {
            if (testForCreationDto == null)
            {
                return BadRequest();
            }

            var testEntity = Mapper.Map<Test>(testForCreationDto);
            _testDataStoreService.Insert(testEntity);

            var resultedEntitiesCount = await _testDataStoreService.SaveChanges();
            if (resultedEntitiesCount == 0)
            {
                throw new Exception("Creating a test failed on save.");
            }

            var testToReturn = Mapper.Map<TestDto>(testEntity);
            return CreatedAtRoute(
                nameof(GetTest), 
                new
                {
                    id = testToReturn.Id
                }, 
                testToReturn);
        }
    }
}
