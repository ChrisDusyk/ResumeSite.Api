namespace ResumeSite.Api.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open System.Net
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Types

module ActionResult =
    let ofAsync (res: Async<IActionResult>) =
        res |> Async.StartAsTask

[<ApiController>]
[<Route("[controller]")>]
type ResumeController (logger: ILogger<ResumeController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get() =
        ActionResult.ofAsync <| async {
            let! resume = Domain.Handlers.getResume
            match resume with
            | Ok r -> return OkObjectResult(r) :> _
            | Error ex -> return BadRequestObjectResult(ex) :> _
        }

    [<HttpGet("/error")>]
    member _.GetError() =
        ActionResult.ofAsync <| async {
            let! error = Domain.Handlers.getResumeError
            match error with
            | Ok r -> return OkObjectResult(r) :> _
            | Error ex ->
                match ex with
                | Domain.Types.ServiceError.UnexpectedError u ->
                    let errorMessage = u |> sprintf "Unexpected error retrieving resume: %s"
                    return UnprocessableEntityObjectResult(errorMessage) :> _
                | Domain.Types.ServiceError.ValidationError v -> return BadRequestObjectResult(v) :> _
        }