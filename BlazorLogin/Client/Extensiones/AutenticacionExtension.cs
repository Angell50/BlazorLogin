using BlazorLogin.Shared;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorLogin.Client.Extensiones
{
    public class AutenticacionExtension : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private ClaimsPrincipal _sinInformacion = new ClaimsPrincipal(new ClaimsIdentity());

        public AutenticacionExtension(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        //guardar informacion
        public async Task ActualizarEstadoAutenticacion(SesionDTO? sesionUsuario)
        {
            ClaimsPrincipal claimsPrincipal;

            if(sesionUsuario != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sesionUsuario.Nombre),
                    new Claim(ClaimTypes.Email,sesionUsuario.Correo),
                    new Claim(ClaimTypes.Role,sesionUsuario.Rol)
                },"JwtAuth"));

                await _sessionStorage.GuardarStorage("sesionUsuario", sesionUsuario);
            }
            else
            {
                claimsPrincipal = _sinInformacion;
                await _sessionStorage.RemoveItemAsync("sesionUsuario");
            }
            //guardar la informacion del usuario
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

        }

        //devuelve la informacion del usuario que esta autenticado
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var sesionUsuario = await _sessionStorage.ObtenerStorage<SesionDTO>("sesionUsuario");

            if (sesionUsuario == null)
                return await Task.FromResult(new AuthenticationState(_sinInformacion));

            //devuelve la informacion del storage para autentificarlo
            var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sesionUsuario.Nombre),
                    new Claim(ClaimTypes.Email,sesionUsuario.Correo),
                    new Claim(ClaimTypes.Role,sesionUsuario.Rol)
                }, "JwtAuth"));


            return await Task.FromResult(new AuthenticationState(claimPrincipal));

        }
    }
}
