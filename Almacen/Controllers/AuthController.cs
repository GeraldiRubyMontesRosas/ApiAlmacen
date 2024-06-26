﻿using Almacen.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AlmacenAuthService = Almacen.Services.IAuthorizationService;

namespace Almacen.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AlmacenAuthService authService;
        private readonly ILogger<AuthController> logger;

        public AuthController(AlmacenAuthService authService, ILogger<AuthController> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AppUserDTO dto)
        {
            try
            {
                var result = await authService.ValidateUser(dto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(405); // Contraseña incorrecta
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error durante la autenticación del usuario.");
                return StatusCode(500, new { ErrorMessage = "Ocurrió un error durante la autenticación del usuario.", Exception = ex.Message });
            }
        }

        [HttpDelete("logout/{userId}")]
        public async Task<IActionResult> Logout(int userId)
        {
            try
            {
                await authService.Logout(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error durante el cierre de sesión del usuario.");
                return StatusCode(500, new { ErrorMessage = "Ocurrió un error durante el cierre de sesión del usuario.", Exception = ex.Message });
            }
        }
    }
}
