module Types

open System

module Option =
    let toNullable =
        function
        | None -> Nullable()
        | Some x -> Nullable(x)
    let toString =
        function
        | None -> ""
        | Some x -> x

type CLIList<'a> = System.Collections.Generic.List<'a>

[<CLIMutable>]
type WorkExperienceResponse =
    { Title: string
      CompanyName: string
      Location: string
      StartDate: DateTime
      EndDate: Nullable<DateTime>
      Description: string }

[<CLIMutable>]
type ReferenceResponse =
    { Name: string
      Phone: string
      Email: string }

[<CLIMutable>]
type EducationResponse =
    { InstituteName: string
      StartDate: DateTime
      EndDate: Nullable<DateTime>
      Certification: string
      InstituteLocation: string }

[<CLIMutable>]
type AdditionalInformationResponse =
    { Languages: CLIList<string>
      Interests: CLIList<string> }

[<CLIMutable>]
type ResumeResponse =
    { Intro: string
      WorkExperience: CLIList<WorkExperienceResponse>
      Skills: CLIList<string>
      References: CLIList<ReferenceResponse>
      Education: CLIList<EducationResponse>
      AdditionalInformation: AdditionalInformationResponse }

let mapDomainResumeToResponse (resume: Domain.Types.Resume) =
    let toWorkExperienceResponse (workExperience: Domain.Types.WorkExperience) =
        { Title = workExperience.Title
          CompanyName = workExperience.CompanyName
          Location = workExperience.Location
          StartDate = workExperience.StartDate
          EndDate = Option.toNullable workExperience.EndDate
          Description = workExperience.Description }: WorkExperienceResponse

    let toReferenceResponse (reference: Domain.Types.Reference) =
        { Name = reference.Name
          Phone = Option.toString reference.Phone
          Email = Option.toString reference.Email }: ReferenceResponse

    let toEducationResponse (education: Domain.Types.Education) =
        { InstituteName = education.InstituteName
          StartDate = education.StartDate
          EndDate = Option.toNullable education.EndDate
          Certification = education.Certification
          InstituteLocation = education.InstituteLocation }: EducationResponse

    let toAdditionalInformationResponse (info: Domain.Types.AdditionalInformation) =
        { Languages = info.Languages |> CLIList<_>
          Interests = info.Interests |> CLIList<_> }: AdditionalInformationResponse

    { Intro = resume.Intro
      WorkExperience = resume.WorkExperience |> Seq.map toWorkExperienceResponse |> CLIList<_>
      Skills = resume.Skills |> CLIList<_>
      References = resume.References |> Seq.map toReferenceResponse |> CLIList<_>
      Education = resume.Education |> Seq.map toEducationResponse |> CLIList<_>
      AdditionalInformation = resume.AdditionalInformation |> toAdditionalInformationResponse }: ResumeResponse