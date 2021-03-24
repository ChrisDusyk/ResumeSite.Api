namespace ResumeSite.Api.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Types

[<ApiController>]
[<Route("[controller]")>]
type ResumeController (logger: ILogger<ResumeController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get() =
        { Test1 = "Test Value"
          Test2 = 32 }