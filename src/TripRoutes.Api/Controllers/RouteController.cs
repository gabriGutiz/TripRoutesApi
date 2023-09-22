using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TripRoutes.Domain.Dtos;
using TripRoutes.Domain.Exceptions;
using TripRoutes.Domain.Interfaces;
using TripRoutes.Domain.Models;

namespace TripRoutes.Api.Controllers
{
    [ApiController]
    [Route("v1/routes")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Upload([FromBody] RouteRequest request)
        {
            try
            {
                var response = await _routeService.AddRoute(request);

                if (!response)
                    return NoContent();

                return Ok(response);
            }
            catch (TripRoutesException ex)
            {
                return StatusCode(ex.CodigoHttp, ex.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("/{departure}/{arrival}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TripPathsResponse>> Get([Required][FromRoute] string departure, [Required][FromRoute] string arrival)
        {
            try
            {
                var route = await _routeService.GetPossiblePaths(departure, arrival);

                if (route is null)
                    return NotFound();

                return Ok(route);
            }
            catch (TripRoutesException ex)
            {
                return StatusCode(ex.CodigoHttp, ex.Error);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("/{departure}/{arrival}/cheaper")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> GetCheaper([Required][FromRoute] string departure, [Required][FromRoute] string arrival)
        {
            try
            {
                var route = await _routeService.GetCheaperPath(departure, arrival);

                if (route is null)
                    return NotFound();

                return Ok(route);
            }
            catch (TripRoutesException ex)
            {
                return StatusCode(ex.CodigoHttp, ex.Error);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
