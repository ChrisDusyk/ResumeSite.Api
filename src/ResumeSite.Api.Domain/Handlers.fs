module Domain.Handlers

open Types

let getResume = 
    let mockExperience =
        { Title = "Programmer"
          CompanyName = "Company"
          Location = "City1"
          StartDate = new System.DateTime(2020, 1, 1)
          EndDate = None
          Description = "Some words and stuff about work"}: WorkExperience
    
    let mockSkill = seq {
        yield "skill1"
        yield "skill2"
    }

    let mockReferences = seq {
        yield
            { Name = "Ref Name"
              Phone = Some "3065229292"
              Email = Some "test@test.com" }
    }

    let mockEducation = seq {
        yield
            { InstituteName = "School name"
              StartDate = new System.DateTime(2018, 1, 1)
              EndDate = Some (new System.DateTime(2020, 4, 30))
              Certification = "Diploma"
              InstituteLocation = "Regina, SK" }
    }

    let mockAdditionalInfo =
        { Languages = [| "English" |]
          Interests = [| "Stuff" |] }

    let mockData =
        { Intro = "test1"
          WorkExperience = [| mockExperience |]
          Skills = mockSkill
          References = mockReferences
          Education = mockEducation
          AdditionalInformation = mockAdditionalInfo }: Resume
    async {
        return mockData |> Result<Resume, ServiceError>.Ok    
    }

let getResumeError =
    let mockError: ServiceError = (UnexpectedError "Test error")

    async {
        return mockError |> Result<Resume, ServiceError>.Error
    }