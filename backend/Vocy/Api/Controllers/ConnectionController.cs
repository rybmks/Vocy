// using Application.Connection;
// using Application.Connection.Commands;
// using Microsoft.AspNetCore.Mvc;
//
// namespace Api.Controllers;
//
// [Route("api/connection")]
// public class ConnectionController(ConnectionService connectionService) : Controller
// {
//     public async Task Create(CreateConnectionCommand connectionCommand)
//     {
//         var access = Request.Cookies["acceess"];
//
//         if (access == null)
//             throw new Exception("no access");
//
//         await connectionService.CreateConnection(connectionCommand, access);
//     }
// }

