module Types

open System

// Errors
type ServiceError =
    | UnexpectedError of string
    | ValidationError of string

// Domain Types

// Eventually I'll use domain primitives everywhere, but not yet
//type WorkExperienceTitle = WorkExperienceTitle of string
//module WorkExperienceTitle =
//    let create x =
//        if String.IsNullOrEmpty x then
//            "Work Experience Title is null or empty" |> ServiceError.ValidationError |> Error
//        else
//            Ok (WorkExperienceTitle x)
//    let value (WorkExperienceTitle x) = x

type WorkExperience =
    { Title: string
      CompanyName: string
      Location: string
      StartDate: DateTime
      EndDate: DateTime option
      Description: string }

type Resume =
    { Intro: string
      WorkExperience: WorkExperience seq }