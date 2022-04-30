module MyBook.FSharp.Services.IEmailService

open MyBook.FSharp.Services.EmailServices

type IEmailService =
   abstract member SendEmail : Message -> Message