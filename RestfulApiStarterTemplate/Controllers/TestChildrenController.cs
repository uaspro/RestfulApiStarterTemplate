using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestfulApiStarterTemplate.DataStore.Services;
using RestfulApiStarterTemplate.Models.Dto.Input;
using RestfulApiStarterTemplate.Models.Dto.Output;
using RestfulApiStarterTemplate.Models.Entities;

namespace RestfulApiStarterTemplate.Controllers
{
    [ApiController]
    [Route("api/tests/{testId}/children")]
    public class TestChildrenController : ControllerBase
    {
        private readonly ITestChildrenDataStoreService _testChildrenDataStoreService;
        private readonly IDataStoreService<Test> _testDataStoreService;

        public TestChildrenController(ITestChildrenDataStoreService testChildrenDataStoreService, IDataStoreService<Test> testDataStoreService)
        {
            _testChildrenDataStoreService = testChildrenDataStoreService;
            _testDataStoreService = testDataStoreService;
        }

        [HttpGet]
        public IActionResult GetChildrenForTest(int testId)
        {
            var testChildren = _testChildrenDataStoreService.GetChildrenForTest(testId);
            if (!testChildren.Any())
            {
                return NotFound();
            }

            var testChildrenDto = Mapper.Map<IEnumerable<TestChild>, IEnumerable<TestChildDto>>(testChildren);
            return Ok(testChildrenDto);
        }

        [HttpGet("{id:int}", Name = nameof(GetChildForTest))]
        public IActionResult GetChildForTest(int testId, int id)
        {
            var testChild = _testChildrenDataStoreService.GetChildForTest(testId, id);
            if (testChild == null)
            {
                return NotFound();
            }

            var testChildDto = Mapper.Map<TestChildDto>(testChild);
            return Ok(testChildDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest(int testId, [FromBody]TestChildForCreationDto testForCreationDto)
        {
            if (testForCreationDto == null)
            {
                return BadRequest();
            }

            var testExists = _testDataStoreService.Query().Any(t => t.Id == testId);
            if (!testExists)
            {
                return NotFound();
            }

            var testChildEntity = Mapper.Map<TestChild>(testForCreationDto);
            _testChildrenDataStoreService.AddChildForTest(testId, testChildEntity);

            var resultedEntitiesCount = await _testChildrenDataStoreService.SaveChanges();
            if (resultedEntitiesCount == 0)
            {
                throw new Exception("Creating a test child failed on save.");
            }

            var testChildToReturn = Mapper.Map<TestChildDto>(testChildEntity);
            return CreatedAtRoute(
                nameof(GetChildForTest),
                new
                {
                    testId,
                    id = testChildToReturn.Id

                },
                testChildToReturn);
        }
    }
}
