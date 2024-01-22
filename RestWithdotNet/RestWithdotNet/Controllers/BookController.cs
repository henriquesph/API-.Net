using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithDotNet.Model;
using RestWithDotNet.Business.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithDotNet.Business;
using RestWithDotNet.Data.VO;

namespace RestWithDotNet.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")] // path evita ambiguidade 
        public IActionResult Get(long id)
        {
            var person = _bookBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookVO book) // Pega o Json do corpo da request e converte num objeto Person
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }

        [HttpPut]
        public IActionResult Put([FromBody] BookVO book) // Pega o Json do corpo da request e converte num objeto Person
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Update(book));
        }

        [HttpDelete("{id}")] // path evita ambiguidade 
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }

        //[HttpGet("sum/{firstNumber}/{secondNumber}")] // path - requisição passada pelo endpoint
        //public IActionResult Sum(string firstNumber, string secondNumber)
        //{
        //    if(isNumeric(firstNumber) && isNumeric(secondNumber)) {
        //        var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
        //        return Ok(sum.ToString()); // ok - status code 200
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("subtraction/{firstNumber}/{secondNumber}")] // path - requisição passada pelo endpoint
        //public IActionResult Subtraction(string firstNumber, string secondNumber)
        //{
        //    if (isNumeric(firstNumber) && isNumeric(secondNumber))
        //    {
        //        var sum = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
        //        return Ok(sum.ToString()); // ok - status code 200
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("multiplication/{firstNumber}/{secondNumber}")] // path - requisição passada pelo endpoint
        //public IActionResult Multiplication(string firstNumber, string secondNumber)
        //{
        //    if (isNumeric(firstNumber) && isNumeric(secondNumber))
        //    {
        //        var sum = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
        //        return Ok(sum.ToString()); // ok - status code 200
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("division/{firstNumber}/{secondNumber}")] // path - requisição passada pelo endpoint
        //public IActionResult Division(string firstNumber, string secondNumber)
        //{
        //    if (isNumeric(firstNumber) && isNumeric(secondNumber))
        //    {
        //        var sum = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
        //        return Ok(sum.ToString()); // ok - status code 200
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("mean/{firstNumber}/{secondNumber}")] // path - requisição passada pelo endpoint
        //public IActionResult Mean(string firstNumber, string secondNumber)
        //{
        //    if (isNumeric(firstNumber) && isNumeric(secondNumber))
        //    {
        //        var sum = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;
        //        return Ok(sum.ToString()); // ok - status code 200
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("square-root/{firstNumber}")] // path - requisição passada pelo endpoint
        //public IActionResult SquareRoot(string firstNumber)
        //{
        //    if (isNumeric(firstNumber))
        //    {
        //        var squareRoot = Math.Sqrt((double)ConvertToDecimal(firstNumber));
        //        return Ok(squareRoot.ToString()); // ok - status code 200
        //    }
        //    return BadRequest("Invalid Input");
        //}


        //private bool isNumeric(string strNumber)
        //{
        //    double number;
        //    bool isNumber = double.TryParse(
        //        strNumber,
        //        System.Globalization.NumberStyles.Any,
        //        System.Globalization.NumberFormatInfo.InvariantInfo,
        //        out number);
        //    return isNumber;
        //}

        //private decimal ConvertToDecimal(string strNumber)
        //{
        //    decimal decimalValue;
        //    if(decimal.TryParse(strNumber, out decimalValue)){
        //        return decimalValue;
        //    }
        //    return 0;
        //}
    }
}
